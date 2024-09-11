using System.ComponentModel.DataAnnotations;

namespace poembook.DTOs
{
    public record CreatePoemDTO
    {
        public string? Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(3000)]
        public required string Content { get; set; }

        [MaxLength(255)]
        public string? Author { get; set; }
    }
}
