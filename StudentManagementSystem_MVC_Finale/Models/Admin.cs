using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Models
{
    public class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        internal static object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
