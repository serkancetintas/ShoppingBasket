using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Api.Controllers.Base;
using ShoppingBasket.Application.Contracts.Dtos;
using ShoppingBasket.Application.Contracts.Services;
using System.Threading.Tasks;

namespace ShoppingBasket.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ShoppingBasketController : BaseApiController
    {
        private readonly IBasketService _basketService;

        public ShoppingBasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket([FromBody]AddItemDto dto)
        {
            var result = await _basketService.AddItemToBasket(dto, UserId);

            return Ok(result);
        }
    }
}