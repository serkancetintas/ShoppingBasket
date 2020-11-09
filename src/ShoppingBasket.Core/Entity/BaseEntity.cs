using MongoDB.Bson;
using ShoppingBasket.Core.Entity.Abstracts;

namespace ShoppingBasket.Core.Entity
{
    public abstract class BaseEntity : IBaseEntity
    {
        protected BaseEntity()
        {
            _id = ObjectId.GenerateNewId();
        }

        public ObjectId Id
        {
            get { return _id; }
            set
            {
                if (ObjectId.Empty == value)
                    _id = ObjectId.GenerateNewId();
                else
                    _id = value;
            }
        }

        private ObjectId _id;
    }
}
