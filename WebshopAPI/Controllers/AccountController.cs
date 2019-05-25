using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Account.Commands.Login;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Account.Queries.GetAccountById;
using Webshop.Application.Address.Commands.Create;
using Webshop.Application.Address.Commands.Update;
//just a comment
namespace WebshopAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand login)
        {
            return new JsonResult(await Mediator.Send(login));
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand createAccount)
        {
            return new JsonResult(await Mediator.Send(createAccount));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Read([FromBody] GetAccountByIdQuery getAccount)
        {
            return new JsonResult(await Mediator.Send(getAccount));
        }

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateAccountCommand updateAccount)
        {
            return new JsonResult(await Mediator.Send(updateAccount));
        }
       
        [Route("address/update")]
        [HttpPost]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand updateAddress)
        {
            return new JsonResult(await Mediator.Send(updateAddress));
        }
    }
}