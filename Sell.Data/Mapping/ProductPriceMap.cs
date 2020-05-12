using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.Mapping
{
    public class ProductPriceMap : IEntityTypeConfiguration<ProductPrice>
    {
       

        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.Property(p => p.ProductPricedesc).HasMaxLength(500);
            builder.Property(p => p.ProductPriceDate).IsRequired();
            builder.Property(p => p.ProductPricePurch).IsRequired();
            builder.Property(p => p.ProductPriceSell).IsRequired();
        }
    }
}
