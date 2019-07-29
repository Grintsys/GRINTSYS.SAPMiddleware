using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductVariant> _productVariantRepository;
        private readonly IRepository<ProductBundle> _productBundleRepository;

        public ProductManager(IRepository<Product> productRepository, 
            IRepository<ProductVariant> productVariantRepository, 
            IRepository<ProductBundle> productBundleRepository)
        {
            this._productRepository = productRepository;
            this._productVariantRepository = productVariantRepository;
            this._productBundleRepository = productBundleRepository;
        }

        public Product GetProduct(int id)
        {
            var entity = _productRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Product not found");
            }

            return entity;
        }

        public ProductBundle GetProductBundle(int id)
        {
            var entity = _productBundleRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Product Bundle not found");
            }
            return entity;
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
    }
}
