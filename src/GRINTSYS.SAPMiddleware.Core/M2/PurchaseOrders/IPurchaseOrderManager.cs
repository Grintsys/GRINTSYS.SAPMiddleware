using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.PurchaseOrders
{
    public interface IPurchaseOrderManager : IDomainService
    {
        Task<int> CreatePurchaseOrder(PurchaseOrder input);
        Task CreatePurchaseOrderDetail(PurchaseOrderDetail input);
        Task CreatePurchaseOrderExpense(PurchaseOrderExpense input);
        PurchaseOrder GetPurchaseOrder(int id);
        List<PurchaseOrder> GetAllPurchaseOrders();
    }
}