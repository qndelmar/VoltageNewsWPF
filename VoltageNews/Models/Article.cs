using System;
using System.Collections.Generic;

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
}
