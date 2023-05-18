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

    public static List<Comment> getComments(int id)
    {
        using(VoltageDbContext dbCtx = new())
        {
            return dbCtx.Comments.Where(r => r.ArticleId == id).ToList();
        }
    }
}
