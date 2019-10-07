using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRINTSYS.SAPMiddleware.M2.Clients
{
    public class ClientManager : DomainService, IClientManager
    {
        private readonly IRepository<Client> _clientRespository;
        private readonly IRepository<ClientDiscount> _clientDiscountRespository;

        public ClientManager(IRepository<Client> clientRespository, 
            IRepository<ClientDiscount> clientDiscountRespository)
        {
            this._clientRespository = clientRespository;
            this._clientDiscountRespository = clientDiscountRespository;
        }

        public Client GetClient(int id)
        {
            var entity = _clientRespository.GetAllIncluding(x => x.Invoices).FirstOrDefault(x => x.Id == id);

            if(entity == null)
            {
                throw new UserFriendlyException("Client not found");
            }

            return entity;
        }

        public Client GetClientByCardCode(string cardCode)
        {
            var entity = _clientRespository.GetAll().FirstOrDefault(x => x.CardCode.Equals(cardCode));

            if (entity == null)
            {
                throw new UserFriendlyException("Client not found");
            }

            return entity;
        }

        public double GetClientDiscountByItemGroupCode(string cardcode, int itemgroup)
        {
            if (String.IsNullOrEmpty(cardcode) || itemgroup <= 0)
                return 0.0;

            var entity = _clientDiscountRespository
                .FirstOrDefault(w => w.CardCode.ToLower().Equals(cardcode.ToLower()) 
                && w.ItemGroup == itemgroup)          
                ;

            if (entity == null)
                return 0.0;

            return entity.Discount;
        }
    }
}
