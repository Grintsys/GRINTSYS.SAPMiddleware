using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Clients
{
    public interface IClientManager: IDomainService
    {
        Client GetClient(int id);
        double GetClientDiscountByItemGroupCode(string cardcode, int itemgroup);
    }
}
