using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkUser
{
    public class IniQuarz
    {
        public static IScheduler Run()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteServerSchedulerClient";
            // 设置线程池
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // 远程输出配置
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "555";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            var schedulerFactory = new StdSchedulerFactory(properties);
            var scheduler = schedulerFactory.GetScheduler().Result;

            var job = JobBuilder.Create<PrintMessageJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("myJobTrigger", "group1")
                .StartNow()
                .WithCronSchedule("/3 * * ? * *")
                .Build();
            var hjob = JobBuilder.Create<HelloJob>()
                .WithIdentity("hJob", "group1")
                .Build();

            var htrigger = TriggerBuilder.Create()
                .WithIdentity("hJobTrigger", "group1")
                .StartNow()
                .WithCronSchedule("/4 * * ? * *")
                .Build();
            scheduler.ScheduleJob(job, trigger);
            scheduler.ScheduleJob(hjob, htrigger);
            return scheduler;
        }
    }
}
