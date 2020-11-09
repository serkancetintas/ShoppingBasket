using Microsoft.AspNetCore.Mvc;

namespace ShoppingBasket.Api.Controllers.Base
{
    [ApiController]
    public class BaseApiController:ControllerBase
    {
        public string UserId => "1";

    }
}
