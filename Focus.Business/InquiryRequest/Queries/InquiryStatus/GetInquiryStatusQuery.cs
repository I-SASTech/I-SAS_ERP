using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryRequest.Queries.InquiryStatus;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.InquiryStatus
{
    public class GetInquiryStatusQuery : IRequest<List<InquiryStatusLookUp>>
    {

        public class Handler : IRequestHandler<GetInquiryStatusQuery, List<InquiryStatusLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetInquiryStatusQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<List<InquiryStatusLookUp>> Handle(GetInquiryStatusQuery request, CancellationToken cancellationToken)
            {
                var status =  Context.InquiryStatusDynamics.ToList();
                var statusList = new List<InquiryStatusLookUp>();

                foreach (var type in status)
                {
                    statusList.Add(new InquiryStatusLookUp()
                    {
                        Id = type.Id,
                        Name = type.Name
                    });
                }
                return statusList;
            }
        }
    }
}
