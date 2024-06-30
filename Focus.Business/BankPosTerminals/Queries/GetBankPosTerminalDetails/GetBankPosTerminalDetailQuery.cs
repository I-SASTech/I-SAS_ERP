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

namespace Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalDetails
{
    public class GetBankPosTerminalDetailQuery : IRequest<BankPosTerminalDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBankPosTerminalDetailQuery, BankPosTerminalDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBankPosTerminalDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<BankPosTerminalDetailsLookUpModel> Handle(GetBankPosTerminalDetailQuery request, CancellationToken cancellationToken)
            {
                var bankPosTerminal = await _context.BankPosTerminals.FindAsync(request.Id);

                try
                {
                    if (bankPosTerminal != null)
                    {
                        return BankPosTerminalDetailsLookUpModel.Create(bankPosTerminal);
                    }
                    throw new NotFoundException(nameof(bankPosTerminal), request.Id);
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
