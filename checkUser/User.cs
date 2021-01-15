using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkUser
{
   
    public class User
    {
        public bool IsActive { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ID { get; set; }
        public string Username { get; set; }
    }
}
