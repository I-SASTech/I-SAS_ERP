using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CustomerDiscounts.Queries.GetCustomerDiscountList
{
    public class GetCustomerDiscountListQuery : IRequest<CustomerDiscountListModel>
    {
        public class Handler : IRequestHandler<GetCustomerDiscountListQuery, CustomerDiscountListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetCustomerDiscountListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CustomerDiscountListModel> Handle(GetCustomerDiscountListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var customerDiscounts = _context
                        .CustomerDiscount.AsQueryable();

                    var customerDiscountList = await customerDiscounts
                        .OrderBy(x => x.DiscountName)
                        .ProjectTo<CustomerDiscountLookUpModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return new CustomerDiscountListModel
                    {
                        CustomerDiscounts = customerDiscountList
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
