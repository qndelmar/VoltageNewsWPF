using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltageNews.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int ArticleId { get; set; }

    public string Text { get; set; } = null!;

    public int AuthorId { get; set; }

    public DateTime Date { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User Author { get; set; } = null!;

    public static List<object> getComments(int id)
    {
        using(VoltageDbContext dbCtx = new())
        {
            return dbCtx.Comments.Where(r => r.ArticleId == id)
                .Join(dbCtx.Users,
                c => c.AuthorId,
                u => u.Id,
                (c, u) => new
                {
                    Author = u.Nickname,
                    Text = c.Text,
                    Created = c.Date

                }).ToList<object>();
        }
    }
    public static bool createComment(int id, string text)
    {
        try
        {
            using (VoltageDbContext dbCtx = new())
            {
                dbCtx.Comments.Add(new Comment
                {
                    ArticleId = id,
                    Text = text,
                    Date = DateTime.Now,
                    AuthorId = UserStore.User.Id
                });
                dbCtx.SaveChanges();
                return true;
            }
        }catch(Exception ex)
        {
            return false;
        }
    }
}
