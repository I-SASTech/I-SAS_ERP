using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.InquiryEmails.Commands.AddUpdateInquiryEmail;
using Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.InquiryPriority
{
    public class AddInquiryPriority : IRequest<InquiryPriorityLookUp>
    {
        //Get all variable in entity
        public InquiryPriorityLookUp Priority { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddInquiryPriority, InquiryPriorityLookUp>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddInquiryPriority> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<InquiryPriorityLookUp> Handle(AddInquiryPriority request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var mediaType = new Domain.Entities.InquiryPriority()
                    {
                         Name= request.Priority.Name
                    };

                    //Add Department to database
                    await Context.InquiryPriorities.AddAsync(mediaType, cancellationToken);


                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    request.Priority.Id = mediaType.Id;
                    return request.Priority;

                }

                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
