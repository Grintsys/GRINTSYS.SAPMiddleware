﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(M2.PaymentInvoiceItem))]
    public class PaymentItemOutput: EntityDto
    {
        public String DocumentCode { get; set; }
        public int DocEntry { get; set; }
    }
}
