using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.WholeSaleQueries
{
    public class IsWholeSaleDayStart : IRequest<WholeSaleDayStartLookUpModel>
    {
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool IsLogin { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsSupervisor { get; set; }

        public class Handler : IRequestHandler<IsWholeSaleDayStart, WholeSaleDayStartLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IPrincipal _principal;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(IApplicationDbContext context,
                ILogger<IsWholeSaleDayStart> logger,
                IMediator mediator, UserManager<ApplicationUser> userManager,
                IPrincipal principal, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _userManager = userManager;
                _logger = logger;
                _mediator = mediator;
                _principal = principal;
                _contextProvider = contextProvider;
            }
            public async Task<WholeSaleDayStartLookUpModel> Handle(IsWholeSaleDayStart request, CancellationToken cancellationToken)
            {
                try
                {
                    var isDayStart =await _context.DayStarts.OrderBy(x => x.IsActive && x.IsDayStart).LastOrDefaultAsync(cancellationToken: cancellationToken);
                    if (isDayStart != null)
                    {
                        return new WholeSaleDayStartLookUpModel()
                        {
                            CounterName = "TR-0001",
                            Username = isDayStart.StartTerminalFor,
                            FromTime = isDayStart.FromTime.Value.ToString("d"),
                            IsDayStart = isDayStart.IsDayStart,
                            IsActive = isDayStart.IsActive,
                            DayStartId = isDayStart.Id,
                            CounterId = isDayStart.CounterId

                        };
                    }
                    else
                    {
                        return new WholeSaleDayStartLookUpModel()
                        {
                            CounterName = "TR-0001",
                            IsDayStart = false,
                            IsActive = false

                        };
                    }

                    
                    

                }
                catch (NotFoundException e)
                {
                    _logger.LogInformation(e.Message);
                    throw new NotFoundException(e.Message, request.UserId);
                }
                catch (ObjectAlreadyExistsException e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ObjectAlreadyExistsException(e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException("Something Went Wrong");
                }

            }
        }
    }
}
