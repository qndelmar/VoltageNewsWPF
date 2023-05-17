using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltageNews.Models;

public partial class Editor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal MonthlySalary { get; set; }

    public DateTime EmployingTime { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual User IdNavigation { get; set; } = null!;

    public static string getName(int id)
    {
        using (VoltageDbContext dbCtx = new())
        {
            return dbCtx.Editors.First(r => r.Id == id).Name;
        }
    }
}
