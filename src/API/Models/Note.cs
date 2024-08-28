using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace simple_api.src.API.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Content")]
        public string Content { get; set; }
    }
}