using EasyFest.Factories;
using EasyFest.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFest.QuartzService
{
    public class QuartzService : IQuartzService
    {
        #region Init

        private readonly IHttpClientFactory _client;
        private readonly IFestMailSettings _mailSettings;

        public QuartzService(IHttpClientFactory httpClientFactory,
                            IFestMailSettings settings)
        {
            _client = httpClientFactory;
            _mailSettings = settings;
        }

        #endregion

        public async Task SendBillingStartInfo()
        {
            var query = GraphQLCommModel
                .QueryGetBillingStartFestivals
                .Replace("{0}", DateTime.Now.AddDays(1).Month.ToString())
                .Replace("{1}", DateTime.Now.AddDays(1).Day.ToString());
            var festivals = await _client.QueryGet<FestivalPaginate>(query);

            var festivalsFound = festivals.Data.FestivalNodes.FestivalEdges;

            if (!festivalsFound.Any())
            {
                return;
            }

            await GetUsersAndSendMailsAsync(festivalsFound, true);
        }

        public async Task SendFestivalStartInfo()
        {
            var query = GraphQLCommModel
                .QueryGetFestStartFestivals
                .Replace("{0}", DateTime.Now.AddDays(3).Month.ToString())
                .Replace("{1}", DateTime.Now.AddDays(3).Day.ToString());
            var festivals = await _client.QueryGet<FestivalPaginate>(query);

            var festivalsFound = festivals.Data.FestivalNodes.FestivalEdges;

            if (!festivalsFound.Any())
            {
                return;
            }

            await GetUsersAndSendMailsAsync(festivalsFound, false);
        }

        #region Private methods

        private async Task SendMailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            foreach(var mail in mailRequest.ToEmails)
            {
                email.To.Add(MailboxAddress.Parse(mail));
            }
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
            }
        }

        private async Task GetUsersAndSendMailsAsync(IEnumerable<FestivalEdge> festivalsFound, bool billing)
        {
            if (festivalsFound is null)
                return;

            foreach (var fest in festivalsFound)
            {
                var query = GraphQLCommModel.QueryGetUsersForFestsMail
                .Replace("{0}", "\"" + fest.Festival.Id + "\"");

                var users = await _client.QueryGet<UserFilterModel>(query);
                var usersToNotify = users.Data.Users;
                if (usersToNotify.Count == 0)
                {
                    continue;
                }

                List<string> userMails = new List<string>();

                foreach (var user in usersToNotify)
                {
                    userMails.Add(user.Email);
                }

                MailRequest mail = new MailRequest
                {
                    ToEmails = userMails
                };

                if (billing)
                {
                    mail.Body = string.Format(ConstantStrings.BillingMailBody, fest.Festival.Name);
                    mail.Subject = ConstantStrings.BillingMailSubject;
                }
                else
                {
                    mail.Body = string.Format(ConstantStrings.StartFestMailBody, fest.Festival.Name);
                    mail.Subject = ConstantStrings.StartFestMailSubject;
                }

                await SendMailAsync(mail);
            }
        }

        #endregion
    }
}
