﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Invoice: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public String DocumentCode { get; set; }
        public String DueDate { get; set; }
        public Double TotalAmount { get; set; }
        public Double BalanceDue { get; set; }
        public Int32 ClientId { get; set; }
        public Int32 DocEntry { get; set; }
        public double OverdueDays { get; set; }

        public DateTime CreationTime { get; set; }
        public virtual Client Client { get; set; }

        public Invoice()
        {
            CreationTime = Clock.Now;

            if(OverdueDays <= 0.00 && !String.IsNullOrEmpty(DueDate))
                OverdueDays = (DateTime.Now - DateTime.Parse(DueDate)).TotalDays;
        }
    }
}
