using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltageNews.Models;

public partial class ViewsByDate
{
    public DateTime Date { get; set; }

    public int Views { get; set; }

    public static void AddViewsToDate()
    {
        using (VoltageDbContext dbCtx = new())
        {
            var currentDate = dbCtx.ViewsByDates.FirstOrDefault(p => p.Date == DateTime.Now.Date);
            if(currentDate != null)
            {
                currentDate.Views++;
            }
            else
            {
                currentDate = new ViewsByDate();
                currentDate.Date = DateTime.Now.Date;
                currentDate.Views++;
                dbCtx.ViewsByDates.Add(currentDate);
            }
            dbCtx.SaveChanges();

        }
    }
    public static List<KeyValuePair<DateTime, int>> GetKeyValuePairs()
    {
        List<KeyValuePair<DateTime, int>> newList = new();
        using (VoltageDbContext dbCtx = new())
        {
            var allViews = dbCtx.ViewsByDates.OrderByDescending(p => p.Date).ToList();
            int i = 0;
            foreach(var p in allViews)
            {
                if(i > 7)
                {
                    break;
                }
                newList.Add(new KeyValuePair<DateTime, int>(p.Date.Date, p.Views));
                i++;
            }
        }
        return newList;
    }
}
