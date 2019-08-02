using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductVariant> _productVariantRepository;
        //private readonly IRepository<ProductBundle> _productBundleRepository;

        public ProductManager(IRepository<Product> productRepository, 
            IRepository<ProductVariant> productVariantRepository)
        {
            this._productRepository = productRepository;
            this._productVariantRepository = productVariantRepository;
            //this._productBundleRepository = productBundleRepository;
        }

        public async Task CreateProduct(Product product)
        {
            await _productRepository.InsertAsync(product);
        }

        public async Task CreateProductBundle(ProductBundle productBundle)
        {
            /*
            await _productBundleRepository.InsertAsync(productBundle);
            */
            throw new NotImplementedException();
        }

        public async Task CreateProductVariant(ProductVariant productVariant)
        {
            await _productVariantRepository.InsertAsync(productVariant);
        }

        public Product GetProduct(int id)
        {
            var entity = _productRepository.GetAllIncluding(a => a.Variants)
                .FirstOrDefault(b => b.Id == id);

            if (entity == null)
            {
                throw new UserFriendlyException("Product not found");
            }

            return entity;
        }

        public ProductBundle GetProductBundle(int id)
        {
            /*
            var entity = _productBundleRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Product Bundle not found");
            }
            return entity;*/
            throw new NotImplementedException();
        }

        public ProductVariant GetProductVariant(int id)
        {
            var entity = _productVariantRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Product Variant not found");
            }
            return entity;
        }

        public async Task<ProductVariant> GetProductVariantAsync(int id)
        {
            var entity = await _productVariantRepository.FirstOrDefaultAsync(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Product Variant not found");
            }
            return entity;
        }

        public async Task UpdateProductStock(int productVariantId, int qty)
        {
            var productVariant = GetProductVariant(productVariantId);

            if (qty <= 0)
                throw new UserFriendlyException("Quantity can't be negative");

            productVariant.IsCommitted += qty;

            await _productVariantRepository.UpdateAsync(productVariant);
        }
    }
}
