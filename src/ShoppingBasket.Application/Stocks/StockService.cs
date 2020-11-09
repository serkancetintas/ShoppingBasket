using ShoppingBasket.Application.Contracts.Services;

namespace ShoppingBasket.Application.Stocks
{
    public class StockService:IStockService
    {
        public bool IsItInStock(string sku, int quantity)
        {
            if (quantity > 10)
            {
                return false;
            }

            return true;
        }
    }
}
