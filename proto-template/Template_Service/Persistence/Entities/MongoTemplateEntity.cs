using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Template_Service.Persistence.Entities {
    public class MongoTemplateEntity {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string Name { get; set; }

        public MongoTemplateEntity() { }

        public MongoTemplateEntity(string name) {
            this.Name = name;
        }
    }
}