using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingDetails;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryMeetings.Commands.AddUpdateInquiryMeeting
{
    public class AddUpdateInquiryMeetingCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public InquiryMeetingDetailsLookUpModel MeetingDetails { get; set; }
       

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryMeetingCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryMeetingCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateInquiryMeetingCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var inquiryMeeting = new InquiryMeeting()
                    {
                        Agenda = request.MeetingDetails.Agenda,
                        Date = request.MeetingDetails.Date,
                        Description = request.MeetingDetails.Description,
                        InquiryId = request.MeetingDetails.InquiryId
                    };

                    //Add Department to database
                    await Context.InquiryMeetings.AddAsync(inquiryMeeting, cancellationToken);
                    var attendantList = new List<InquiryMeetingAttendant>();
                    foreach (var attendant in request.MeetingDetails.InquiryMeetingAttendantLookUp)
                    {
                        attendantList.Add(new InquiryMeetingAttendant()
                        {
                            EmployeeId = attendant.EmployeeId,
                            MeetingId = inquiryMeeting.Id
                        });
                    }

                    await Context.InquiryMeetingAttendants.AddRangeAsync(attendantList, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return inquiryMeeting.Id;

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
