using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace checkUser
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //DatabaseHelper db = new DatabaseHelper();
            //var users = db.GetUsers();
            await Console.Out.WriteLineAsync(" HelloJob!");
        }

    }
}
