using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace back_end.Config.Mappings
{
    public class BsonDocumentToJsonConverter : JsonConverter<BsonDocument>
    {
        public override BsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BsonDocument value, JsonSerializerOptions options)
        {
            var obj = BsonSerializer.Deserialize<object>(value);
            JsonSerializer.Serialize(writer, obj, options);
        }
    }
}