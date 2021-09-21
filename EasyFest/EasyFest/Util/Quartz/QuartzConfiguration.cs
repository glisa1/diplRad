using EasyFest.Util.Quartz.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Util.Quartz
{
    public static class QuartzConfiguration
    {

        public static void ConfigureQuartz(IServiceCollection services)
        {

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<BillingInfoJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(BillingInfoJob),
                cronExpression: "0 0 3 ? * * *")); // run at 3am every day

            services.AddSingleton<FestStartJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(FestStartJob),
                cronExpression: "0 0 */2 ? * *")); // evry hour
        }
    }
}
