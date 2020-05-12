using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Sell.Core.Domain;
using Sell.Core.Extension;
using Sell.Data.Repositores;
using Sell.Serivces.Extentions;
using Sell.Services.DTOs.Invoice;
using Sell.Services.DTOs.InvoiceItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        #region Filed
        private readonly IRepository<Sell.Core.Domain.Invoice> _invoceRepository = null;
        private readonly IRepository<Sell.Core.Domain.InvoiceItem> _invoiceItemRepository = null;
        private readonly IRepository<Sell.Core.Domain.Product> _productRepository = null;
        private readonly IRepository<Sell.Core.Domain.Customer> _customerRepository = null;

        public InvoiceService(IRepository<Core.Domain.Invoice> invoceRepository, IRepository<InvoiceItem> invoiceItemRepository, IRepository<Core.Domain.Product> productRepository, IRepository<Customer> customerRepository)
        {
            _invoceRepository = invoceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }


        #endregion
        public async Task<bool> IsExistInvoiceAsync(int id)
        {
            var invoice = await _invoceRepository.GetByIdAsNoTrackingAsync(id);
            if (invoice == null)
                return false;
            return true;
        }
        public async Task<bool> IsExistInvoiceItemAsync(int id)
        {
            var invoice = await _invoiceItemRepository.GetByIdAsNoTrackingAsync(id);
            if (invoice == null)
                return false;
            return true;
        }
        public async Task<IEnumerable<InvoiceItemDTO>> GetAllInvoice()
        {
            var _list = await _invoceRepository.TableNoTracking
                .Select(p => new InvoiceItemDTO
                {
                    InvoiceType = p.InvoiceType,
                    TypeName = p.InvoiceType.ConvertInvoiceType(),
                    ID = p.ID,
                    InvoiceDate = p.InvoiceDate.ToPersian(),
                    CustomerID = p.CustomerID,
                    CustomerName = p.customer.CustomerName,
                    CustomerMobile = p.customer.CustomerMobile,
                    CustomerTell = p.customer.CustomerTell,
                    CustomerAddres = p.customer.CustomerAddres,
                    InvocePricePourche = p.InvocePricePourche,
                    InvoicePrice = p.InvoicePrice,
                    InvoiceDesc = p.InvoiceDesc,
                    CreateOn = p.CreateOn,
                    UpdateOn = p.UpdateOn,
                    LocalCreateOn = p.CreateOn.ToPersian(),
                    LocalUpdateOn = p.UpdateOn.ToPersian()

                }).ToListAsync();
            return _list;
        }
        public async Task<IEnumerable<InvoiceItemDTO>> GetInvoiceByFilter(InvoiceFilterDTO invoiceFilterDTO)
        {
            var query = _invoceRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(invoiceFilterDTO.CustomerName))
                query = query.Where(p => p.customer.CustomerName.Contains(invoiceFilterDTO.CustomerName));
            if (!string.IsNullOrEmpty(invoiceFilterDTO.CustomerMobile))
                query = query.Where(p => p.customer.CustomerMobile.Contains(invoiceFilterDTO.CustomerMobile));
            if (!string.IsNullOrEmpty(invoiceFilterDTO.CustomerAddres))
                query = query.Where(p => p.customer.CustomerAddres.Contains(invoiceFilterDTO.CustomerAddres));
            //if (invoiceFilterDTO.FromInvoiceDate!=null)
            //    query = query.Where(p => p.InvoiceDate>= invoiceFilterDTO.FromInvoiceDate);
            //if (invoiceFilterDTO.ToInvoiceDate != null)
            //    query = query.Where(p => p.InvoiceDate <= invoiceFilterDTO.ToInvoiceDate);
            if (invoiceFilterDTO.FromInvoicePrice != null)
                query = query.Where(p => p.InvoicePrice >= invoiceFilterDTO.FromInvoicePrice);
            if (invoiceFilterDTO.ToInvoicePrice != null)
                query = query.Where(p => p.InvoicePrice <= invoiceFilterDTO.ToInvoicePrice);
            if (invoiceFilterDTO.InvoiceType == 1)
                query = query.Where(p => p.InvoiceType == 1);
            if (invoiceFilterDTO.InvoiceType == 2)
                query = query.Where(p => p.InvoiceType == 2);
            var _list = await query
                .Select(p => new InvoiceItemDTO
                {
                    InvoiceType = p.InvoiceType,
                    TypeName = p.InvoiceType.ConvertInvoiceType(),
                    ID = p.ID,
                    InvoiceDate = p.InvoiceDate.ToPersian(),
                    CustomerID = p.CustomerID,
                    CustomerName = p.customer.CustomerName,
                    CustomerMobile = p.customer.CustomerMobile,
                    CustomerTell = p.customer.CustomerTell,
                    CustomerAddres = p.customer.CustomerAddres,
                    InvocePricePourche = p.InvocePricePourche,
                    InvoicePrice = p.InvoicePrice,
                    InvoiceDesc = p.InvoiceDesc,
                    CreateOn = p.CreateOn,
                    UpdateOn = p.UpdateOn,
                    LocalCreateOn = p.CreateOn.ToPersian(),
                    LocalUpdateOn = p.UpdateOn.ToPersian()

                }).ToListAsync();
            return _list;
        }
        public async Task<IEnumerable<InvoiceItemItemDTO>> GetInvoiceItemByInvoiceID(int id)
        {
            var _list = await _invoiceItemRepository.TableNoTracking
                .Where(p => p.InvoiceID == id)
               .Select(p => new InvoiceItemItemDTO
               {
                   ID = p.ID,
                   InvoceItemCount = p.InvoceItemCount,
                   FeePricePurche = p.InvoceItemFeePurche,
                   FeePriceSell = p.InvoceItemFeeSell,
                   ProductName = p.Product.ProductName,
                   CustomerName = p.Invoice.customer.CustomerName,
                   CreateOn = p.CreateOn,
                   UpdateOn = p.UpdateOn,
                   LocalCreateOn = p.CreateOn.ToPersian(),
                   LocalUpdateOn = p.UpdateOn.ToPersian()

               }).ToListAsync();
            return _list;
        }
        public async Task<InvoiceDTO> RegisterInvoiceAsync(InvoiceDTO invoiceDTO)
        {
            var invoice = invoiceDTO.ToEntity<Sell.Core.Domain.Invoice>();
            await _invoceRepository.InsertAsync(invoice);
            invoiceDTO.ID = invoice.ID;
            return invoiceDTO;
        }
        public async Task FinishInvoiceAync(int id)
        {
            var invoice = await _invoceRepository.GetByIdAsync(id);
            var invoiceitem = await _invoiceItemRepository.TableNoTracking.Where(p => p.InvoiceID == id).ToListAsync();
            long FeeSell = 0;
            long FeePurche = 0;
            foreach (var item in invoiceitem)
            {
                FeeSell = FeeSell + item.InvoceItemFeeSell;
                FeePurche = FeePurche + item.InvoceItemFeePurche;
            }
            invoice.InvocePricePourche = FeePurche;
            invoice.InvoicePrice = FeeSell;
            _invoceRepository.Update(invoice);
            await SendEmailAsync(invoice);
        }


        public async Task RefInvoiceAsync(int id)
        {
            var invoice = await _invoceRepository.GetByIdAsync(id);
            var invoiceitem = await _invoiceItemRepository.Table.Where(p => p.InvoiceID == id).ToListAsync();

            foreach (var item in invoiceitem)
            {
                var product = _productRepository.GetById(item.ProductID);
                product.ProductLastSuply = product.ProductLastSuply + item.InvoceItemCount;
                await _productRepository.UpdateAsync(product);

            }
            invoice.InvoiceType = 2;
            await _invoceRepository.UpdateAsync(invoice);
        }
        public async Task<Sell.Core.Domain.InvoiceItem> RegisterInvoiceItemAsync(InvoiceItemRegisterDTO InvoiceItemRegisterDTO)
        {
            var invoiceitem = InvoiceItemRegisterDTO.ToEntity<Sell.Core.Domain.InvoiceItem>();
            var product = await _productRepository.GetByIdAsync(InvoiceItemRegisterDTO.ProductID);
            await InsertParams(InvoiceItemRegisterDTO, invoiceitem, product);
            await UpdateSuplay(InvoiceItemRegisterDTO, product);
            return invoiceitem;
        }
        public async Task RemoveInvoiceItemAsync(int id)
        {
            var invoiceitem = await _invoiceItemRepository.GetByIdAsync(id);
            await UpdateSuplayAfterRemove(invoiceitem);
            await _invoiceItemRepository.DeleteAsync(invoiceitem);

        }

        #region Extra
        private  async Task SendEmailAsync(Core.Domain.Invoice invoice)
        {
            var customer = await _customerRepository.GetByIdAsync(invoice.CustomerID);
            string Reciver = customer.CustomerEmail;
            string ReciverName = customer.CustomerName;
            string InvoiceID = invoice.ID.ToString();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Test testian", "testalihf@gmail.com"));
            message.To.Add(new MailboxAddress(ReciverName, Reciver));
            message.Subject = "شرکت مهدیسان شرق";
            message.Body = new TextPart("plain")
            {
                Text = " سلام مشتری گرامی"+ " "+  ReciverName  + " فاکنور شما با شماره  " +  InvoiceID  + " با موفقیت صادر شد مبلغ  :" + invoice.InvoicePrice +"تومان"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("testalihf@gmail.com", "Test123#");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        private async Task UpdateSuplayAfterRemove(InvoiceItem invoiceitem)
        {
            var product = await _productRepository.GetByIdAsync(invoiceitem.ProductID);
            product.ProductLastSuply = product.ProductLastSuply + invoiceitem.InvoceItemCount;
            await _productRepository.UpdateAsync(product);
        }

        private async Task UpdateSuplay(InvoiceItemRegisterDTO InvoiceItemRegisterDTO, Core.Domain.Product product)
        {
            product.ProductLastSuply = product.ProductLastSuply - InvoiceItemRegisterDTO.InvoceItemCount;
            await _productRepository.UpdateAsync(product);
        }
        private async Task InsertParams(InvoiceItemRegisterDTO InvoiceItemRegisterDTO, InvoiceItem invoiceitem, Core.Domain.Product product)
        {
            if (InvoiceItemRegisterDTO.InvoceItemCount > product.ProductLastSuply)
            {
                throw new System.ArgumentException("تعداد وارده بیشتر از موجودی میباشد", "original");
            }
            invoiceitem.InvoceItemCount = InvoiceItemRegisterDTO.InvoceItemCount;
            invoiceitem.InvoceItemFeePurche = product.ProductLastPourchFee * InvoiceItemRegisterDTO.InvoceItemCount;
            invoiceitem.InvoceItemFeeSell = product.ProductLastPrice * InvoiceItemRegisterDTO.InvoceItemCount;
            invoiceitem.InvoiceID = InvoiceItemRegisterDTO.InvoiceID;
            invoiceitem.ProductID = InvoiceItemRegisterDTO.ProductID;
            var query = await _invoiceItemRepository.TableNoTracking
                .Where(p => p.InvoiceID == InvoiceItemRegisterDTO.InvoiceID)
                .ToListAsync();
            foreach (var item in query)
            {
                if (item.ProductID == InvoiceItemRegisterDTO.ProductID)
                {
                    throw new System.ArgumentException("کالای وارد شده در فاکتور وجود دارد", "original");
                }
            }

            await _invoiceItemRepository.InsertAsync(invoiceitem);

        }
        #endregion
    }
}
