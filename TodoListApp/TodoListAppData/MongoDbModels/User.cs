using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TodoListAppData.MongoDbModels
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string Nmae { get; set; } = null!;

        [BsonElement("Surname")]
        [JsonPropertyName("Surname")]
        public string Surname { get; set; } = null!;

        [BsonElement("Categories")]
        [JsonPropertyName("Categories")]
        public List<Category> Categories { get; set; }
    }
}
