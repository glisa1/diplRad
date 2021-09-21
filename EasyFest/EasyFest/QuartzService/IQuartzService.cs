using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.QuartzService
{
    public interface IQuartzService
    {
        Task SendBillingStartInfo();

        Task SendFestivalStartInfo();
    }
}
