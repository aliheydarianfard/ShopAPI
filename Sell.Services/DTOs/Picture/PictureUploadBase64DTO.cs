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

  
    public class PictureUploadBase64DTO : BaseEntityDTO
    {
        public string File { get; set; }
        public string ContentType { get; set; }
        public string fileExtension { get; set; }

    }
  
}
