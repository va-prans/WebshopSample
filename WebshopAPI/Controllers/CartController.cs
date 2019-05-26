using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Cart.Commands.Add;
using Webshop.Application.Cart.Commands.Remove;
using Webshop.Application.Cart.Queries.GetCartByAccountId;
using Webshop.Application.Category.Queries.GetCategories;

namespace WebshopAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/cart")]
    [ApiController]
    public class CartController : BaseController
    {
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartCommand addItemToCart)
        {
            return new JsonResult(await Mediator.Send(addItemToCart));
        }

        [Route("remove")]
        [HttpPost]
        public async Task<IActionResult> RemoveItemFromCart([FromBody] RemoveItemFromCartCommand removeItemFromCart)
        {
            return new JsonResult(await Mediator.Send(removeItemFromCart));
        }
    }
}