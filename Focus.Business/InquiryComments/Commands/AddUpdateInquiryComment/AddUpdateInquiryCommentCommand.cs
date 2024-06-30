using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryComments.Queries.GetInquiryCommentDetails;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryComments.Commands.AddUpdateInquiryComment
{
    public class AddUpdateInquiryCommentCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public InquiryCommentDetailsLookUpModel CommentDetails { get; set; }
       

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryCommentCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryCommentCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateInquiryCommentCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var inquiryComment = new InquiryComment()
                    {
                        Name = request.CommentDetails.Name,
                        DateTime = request.CommentDetails.DateTime,
                        Comment = request.CommentDetails.Comment,
                        ReplyCommentedId = request.CommentDetails.ReplyCommentedId,
                        InquiryId = request.CommentDetails.InquiryId
                    };

                    //Add Department to database
                    await _context.InquiryComments.AddAsync(inquiryComment, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    return inquiryComment.Id;

                }
                
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
