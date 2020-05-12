using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.ProductName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.ProductLastPrice).IsRequired();
            builder.Property(p => p.ProductLastSuply).IsRequired();
            builder.Property(p => p.ProductStartTime).IsRequired();
            builder.HasMany(p => p.InvoiceItems).WithOne(p => p.Product).HasForeignKey(p => p.ProductID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
