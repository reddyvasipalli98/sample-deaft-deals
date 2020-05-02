using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace sampledraft.Models
{
    public class Deal
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Object Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("imageurl")]
        public string ImageUrl { get; set; }
    }
}
