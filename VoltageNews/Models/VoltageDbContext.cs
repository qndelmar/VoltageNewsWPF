using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VoltageNews.Models;

public partial class VoltageDbContext : DbContext
{
    public VoltageDbContext()
    {
    }

    public VoltageDbContext(DbContextOptions<VoltageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Editor> Editors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewsByDate> ViewsByDates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=REINCARNDEITY;Database=VoltageDB;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.NewsId);

            entity.Property(e => e.NewsId).HasColumnName("newsID");
            entity.Property(e => e.ArticleText)
                .HasColumnType("nvarchar(MAX)")
                .HasColumnName("articleText");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("created");
            entity.Property(e => e.ImageUri)
                .HasMaxLength(100)
                .HasColumnName("imageUri");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(400)
                .HasColumnName("shortDescription");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.Views).HasColumnName("views");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Articles_Editors");

            entity.HasMany(d => d.Categories).WithMany(p => p.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticlesCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ArticlesCategory_Categories"),
                    l => l.HasOne<Article>().WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_ArticlesCategory_Articles"),
                    j =>
                    {
                        j.HasKey("ArticleId", "CategoryId");
                        j.ToTable("ArticlesCategory");
                        j.IndexerProperty<int>("ArticleId").HasColumnName("articleId");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("categoryId");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(150)
                .HasColumnName("shortDescription");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticleId).HasColumnName("articleID");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Date)
                .HasColumnType("smalldatetime")
                .HasColumnName("date");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .HasColumnName("text");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Articles");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Editor>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EmployingTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("employingTime");
            entity.Property(e => e.MonthlySalary)
                .HasColumnType("money")
                .HasColumnName("monthlySalary");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Editor)
                .HasForeignKey<Editor>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Editors_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarUri)
                .HasMaxLength(200)
                .HasColumnName("avatarUri");
            entity.Property(e => e.BirthDate)
                .HasColumnType("smalldatetime")
                .HasColumnName("birthDate");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(400)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<ViewsByDate>(entity =>
        {
            entity.HasKey(e => e.Date);

            entity.ToTable("ViewsByDate");

            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Views).HasColumnName("views");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
