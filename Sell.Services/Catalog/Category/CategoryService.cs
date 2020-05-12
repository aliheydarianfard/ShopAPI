using Microsoft.EntityFrameworkCore;
using Sell.Core.Extension;
using Sell.Data.Repositores;
using Sell.Serivces.Extentions;
using Sell.Services.DTOs.Category;
using Sell.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Category
{
    public class CategoryService : ICategoryService
    {
        #region filed
        private readonly IRepository<Sell.Core.Domain.Category> _categoryRepository = null;

        public CategoryService(IRepository<Core.Domain.Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion
        public async Task<bool> IsExistCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsNoTrackingAsync(id);
            if (category == null)
                return false;
            return true;
        }
        public async Task<CategoryDTO> RegisterCategoryAsync(CategoryDTO categoryDTO)
        {
			string test = "sad";
			
			var category = categoryDTO.ToEntity<Sell.Core.Domain.Category>();
            await _categoryRepository.InsertAsync(category);
            categoryDTO.ID = category.ID;
            return categoryDTO;
        }
        
        public async Task<IEnumerable<CategoryItemDTO>> SearchAllCategoryAsync()
        {
    
            var _list = await _categoryRepository.TableNoTracking
                 .Select(p => new CategoryItemDTO
                 {
                     ID = p.ID,
                     Name = p.Name,
                     Description = p.Description,
                     LocalCreateOn = p.CreateOn.ToPersian(),
                     LocalUpdateOn = p.UpdateOn.ToPersian(),
                     CreateOn = p.CreateOn,
                     UpdateOn = p.UpdateOn
                 }).ToListAsync();

            return _list;
        }
        public async Task<CategoryItemDTO> SearchCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryDTO = category.TODTO<CategoryItemDTO>();
            return categoryDTO;

        }
        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDTO.ID);
            category.Name = categoryDTO.Name;
            await _categoryRepository.UpdateAsync(category);
        }
        public async Task RemoveCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsNoTrackingAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }
    }
}
