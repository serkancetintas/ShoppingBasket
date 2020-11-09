using ShoppingBasket.Core.Entity;
using ShoppingBasket.Core.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Domain.BasketAggregate
{
    [BsonCollection("baskets")]
    public class Basket: BaseEntity, IAggregateRoot
    {
        public string BuyerId { get; private set; }
        private  ICollection<BasketItem> _items;
        public virtual ICollection<BasketItem> Items
        {
            get { return _items ??= new List<BasketItem>(); }
            protected set { _items = value; }
        }

        public DateTime CreatedAt { get; private set; }

        public Basket(string buyerId)
        {
            BuyerId = buyerId;
            CreatedAt = DateTime.Now;
        }

        public void AddItem(string catalogItemId, string sku, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.CatalogItemId == catalogItemId))
            {
                _items.Add(new BasketItem(catalogItemId, sku, quantity, unitPrice));
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            existingItem.AddQuantity(quantity);
        }
    }
}
