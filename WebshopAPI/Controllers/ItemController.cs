using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Item.Queries.GetItemById;
using Webshop.Application.Item.Queries.GetRecommendedItems;

namespace WebshopAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/item")]
    [ApiController]
    public class ItemController : BaseController
    {
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> GetItem([FromBody] GetItemByIdQuery getItem)
        {
            return new JsonResult(await Mediator.Send(getItem));
        }

        [Route("recommended")]
        [HttpPost]
        public async Task<IActionResult> GetItem([FromBody] GetRecommendedItemsQuery getRecommended)
        {
            return new JsonResult(await Mediator.Send(getRecommended));
        }
    }
}