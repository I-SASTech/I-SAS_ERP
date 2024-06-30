using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Emails.Queries
{
    public class GetPdfBytesFromReportServer : IRequest<Message>
    {
        public class Handler : IRequestHandler<GetPdfBytesFromReportServer, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly IUserHttpContextProvider _contextProvider;
            public readonly IConfiguration _configuration;

            public Handler(IApplicationDbContext context, IUserHttpContextProvider contextProvider, IConfiguration configuration)
            {
                _context = context;
                _contextProvider = contextProvider;
                _configuration = configuration;
            }
            public async Task<Message> Handle(GetPdfBytesFromReportServer request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Message();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
