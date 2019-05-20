using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webshop.Domain.Entities;

namespace Webshop.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.CountryId);
            builder.Property(e => e.CountryId).HasColumnName("CountryId");
            builder.Property(e => e.Name).IsRequired();
            builder.HasMany(x => x.Addresses)
                .WithOne(y => y.Country)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(r => r.IsShippable).HasConversion(new BoolToZeroOneConverter<Int16>());
        }
    }
}
