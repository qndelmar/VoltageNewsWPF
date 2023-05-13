using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VoltageNews.Helpers;
using VoltageNews.Models;

namespace VoltageNews.ViewModels
{
    internal class Auth : ObservableObject
    {
        private static string email { get; set; }
        private static string password { get; set; }

        public  string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set { 
            email = value;
            }
        }

        public async Task<int> SignIn()
        {
            int result = await User.authorize(email, password);
            return result;
        }

    }
}
