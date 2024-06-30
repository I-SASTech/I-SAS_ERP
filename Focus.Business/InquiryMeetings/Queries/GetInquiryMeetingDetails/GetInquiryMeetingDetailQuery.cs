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

namespace Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingDetails
{
    public class GetInquiryMeetingDetailQuery : IRequest<InquiryMeetingDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryMeetingDetailQuery, InquiryMeetingDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryMeetingDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryMeetingDetailsLookUpModel> Handle(GetInquiryMeetingDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var inquiryMeeting = await _context.InquiryMeetings.FindAsync(request.Id);

                    if (inquiryMeeting != null)
                    {
                        var attendantList = new List<InquiryMeetingAttendantLookUp>();
                        foreach (var attendant in inquiryMeeting.InquiryMeetingAttendants)
                        {
                            attendantList.Add(new InquiryMeetingAttendantLookUp()
                            {
                                Id = attendant.Id,
                                EmployeeId = attendant.EmployeeId,
                                MeetingId = attendant.MeetingId
                            });
                        }
                        return new InquiryMeetingDetailsLookUpModel()
                        {
                            Id = inquiryMeeting.Id,
                            Agenda = inquiryMeeting.Agenda,
                            Description = inquiryMeeting.Description,
                            Date = inquiryMeeting.Date,
                            InquiryId = inquiryMeeting.InquiryId,
                            InquiryMeetingAttendantLookUp = attendantList
                        };
                    }
                    throw new NotFoundException(nameof(inquiryMeeting), request.Id);
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
