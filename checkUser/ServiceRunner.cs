using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace checkUser
{
    public class ServiceRunner : ServiceControl, ServiceSuspend
    {
        private readonly IScheduler _scheduler;
        public ServiceRunner()
        {
            _scheduler = IniQuarz.Run();
        }
        public bool Start(HostControl hostControl)
        {
            _scheduler.Start();
            return true;
        }
        public bool Stop(HostControl hostControl)
        {
            _scheduler.Shutdown(false);
            return true;
        }
        public bool Continue(HostControl hostControl)
        {
            _scheduler.ResumeAll();
            return true;
        }
        public bool Pause(HostControl hostControl)
        {
            _scheduler.PauseAll();
            return true;
        }
    }
}
