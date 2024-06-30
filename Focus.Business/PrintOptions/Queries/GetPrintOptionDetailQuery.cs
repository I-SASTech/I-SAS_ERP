using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.CompaniesOption.Commands.AddUpdateOption;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PrintOptions.Commands.AddPrintOption;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PrintOptions.Queries
{
    public class GetPrintOptionDetailQuery:IRequest<PrintOptionLookUp>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPrintOptionDetailQuery, PrintOptionLookUp>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPrintOptionDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PrintOptionLookUp> Handle(GetPrintOptionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var printOption = await _context.PrintOptions.FindAsync(request.Id);

                    if (printOption != null)
                    {
                        return new PrintOptionLookUp()
                        {
                            Id = printOption.Id,
                            Label = printOption.Label,
                            Value = printOption.Value,
                        };
                    }
                    throw new NotFoundException(nameof(printOption), request.Id);
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
