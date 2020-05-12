using Microsoft.AspNetCore.Http;
using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Sell.Service.DTOs
{

    public class PictureDTO : BaseEntityDTO
    {
        public string VirtualPath { get; set; }
        public int ProductID { get; set; }

    }
    

}
