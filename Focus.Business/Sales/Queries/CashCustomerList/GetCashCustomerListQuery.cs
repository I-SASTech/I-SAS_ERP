using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using Focus.Business.Sales.Commands.AddCashCustomer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Sales.Queries.CashCustomerList
{
    public class GetCashCustomerListQuery : IRequest<List<CashCustomerLookupModel>>
    {
        public class Handler : IRequestHandler<GetCashCustomerListQuery, List<CashCustomerLookupModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<GetCashCustomerListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<List<CashCustomerLookupModel>> Handle(GetCashCustomerListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cashCustomers = _context.CashCustomer.AsNoTracking().ToList();
                    var cashCustomerList = new List<CashCustomerLookupModel>();
                    foreach(var customer in cashCustomers)
                    {
                        cashCustomerList.Add(new CashCustomerLookupModel()
                        {
                            Id = customer.Id,
                            Mobile = customer.Mobile,
                            Email = customer.Email,
                            Name = customer.Name,
                            CustomerId = customer.CustomerId,
                            Address = customer.Address,
                        });
                    }
                        return cashCustomerList;
                    

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
