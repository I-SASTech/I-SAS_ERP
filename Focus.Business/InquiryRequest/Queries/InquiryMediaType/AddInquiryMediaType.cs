using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.InquiryMediaType
{
    public class AddInquiryMediaType : IRequest<InquiryMediaTypeLookUp>
    {
        //Get all variable in entity
        public InquiryMediaTypeLookUp MediaType { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddInquiryMediaType, InquiryMediaTypeLookUp>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddInquiryMediaType> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<InquiryMediaTypeLookUp> Handle(AddInquiryMediaType request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var mediaType = new MediaType()
                    {
                         Name= request.MediaType.Name
                    };

                    //Add Department to database
                    await Context.MediaTypes.AddAsync(mediaType, cancellationToken);


                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                        
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    request.MediaType.Id = mediaType.Id;
                    return request.MediaType;

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
