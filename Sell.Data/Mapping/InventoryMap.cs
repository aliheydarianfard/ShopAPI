using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.Mapping
{
    public class InventoryMap : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(p => p.InventoryCount).IsRequired();
            builder.Property(p => p.InventoryDate).IsRequired();
        }
    }
}
