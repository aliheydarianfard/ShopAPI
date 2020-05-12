using Microsoft.EntityFrameworkCore;
using Sell.Core.Caching;
using Sell.Core.Domain;
using Sell.Core.Extension;
using Sell.Data.Repositores;
using Sell.Serivces.Extentions;
using Sell.Services.DTOs.Product;
using Sell.Services.DTOs.ProductPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sell.Services.Catalog.Product
{
    public class ProductService : IProductService
    {
        #region Filed
        private readonly IRepository<Sell.Core.Domain.Product> _Productrepository = null;
        private readonly IRepository<Sell.Core.Domain.Category> _categoryRepository = null;
        private readonly IRepository<Sell.Core.Domain.ProductPrice> _prdouctPriceRepository = null;
        private readonly ICacheManager _cacheManager = null;
        public ProductService(IRepository<Core.Domain.Product> productrepository, IRepository<Core.Domain.Category> categoryRepository, IRepository<ProductPrice> prdouctPriceRepository, ICacheManager cacheManager)
        {
            _Productrepository = productrepository;
            _categoryRepository = categoryRepository;
            _prdouctPriceRepository = prdouctPriceRepository;
            _cacheManager = cacheManager;
        }

        #endregion
        public async Task<bool> IsExistProducyAsync(int id)
        {
            var Product = await _Productrepository.GetByIdAsNoTrackingAsync(id);
            if (Product == null)
                return false;
            return true;
        }
        public async Task<ProductDTO> RegisterProductAsync(ProductDTO productDTO)
        {
            var product = productDTO.ToEntity<Sell.Core.Domain.Product>();
            await _Productrepository.InsertAsync(product);
            productDTO.ID = product.ID;
            ProductPriceDTO productPriceDTO = new ProductPriceDTO();
            productPriceDTO.ProductPriceDate = DateTime.Now;
            productPriceDTO.ProductPricePurch = product.ProductLastPourchFee;
            productPriceDTO.ProductPriceSell = product.ProductLastPrice;
            productPriceDTO.ProductID = product.ID;
            var productprice = productPriceDTO.ToEntity<ProductPrice>();
            await _prdouctPriceRepository.InsertAsync(productprice);
            return productDTO;
        }
        public async Task<IEnumerable<ProductItemDTO>> SearchAllProductAsync()
        {
            var _list = await _Productrepository.TableNoTracking
                .Select(p => new ProductItemDTO
                {
                    ID = p.ID,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductLastPrice = p.ProductLastPrice,
                    ProductLastPourchFee = p.ProductLastPourchFee,
                    ProductLastSuply = p.ProductLastSuply,
                    ProductStartTime = p.ProductStartTime.ToPersian(),
                    ProductActive = p.ProductActive,
                    ProductActivePersian = p.ProductActive.ConvertActiveProduct(),
                    CreateOn = p.CreateOn,
                    UpdateOn = p.UpdateOn,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category.Name,
                    LocalCreateOn = p.CreateOn.ToPersian(),
                    LocalUpdateOn = p.UpdateOn.ToPersian()
                }).ToListAsync();
            foreach (var item in _list)
            {
                if (item.ProductLastSuply == 0)
                {
                    item.ProductActive = 2;
                    item.ProductActivePersian = "نا موجود";
                }
            }
            return _list;
        }
        public async Task<IEnumerable<ProductItemDTO>> SearchUnAvailableProductAsync()
        {
            var _list = await _Productrepository.TableNoTracking
                .Where(p=>p.ProductLastSuply<=0)
                .Select(p => new ProductItemDTO
                {
                    ID = p.ID,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductLastPrice = p.ProductLastPrice,
                    ProductLastPourchFee = p.ProductLastPourchFee,
                    ProductLastSuply = p.ProductLastSuply,
                    ProductStartTime = p.ProductStartTime.ToPersian(),
                    ProductActive = p.ProductActive,
                    ProductActivePersian = p.ProductActive.ConvertActiveProduct(),
                    CreateOn = p.CreateOn,
                    UpdateOn = p.UpdateOn,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category.Name,
                    LocalCreateOn = p.CreateOn.ToPersian(),
                    LocalUpdateOn = p.UpdateOn.ToPersian()
                }).ToListAsync();
            foreach (var item in _list)
            {
                if (item.ProductLastSuply == 0)
                {
                    item.ProductActive = 2;
                    item.ProductActivePersian = "نا موجود";
                }
            }
            return _list;
        }
        public async Task<ProductItemDTO> SearchProductByIDAsync(int id)
        {
            var product = await _Productrepository.GetByIdAsync(id);

            var ProductdTO = product.TODTO<ProductItemDTO>();
            var category = await _categoryRepository.GetByIdAsync(product.CategoryID);
            product.Category = category;
            ProductdTO.ProductActivePersian = product.ProductActive.ConvertActiveProduct();
            ProductdTO.CategoryName = product.Category.Name;
            return ProductdTO;
        }
        public async Task<IEnumerable<ProductItemDTO>> SearchProductByFilterAsync(ProductFilterDTO productFilterDTO)
        {
           var _list= await _cacheManager.GetAsych("Product", 60, async () =>
            {
                var Query = _Productrepository.TableNoTracking;
                if (!string.IsNullOrEmpty(productFilterDTO.ProductName))
                    Query = Query.Where(p => p.ProductName.Contains(productFilterDTO.ProductName));
                if (productFilterDTO.ProductFromPrice.HasValue && productFilterDTO.ProductFromPrice != 0)
                    Query = Query.Where(p => p.ProductLastPrice >= productFilterDTO.ProductFromPrice);
                if (productFilterDTO.ProductToPrice.HasValue && productFilterDTO.ProductToPrice != 0)
                    Query = Query.Where(p => p.ProductLastPrice <= productFilterDTO.ProductToPrice);
                if (productFilterDTO.CategoryID.HasValue && productFilterDTO.CategoryID != 0)
                    Query = Query.Where(p => p.CategoryID == productFilterDTO.CategoryID);
                if (productFilterDTO.ProductActive == 1)
                    Query = Query.Where(p => p.ProductActive == 1);
                if (productFilterDTO.ProductActive == 2)
                    Query = Query.Where(p => p.ProductActive == 2);
                return await Query
                  .Select(p => new ProductItemDTO
                  {

                      ID = p.ID,
                      ProductName = p.ProductName,
                      ProductDescription = p.ProductDescription,
                      ProductLastPrice = p.ProductLastPrice,
                      ProductLastPourchFee = p.ProductLastPourchFee,
                      ProductLastSuply = p.ProductLastSuply,
                      ProductStartTime = p.ProductStartTime.ToPersian(),
                      ProductActive = p.ProductActive,
                      ProductActivePersian = p.ProductActive.ConvertActiveProduct(),
                      CategoryID = p.CategoryID,
                      CategoryName = p.Category.Name,
                      CreateOn = p.CreateOn,
                      UpdateOn = p.UpdateOn,
                      LocalCreateOn = p.CreateOn.ToPersian(),
                      LocalUpdateOn = p.UpdateOn.ToPersian()
                  }).ToListAsync();
               

            });
            foreach (var item in _list)
            {
                if (item.ProductLastSuply == 0)
                {
                    item.ProductActive = 2;
                    item.ProductActivePersian = "نا موجود";
                }
            }

            return _list;
        }
        public async Task RemoveProductAsync(int id)
        {
            var product = _Productrepository.GetByIdAsNoTracking(id);
            await _Productrepository.DeleteAsync(product);
        }
        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            var product = await _Productrepository.GetByIdAsync(productDTO.ID);
            if (product.ProductLastPrice != productDTO.ProductLastPrice && product.ProductLastPrice != productDTO.ProductLastPrice)
            {
                ProductPriceDTO productPriceDTO = new ProductPriceDTO();
                productPriceDTO.ProductPriceDate = DateTime.Now;
                productPriceDTO.ProductPricePurch = productDTO.ProductLastPourchFee;
                productPriceDTO.ProductPriceSell = productDTO.ProductLastPrice;
                productPriceDTO.ProductID = productDTO.ID;
                var productprice = productPriceDTO.ToEntity<ProductPrice>();
                await _prdouctPriceRepository.InsertAsync(productprice);
            }
            product.ProductName = productDTO.ProductName;
            product.ProductDescription = productDTO.ProductDescription;
            product.ProductLastPrice = productDTO.ProductLastPrice;
            product.ProductLastPourchFee = productDTO.ProductLastPourchFee;
            product.ProductLastSuply = productDTO.ProductLastSuply;
            product.ProductStartTime = productDTO.ProductStartTime;
            product.ProductActive = productDTO.ProductActive;


            await _Productrepository.UpdateAsync(product);
        }
        public IEnumerable<ProductPriceItemDTO> SearchProductPrice(int ProducID)
        {
            var Query = _prdouctPriceRepository.TableNoTracking;
            if (ProducID != 0)
               Query = Query.Where(p => p.ProductID == ProducID);             
            var _list =  Query
                .Select(p => new ProductPriceItemDTO
                {

                    ProductName = p.product.ProductName,
                    ProductID = p.ProductID,
                    ProductPricePurch = p.ProductPricePurch,
                    ProductPriceSell = p.ProductPriceSell,
                    ProductPriceDate = p.ProductPriceDate.ToPersian(),

                }).ToList();
            return _list;

        }

      
    }
}
