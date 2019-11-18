using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.PurchaseOrders;
using GRINTSYS.SAPMiddleware.M2.vwSapInvoices;
using GRINTSYS.SAPMiddleware.PurchaseOrders.Dto;
using System.Threading.Tasks;
using GRINTSYS.SAPMiddleware.PurchaseOrders.Job;

namespace GRINTSYS.SAPMiddleware.PurchaseOrders
{
    public class PurchaseOrderAppService : SAPMiddlewareAppServiceBase, IPurchaseOrderAppService
    {
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        private readonly ISapInvoiceManager _sapInvoiceManager;
        //TODO: please find a better solution insted of create a user repository
        private readonly IRepository<User, long> _userRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IAbpSession _session;

        public PurchaseOrderAppService(IPurchaseOrderManager purchaseOrderManager,
            ISapInvoiceManager sapInvoiceManager,
            IRepository<User, long> userRepository,
            IBackgroundJobManager backgroundJobManager,
            IAbpSession session)
        {
            _purchaseOrderManager = purchaseOrderManager;
            _sapInvoiceManager = sapInvoiceManager;
            _userRepository = userRepository;
            _backgroundJobManager = backgroundJobManager;
            _session = session;
        }

        public async Task CreatePurchaseOrder(GetPurchaseOrderInput input)
        {
            var sapInvoice = _sapInvoiceManager.GetSapInvoice(input.Id);

            var newPurchaseOrder = new PurchaseOrder()
            {
                Id = sapInvoice.Id,
                TenantId = sapInvoice.TenantId,
                RemoteId = sapInvoice.RemoteId,
                Status = sapInvoice.Status,
                LastMessage = sapInvoice.LastMessage,
                UserId = sapInvoice.UserId,
                DocEntry = sapInvoice.DocEntry,
                DocNum = sapInvoice.DocNum,
                DocCreateDate = sapInvoice.DocCreateDate,
                DocDate = sapInvoice.DocDate,
                CardCode = sapInvoice.CardCode,
                CardName = sapInvoice.CardName,
                DocTotal = sapInvoice.DocTotal,
                DocTotalExp = sapInvoice.DocTotalExp,
                DocCurrency = sapInvoice.DocCurrency,
                Comments = sapInvoice.Comments,
                SlpCode = sapInvoice.SlpCode
            };

            var newPurchaseOrderId = await _purchaseOrderManager.CreatePurchaseOrder(newPurchaseOrder);

            if (newPurchaseOrderId == input.Id)
            {
                var sapInvoiceDetail = _sapInvoiceManager.GetSapInvoiceDetail(input.Id);

                foreach (var item in sapInvoiceDetail)
                {
                    var newPurchaseOrderDetail = new PurchaseOrderDetail()
                    {
                        PurchaseOrderId = item.VwSapInvoiceId,
                        ItemCode = item.ItemCode,
                        Dscription = item.Dscription,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        LineCurrency = item.LineCurrency,
                        TaxCode = item.TaxCode,
                        LineTotal = item.LineTotal
                    };

                    await _purchaseOrderManager.CreatePurchaseOrderDetail(newPurchaseOrderDetail);
                }

                var sapInvoiceExpense = _sapInvoiceManager.GetSapInvoiceExpense(input.Id);

                foreach (var item in sapInvoiceExpense)
                {
                    var newPurchaseOrderExpense = new PurchaseOrderExpense()
                    {
                        PurchaseOrderId = item.VwSapInvoiceId,
                        ExpnsCode = item.ExpnsCode,
                        Comments = item.Comments,
                        TaxCode = item.TaxCode,
                        LineVat = item.LineVat,
                        DistrbMthd = item.DistrbMthd,
                        LineTotal = item.LineTotal,
                        LineCurrency = item.LineCurrency,
                        U_TipoA = item.U_TipoA
                    };

                    await _purchaseOrderManager.CreatePurchaseOrderExpense(newPurchaseOrderExpense);
                }
            }
        }
        public PurchaseOrderOutput GetPurchaseOrder(GetPurchaseOrderInput input)
        {
            var order = _purchaseOrderManager.GetPurchaseOrder(input.Id);

            PurchaseOrderOutput result = new PurchaseOrderOutput();

            if (order != null)
            {
                result.Id = order.Id;
                result.TenantId = order.TenantId;
                result.RemoteId = order.RemoteId;
                result.Status = order.Status;
                result.LastMessage = order.LastMessage;
                result.UserId = order.UserId;
                result.DocEntry = order.DocEntry;
                result.DocNum = order.DocNum;
                result.DocCreateDate = order.DocCreateDate;
                result.DocDate = order.DocDate;
                result.CardCode = order.CardCode;
                result.CardName = order.CardName;
                result.DocTotal = order.DocTotal;
                result.DocCurrency = order.DocCurrency;
                result.Comments = order.Comments;
                result.SlpCode = order.SlpCode;
            }
            return result;
        }

        public async Task SendPurchaseOrderToSap(GetPurchaseOrderInput input)
        {
            await _backgroundJobManager.EnqueueAsync<PurchaseOrderToSAPJob, int>(input.Id);
        }
    }
}
