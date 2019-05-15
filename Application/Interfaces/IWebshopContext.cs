using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;

namespace Webshop.Application.Interfaces
{
    public interface IWebshopContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<Domain.Entities.Account> Accounts { get; set; }
        DbSet<Domain.Entities.Address> Addresses { get; set; }
        DbSet<Domain.Entities.Cart> Carts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Order> Orders { get; set; }
    }
}
