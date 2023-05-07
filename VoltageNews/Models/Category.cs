using System;
using System.Collections.Generic;

namespace VoltageNews.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
