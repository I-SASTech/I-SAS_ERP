using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;

namespace Focus.Business.Sales.Commands.AddCashCustomer
{
    public class CashCustomerLookupModel: IMapFrom<CashCustomer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string CashCustomerId { get; set; }
        public string Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CashCustomer, CashCustomerLookupModel>();
        }
    }
}
