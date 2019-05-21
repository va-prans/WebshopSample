using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Application.Interfaces
{
    public interface IRecommendedCategoryService
    {
        Task<List<string>> GetRecommendedCategories(string currentCategory);
    }
}
