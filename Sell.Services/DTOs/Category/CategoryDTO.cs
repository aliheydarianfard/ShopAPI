using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Category
{
   public class CategoryDTO:BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
