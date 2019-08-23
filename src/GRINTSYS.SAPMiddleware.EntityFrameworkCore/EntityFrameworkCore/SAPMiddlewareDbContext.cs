using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using GRINTSYS.SAPMiddleware.Authorization.Roles;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.MultiTenancy;
using GRINTSYS.SAPMiddleware.M2;

namespace GRINTSYS.SAPMiddleware.EntityFrameworkCore
{
    public class SAPMiddlewareDbContext : AbpZeroDbContext<Tenant, Role, User, SAPMiddlewareDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProductItem> CartProductItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDiscount> ClientDiscounts { get; set; }
        public DbSet<ClientTransaction> ClientTransactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentInvoiceItem> paymentInvoiceItems { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceHistory> InvoiceHistory { get; set; }
        public DbSet<Fees> Fees { get; set; }

        public SAPMiddlewareDbContext(DbContextOptions<SAPMiddlewareDbContext> options)
            : base(options)
        {
        }
    }
}
