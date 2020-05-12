
using Sell.Core.Domain;
using Sell.Data.Repositores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Serivces.Extentions
{
    public static class ActiveExtension
    {
        #region Filed
        private static readonly IRepository<Category> _repositoryCategory = null;
        #endregion
        public static string ConvertActiveProduct(this byte ProductActive)
        {
            string active = "";
            if (ProductActive == 1)
                active = "موجود";
            if (ProductActive == 2)
                active = "غیر موجود";

            return active;

        }
        public static string ConvertActiveCustomer(this byte CustomerActive)
        {
            string active = "";
            if (CustomerActive == 1)
                active = "فعال";
            if (CustomerActive == 2)
                active = "غیر فعال";

            return active;

        }
        public static string ConvertInvoiceType(this byte InvoiceType)
        {
            string active = "";
            if (InvoiceType == 1)
                active = "فروش";
            if (InvoiceType == 2)
                active = "مرجوعی";
            return active;

        }
    }
}
