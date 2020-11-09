using MongoDB.Driver;
using ShoppingBasket.Domain.BasketAggregate;

namespace ShoppingBasket.Repository.Abstract
{
    public interface IBasketContext
    {
        IMongoCollection<Basket> Baskets { get; }
    }
}
