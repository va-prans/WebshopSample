using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Webshop.Application.Interfaces;
using Component = Webshop.Domain.Entities.Component;

namespace Webshop.Infrastructure
{
    public class RecommendedCategoryService : IRecommendedCategoryService
    {
        private IGraphClient _client;
        public RecommendedCategoryService(IGraphClient client)
        {
            _client = client;
        }

        public async Task<List<Component>> GetRecommendedCategories(string currentCategory)
        {
            var query1 = await _client.Cypher
                .OptionalMatch("(a:Component)-[CONNECTS_TO]-(b:Component)")
                .Where((Component a) => a.name == currentCategory)
                .Return((a, b) => new
                {
                    Source = a.As<Component>(),
                    Relation = b.CollectAs<Component>()
                })
                .ResultsAsync;
            return query1.ElementAt(0).Relation.ToList();
        }
    }
}
