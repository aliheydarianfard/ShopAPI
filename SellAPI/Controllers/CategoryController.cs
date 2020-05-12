using Microsoft.AspNetCore.Mvc;
using Sell.FrameWork;
using Sell.Services.Catalog.Category;
using Sell.Services.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace SellAPI.Controllers
{
    public class CategoryController : MyBaseController
    {
        #region Filed
        private readonly ICategoryService _categoryService = null;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        #endregion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public async Task<IActionResult> GetAll()
        {
            var _list = await _categoryService.SearchAllCategoryAsync();
            return Ok(_list);
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromForm] CategoryDTO categoryDTO)
        {
            if (categoryDTO.ID != 0)
                return BadRequest();
            await _categoryService.RegisterCategoryAsync(categoryDTO);
            return Ok();
        }
        [HttpDelete("remove/{ids}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> Remove(int id)
        {
            if (!await _categoryService.IsExistCategoryAsync(id))
                return NotFound();
            await _categoryService.RemoveCategoryAsync(id);
            return Ok();

        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] CategoryDTO categoryDTO)
        {
            if (!await _categoryService.IsExistCategoryAsync(categoryDTO.ID))
                return NotFound();
            await _categoryService.UpdateCategoryAsync(categoryDTO);
            return NoContent();
        }
        [HttpGet("find/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByID(int id)
        {
            if (! await _categoryService.IsExistCategoryAsync(id))
                return NotFound();
            var _list= await _categoryService.SearchCategoryByIdAsync(id);
            return Ok(_list);
        }
    }
}
