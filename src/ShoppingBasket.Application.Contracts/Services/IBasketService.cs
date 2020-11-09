using ShoppingBasket.Application.Contracts.Dtos;
using System.Threading.Tasks;

namespace ShoppingBasket.Application.Contracts.Services
{
    public interface IBasketService
    {
        Task<AddItemResultDto> AddItemToBasket(AddItemDto dto, string buyerId);
    }
}
