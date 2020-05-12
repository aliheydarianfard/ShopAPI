using Sell.Services.DTOs.Product;
using Sell.Services.DTOs.ProductPrice;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Sell.Services.Catalog.Product
{
    public interface IProductService
    {
        Task<bool> IsExistProducyAsync(int id);
        Task<ProductDTO> RegisterProductAsync(ProductDTO productDTO);
        Task RemoveProductAsync(int id);
        Task<IEnumerable<ProductItemDTO>> SearchAllProductAsync();
        Task<IEnumerable<ProductItemDTO>> SearchProductByFilterAsync(ProductFilterDTO productFilterDTO);
        Task<ProductItemDTO> SearchProductByIDAsync(int id);
        IEnumerable<ProductPriceItemDTO> SearchProductPrice(int ProducID);
        Task<IEnumerable<ProductItemDTO>> SearchUnAvailableProductAsync();
        Task UpdateProductAsync(ProductDTO productDTO);
    }
}