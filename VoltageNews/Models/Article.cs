using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace VoltageNews.Models;

public partial class Article
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string ArticleText { get; set; } = null!;

    public int Author { get; set; }

    public string ImageUri { get; set; } = null!;

    public long Views { get; set; }

    public DateTime Created { get; set; }

    public virtual Editor AuthorNavigation { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public Article(string title, string shortDescription, string ArticleText, int author, string imageUri)
    {
        this.ArticleText = ArticleText;
        this.Title = title;
        this.ShortDescription = shortDescription;
        this.Author = author;
        this.ImageUri = imageUri;
        this.Views = 0;
        
    }

    public async Task<string> createArticle(string category)
    {
        try
        {
            using (VoltageDbContext db = new())
            {

                this.Categories.Add(db.Categories.First(r => r.Title == category));
                db.Articles.Add(this);
                await db.SaveChangesAsync();
                return "All posts has been succesfully created!";
            }
        }
        catch (Exception ex)
        {
            return "Ooops... Try later...";
        }
    }

    public static List<Article> GetArticlesAsync()
    {
        using(VoltageDbContext dbContext = new())
        {
            return dbContext.Articles.ToList();
        }
    }
    public static List<Article> GetFixedAmount(int pageNum, int amount)
    {
        using(VoltageDbContext dbContext = new())
        {
            return dbContext.Articles.OrderBy(b => -b.NewsId).Skip((pageNum - 1) * 10).Take(amount).ToList();
        }
    }

    public static double GetCount()
    {
        using(VoltageDbContext ctx = new())
        {
            return ctx.Articles.Count();
        }
    }
}
