using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class LogEntry: Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Logger { get; set; }
        public string CallSite { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string Url { get; set; }
        public string RemoteAddress { get; set; }
        public LogEntry()
        {
        }
    }
}
