using System.ComponentModel.DataAnnotations;

namespace poembook.DTOs
{
    public record UpdatePoemDTO
    {
        public string? Title { get; init; }

        [MinLength(10)]
        [MaxLength(3000)]
        public string? Content { get; init; }
        public string? Author { get; init; }
    }
}
