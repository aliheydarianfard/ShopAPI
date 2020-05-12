using Sell.Services.DTOs.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Category
{
    public interface ICategoryService
    {
        Task<bool> IsExistCategoryAsync(int id);
        Task<CategoryDTO> RegisterCategoryAsync(CategoryDTO categoryDTO);
        Task RemoveCategoryAsync(int id);
        Task<IEnumerable<CategoryItemDTO>> SearchAllCategoryAsync();
        Task<CategoryItemDTO> SearchCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO);
    }
}