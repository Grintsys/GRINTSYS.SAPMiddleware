using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2.Clients
{
    public interface IClientManager: IDomainService
    {
        Client GetClient(int id);
        List<ClientDiscount> GetClientDiscount(string cardcode);
        ClientDiscount GetClientDiscountAndItemGroupCode(string cardcode, int itemGroupCode);
    }
}
