using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CustomerDiscounts.Queries.GetCustomerDiscountDetails
{
    public class GetCustomerDiscountDetailQuery: IRequest<CustomerDiscountDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCustomerDiscountDetailQuery, CustomerDiscountDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<GetCustomerDiscountDetailQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<CustomerDiscountDetailLookUpModel> Handle(GetCustomerDiscountDetailQuery request, CancellationToken cancellationToken)
            {
                var customerDiscount = await _context.CustomerDiscount.FindAsync(request.Id);
                if (customerDiscount == null)
                {
                    throw new NotFoundException(nameof(customerDiscount), request.Id);
                }

                return CustomerDiscountDetailLookUpModel.Create(customerDiscount);
            }
        }
    }
}
