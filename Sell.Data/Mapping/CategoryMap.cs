using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
    
            builder.HasData(new Category
            {
                ID = 1,
                CreateOn = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                Name = "دسته",
                UpdateOn = Convert.ToDateTime(DateTime.Now.ToShortDateString())
            });
        }
    }
}
