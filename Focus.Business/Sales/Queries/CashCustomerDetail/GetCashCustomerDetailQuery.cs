using AutoMapper;
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

namespace Focus.Business.Sales.Queries.CashCustomerDetail
{
    public class GetCashCustomerDetailQuery : IRequest<List<CashCustomerLookupModel>>
    {
        public string Search { get; set; }
        public class Handler : IRequestHandler<GetCashCustomerDetailQuery, List<CashCustomerLookupModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<List<CashCustomerLookupModel>> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<List<CashCustomerLookupModel>> Handle(GetCashCustomerDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cashCustomers = await _context.CashCustomer.AsNoTracking()
                        .Where(x => x.CustomerId.ToLower().Contains(request.Search)  || x.Mobile.ToLower().Contains(request.Search) || x.Email.Contains(request.Search) || x.Name.ToLower().Contains(request.Search)|| x.NameArabic.ToLower().Contains(request.Search)).ToListAsync(cancellationToken: cancellationToken);

                    var customerList = new List<CashCustomerLookupModel>();
                    foreach(var data in cashCustomers)
                    {
                        customerList.Add(new CashCustomerLookupModel()
                        {
                            Id = Guid.NewGuid(),
                            Mobile = data.Mobile,
                            Email = data.Email,
                            Name = data.Name,
                            CustomerId = data.CustomerId,
                            Address = data.Address,
                        });
                    }
                    return customerList;  
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
