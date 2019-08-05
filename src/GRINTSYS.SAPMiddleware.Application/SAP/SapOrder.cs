using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.M2.Orders;
using SAPbobsCOM;
using System;

namespace GRINTSYS.SAPMiddleware.SAP
{
    public class SapOrder: SapDocument, IApplicationService
    {
        private readonly OrderManager _orderManager;

        public SapOrder(OrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public override void Execute(SapDocumentInput input)
        {
            String message = "";
            var order = _orderManager.GetOrder(input.Id);

            Company company = this.Connect(new SapInput());

            IDocuments salesOrder = (IDocuments)company.GetBusinessObject(BoObjectTypes.oOrders);
            salesOrder.CardCode = order.CardCode;
            salesOrder.Comments = order.Comment;
            salesOrder.Series = order.Series;
            salesOrder.SalesPersonCode = order.User.SalesPersonId;
            salesOrder.DocDueDate = order.CreationTime;      

            foreach (var item in order.OrderItems)
            {
                salesOrder.Lines.ItemCode = item.Code;
                salesOrder.Lines.Quantity = item.Quantity;
                salesOrder.Lines.TaxCode = item.TaxCode;
                salesOrder.Lines.DiscountPercent = item.DiscountPercent;
                salesOrder.Lines.WarehouseCode = item.WarehouseCode;
                //Add Comercial Canal, Tradicion Center Cost. DEM. July 8th. 2018. 
                salesOrder.Lines.CostingCode = "301";
                salesOrder.Lines.CostingCode2 = "3001-01";
                salesOrder.Lines.Add();
            }
            // add Sales Order
            if (salesOrder.Add() == 0)
            {
                message = String.Format("Successfully added Sales Order DocEntry: {0}", company.GetNewObjectKey());
                Logger.Info(message);             
            }
            else
            {
                message = "Error Code: "
                        + company.GetLastErrorCode().ToString()
                        + " - "
                        + company.GetLastErrorDescription();
                Logger.Error(message);
            }

            order.LastMessage = message;
            _orderManager.UpdateOrder(order);

            company.Disconnect();
        }
    }
}
