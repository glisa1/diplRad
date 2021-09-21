using EasyFest.QuartzService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Util.Quartz.Jobs
{
    public class FestStartJob : IJob
    {
        private readonly IQuartzService _service;

        public FestStartJob(IQuartzService quartzService)
        {
            _service = quartzService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _service.SendFestivalStartInfo();
        }
    }
}
