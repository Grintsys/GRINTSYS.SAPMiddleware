using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Payments.Dto
{
    [AutoMap(typeof(Payment))]
    public class AddPaymentOutput : EntityDto
    {

    }
}
