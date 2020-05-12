
using Microsoft.AspNetCore.Http;
using Sell.Serivces.DTOs;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Shop.Service.DTOs
{

    public class PictureUploadDTO : BaseDTO
    {
 
        public IFormFile File { get; set; }
        public string ContentType { get; set; }
        public string fileExtension { get; set; }
        public int ProductID { get; set; }

    }



}
