using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.InquiryStatus
{
    public class AddInquiryStatus : IRequest<InquiryStatusLookUp>
    {
        //Get all variable in entity
        public InquiryStatusLookUp Status { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddInquiryStatus, InquiryStatusLookUp>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddInquiryStatus> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<InquiryStatusLookUp> Handle(AddInquiryStatus request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    var status = new Domain.Entities.InquiryStatusDynamic()
                    {
                         Name= request.Status.Name
                    };

                    //Add Department to database
                    await Context.InquiryStatusDynamics.AddAsync(status, cancellationToken);


                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                        
                    }, cancellationToken);


                    await Context.SaveChangesAsync(cancellationToken);
                    request.Status.Id = status.Id;
                    return request.Status;

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
