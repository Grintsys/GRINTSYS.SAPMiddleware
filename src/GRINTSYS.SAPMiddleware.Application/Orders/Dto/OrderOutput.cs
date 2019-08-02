﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Orders.Dto
{
    public class OrderOutput : EntityDto
    {
        public String RemoteId { get; set; }
        public DateTime CreationTime { get; set; }
        public String Status { get; set; }
        public String CardCode { get; set; }
        public String Comment { get; set; }
        public Int32 Series { get; set; }
        public String DeliveryDate { get; set; }
        public List<OrderItemOutput> Items { get; set;}
    }
}