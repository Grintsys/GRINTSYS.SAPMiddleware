using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Products
{
    public interface IProductManager: IDomainService
    {
        Task CreateProduct(Product product);
        Task CreateProductVariant(ProductVariant productVariant);
        Task CreateProductBundle(ProductBundle productBundle);
        Product GetProduct(int id);
        ProductVariant GetProductVariant(int id);
        ProductBundle GetProductBundle(int id);
    }
}
