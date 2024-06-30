using Focus.Business.Colors.Queries.GetColorDetails;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmailConfigurationSetup.Queries
{
    public class GetEmailConfigurationDeatilsQuery : IRequest<EmailConfigurationLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetEmailConfigurationDeatilsQuery, EmailConfigurationLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetEmailConfigurationDeatilsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<EmailConfigurationLookupModel> Handle(GetEmailConfigurationDeatilsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var emailConfig = await _context.EmailConfiguration.FindAsync(request.Id);

                    if (emailConfig == null)
                    {
                        throw new NotFoundException(nameof(emailConfig), request.Id);
                    } 

                    return new EmailConfigurationLookupModel()
                    {
                        Id = emailConfig.Id,
                        Email = emailConfig.Email,
                        Password = emailConfig.Password,
                        SmtpServer = emailConfig.SmtpServer,
                        Port = emailConfig.Port,
                        Server = emailConfig.Server,
                        IsActive = emailConfig.IsActive,
                    };
                   
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
