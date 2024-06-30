using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Exceptions;
using Focus.Business.InquiryRequest.Queries.GetInquiryDetails;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Commands.AddUpdateInquiry
{
    public class UpdateInquiryStatus : IRequest<InquiryStatusLookUpModel>
    {
        //Get all variable in entity
        public InquiryStatusLookUpModel LookUp { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<UpdateInquiryStatus, InquiryStatusLookUpModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<UpdateInquiryStatus> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<InquiryStatusLookUpModel> Handle(UpdateInquiryStatus request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var inquiry = Context.Inquiries.FirstOrDefault(x =>
                        x.Id == request.LookUp.InquiryId);
                    if (inquiry == null)
                        throw new ObjectAlreadyExistsException("Selected Inquiry not exist");
                    var inquiryStatus = new InquiryStatus()
                    {
                        DateTime = DateTime.Now,
                        InquiryStatusDynamicId = request.LookUp.InquiryStatusDynamicId,
                        InquiryId = request.LookUp.InquiryId,
                        Reason = request.LookUp.Reason,
                    };
                    await Context.InquiryStatuses.AddAsync(inquiryStatus, cancellationToken);
                    //inquiry.InquiryStatus =
                    //(InquiryStatus) Enum.Parse(typeof(InquiryStatus), request.Status);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    var status = new InquiryStatusLookUpModel()
                    {
                        DateTime = inquiryStatus.DateTime.ToString("d"),
                        Reason = inquiryStatus.Reason,
                        InquiryStatusDynamicId = request.LookUp.InquiryStatusDynamicId,
                        InquiryId = inquiryStatus.InquiryId,
                    };
                    return status;
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
