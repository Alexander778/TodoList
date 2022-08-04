using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TodoListAppData.MongoDbModels
{
    public class TodoTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("DoneStatus")]
        [JsonPropertyName("DoneStatus")]
        public bool DoneStatus { get; set; }
    }
}
