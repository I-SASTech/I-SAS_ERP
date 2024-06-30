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
    public class InquiryDetailForList : IRequest<InquiryDetailForListLookUp>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<InquiryDetailForList, InquiryDetailForListLookUp>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<InquiryDetailForList> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryDetailForListLookUp> Handle(InquiryDetailForList request, CancellationToken cancellationToken)
            {
                try
                {
                    var inquiry = await _context.Inquiries.FindAsync(request.Id);
                    var lookUp = new InquiryDetailForListLookUp()
                    {
                        Id = inquiry.Id,
                        InquiryNumber = inquiry.InquiryNumber,
                        CustomerName = string.IsNullOrEmpty(inquiry.Contact.EnglishName)? inquiry.Contact.ArabicName: inquiry.Contact.EnglishName,
                        MediaType = inquiry.MediaType.Name,
                        InquiryPriorityId = inquiry.InquiryPriorityId,
                        InquiryType = inquiry.InquiryType.Name,
                        Status = inquiry.InquiryStatus.LastOrDefault()?.InquiryStatusDynamic.Name,
                        Date = inquiry.DateTime.ToString("yyyy MMMM dd")
                    };

                    return lookUp;
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
