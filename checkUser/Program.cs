using checkUser.Helper;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace checkUser
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();                            // 服务使用NETWORK_SERVICE内置帐户运行。身份标识，有好几种方式，如：x.RunAs("username", "password");  x.RunAsPrompt(); x.RunAsNetworkService(); 等
                x.SetDescription("Sample Topshelf Host服务的描述");        //安装服务后，服务的描述
                x.SetDisplayName("Stuff显示名称");                       //显示名称
                x.SetServiceName("Stuff服务名称");                       //服务名称
                x.Service<ServiceRunner>();
                x.EnablePauseAndContinue();
            });

            //HostFactory.Run(x =>                                
            //{
            //    x.Service<ServiceRunner>(s =>                        
            //    {
            //        s.ConstructUsing(name => new ServiceRunner(IniQuarz.Run()));    
            //        s.WhenStarted(tc => tc.Start());            
            //        s.WhenStopped(tc => tc.Stop());

            //    });
            //    x.RunAsLocalSystem();                            // 服务使用NETWORK_SERVICE内置帐户运行。身份标识，有好几种方式，如：x.RunAs("username", "password");  x.RunAsPrompt(); x.RunAsNetworkService(); 等

            //    x.SetDescription("Sample Topshelf Host服务的描述");        //安装服务后，服务的描述
            //    x.SetDisplayName("Stuff显示名称");                       //显示名称
            //    x.SetServiceName("Stuff服务名称");                       //服务名称
            //});

        }
    }
}




