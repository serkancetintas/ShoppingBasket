using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using ShoppingBasket.Application.Baskets;
using ShoppingBasket.Application.Contracts.Dtos;
using ShoppingBasket.Application.Contracts.Services;
using ShoppingBasket.Domain.BasketAggregate;
using ShoppingBasket.Repository.Abstract;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingBasket.Application.Test
{
    public class BasketServiceTest
    {
        [Theory, AutoMoqData]
        public void AddItemToBasket_Should_Return_ItemCount([Frozen]Mock<IMongoRepository<Basket>> basketRepository,
                                                            [Frozen]Mock<IStockService> stockService,
                                                            AddItemDto addItemDto,
                                                            string buyerId,
                                                            BasketService sut
                                                            )
        {
            Basket basket = new Basket(buyerId);

            basketRepository.Setup(r => r.FindOneAsync(p => p.BuyerId == buyerId)).ReturnsAsync(basket);
            basketRepository.Setup(r => r.UpdateOneAsync(basket));
            stockService.Setup(s => s.IsItInStock(addItemDto.Sku, addItemDto.Quantity)).Returns(true);

            Func<Task> action = async () =>
            {
                var result = await sut.AddItemToBasket(addItemDto, buyerId);
                result.Should().BeEquivalentTo(new AddItemResultDto { ItemCount = 1 });
            };

            action.Should().NotThrow<Exception>();
        }
    }

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() =>
        {
            var fixture = new Fixture()
                 .Customize(new AutoMoqCustomization());

            return fixture;
        })
        {
        }
    }
}
