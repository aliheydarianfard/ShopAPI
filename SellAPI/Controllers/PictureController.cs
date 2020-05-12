using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sell.FrameWork;
using Sell.Services.Catalog.Picture;
using Shop.Service.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SellAPI.Controllers
{
    public class PictureController:MyBaseController
    {
        #region Field
        private readonly IPictureService _pictureService = null;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        #endregion

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!await _pictureService.CheckExists(id.Value))
            {
                return NotFound();
            }
            var image = await _pictureService.SearchPictureByIdAsync(id.Value);

            return PhysicalFile(image.VirtualPath, "image/png");
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadAsync([FromForm]PictureUploadDTO image)
        {
            image.ContentType = image.File.FileName;
            image.fileExtension = Path.GetExtension(image.File.FileName);
            var pictureDTO = await _pictureService.RegisterPictureAsync(image);
            return CreatedAtAction("Get", new { id = pictureDTO.ID }, pictureDTO.ID);
        }
    }
}
