using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Clients;
using GRINTSYS.SAPMiddleware.M2.vwSapInvoices;
using GRINTSYS.SAPMiddleware.M2.PurchaseOrders;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.SapInvoices.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using GRINTSYS.SAPMiddleware.Orders.Dto;

namespace GRINTSYS.SAPMiddleware.SapInvoices
{
    public class SapInvoiceAppService : SAPMiddlewareAppServiceBase, ISapInvoiceAppService
    {
        private readonly ISapInvoiceManager _sapInvoiceManager;
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IAbpSession _session;

        public SapInvoiceAppService(ISapInvoiceManager sapInvoiceManager,
            IPurchaseOrderManager purchaseOrderManager,
            IBackgroundJobManager backgroundJobManager,
            IAbpSession session)
        {
            _sapInvoiceManager = sapInvoiceManager;
            _purchaseOrderManager = purchaseOrderManager;
            _backgroundJobManager = backgroundJobManager;
            _session = session;
        }

        public VwSapInvoiceOutput GetSapInvoice(GetSapInvoiceInput input)
        {
            VwSapInvoiceOutput result = new VwSapInvoiceOutput();

            var invoice = _sapInvoiceManager.GetSapInvoice(input.Id);

            if (invoice != null)
            {
                result.Id = invoice.Id;
                result.TenantId = invoice.TenantId;
                result.RemoteId = invoice.RemoteId;
                result.Status = invoice.Status;
                result.LastMessage = invoice.LastMessage;
                result.UserId = invoice.UserId;
                result.DocEntry = invoice.DocEntry;
                result.DocNum = invoice.DocNum;
                result.DocCreateDate = invoice.DocCreateDate;
                result.DocDate = invoice.DocDate;
                result.CardCode = invoice.CardCode;
                result.CardName = invoice.CardName;
                result.DocTotal = invoice.DocTotal;
                result.DocCurrency = invoice.DocCurrency;
                result.Comments = invoice.Comments;
                result.SlpCode = invoice.SlpCode;
            }
            return result;
        }
        public PagedResultDto<VwSapInvoiceDetailOutput> GetSapInvoiceDetail(GetSapInvoiceInput input)
        {
            List<VwSapInvoiceDetailOutput> result = new List<VwSapInvoiceDetailOutput>();

            var detail = _sapInvoiceManager.GetSapInvoiceDetail(input.Id);

            if (detail != null)
            {
                result = detail.Select(s => new VwSapInvoiceDetailOutput()
                {
                    Id = s.Id,
                    VwSapInvoiceId = s.VwSapInvoiceId,
                    ItemCode = s.ItemCode,
                    Dscription = s.Dscription,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    LineCurrency = s.LineCurrency,
                    TaxCode = s.TaxCode,
                    LineTotal = s.LineTotal
                }).OrderBy(o => o.Id)
               .ToList();
            }

            return new PagedResultDto<VwSapInvoiceDetailOutput>()
            {
                TotalCount = result.Count(),
                Items = result
            };
        }
        public PagedResultDto<VwSapInvoiceOutput> GetAllSapInvoices()
        {
            var invoices = _sapInvoiceManager.GetAllSapInvoices();

            var invoicesOutput = invoices.Select(s => new VwSapInvoiceOutput()
            {
                Id = s.Id,
                RemoteId = s.RemoteId,
                DocCreateDate = s.DocCreateDate,
                DocDate = s.DocDate,
                CardCode = s.CardCode,
                CardName = s.CardName,
                DocTotal = s.DocTotal,
                DocCurrency = s.DocCurrency,
                Comments = s.Comments,
                Status = s.Status,
                LastMessage = s.LastMessage
            }).OrderByDescending(o => o.DocCreateDate)
              .ToList();

            return new PagedResultDto<VwSapInvoiceOutput>()
            {
                Items = invoicesOutput
            };
        }
    }
}