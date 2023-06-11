using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageNews.Models
{
    internal class UserStore
    {
        private static User? user { get; set; }

        public static User? User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
    }
}
