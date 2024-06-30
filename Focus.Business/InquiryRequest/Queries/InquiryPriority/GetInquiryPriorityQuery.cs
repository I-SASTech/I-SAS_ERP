using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryRequest.Queries.InquiryPriority;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.InquiryPriority
{
    public class GetInquiryPriorityQuery : IRequest<List<InquiryPriorityLookUp>>
    {

        public class Handler : IRequestHandler<GetInquiryPriorityQuery, List<InquiryPriorityLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetInquiryPriorityQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<List<InquiryPriorityLookUp>> Handle(GetInquiryPriorityQuery request, CancellationToken cancellationToken)
            {
                var mediaTypes =  Context.InquiryPriorities.ToList();
                var mediaTypeList = new List<InquiryPriorityLookUp>();

                foreach (var type in mediaTypes)
                {
                    mediaTypeList.Add(new InquiryPriorityLookUp()
                    {
                        Id = type.Id,
                        Name = type.Name
                    });
                }
                return mediaTypeList;
            }
        }
    }
}
