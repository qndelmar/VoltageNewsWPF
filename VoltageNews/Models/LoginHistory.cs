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

        public virtual User User { get; set; } = null!;
       
    }
}
