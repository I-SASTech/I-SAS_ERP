using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Banks.Queries.GetBankDetails
{
    public class GetBankDetailQuery : IRequest<BankDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBankDetailQuery, BankDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBankDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<BankDetailsLookUpModel> Handle(GetBankDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var brand = await _context.Banks.FindAsync(request.Id);

                    if (brand != null)
                    {
                        return BankDetailsLookUpModel.Create(brand);
                    }
                    throw new NotFoundException(nameof(brand), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
