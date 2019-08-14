﻿using Abp.Domain.Repositories;
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

        public List<ClientDiscount> GetClientDiscount(string cardcode)
        {
            var entity = _clientDiscountRespository.GetAll()
                .Where(w => w.CardCode.ToLower().Equals(cardcode.ToLower()))
                ;

            if (entity.Count() == 0)
            {
                throw new UserFriendlyException("Client discounts not found");
            }

            return entity.ToList();
        }

        public Double GetClientDiscountByItemGroupCode(string cardcode, int itemGroupCode)
        {
            if (String.IsNullOrEmpty(cardcode) || itemGroupCode <= 0)
                return 0.0;

            var entity = _clientDiscountRespository.GetAll()
                 .Where(w => w.CardCode.ToLower().Equals(cardcode.ToLower())
                      && w.ItemGroup == itemGroupCode)
                 .FirstOrDefault()
                 ;

            return entity.Discount;
        }
    }
}
