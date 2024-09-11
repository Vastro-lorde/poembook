
using MongoDB.Bson.Serialization.Attributes;

namespace poembook.Models
{
    public class PoemModel : BaseEntity
    {

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("author")]
        public string? Author { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
