using Sell.Services.DTOs.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell.Services.Catalog
{
    public interface ICustomerService
    {
        Task<bool> IsExistCustomerAsync(int id);
        Task<CustomerDTO> RegisterCustomerAsync(CustomerDTO customerDTO);
        Task RemoveCustomerAsync(int id);
        Task<IEnumerable<CustomerItemDTO>> SearchAllCustomerAsync();
        Task<IEnumerable<CustomerItemDTO>> SearchCustomerByFilter(CustomerItemFilterDTO customerItemFilterDTO);
        Task<CustomerItemDTO> SearchCustomerByIDAsync(int id);
        Task UpdateCoustomerAsync(CustomerDTO customerDTO);
    }
}