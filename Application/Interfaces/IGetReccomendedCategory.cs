using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Entities;

namespace Webshop.Application.Interfaces
{
    public interface IRecommendedCategoryService
    {
        Task<List<Component>> GetRecommendedCategories(string currentCategory);
    }
}
