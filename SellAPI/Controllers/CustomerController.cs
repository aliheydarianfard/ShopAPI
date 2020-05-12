using Microsoft.AspNetCore.Mvc;
using Sell.FrameWork;
using Sell.Services.Catalog;
using Sell.Services.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SellAPI.Controllers
{
    public class CustomerController : MyBaseController
    {
        #region Filed
        private readonly ICustomerService _customerService = null;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var _list = await _customerService.SearchAllCustomerAsync();
            return Ok(_list);
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegsiterAsync([FromForm] CustomerDTO customerDTO)
        {
            if (customerDTO.ID != 0)
                return BadRequest();
            await _customerService.RegisterCustomerAsync(customerDTO);
            return Ok();
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            if (!await _customerService.IsExistCustomerAsync(id))
                return NotFound();
            var _list = await _customerService.SearchCustomerByIDAsync(id);
            return Ok(_list);
        }
        [HttpGet("getbyfilter")]
        public async Task<IActionResult> GetByFilterAsync([FromQuery]CustomerItemFilterDTO customerItemFilterDTO)
        {
            var _list = await _customerService.SearchCustomerByFilter(customerItemFilterDTO);
            return Ok(_list);
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCustomerAync([FromForm]CustomerDTO customerDTO)
        {
            if (!await _customerService.IsExistCustomerAsync(customerDTO.ID))
                return NotFound();
           await _customerService.UpdateCoustomerAsync(customerDTO);
            return NoContent();
        }
        [HttpDelete("remove/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveCustomerAsync(int id)
        {
            if (!await _customerService.IsExistCustomerAsync(id))
                return NotFound();
            await _customerService.RemoveCustomerAsync(id);
            return Ok();

        }
    }
}
