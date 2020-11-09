using MongoDB.Driver;
using ShoppingBasket.Domain.BasketAggregate;
using ShoppingBasket.Repository.Abstract;

namespace ShoppingBasket.Repository.Concrete
{
    public class BasketContext : IBasketContext
    {
        public BasketContext(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Baskets = database.GetCollection<Basket>("Baskets");
        }
        public IMongoCollection<Basket> Baskets { get; }
    }
}
