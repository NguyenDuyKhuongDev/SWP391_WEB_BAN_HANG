using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModel
{
    public class BlogVM
    {
        [Required]

        public string? Title { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Meta Title should not be more than 60 characters for SEO")]
        public string? MetaTitle { get; set; }
        [MaxLength(150, ErrorMessage = "Meta Description should not be more than 150 characters for SEO")]
        [Required]
        public string? MetaDescription { get; set; }
        [Required]
        public string? Slug { get; set; }

        public string? Summary { get; set; }

        public string? Content { get; set; }


        public string? AuthorId { get; set; }

        public DateTime? PublishedDate { get; set; }


        public int? CategoryId { get; set; }
        public int? ThumbnailId { get; set; }

    }
}
