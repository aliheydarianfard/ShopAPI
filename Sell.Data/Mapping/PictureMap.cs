using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.Mapping
{
    public class PictureMap : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
           
        }
    }
}
