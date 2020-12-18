using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Back_End.Persistence.Entities
{
    public class TemplateEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string Name { get; set; }

        public TemplateEntity() { }

        public TemplateEntity(string name)
        {
            this.Name = name;
        }
    }
}