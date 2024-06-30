//using AutoMapper;
//using Focus.Business.Exceptions;
//using Focus.Business.Extensions;
//using Focus.Business.Interface;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Linq;
//using System.Security.Principal;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Focus.Business.DayStarts.Queries.GetDayStartDetails
//{
//    public class GetDayStartDetailQuery : IRequest<DayStartDetailsLookUpModel>
//    {
//        public Guid? UserId { get; set; }
//        public Guid? EmployeeId { get; set; }

//        public class Handler : IRequestHandler<GetDayStartDetailQuery, DayStartDetailsLookUpModel>
//        {
//            private readonly IApplicationDbContext _context;
//            private readonly ILogger _logger;
//            private readonly IMapper _mapper;
//            private readonly IPrincipal _principal;

//            public Handler(IApplicationDbContext context,
//                ILogger<GetDayStartDetailQuery> logger,
//                IMapper mapper, IPrincipal principal)
//            {
//                _context = context;
//                _logger = logger;
//                _mapper = mapper;
//                _principal = principal;
//            }
//            public async Task<DayStartDetailsLookUpModel> Handle(GetDayStartDetailQuery request, CancellationToken cancellationToken)
//            {
//                try
//                {
//                    if (_principal.Identity.UserId() == request.UserId.ToString())
//                    {
//                        var userTerminal = await _context.UserTerminals
//                            .Include(x => x.Terminal)
//                            .Where(x => x.UserId == request.UserId)
//                            .LastOrDefaultAsync(cancellationToken);

//                        if (userTerminal == null)
//                            throw new NotFoundException("User Terminal", request.UserId);

//                        var openingCash = _context.Transactions.AsNoTracking()
//                            .Include(x => x.Account)
//                            .Where(x => x.Account.Code == "101000").Sum(x => x.Debit - x.Credit);

//                        return new DayStartDetailsLookUpModel
//                        {
//                            CounterCode = userTerminal.Terminal.Code,
//                            CounterId = userTerminal.Terminal.Id,
//                            OpeningCash = openingCash
//                        };
//                    }

//                    return new DayStartDetailsLookUpModel();
//                }
//                catch (Exception e)
//                {
//                    _logger.LogInformation(e.Message);
//                    throw new NotFoundException("Day Start", request.UserId);
//                }
//            }
//        }
//    }
//}
