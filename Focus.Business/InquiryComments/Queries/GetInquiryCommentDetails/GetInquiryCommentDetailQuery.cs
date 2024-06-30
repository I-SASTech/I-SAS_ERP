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

namespace Focus.Business.InquiryComments.Queries.GetInquiryCommentDetails
{
    public class GetInquiryCommentDetailQuery : IRequest<InquiryCommentDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryCommentDetailQuery, InquiryCommentDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryCommentDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryCommentDetailsLookUpModel> Handle(GetInquiryCommentDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var inquiryComment = await _context.InquiryComments.FindAsync(request.Id);

                    if (inquiryComment != null)
                    {
                        return new InquiryCommentDetailsLookUpModel()
                        {
                            Id = inquiryComment.Id,
                            Comment = inquiryComment.Comment,
                            Name = inquiryComment.Name,
                            DateTime = inquiryComment.DateTime,
                            ReplyCommentedId = inquiryComment.ReplyCommentedId,
                            InquiryId = inquiryComment.InquiryId
                        };
                    }
                    throw new NotFoundException(nameof(inquiryComment), request.Id);
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
