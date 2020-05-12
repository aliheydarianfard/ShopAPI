using Microsoft.EntityFrameworkCore;
using Sell.Core.Extension;
using Sell.Data.Repositores;
using Sell.Serivces.Extentions;
using Sell.Services.DTOs.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Inventory
{
    public class InventoryService : IInventoryService
    {
        #region Filed
        private readonly IRepository<Sell.Core.Domain.Inventory> _inventoryRepository = null;
        private readonly IRepository<Sell.Core.Domain.Product> _productRepository = null;
        public InventoryService(IRepository<Core.Domain.Inventory> inventoryRepository, IRepository<Core.Domain.Product> productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }
        #endregion
        public async Task<bool> IsExistInventoryAsync(int id)
        {
            var Inventory = await _inventoryRepository.GetByIdAsNoTrackingAsync(id);
            if (Inventory == null)
                return false;
            return true;
        }
        public async Task<InventoryDTO> RegisterInventoryAsync(InventoryDTO inventoryDTO)
        {
            var inventory = inventoryDTO.ToEntity<Sell.Core.Domain.Inventory>();
            await _inventoryRepository.InsertAsync(inventory);
            inventoryDTO.ID = inventory.ID;
           await AddNewSuplayToProduct(inventoryDTO);
            return inventoryDTO;
        }
        public async Task<IEnumerable<InventoryItemDTO>> SearchAllInventoryAsync()
        {
            var _list = await _inventoryRepository.TableNoTracking
                 .Select(p => new InventoryItemDTO
                 {
                     ID = p.ID,
                     ProductName = p.product.ProductName,
                     InventoryCount = p.InventoryCount,
                     InventoryDate = p.InventoryDate,
                     InventoryDesc = p.InventoryDesc,
                     ProductID = p.ProductID,
                     CreateOn = p.CreateOn,
                     UpdateOn = p.UpdateOn,
                     LocalCreateOn = p.CreateOn.ToPersian(),
                     LocalUpdateOn = p.UpdateOn.ToPersian()
                 }).ToListAsync();

            return _list;
        }
        public async Task<IEnumerable<InventoryItemDTO>> SearchAllInventoryforProductAsync(int productid)
        {
            var _list = await _inventoryRepository.TableNoTracking
                .Where(p=>p.ProductID==productid)
                 .Select(p => new InventoryItemDTO
                 {
                     ID = p.ID,
                     ProductName = p.product.ProductName,
                     InventoryCount = p.InventoryCount,
                     InventoryDate = p.InventoryDate,
                     InventoryDesc = p.InventoryDesc,
                     ProductID = p.ProductID,
                     CreateOn = p.CreateOn,
                     UpdateOn = p.UpdateOn,
                     LocalCreateOn = p.CreateOn.ToPersian(),
                     LocalUpdateOn = p.UpdateOn.ToPersian()
                 }).ToListAsync();

            return _list;
        }
        public async Task<InventoryItemDTO> SearchInventoryByIdAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            var invnetorydto = inventory.TODTO<InventoryItemDTO>();
            invnetorydto.ProductName = inventory.product.ProductName;
            return invnetorydto;
        }
        public async Task UpdateInventoryAsync(InventoryDTO inventoryDTO)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryDTO.ID);
            if (inventory.InventoryCount != inventoryDTO.InventoryCount)
                await AddNewSuplayToProduct(inventoryDTO);
            inventory.InventoryCount = inventoryDTO.InventoryCount;
            inventory.InventoryDate = inventoryDTO.InventoryDate;
            inventory.InventoryDesc = inventoryDTO.InventoryDesc;
            inventory.ProductID = inventoryDTO.ProductID;
            await _inventoryRepository.UpdateAsync(inventory);
        }
        public async Task RemoveInventoryAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsNoTrackingAsync(id);
            await _inventoryRepository.DeleteAsync(inventory);
        }
        public async Task AddNewSuplayToProduct(InventoryDTO inventoryDTO) 
        {
            var product = await _productRepository.GetByIdAsync(inventoryDTO.ProductID);
            product.ProductLastSuply = product.ProductLastSuply + inventoryDTO.InventoryCount;
            await _productRepository.UpdateAsync(product);
        }
    }
}
