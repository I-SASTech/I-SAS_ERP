using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleList;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryDetails
{
    public class CheckCustomerAlreadyInquiry : IRequest<bool>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<CheckCustomerAlreadyInquiry, bool>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IMediator _Mediator;

            public Handler(IApplicationDbContext context,
                ILogger<CheckCustomerAlreadyInquiry> logger,
                IMapper mapper,
                IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _Mediator = mediator;
            }
            public async Task<bool> Handle(CheckCustomerAlreadyInquiry request, CancellationToken cancellationToken)
            {
                try
                {
                    var inquiry =   _context.Inquiries.Any(x=>x.CustomerId == request.Id);
                    
                    return inquiry;
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
