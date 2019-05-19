using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Order.Commands.Create;
using Webshop.Application.Order.Commands.Pay;

namespace WebshopAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand createOrder)
        {
            return new JsonResult(await Mediator.Send(createOrder));
        }

        [Route("pay")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PayOrderCommand payOrder)
        {
            return new JsonResult(await Mediator.Send(payOrder));
        }
    }
}