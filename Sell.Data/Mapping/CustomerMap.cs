using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.Mapping
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.CustomerName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CustomerMobile).HasMaxLength(12).IsRequired();
            builder.Property(p => p.RegisterDate).HasMaxLength(30).IsRequired();
            builder.Property(p => p.CustomerTell).HasMaxLength(12);
           
        }
    }
}
