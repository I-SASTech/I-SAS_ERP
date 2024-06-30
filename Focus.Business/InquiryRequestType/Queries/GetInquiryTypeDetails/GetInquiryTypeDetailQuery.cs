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

namespace Focus.Business.InquiryRequestType.Queries.GetInquiryTypeDetails
{
    public class GetInquiryTypeDetailQuery : IRequest<InquiryTypeDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryTypeDetailQuery, InquiryTypeDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryTypeDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryTypeDetailsLookUpModel> Handle(GetInquiryTypeDetailQuery request, CancellationToken cancellationToken)
            {
                var type = await _context.InquiryTypes.FindAsync(request.Id);

                try
                {
                    if (type != null)
                    {
                        return new InquiryTypeDetailsLookUpModel()
                        {
                            Id = type.Id,
                            IsActive = type.IsActive,
                            Description = type.Description,
                            Code = type.Code,
                            Label = type.Label,
                            Name = type.Name,
                        };
                    }
                    throw new NotFoundException(nameof(type), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to type your request please contact support");
                }
            }
        }
    }
}
