using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Item.Queries.GetItemById;

namespace WebshopAPI.Controllers
{
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
    }
}