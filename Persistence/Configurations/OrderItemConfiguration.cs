using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.OrderItemId);
        builder.Property(e => e.OrderItemId).HasColumnName("OrderItemId");

            builder.HasOne(c => c.Order)
            .WithMany(ci => ci.OrderItems)
            .HasForeignKey(c => c.OrderFk);
        builder.HasOne(i => i.Item)
            .WithMany(ci => ci.OrderItems)
            .HasForeignKey(i => i.ItemFk);
    }
    }

}

