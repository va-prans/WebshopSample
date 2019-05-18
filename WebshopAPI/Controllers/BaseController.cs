using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebshopAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => (IMediator) (_mediator ?? HttpContext.RequestServices.GetService(typeof(IMediator)));
    }
}