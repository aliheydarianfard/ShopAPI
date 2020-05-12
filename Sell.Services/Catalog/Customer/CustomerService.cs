using Microsoft.EntityFrameworkCore;
using Sell.Core.Extension;
using Sell.Data.Repositores;
using Sell.Serivces.Extentions;
using Sell.Services.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Services.Catalog
{
    public class CustomerService : ICustomerService
    {
        #region Field
        private readonly IRepository<Sell.Core.Domain.Customer> _customerRepository = null;

        public CustomerService(IRepository<Core.Domain.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion
        public async Task<bool> IsExistCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsNoTrackingAsync(id);
            if (customer == null)
                return false;
            return true;
        }
        public async Task<CustomerDTO> RegisterCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = customerDTO.ToEntity<Sell.Core.Domain.Customer>();
            await _customerRepository.InsertAsync(customer);
            customerDTO.ID = customer.ID;
            return customerDTO;
        }
        public async Task<IEnumerable<CustomerItemDTO>> SearchAllCustomerAsync()
        {
            var _list = await _customerRepository.TableNoTracking
                .Select(p => new CustomerItemDTO
                {
                    ID = p.ID,
                    CustomerName = p.CustomerName,
                    CustomerMobile = p.CustomerMobile,
                    CustomerTell = p.CustomerTell,
                    CustomerEmail = p.CustomerEmail,
                    CustomerAddres = p.CustomerAddres,
                    RegisterDate = p.RegisterDate,
                    CustomerActive = p.CustomerActive,
                    CustomerActiveName = p.CustomerActive.ConvertActiveCustomer(),
                    LocalCreateOn = p.CreateOn.ToPersian(),
                    LocalUpdateOn = p.UpdateOn.ToPersian(),
                    CreateOn = p.CreateOn,
                    UpdateOn = p.UpdateOn
                }).ToListAsync();
            return _list;
        }
        public async Task<IEnumerable<CustomerItemDTO>> SearchCustomerByFilter(CustomerItemFilterDTO customerItemFilterDTO)
        {
            var query = _customerRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(customerItemFilterDTO.CustomerName))
                query = query.Where(p => p.CustomerName.Contains(customerItemFilterDTO.CustomerName));
            if (!string.IsNullOrEmpty(customerItemFilterDTO.CustomerMobile))
                query = query.Where(p => p.CustomerMobile.Contains(customerItemFilterDTO.CustomerMobile));
            if (customerItemFilterDTO.CustomerActive == 1)
                query = query.Where(p => p.CustomerActive == 1);
            if (customerItemFilterDTO.CustomerActive == 2)
                query = query.Where(p => p.CustomerActive == 2);


            var _list = await query
                .Select(p => new CustomerItemDTO
                {
                    ID = p.ID,
                    CustomerName = p.CustomerName,
                    CustomerMobile = p.CustomerMobile,
                    CustomerTell = p.CustomerTell,
                    CustomerEmail = p.CustomerEmail,
                    CustomerAddres = p.CustomerAddres,
                    RegisterDate = p.RegisterDate,
                    CustomerActive = p.CustomerActive,
                    CustomerActiveName = p.CustomerActive.ConvertActiveCustomer(),
                    LocalCreateOn = p.CreateOn.ToPersian(),
                    LocalUpdateOn = p.UpdateOn.ToPersian(),
                    CreateOn = p.CreateOn,
                    UpdateOn = p.UpdateOn
                }).ToListAsync();
            return _list;
        }
        public async Task<CustomerItemDTO> SearchCustomerByIDAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            var customerDTO = customer.TODTO<CustomerItemDTO>();
            customerDTO.CustomerActiveName = customer.CustomerActive.ConvertActiveCustomer();
            return customerDTO;
        }
        public async Task UpdateCoustomerAsync(CustomerDTO customerDTO)
        {
            var customer = await _customerRepository.GetByIdAsync(customerDTO.ID);
            customer.CustomerName = customerDTO.CustomerName;
            customer.CustomerMobile = customerDTO.CustomerMobile;
            customer.CustomerEmail = customerDTO.CustomerEmail;
            customer.CustomerAddres = customerDTO.CustomerAddres;
            customer.CustomerActive = customerDTO.CustomerActive;
            await _customerRepository.UpdateAsync(customer);
        }
        public async Task RemoveCustomerAsync(int id) 
        {
            var customer =await _customerRepository.GetByIdAsync(id);
            await _customerRepository.DeleteAsync(customer);
        }
    }
}
