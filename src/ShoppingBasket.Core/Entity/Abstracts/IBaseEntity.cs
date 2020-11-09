using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingBasket.Core.Entity.Abstracts
{
    public interface IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
