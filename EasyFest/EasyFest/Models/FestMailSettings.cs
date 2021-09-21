using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    public class FestMailSettings : IFestMailSettings
    {
        public string Mail { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }

    public interface IFestMailSettings
    {
        string Mail { get; set; }

        string DisplayName { get; set; }

        string Password { get; set; }

        string Host { get; set; }

        int Port { get; set; }

    }
}
