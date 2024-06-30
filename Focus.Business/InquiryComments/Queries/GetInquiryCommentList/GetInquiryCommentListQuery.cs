using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.InquiryComments.Queries.GetInquiryCommentDetails;

namespace Focus.Business.InquiryComments.Queries.GetInquiryCommentList
{
    public class GetInquiryCommentListQuery : PagedRequest, IRequest<PagedResult<InquiryCommentListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public Guid InquiryId { get; set; }
        public class Handler : IRequestHandler<GetInquiryCommentListQuery, PagedResult<InquiryCommentListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryCommentListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<InquiryCommentListModel>> Handle(GetInquiryCommentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //IQueryable<InquiryComment> query;
                    var query = _context.InquiryComments.Where(x=>x.ReplyCommentedId == null && x.InquiryId == request.InquiryId).ToList();

                    var inquiryCommentList = new List<InquiryCommentLookUpModel>();
                    
                    foreach (var inquiryComment in query)
                    {
                        var inquiryCommentChildList = new List<InquiryCommentLookUpModel>();
                        if (inquiryComment.InquiryCommentChild != null)
                        {
                            foreach (var child in inquiryComment.InquiryCommentChild)
                            {
                                inquiryCommentChildList.Add(new InquiryCommentLookUpModel()
                                {
                                    Id = child.Id,
                                    Comment = child.Comment,
                                    Name = child.Name,
                                    DateTime = child.DateTime,
                                    ReplyCommentedId = child.ReplyCommentedId,
                                    TimeSpan = GetRelativeDate(child.DateTime),
                                    InquiryId = child.InquiryId

                                });
                            }
                        }
                        inquiryCommentList.Add(new InquiryCommentLookUpModel()
                        {
                            Id = inquiryComment.Id,
                            Comment = inquiryComment.Comment,
                            Name = inquiryComment.Name,
                            DateTime = inquiryComment.DateTime,
                            ReplyCommentedId = inquiryComment.ReplyCommentedId,
                            CommentedData = inquiryCommentChildList,
                            TimeSpan = GetRelativeDate(inquiryComment.DateTime),
                            InquiryId = inquiryComment.InquiryId
                        });
                    }

                    return new PagedResult<InquiryCommentListModel>
                    {
                        Results = new InquiryCommentListModel
                        {
                            InquiryCommentLookUpModels = inquiryCommentList
                        },
                    };


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }

        public static string GetRelativeDate(DateTime commentDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - commentDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}
