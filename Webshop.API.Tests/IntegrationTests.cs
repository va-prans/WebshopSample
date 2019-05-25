using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Webshop.Domain.Entities;
using Xunit;

namespace Webshop.API.Tests
{
    public class IntegrationTests
    {
       
        private readonly TestContext _context;

        public IntegrationTests()
        {
            _context = new TestContext();
        }

        [Theory]
        [InlineData("/api/categories")]
        public async Task GetAllCategories_ReturnsOkResponse(string url)
        {            
            // Act
            var response = await _context.Client.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/categories")]
        public async Task GetAllCategories_ReturnsAListOfCategories(string url)
        {
            // Act
            var response = await _context.Client.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(responseString);

            // Assert
            Assert.Empty(categories);
        }
    }
}


