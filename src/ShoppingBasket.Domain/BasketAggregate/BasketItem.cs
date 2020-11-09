using ShoppingBasket.Core.Entity;
using ShoppingBasket.Core.ExceptionHandling;
using System;

namespace ShoppingBasket.Domain.BasketAggregate
{
    public class BasketItem: BaseEntity
    {
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public string CatalogItemId { get; private set; }
        public string Sku { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public BasketItem(string catalogItemId, string sku, int quantity, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(catalogItemId))
            {
                throw new WarningNotificationException("Catalog item can not be empty!", "777");
            }

            if (string.IsNullOrEmpty(sku))
            {
                throw new WarningNotificationException("Stock code can not be empty!", "777");
            }

            if (unitPrice < 0)
            {
                throw new WarningNotificationException("The product price is not valid!", "777");
            }

            if (quantity < 0)
            {
                throw new WarningNotificationException("The quantity is not valid!", "777");
            }

            CatalogItemId = catalogItemId;
            Sku = sku;
            UnitPrice = unitPrice;
            SetQuantity(quantity);
            CreatedAt = DateTime.Now;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
