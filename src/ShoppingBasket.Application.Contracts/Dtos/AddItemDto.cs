namespace ShoppingBasket.Application.Contracts.Dtos
{
    public class AddItemDto
    {
        public string CatalogItemId { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
