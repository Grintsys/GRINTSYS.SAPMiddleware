using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GRINTSYS.SAPMiddleware.M2.vwSapInvoices
{
    public class SapInvoiceManager : DomainService, ISapInvoiceManager
    {
        private readonly IRepository<VwSapInvoice> _repository;
        private readonly IRepository<VwSapInvoiceDetail> _repositoryDetail;
        private readonly IRepository<VwSapInvoiceExpense> _repositoryExpense;

        public SapInvoiceManager(IRepository<VwSapInvoice> Repository, 
            IRepository<VwSapInvoiceDetail> RepositoryDetail,
            IRepository<VwSapInvoiceExpense> RepositoryExpense)
        {
            _repository = Repository;
            _repositoryDetail = RepositoryDetail;
            _repositoryExpense = RepositoryExpense;
        }
        public VwSapInvoice GetSapInvoice(int Id)
        {
            var invoice = _repository.GetAll().Where(s => s.Id == Id).FirstOrDefault();
            return invoice;
        }
        public List<VwSapInvoiceDetail> GetSapInvoiceDetail(int Id)
        {
            var detail = _repositoryDetail.GetAll().Where(s => s.VwSapInvoiceId == Id).ToList();
            return detail;
        }
        public List<VwSapInvoiceExpense> GetSapInvoiceExpense(int Id)
        {
            var detail = _repositoryExpense.GetAll().Where(s => s.VwSapInvoiceId == Id).ToList();
            return detail;
        }
        public List<VwSapInvoice> GetAllSapInvoices()
        {
            var invoices = _repository.GetAll().ToList();
            return invoices;
        }
    }
}