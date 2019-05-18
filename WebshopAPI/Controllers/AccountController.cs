using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Account.Commands.Create;

namespace WebshopAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
       

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand createAccount)
        {
            return new JsonResult(await Mediator.Send(createAccount));
        }       
    }
}