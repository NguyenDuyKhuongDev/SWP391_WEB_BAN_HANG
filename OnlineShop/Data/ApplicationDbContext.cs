using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data;

public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdCategory> AdCategories { get; set; }

    public virtual DbSet<AdClickLog> AdClickLogs { get; set; }

    public virtual DbSet<AdPlacement> AdPlacements { get; set; }

    public virtual DbSet<AdPosition> AdPositions { get; set; }

    public virtual DbSet<AdTemplate> AdTemplates { get; set; }

    public virtual DbSet<Advertisement> Advertisements { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }


    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagBlog> TagBlogs { get; set; }

    public virtual DbSet<Thumbnail> Thumbnails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\MSSQLSERVER03;Database=SWP391DB;User Id=sa;Password=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AdCategory>(entity =>
        {
            entity.ToTable("AdCategory");

            entity.HasOne(d => d.Ad).WithMany(p => p.AdCategories)
                .HasForeignKey(d => d.AdId)
                .HasConstraintName("FK_AdCategory_Advertisement");

            entity.HasOne(d => d.Category).WithMany(p => p.AdCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_AdCategory_ProductCategory");
        });

        modelBuilder.Entity<AdClickLog>(entity =>
        {
            entity.ToTable("AdCLickLog");

            entity.Property(e => e.ClickedAt).HasColumnType("date");
            entity.Property(e => e.CreatedAt).HasColumnType("date");
            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.UserDevice).HasMaxLength(1000);

            entity.HasOne(d => d.Ad).WithMany(p => p.AdClickLogs)
                .HasForeignKey(d => d.AdId)
                .HasConstraintName("FK_AdCLickLog_Advertisement");
        });

        modelBuilder.Entity<AdPlacement>(entity =>
        {
            entity.ToTable("AdPlacement");

            entity.Property(e => e.CreatedAt).HasColumnType("date");

            entity.HasOne(d => d.Ad).WithMany(p => p.AdPlacements)
                .HasForeignKey(d => d.AdId)
                .HasConstraintName("FK_AdPlacement_Advertisement");

            entity.HasOne(d => d.Blog).WithMany(p => p.AdPlacements)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK_AdPlacement_Blog");

            entity.HasOne(d => d.Position).WithMany(p => p.AdPlacements)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_AdPlacement_AdPosition");
        });

        modelBuilder.Entity<AdPosition>(entity =>
        {
            entity.ToTable("AdPosition");

            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<AdTemplate>(entity =>
        {
            entity.ToTable("AdTemplate");

            entity.Property(e => e.CreatedAt).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PreviewImageUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<Advertisement>(entity =>
        {
            entity.ToTable("Advertisement");

            entity.Property(e => e.CreatedAt).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LinkUrl).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("date");

            entity.HasOne(d => d.AdTemplate).WithMany(p => p.Advertisements)
                .HasForeignKey(d => d.AdTemplateId)
                .HasConstraintName("FK_Advertisement_AdTemplate");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.HasIndex(e => e.ThumbnailId, "IX_Blog_ThumbnailId").IsUnique();

            entity.Property(e => e.AuthorId).HasMaxLength(450);
            entity.Property(e => e.LastUpdated).HasColumnType("date");
            entity.Property(e => e.PublishedDate).HasColumnType("date");
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.ThumbnailId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Blog_BlogCategory");

            entity.HasOne(b => b.Thumbnail)
                 .WithMany(t => t.Blogs)
                 .HasForeignKey(b => b.ThumbnailId);
        });

        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.ToTable("BlogCategory");

            entity.Property(e => e.Name).HasMaxLength(100);
        });


        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CreatedAt).HasColumnType("date");
            entity.Property(e => e.TargetType).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("date");
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.CreateAt).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(120);
        });

        modelBuilder.Entity<TagBlog>(entity =>
        {
            entity.ToTable("TagBlog");

            entity.Property(e => e.CreatedAt).HasColumnType("date");

            entity.HasOne(d => d.Blog).WithMany(p => p.TagBlogs)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagBlog_Blog");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagBlogs)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagBlog_Tag");
        });

        modelBuilder.Entity<Thumbnail>(entity =>
        {
            entity.ToTable("Thumbnail");

            entity.Property(e => e.ThumbnailName).HasDefaultValueSql("(N'')");
            entity.Property(e => e.ThumnailUrl).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
