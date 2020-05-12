using Sell.Core.Domain;
using Sell.Services.DTOs.Invoice;
using Sell.Services.DTOs.InvoiceItem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Invoice
{
    public interface IInvoiceService
    {
        Task FinishInvoiceAync(int id);
        Task<IEnumerable<InvoiceItemDTO>> GetAllInvoice();
        Task<IEnumerable<InvoiceItemDTO>> GetInvoiceByFilter(InvoiceFilterDTO invoiceFilterDTO);
        Task<IEnumerable<InvoiceItemItemDTO>> GetInvoiceItemByInvoiceID(int id);
        Task<bool> IsExistInvoiceAsync(int id);
        Task<bool> IsExistInvoiceItemAsync(int id);
        Task RefInvoiceAsync(int id);
        Task<InvoiceDTO> RegisterInvoiceAsync(InvoiceDTO invoiceDTO);
        Task<InvoiceItem> RegisterInvoiceItemAsync(InvoiceItemRegisterDTO InvoiceItemRegisterDTO);
        Task RemoveInvoiceItemAsync(int id);
    }
}