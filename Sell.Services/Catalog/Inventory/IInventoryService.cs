using Sell.Services.DTOs.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Inventory
{
    public interface IInventoryService
    {
        Task<bool> IsExistInventoryAsync(int id);
        Task<InventoryDTO> RegisterInventoryAsync(InventoryDTO inventoryDTO);
        Task RemoveInventoryAsync(int id);
        Task<IEnumerable<InventoryItemDTO>> SearchAllInventoryAsync();
        Task<IEnumerable<InventoryItemDTO>> SearchAllInventoryforProductAsync(int productid);
        Task<InventoryItemDTO> SearchInventoryByIdAsync(int id);
        Task UpdateInventoryAsync(InventoryDTO inventoryDTO);
    }
}