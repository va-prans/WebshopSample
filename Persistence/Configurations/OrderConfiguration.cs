using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webshop.Domain.Entities;

namespace Webshop.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderId);
            builder.Property(e => e.OrderId).HasColumnName("CartId");

            builder.HasOne(s => s.Invoice)
                .WithOne(ad => ad.Order)
                .HasForeignKey<Invoice>(ad => ad.OrderId);
        }
    }
}
