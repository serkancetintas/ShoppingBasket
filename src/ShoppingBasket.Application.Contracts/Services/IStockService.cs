namespace ShoppingBasket.Application.Contracts.Services
{
    public interface IStockService
    {
        bool IsItInStock(string sku, int quantity);
    }
}
