using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sell.FrameWork;
using Sell.Services.Catalog.Invoice;
using Sell.Services.DTOs.Invoice;
using Sell.Services.DTOs.InvoiceItem;

namespace SellAPI.Controllers
{

    public class InvoiceController : MyBaseController
    {
        #region Filed
        private readonly IInvoiceService _invoiceService = null;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> GetAllInvoice()
        {
            var list = await _invoiceService.GetAllInvoice();
            return Ok(list);
        }
        [HttpGet("invoicebyfilter")]
        public async Task<IActionResult> GetInvoiceByFilter([FromQuery]InvoiceFilterDTO invoiceFilterDTO)
        {
          var list= await _invoiceService.GetInvoiceByFilter(invoiceFilterDTO);
            return Ok(list);
        }
        [HttpGet("invoiceitem/{id}")]
        public async Task<IActionResult> GetInvoiceItemByInvoiceID(int id)
        {
            return Ok(await _invoiceService.GetInvoiceItemByInvoiceID(id));
        }
        [HttpPost("invoice")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegsiterInvoice([FromForm]InvoiceDTO invoiceDTO)
        {
            if (invoiceDTO.ID != 0)
                return BadRequest();
            await _invoiceService.RegisterInvoiceAsync(invoiceDTO);
            return Ok("فاکتور ثبت شد");

        }
        [HttpPost("invoiceitem")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterInvoiceItem([FromForm]InvoiceItemRegisterDTO invoiceItemRegisterDTO)
        {
            if (invoiceItemRegisterDTO.ID != 0)
                return BadRequest();
            await _invoiceService.RegisterInvoiceItemAsync(invoiceItemRegisterDTO);
            return Ok();
        }
        [HttpDelete("removeinvoiceitem/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveInvoiceItem(int id)
        {
            if (!await _invoiceService.IsExistInvoiceItemAsync(id))
                return NotFound("اقلام فاکتور مورد نظر موجود نیست");
            await _invoiceService.RemoveInvoiceItemAsync(id);
            return Ok(" اقلام فاکتور مورد نظر حذف شد");
        }
        [HttpPost("finishinvoice/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FinishInvoice(int id)
        {
            if (!await _invoiceService.IsExistInvoiceAsync(id))
                return NotFound("فاکتور مورد نظر یافت نشد");
            await _invoiceService.FinishInvoiceAync(id);
            return Ok("فاکتور مورد نظر بسته شد ");
        }
        [HttpPost("refinvoice/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RefInvoice(int id)
        {
            if (!await _invoiceService.IsExistInvoiceAsync(id))
                return NotFound("فاکتور مورد نظر یافت نشد");
            await _invoiceService.RefInvoiceAsync(id);
            return Ok("فاکتور مورد نظر برگشت داده شد");
        }
    }
}