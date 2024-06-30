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

namespace Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails
{
    public class GetInquiryEmailDetailQuery : IRequest<InquiryEmailDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryEmailDetailQuery, InquiryEmailDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryEmailDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryEmailDetailsLookUpModel> Handle(GetInquiryEmailDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var inquiryEmail = await _context.InquiryEmails.FindAsync(request.Id);

                    if (inquiryEmail != null)
                    {
                        var emailCcList = new List<InquiryEmailCCDetailsLookUpModel>();
                        foreach (var emailCc in inquiryEmail.InquiryEmailCcs)
                        {
                            emailCcList.Add(new InquiryEmailCCDetailsLookUpModel()
                            {
                                Id = emailCc.Id,
                                Email = emailCc.Email,
                                InquiryEmailId = emailCc.InquiryEmailId
                            });
                        }
                        return new InquiryEmailDetailsLookUpModel()
                        {
                            Id = inquiryEmail.Id,
                            Subject = inquiryEmail.Subject,
                            Description = inquiryEmail.Description,
                            IsReceived = inquiryEmail.IsReceived,
                            InquiryId = inquiryEmail.InquiryId,
                            InquiryEmailCcDetails = emailCcList
                        };
                    }
                    throw new NotFoundException(nameof(inquiryEmail), request.Id);
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
