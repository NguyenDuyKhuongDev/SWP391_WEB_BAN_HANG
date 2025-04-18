using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? Slug { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public string? AuthorId { get; set; }

    public DateTime? PublishedDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public bool IsPublished { get; set; } = false;

    public int? CategoryId { get; set; }

    public int? ViewCount { get; set; } = 0;

    public int? LikeCount { get; set; } = 0;

    public int? CommentCount { get; set; } = 0;

    public int? ThumbnailId { get; set; }

    public virtual ICollection<AdPlacement> AdPlacements { get; set; } = new List<AdPlacement>();
    public virtual  Thumbnail Thumbnail { get; set; }

    public virtual BlogCategory? Category { get; set; }

    public virtual ICollection<TagBlog> TagBlogs { get; set; } = new List<TagBlog>();
}
