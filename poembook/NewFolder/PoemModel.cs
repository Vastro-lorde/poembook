namespace poembook.NewFolder
{
    public class PoemModel
    {
        public required string Title { get; set; }

        public required string Content { get; set; }

        public string? Author { get; set; }

        public DateTime Date { get; set; }
    }
}
