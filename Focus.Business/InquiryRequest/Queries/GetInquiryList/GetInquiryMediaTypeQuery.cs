using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryRequest.Queries.InquiryMediaType;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryList
{
    public class GetInquiryMediaTypeQuery : IRequest<List<InquiryMediaTypeLookUp>>
    {

        public class Handler : IRequestHandler<GetInquiryMediaTypeQuery, List<InquiryMediaTypeLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetInquiryMediaTypeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<List<InquiryMediaTypeLookUp>> Handle(GetInquiryMediaTypeQuery request, CancellationToken cancellationToken)
            {
                var mediaTypes =  Context.MediaTypes.ToList();
                var mediaTypeList = new List<InquiryMediaTypeLookUp>();

                foreach (var type in mediaTypes)
                {
                    mediaTypeList.Add(new InquiryMediaTypeLookUp()
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
