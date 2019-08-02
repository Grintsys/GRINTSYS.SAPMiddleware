using Abp.Dependency;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Products
{
    public interface IProductManager: IDomainService, ITransientDependency
    {
        Task CreateProduct(Product product);
        Task CreateProductVariant(ProductVariant productVariant);
        Task CreateProductBundle(ProductBundle productBundle);
        Product GetProduct(int id);
        ProductVariant GetProductVariant(int id);
        //List<ProductVariant> GetProductVariantByProductId(int id);
        ProductBundle GetProductBundle(int id);
    }
}
