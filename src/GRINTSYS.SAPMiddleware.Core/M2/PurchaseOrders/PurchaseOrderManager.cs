using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.PurchaseOrders
{
    public class PurchaseOrderManager : DomainService, IPurchaseOrderManager
    {
        private readonly IRepository<PurchaseOrder> _repository;
        private readonly IRepository<PurchaseOrderDetail> _repositoryDetail;
        private readonly IRepository<PurchaseOrderExpense> _repositoryExpense;

        public PurchaseOrderManager(IRepository<PurchaseOrder> Repository,
            IRepository<PurchaseOrderDetail> RepositoryDetail,
            IRepository<PurchaseOrderExpense> RepositoryExpense)
        {
            _repository = Repository;
            _repositoryDetail = RepositoryDetail;
            _repositoryExpense = RepositoryExpense;
        }

        public async Task<int> CreatePurchaseOrder(PurchaseOrder input)
        {
            //var cart = await _cartRespository
            //    .FirstOrDefaultAsync(w =>
            //        w.UserId == order.UserId
            //        && w.TenantId == order.TenantId);

            //if (cart == null)
            //{
            //    throw new UserFriendlyException("You can't create a new Order because your cart is empty");
            //}

            return await _repository.InsertAndGetIdAsync(input);
        }

        public Task CreatePurchaseOrderDetail(PurchaseOrderDetail input)
        {
             return _repositoryDetail.InsertAsync(input);
        }

        public Task CreatePurchaseOrderExpense(PurchaseOrderExpense input)
        {
            return _repositoryExpense.InsertAsync(input);
        }

        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            var results = _repository.GetAll().ToList();
            return results;
        }

        public PurchaseOrder GetPurchaseOrder(int id)
        {
            var result = _repository.GetAll().Where(s => s.Id == id).FirstOrDefault();
            return result;
        }
    }
}
