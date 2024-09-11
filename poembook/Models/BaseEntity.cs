using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace poembook.Models
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  // Ensures proper conversion from string to ObjectId
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString(); // Default to a new ObjectId

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
