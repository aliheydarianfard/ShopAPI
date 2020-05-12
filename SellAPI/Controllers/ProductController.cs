using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devsharp.Framwork.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sell.FrameWork;
using Sell.Services.Catalog.Product;
using Sell.Services.DTOs.Product;
using Sell.Services.DTOs.ProductPrice;

namespace SellAPI.Controllers
{
    [Authorize]
    public class ProductController : MyBaseController
    {
        #region Field
        private readonly IProductService _productService = null;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [MyAutorize(Roles ="admin")]
        public async Task<IActionResult> GetAll()
        {
            var _list = await _productService.SearchAllProductAsync();
            return Ok(_list);
        }
   
        [HttpGet("find/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FindByID(int id)
        {
            var product = await _productService.SearchProductByIDAsync(id);
            return Ok(product);
        }
        [HttpGet("findbyfilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FindByFilter([FromQuery] ProductFilterDTO productFilterDTO) 
        {
            return Ok(await _productService.SearchProductByFilterAsync(productFilterDTO));
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromForm] ProductDTO productDTO)
        {
            if (productDTO.ID != 0)
                return BadRequest();
            await _productService.RegisterProductAsync(productDTO);
            return Ok();
        }
        [HttpDelete("remove/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(int id)
        {
            if (!await _productService.IsExistProducyAsync(id))
                return NotFound();
            await _productService.RemoveProductAsync(id);
            return Ok();
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] ProductDTO productDTO)
        {
            if (!await _productService.IsExistProducyAsync(productDTO.ID))
                return NotFound();
            await _productService.UpdateProductAsync(productDTO);
            return NoContent();
        }
        [HttpGet("getprice/{ProducID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductPriceAsync(int ProducID)
        {

            if (!await _productService.IsExistProducyAsync(ProducID))
                return NotFound();
            var _list = _productService.SearchProductPrice( ProducID);
            return Ok(_list);
        }

    }
}