using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmailConfigurationSetup.Commands
{
    public class UpdateDefaultEmailConfiguration : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<UpdateDefaultEmailConfiguration, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<UpdateDefaultEmailConfiguration> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Message> Handle(UpdateDefaultEmailConfiguration request, CancellationToken cancellationToken)
            {
                try
                {
                    var emailsList = await _context.EmailConfiguration.Where(x => x.IsDefault).ToListAsync();

                    if(emailsList.Count >0)
                    {
                        var emailConfiguration = new List<EmailConfiguration>();

                        foreach (var item in emailsList)
                        {
                            item.IsDefault = false;
                            emailConfiguration.Add(item);
                        }

                        _context.EmailConfiguration.UpdateRange(emailConfiguration);

                        await _context.SaveChangesAsync();
                    }
                   

                   
                    var emailConfig = await _context.EmailConfiguration.FindAsync(request.Id);

                    emailConfig.IsDefault = true;
                    await _context.SaveChangesAsync();

                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Now this " + " " + emailConfig.Email + " ïs your default email.",
                    };
                }
                catch (Exception ex)
                {
                    return new Message
                    {
                        IsSuccess = false,
                        IsAddUpdate = ex.Message,
                    };
                }
            }
        }
    }
}
