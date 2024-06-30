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

namespace Focus.Business.InquiryRequestProcess.Queries.GetInquiryProcessDetails
{
    public class GetInquiryProcessDetailQuery : IRequest<InquiryProcessDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryProcessDetailQuery, InquiryProcessDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryProcessDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryProcessDetailsLookUpModel> Handle(GetInquiryProcessDetailQuery request, CancellationToken cancellationToken)
            {
                var process = await _context.InquiryProcesses.FindAsync(request.Id);

                try
                {
                    if (process != null)
                    {
                        return new InquiryProcessDetailsLookUpModel()
                        {
                            Id = process.Id,
                            IsActive = process.IsActive,
                            Description = process.Description,
                            Code = process.Code,
                            Label = process.Label,
                            Name = process.Name,
                        };
                    }
                    throw new NotFoundException(nameof(process), request.Id);
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
