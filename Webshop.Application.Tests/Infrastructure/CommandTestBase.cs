using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Persistence;

namespace Webshop.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly WebshopContext _context;

        public CommandTestBase()
        {
            _context = WebshopContextFactory.Create();
        }

        public void Dispose()
        {
            WebshopContextFactory.Destroy(_context);
        }
    }
}
