using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Rise.Core.Mongo
{
    public abstract class MongoDbEntity : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; }// = ObjectId.GenerateNewId().ToString();
    }
}
