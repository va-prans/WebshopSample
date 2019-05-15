using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webshop.Domain.Entities;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => new {ci.CartFk, ci.ItemFk});

            builder.HasOne(c => c.Cart)
                .WithMany(ci => ci.CartItems)
                .HasForeignKey(c => c.CartFk);
            builder.HasOne(i => i.Item)
                .WithMany(ci => ci.CartItems)
                .HasForeignKey(i => i.ItemFk);
        }
    }
}
