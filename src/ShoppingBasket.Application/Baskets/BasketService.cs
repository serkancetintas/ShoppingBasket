using ShoppingBasket.Domain.BasketAggregate;
using System.Threading.Tasks;
using ShoppingBasket.Core.ExceptionHandling;
using ShoppingBasket.Application.Contracts.Services;
using ShoppingBasket.Application.Contracts.Dtos;
using System.Linq;
using ShoppingBasket.Repository.Abstract;

namespace ShoppingBasket.Application.Baskets
{
    public class BasketService: IBasketService
    {
        private readonly IMongoRepository<Basket> _basketRepository;
        private readonly IStockService _stockService;
        public BasketService(IMongoRepository<Basket> basketRepository,
                             IStockService stockService)
        {
            _basketRepository = basketRepository;
            _stockService = stockService;
        }

        public async Task<AddItemResultDto> AddItemToBasket(AddItemDto dto, string buyerId)
        {
            bool inStock = _stockService.IsItInStock(dto.Sku, dto.Quantity);
            if (!inStock)
            {
                throw new WarningNotificationException("The product is out of stock.", "999");
            }

            var basket = await _basketRepository.FindOneAsync(p=> p.BuyerId == buyerId);
            if (basket == null)
            {
                basket = new Basket(buyerId);
                basket.AddItem(dto.CatalogItemId, dto.Sku, dto.Price, dto.Quantity);

                await _basketRepository.InsertOneAsync(basket);
            }
            else
            {
                basket.AddItem(dto.CatalogItemId, dto.Sku, dto.Price, dto.Quantity);
                await _basketRepository.UpdateOneAsync(basket);
            }

            int itemCount =  basket.Items.GroupBy(p => p.CatalogItemId).Count();

            return new AddItemResultDto { ItemCount = itemCount };
        }
    }
}
