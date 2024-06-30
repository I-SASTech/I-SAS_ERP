using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.GetTerminalOfCurrentUser
{
    public class GetTerminalOfCurrentUserQuery : IRequest<Guid?>
    {
        //Get all variable in entity
        public string Id { get; set; }
        //public ApplicationUser User { get; set; }
        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<GetTerminalOfCurrentUserQuery, Guid?>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            public readonly UserManager<ApplicationUser> UserManager;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetTerminalOfCurrentUserQuery> logger,
                UserManager<ApplicationUser> userManager)
            {
                Context = context;
                Logger = logger;
                UserManager = userManager;
            }

            public async Task<Guid?> Handle(GetTerminalOfCurrentUserQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user =await UserManager.FindByIdAsync(request.Id);
                    var dayaStart =await Context.DayStarts.FirstOrDefaultAsync(x => x.StartTerminalFor == user.UserName && x.IsActive && x.ToTime == null,
                        cancellationToken: cancellationToken);
                    var terminal = dayaStart != null ? dayaStart.CounterId : user.TerminalId;
                    if(terminal == null)
                        return Guid.Empty;
                    
                    return terminal;
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Some error occured");
                }
            }
        }
    }
}
