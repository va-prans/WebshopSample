using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webshop.Domain.Entities;

namespace Webshop.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(e => e.InvoiceId);
            builder.Property(e => e.InvoiceId).HasColumnName("InvoiceId");
            builder.Property(r => r.IsPaid).HasConversion(new BoolToZeroOneConverter<Int16>());

        }
    }
}
