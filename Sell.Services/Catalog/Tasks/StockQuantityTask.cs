using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Sell.Core.Tasks;
using Sell.Services.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace Sell.Service.Catalog.Tasks
{
    public class StockQuantityTask : ITaskSchduler
    {
        private readonly IProductService _productService;

        public StockQuantityTask(IProductService productService, ILogger<StockQuantityTask> logger)
        {
            _productService = productService;
            IsActiveInStartup = false;
            Cron = "0 0 0 1/1 * ? *";
        }
        public bool IsActiveInStartup { get; set; }
        public string Cron { get; set; }
        public void Run()
        {
            var _list = _productService.SearchUnAvailableProductAsync().Result;
            if (_list.Count() > 0)
            {
                foreach (var item in _list)
                {
                    #region SendEmail
                    var productName = item.ProductName;
                   
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Test testian", "testalihf@gmail.com"));
                    message.To.Add(new MailboxAddress("alihf", "alihf74@gmail.com"));
                    message.Subject = "شرکت مهدیسان شرق";
                    message.Body = new TextPart("plain")
                    {
                        Text = " مدیر گرامی محصول " + productName + " موجودی آن در انبار صفر میباشد"
                    };
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("testalihf@gmail.com", "Test123#");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    #endregion
                }
            }
        }
    }
}
