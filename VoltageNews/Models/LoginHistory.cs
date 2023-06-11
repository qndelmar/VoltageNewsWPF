using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageNews.Models
{
    public partial class LoginHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime LoginDate { get; set; }
        public string UserEmail
        {
            get
            {
                using (VoltageDbContext dbCtx = new())
                {
                    return dbCtx.Users.FirstOrDefault(p => p.Id == UserId).Email;
                }
            }
        }

        public virtual User User { get; set; } = null!;

        public static List<LoginHistory> getLoginHistory()
        {
            using (VoltageDbContext dbCtx = new())
            {
                return dbCtx.LoginHistories.ToList();
            }
        }
    }
}
