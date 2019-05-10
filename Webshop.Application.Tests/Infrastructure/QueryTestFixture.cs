using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public WebshopContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = WebshopContextFactory.Create();
        }

        public void Dispose()
        {
            WebshopContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
