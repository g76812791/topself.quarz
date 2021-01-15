using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkUser.Helper
{
    public class DatabaseHelper
    {
        private string sqlConnString { get; set; }
        public DatabaseHelper()
        {
            sqlConnString = Shared.CustomSettingsHelper.GetDBConnectionString(Shared.DatabaseKeys.managementservice);
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            int retryTimes = 3;
            while (retryTimes > 0)
            {
                try
                {
                    string sql = @"SELECT * FROM [User] WHERE  id IN(
'F387AA3D-CEAA-474A-9AB8-F804855C3F9D',
'39AA7EE5-8C80-49B4-AAB3-4A0DA3FAFFC3',
'520F8F78-32F3-45FD-B7F1-2082E3EE806F',
'B715B89E-90DE-44DA-BAF1-D8D526CFC8B8',
'22333B9A-8035-40CA-847F-80C0920E1921'
)
";
                    using (IDbConnection connection = new SqlConnection(sqlConnString))
                    {
                        return connection.Query<User>(sql).ToList();
                    }
                }
                catch (Exception ex)
                {
                    retryTimes--;
                    Trace.TraceError("Could not get users. {0}", ex.Message);
                }
            }
            return users;
        }
    }
}

