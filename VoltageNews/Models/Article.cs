using Microsoft.EntityFrameworkCore;
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
        catch (Exception)
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
    public static List<Article> GetSortedArticlesByPageNumber(string sortType, int pageNum, int amount)
    {
        using(VoltageDbContext dbContext = new())
        {
            return dbContext.Articles.OrderBy(b => -b.NewsId).Where(r => r.Categories.FirstOrDefault(r => r.Title == sortType) != null)
                .Skip((pageNum - 1) * 9).Take(amount).ToList();
        }
    }
    public static List<Article> GetFixedAmount(int pageNum, int amount)
    {
        using(VoltageDbContext dbContext = new())
        {
            return dbContext.Articles.OrderBy(b => -b.NewsId).Skip((pageNum - 1) * 9).Take(amount).ToList();
        }
    }

    public static double GetCount()
    {
        using(VoltageDbContext ctx = new())
        {
            return ctx.Articles.Count();
        }
    }

    public static List<Article> SearchItems(string text, int pageNum, int amount)
    {
        using (VoltageDbContext dbContext = new())
        {
            return dbContext.Articles.OrderBy(b => -b.NewsId).Where(r => r.Title.Contains(text)).Skip((pageNum-1)*9).Take(amount).ToList();
        }
    }

    public static double GetItemsCount(string text)
    {
        using(VoltageDbContext dbContext = new())
        {
            return dbContext.Articles.Where(r => r.Title.Contains(text)).Count();
        }
    }
    public static Article GetOnePost(int id)
    {
        using(VoltageDbContext dbContext = new())
        {
            Article article = dbContext.Articles.First(r => r.NewsId == id);
            article.Views++;
            dbContext.SaveChanges();
            ViewsByDate.AddViewsToDate();
            return article;

        }
    }

    public static bool deleteArticle(int id)
    {
        try
        {
            using(VoltageDbContext dbContext = new())
            {
                Article article = dbContext.Articles.First(r => r.NewsId == id);
                dbContext.Articles.Remove(article);
                    dbContext.SaveChanges();
                return true;
            }
        }catch(Exception) {
            return false;
        }
    }
    public static bool editArticle(string newTitle, string newShort, string longDesc, int id)
    {
        try
        {
            using(VoltageDbContext dbCtx = new()){
                Article article = dbCtx.Articles.First(r => r.NewsId == id);
                article.Title = newTitle;
                article.ShortDescription = newShort;
                article.ArticleText = longDesc;
                dbCtx.SaveChanges();
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
