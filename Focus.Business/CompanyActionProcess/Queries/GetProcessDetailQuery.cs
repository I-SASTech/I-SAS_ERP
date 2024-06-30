using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.CompanyActionProcess.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CompanyActionProcess.Queries
{
    public class GetProcessDetailQuery:IRequest<ProcessLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetProcessDetailQuery, ProcessLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProcessDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProcessLookUpModel> Handle(GetProcessDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var process = await _context.CompanyProcess.FindAsync(request.Id);

                    if (process != null)
                    {
                        return new ProcessLookUpModel()
                        {
                            Id = process.Id,
                            ProcessName = process.ProcessName,
                            ProcessNameArabic = process.ProcessNameArabic,
                            Type = process.Type,
                            Status = process.Status,
                        };
                    }
                    throw new NotFoundException(nameof(process), request.Id);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
