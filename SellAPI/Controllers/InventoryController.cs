using Microsoft.AspNetCore.Mvc;
using Sell.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sell.Services.Catalog.Inventory;
using Sell.Services.DTOs.Inventory;
using MimeKit;
using MailKit.Net.Smtp;

namespace SellAPI.Controllers
{
    public class InventoryController : MyBaseController
    {
        #region Filed
        private readonly IInventoryService _inventoryService = null;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        #endregion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {

            var _list = await _inventoryService.SearchAllInventoryAsync();
            return Ok();
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getallbyproduct/{productid}")]
        public async Task<IActionResult> GetAllForPrdouct(int productid)
        {
            var _list = await _inventoryService.SearchAllInventoryforProductAsync(productid);
            return Ok(_list);
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromForm] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO.ID != 0)
                return BadRequest();
            await _inventoryService.RegisterInventoryAsync(inventoryDTO);
            return Ok();
        }
        [HttpDelete("remove/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(int id)
        {
            if (!await _inventoryService.IsExistInventoryAsync(id))
                return NotFound();
            await _inventoryService.RemoveInventoryAsync(id);
            return Ok();

        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] InventoryDTO inventoryDTO)
        {
            if (!await _inventoryService.IsExistInventoryAsync(inventoryDTO.ID))
                return NotFound();
            await _inventoryService.UpdateInventoryAsync(inventoryDTO);
            return NoContent();
        }
        [HttpGet("find/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByID(int id)
        {
            if (!await _inventoryService.IsExistInventoryAsync(id))
                return NotFound();
            var _list = await _inventoryService.SearchInventoryByIdAsync(id);
            return Ok(_list);
        }
    }
}
