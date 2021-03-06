﻿using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webshop.Domain.Entities;

namespace Webshop.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(e => e.AccountId);
            builder.Property(e => e.AccountId).HasColumnName("AccountId");
            builder.HasIndex(e => e.Name).IsUnique(true);
            builder.Property(e => e.Name)
                .IsRequired()              
                .HasMaxLength(20);

            builder.Property(e => e.Password)
                .IsRequired();
       
            

            builder.HasOne(x => x.Address)
                .WithOne(y => y.Account)
                .HasForeignKey<Address>(z => z.AddressId);

            builder.HasOne(x => x.Cart)
                .WithOne(x => x.Account)
                .HasForeignKey<Cart>(z => z.CartId);


            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Account)
                .HasForeignKey(z => z.AccountId);
        }
    }
}
