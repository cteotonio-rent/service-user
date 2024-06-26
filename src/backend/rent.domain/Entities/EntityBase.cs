﻿using MongoDB.Bson;

namespace rent.domain.Entities
{
    public class EntityBase
    {
        
        public ObjectId _id { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
    }
}
