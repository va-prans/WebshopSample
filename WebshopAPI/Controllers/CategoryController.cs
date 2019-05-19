using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Category.Queries.GetCategories;
using Webshop.Application.Category.Queries.GetCategoryItems;

namespace WebshopAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController
    {
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> GetCategories()
        {
            return new JsonResult(await Mediator.Send(new GetCategoriesQuery()));
        }

        [Route("items")]
        [HttpPost]
        public async Task<IActionResult> GetCategoryItems([FromBody] GetCategoryItemsQuery getCategoryItems)
        {
            return new JsonResult(await Mediator.Send(getCategoryItems));
        }
    }
}