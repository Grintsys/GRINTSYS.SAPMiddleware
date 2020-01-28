using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using Abp.UI;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.Banks;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Clients;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Mail;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Payments.Job;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public class PaymentAppService : SAPMiddlewareAppServiceBase, IPaymentAppService
    {
        private readonly UserManager _userManager;
        private readonly PaymentManager _paymentManager;
        private readonly ClientManager _clientManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IAbpSession _session;


        public PaymentAppService(IBackgroundJobManager backgroundJobManager,
            UserManager userManager,
            PaymentManager paymentManager,
            ClientManager clientManager,
            IAbpSession session)
        {
            _backgroundJobManager = backgroundJobManager;
            _userManager = userManager;
            _paymentManager = paymentManager;
            _clientManager = clientManager;
            _session = session;
        }

        public async Task AutorizePayment(GetPaymentInput input)
        {
            var userId = GetUserId();
            //GetCurrentTenantAsync()
            var user = await _userManager.FindByIdAsync(userId.ToString());

            Logger.Debug(String.Format("SendToSap({0})", input.Id));
            string url = String.Format("{0}api/payments/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], input.Id);
            var response = await AppConsts.Instance.GetClient().GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Logger.Info("Success to send to SAP");
                /*aqui manda email de que se ejecuto correctamente o ocurrio algun error*/
            }
        }

        public async Task CreateInvoice(AddInvoiceInput input)
        {
            var invoice = ObjectMapper.Map<Invoice>(input);

            await _paymentManager.CreateInvoice(invoice);
        }

        public async Task<PaymentOutput> CreatePayment(AddPaymentInput input)
        {
            var payment = ObjectMapper.Map<Payment>(input);

            payment.InvoicesItems = input.PaymentItemList
                                         .Select(s => new PaymentInvoiceItem()
                                         {
                                             TenantId = input.TenantId,
                                             PaymentId = payment.Id,
                                             DocumentCode = s.DocumentCode,
                                             TotalAmount = s.TotalAmount,
                                             BalanceDue = s.BalanceDue,
                                             PayedAmount = s.PayedAmount,
                                             DocEntry = s.DocEntry
                                         }).ToList();

            var newPaymentId = await _paymentManager.CreatePayment(payment);

            PaymentOutput paymentOutput = new PaymentOutput()
            {
                Id = newPaymentId,
                TenantId = payment.TenantId,
                DocEntry = payment.DocEntry,
                PayedAmount = Convert.ToDecimal(payment.PayedAmount),
                LastMessage = payment.LastMessage,
                Status = payment.Status,
                StatusDesc = ((PaymentStatus)payment.Status).ToString(),
                Comment = payment.Comment,
                ReferenceNumber = payment.ReferenceNumber,
                CreationTime = payment.CreationTime,
                UserId = payment.UserId,
                BankId = payment.BankId,
                Type = payment.Type,
                TypeDesc = ((PaymentType)payment.Type).ToString(),
                PayedDate = payment.PayedDate,
                CardCode = payment.CardCode,
                CardName = string.Empty,
                PaymentItemsOutput = payment.InvoicesItems.Select(s => new PaymentItemOutput()
                {
                    DocumentCode = s.DocumentCode,
                    TotalAmount = s.TotalAmount,
                    BalanceDue = s.BalanceDue,
                    PayedAmount = s.PayedAmount,
                    DocEntry = s.DocEntry
                }).ToList()
            };

            return paymentOutput;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<PaymentOutput> UpdatePayment(UpdatePaymentInput input)
        {
            //CheckUpdatePermission();

            var payment = _paymentManager.GetPayment(input.Id);
            var client = _clientManager.GetClient(payment.CardCode);

            ObjectMapper.Map(input, payment);

            var selectedInvoices = _clientManager.GetClientByCardCode(payment.CardCode, payment.TenantId)
                                                 .Invoices
                                                 .Where(w => input.Invoices.Contains(w.DocEntry.ToString()))
                                                 .OrderByDescending(o=>o.OverdueDays)
                                                 .Select(s => new PaymentInvoiceItem()
                                                 {
                                                     TenantId = s.TenantId,
                                                     PaymentId = payment.Id,
                                                     DocumentCode = s.DocumentCode,
                                                     TotalAmount = s.TotalAmount,
                                                     BalanceDue = s.BalanceDue,
                                                     PayedAmount = 0,
                                                     DocEntry = s.DocEntry
                                                 }).ToList();

            double amountLeft = payment.PayedAmount;
            foreach (var item in selectedInvoices)
            {
                if (amountLeft > 0)
                {
                    item.PayedAmount = item.BalanceDue <= amountLeft ? item.BalanceDue : amountLeft;
                    amountLeft = amountLeft - item.PayedAmount;
                }
            }

            payment.InvoicesItems = selectedInvoices;

            await _paymentManager.UpdatePaymentAsync(payment);

            PaymentOutput paymentOutput = new PaymentOutput()
            {
                Id = payment.Id,
                TenantId = payment.TenantId,
                DocEntry = payment.DocEntry,
                PayedAmount = Convert.ToDecimal(payment.PayedAmount),
                LastMessage = payment.LastMessage,
                Status = payment.Status,
                StatusDesc = ((PaymentStatus)payment.Status).ToString(),
                Comment = payment.Comment,
                ReferenceNumber = payment.ReferenceNumber,
                CreationTime = payment.CreationTime,
                UserId = payment.UserId,
                BankId = payment.BankId,
                Type = payment.Type,
                TypeDesc = ((PaymentType)payment.Type).ToString(),
                PayedDate = payment.PayedDate,
                CardCode = payment.CardCode,
                CardName = client.Name,
                PaymentItemsOutput = payment.InvoicesItems.Select(s => new PaymentItemOutput()
                {
                    DocumentCode = s.DocumentCode,
                    TotalAmount = s.TotalAmount,
                    BalanceDue = s.BalanceDue,
                    PayedAmount = s.PayedAmount,
                    DocEntry = s.DocEntry
                }).ToList()
            };

            return paymentOutput;
        }

        public async Task<PaymentOutput> DeclinePayment(GetPaymentInput input)
        {
            var entity = _paymentManager.GetPayment(input.Id);
            entity.Status = PaymentStatus.CanceladoPorFinanzas;
            var payment = _paymentManager.UpdatePayment(entity);

            string htmlItemTemplate = "<tr class='service'> <td class='tableitem'> <p class='itemtext'>@DocNum</p> </td> <td class='tableitem'> <p class='itemtext'>@BalanceDue</p> </td> <td class='tableitem'> <p class='itemtext'>@PayedAmount</p> </td> </tr>";
            string str = "";
            foreach (var item in entity.InvoicesItems)
            {
                str += htmlItemTemplate.Replace("@DocNum", item.DocumentCode)
                                       .Replace("@BalanceDue", string.Format(new CultureInfo("es-HN"), "{0:c}", item.BalanceDue))
                                       .Replace("@PayedAmount", string.Format(new CultureInfo("es-HN"), "{0:c}", item.PayedAmount));
            }
            htmlItemTemplate = str;
                       
            string htmlTemplate = "<html lang='en'><head> <title>CodePen - POS Receipt Template Html Css</title> <style> #invoice-POS { box-shadow: 0 0 1in -0.25in rgba(0, 0, 0, 0.5); padding: 2mm; margin: 0 auto; width: 44mm; background: #FFF;background-image: url('KAD.png');background-repeat: repeat-y;background-position: 50% 50%;background-size: 90px 40px; background-color: rgba(255,255,255,0.9); background-blend-mode: lighten;} #invoice-POS::selection { background: #f31544; color: #FFF; } #invoice-POS::moz-selection { background: #f31544; color: #FFF; } #invoice-POS h1 { font-size: 1.5em; color: #222; } #invoice-POS h2 { font-size: .9em; } #invoice-POS h3 { font-size: 1.2em; font-weight: 300; line-height: 2em; } #invoice-POS p { font-size: .7em; color: #666; line-height: 1.2em; } #invoice-POS #top, #invoice-POS #mid, #invoice-POS #bot { /* Targets all id with 'col-' */ border-bottom: 1px solid #EEE; } #invoice-POS #top { min-height: 10px; } #invoice-POS #mid { min-height: 80px; } #invoice-POS #bot { min-height: 50px; } #invoice-POS #top .logo { height: 60px; width: 60px; background: url(http://michaeltruong.ca/images/logo1.png) no-repeat; background-size: 60px 60px; } #invoice-POS .clientlogo { float: left; height: 60px; width: 60px; background: url(http://michaeltruong.ca/images/client.jpg) no-repeat; background-size: 60px 60px; border-radius: 50px; } #invoice-POS .info { display: block; margin-left: 0; } #invoice-POS .title { float: right; } #invoice-POS .title p { text-align: right; } #invoice-POS table { width: 100%; border-collapse: collapse; } #invoice-POS .tabletitle { font-size: .5em; background: #EEE; } #invoice-POS .service { border-bottom: 1px solid #EEE; } #invoice-POS .item { width: 17mm; } #invoice-POS .itemtext { font-size: .5em; } #invoice-POS #legalcopy { margin-top: 5mm; } </style> <script> window.console = window.console || function(t) {}; </script> <script> if (document.location.search.match(/type=embed/gi)) { window.parent.postMessage('resize', '*'); } </script></head><body translate='no'> <div id='invoice-POS' > <center id='top'> <!--<div class='logo'></div>--> <div class='info'> <h3>Recibo de Pago</h3> </div> </center> <div id='mid'> <div class='info'> <h2>VAN HEUSEN DE C.A.</h2> <p> Dirección : Col. San Fernando Ave. Juan Pablo II, frente a la Leyde. Apartado Postal #1. San Pedro Sula, Honduras, C.A. <br> Correo : vheusen@kattangroup.com <br> Teléfono : (504)2516-0100<br> Fax : (504)2516-4080<br> R.T.N. : 05019995143200 <br> </p> </div> <div class='info'> <h2>Recibo # @Receipt</h2> <p> Fecha de Recibo : @RDate <br> Cliente : @Client <br> R.T.N. : @CRTN <br> </p> </div> </div> <div id='bot'> <div id='table'> <table> <tbody> <tr class='tabletitle'> <td class='item'> <h2>Factura</h2></td> <td class='Hours'> <h2>Monto Pendiente</h2></td> <td class='Rate'> <h2>Monto Aplicado</h2></td> </tr>@PaymentItems <tr class='tabletitle'> <td></td> <td class='Rate'> <h2>Total</h2></td> <td class='payment'> <h2>@TotalAmount</h2></td> </tr> </tbody> </table> </div> <div id='legalcopy'> <p class='legal'>Forma de Pago : @PaymentType <br> No. de Referencia : @ReferenceNumber<br> Fecha de Pago : @PayedDate <br> Vendedor : @UserName<br><br><strong>¡ Gracias por su pago !</strong>&nbsp; </p> </div> </div> </div></body></html>";
            htmlTemplate = htmlTemplate.Replace("@PaymentItems", htmlItemTemplate);
            htmlTemplate = htmlTemplate.Replace("@Receipt", payment.Id.ToString());
            htmlTemplate = htmlTemplate.Replace("@PayedDate", payment.PayedDate.ToString("dd-MM-yyyy"));
            htmlTemplate = htmlTemplate.Replace("@RDate", payment.CreationTime.ToString("dd-MM-yyyy HH:mm:ss"));
            htmlTemplate = htmlTemplate.Replace("@Client",$"{payment.CardCode} {_clientManager.GetClient(payment.CardCode).Name}");
            htmlTemplate = htmlTemplate.Replace("@CRTN", _clientManager.GetClient(payment.CardCode).RTN);
            htmlTemplate = htmlTemplate.Replace("@PaymentType", payment.Type.ToString());
            htmlTemplate = htmlTemplate.Replace("@ReferenceNumber", payment.ReferenceNumber);
            htmlTemplate = htmlTemplate.Replace("@UserName", entity.User.Name);
            htmlTemplate = htmlTemplate.Replace("@TotalAmount", string.Format(new CultureInfo("es-HN"), "{0:c}", payment.PayedAmount));

            await new EmailHelper().Send(new EmailArgs()
            {
                To = entity.User.EmailAddress,
                Subject = $"Notificación De Pago Cancelado Por Finanzas, Recibo # {entity.Id}",
                Body = htmlTemplate
            });

            return new PaymentOutput()
            {
                Id = payment.Id,
                TenantId = payment.TenantId,
                DocEntry = payment.DocEntry,
                PayedAmount = Convert.ToDecimal(payment.PayedAmount),
                LastMessage = payment.LastMessage,
                Status = payment.Status,
                Comment = payment.Comment,
                ReferenceNumber = payment.ReferenceNumber,
                CreationTime = payment.CreationTime,
                UserId = payment.UserId,
                BankId = payment.BankId,
                Type = payment.Type,
                PayedDate = payment.PayedDate
            };
        }
        public async Task DeletePayment(DeletePaymentInput input)
        {
            var payment = _paymentManager.GetPayment(input.Id);

            var user = await GetCurrentUserAsync();

            if (payment.UserId != user.Id)
            {
                throw new UserFriendlyException("You can not delete a payment of another person");
            }

            await _paymentManager.DeletePayment(input.Id);
        }

        public PaymentOutput GetPayment(GetPaymentInput input)
        {
            var payment = _paymentManager.GetPayment(input.Id);

            return new PaymentOutput()
            {
                Id = payment.Id,
                TenantId = payment.TenantId,
                DocEntry = payment.DocEntry,
                PayedAmount = Convert.ToDecimal(payment.PayedAmount),
                LastMessage = payment.LastMessage,
                Status = payment.Status,
                StatusDesc = ((PaymentStatus)payment.Status).ToString(),
                Comment = payment.Comment,
                ReferenceNumber = payment.ReferenceNumber,
                CreationTime = payment.CreationTime,
                UserId = payment.UserId,
                BankId = payment.BankId,
                Type = payment.Type,
                TypeDesc = ((PaymentType)payment.Type).ToString(),
                PayedDate = payment.PayedDate,
                CardCode = payment.CardCode,
                PaymentItemsOutput = payment.InvoicesItems.Select(s => new PaymentItemOutput()
                {
                    DocumentCode = s.DocumentCode,
                    TotalAmount = s.TotalAmount,
                    BalanceDue = s.BalanceDue,
                    PayedAmount = s.PayedAmount,
                    DocEntry = s.DocEntry
                }).ToList()
            };
        }

        public PagedResultDto<PaymentOutput> GetPayments(GetAllPaymentInput input)
        {
            if (_session.TenantId.HasValue)
                input.TenantId = (int)_session.TenantId;
            else
                return new PagedResultDto<PaymentOutput>();

            if (String.IsNullOrEmpty(input.Begin))
                input.Begin = DateTime.MinValue.ToString();

            if (String.IsNullOrEmpty(input.End))
                input.End = DateTime.MaxValue.ToString();

            var payments = _paymentManager.GetPayments(input.TenantId,
                DateTime.Parse(input.Begin),
                DateTime.Parse(input.End));

            var total = payments.Count();

            return new PagedResultDto<PaymentOutput>
            {
                TotalCount = total,
                Items = payments.Select(s => new PaymentOutput()
                {
                    Id = s.Id,
                    TenantId = s.TenantId,
                    DocEntry = s.DocEntry,
                    PayedAmount = Convert.ToDecimal(s.PayedAmount),
                    LastMessage = s.LastMessage,
                    Status = s.Status,
                    StatusDesc = ((PaymentStatus)s.Status).ToString(),
                    Comment = s.Comment,
                    ReferenceNumber = s.ReferenceNumber,
                    CreationTime = s.CreationTime,
                    UserId = s.UserId,
                    BankId = s.BankId,
                    Type = s.Type,
                    TypeDesc = ((PaymentType)s.Type).ToString(),
                    PayedDate = s.PayedDate,
                    CardCode = s.CardCode,
                    CardName = _clientManager.GetClient(s.CardCode).Name,
                    UserName = s.User.FullName
                }).ToList()
            };
        }

        public PagedResultDto<PaymentOutput> GetPaymentsByUser(GetAllPaymentInput input)
        {
            var userId = GetUserId();

            var payments = _paymentManager.GetPaymentsByUser(input.TenantId,
                userId,
                DateTime.Parse(input.Begin),
                DateTime.Parse(input.End));

            return new PagedResultDto<PaymentOutput>
            {
                Items = payments.Select(s=> new PaymentOutput()
                {
                    Id = s.Id,
                    TenantId = s.TenantId,
                    DocEntry = s.DocEntry,
                    PayedAmount = Convert.ToDecimal(s.PayedAmount),
                    LastMessage = s.LastMessage,
                    Status = s.Status,
                    StatusDesc = ((PaymentStatus)s.Status).ToString(),
                    Comment = s.Comment,
                    ReferenceNumber = s.ReferenceNumber,
                    CreationTime = s.CreationTime,
                    UserId = s.UserId,
                    BankId = s.BankId,
                    Type = s.Type,
                    TypeDesc = ((PaymentType)s.Type).ToString(),
                    PayedDate = s.PayedDate,
                    CardCode = $"{s.CardCode} {_clientManager.GetClient(s.CardCode).Name}",
                    CardName = _clientManager.GetClient(s.CardCode).Name,
                    UserName = s.User.Name
                }).ToList()
            };
        }
    }
}