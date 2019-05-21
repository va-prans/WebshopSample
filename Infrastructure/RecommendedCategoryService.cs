using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application.Interfaces;

namespace Webshop.Infrastructure
{
    public class RecommendedCategoryService : IRecommendedCategoryService
    {
        public Task<List<string>> GetRecommendedCategories(string currentCategory)
        {
            throw new NotImplementedException();
        }
    }
}
