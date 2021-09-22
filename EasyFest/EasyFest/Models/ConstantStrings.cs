using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    public static class ConstantStrings
    {
        public static string BillingMailBody =>
            "Hello festival person,<br><br>" +
            "Festival {0} starts selling tickets tommorow! Be sure to get them!<br><br>" +
            "Yours EasyFest!";

        public static string BillingMailSubject =>
            "Billing starts soon!";

        public static string StartFestMailBody =>
            "Hello festival person,<br><br>" +
            "Festival {0} starts rocking in three days! Be sure to get there!<br><br>" +
            "Yours EasyFest!";

        public static string StartFestMailSubject =>
            "Festival starts soon!";
    }
}
