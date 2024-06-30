//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Focus.Business.Common;
//using Focus.Business.Interface;
//using Focus.Domain.Enums;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Noble.Business.DailyExpenses;

//namespace Focus.Business.DailyExpenses
//{
//    public class GetDailyExpenseListQuery : IRequest<DailyExpenseVm>
//    {
//        public bool isActive;

//        public class Handler : IRequestHandler<GetDailyExpenseListQuery, DailyExpenseVm>
//        {
//            public readonly IApplicationDbContext _context;
//            public readonly ILogger _logger;
//            public readonly IMapper _mapper;

//            public Handler(IApplicationDbContext context,
//                ILogger<GetDailyExpenseListQuery> logger,
//                IMapper mapper)
//            {
//                _context = context;
//                _logger = logger;
//                _mapper = mapper;
//            }
//            public async Task<DailyExpenseVm> Handle(GetDailyExpenseListQuery request, CancellationToken cancellationToken)
//            {
//                try
//                {
//                    if (request.isActive == false)
//                    {
//                        var Id = _context.DailyExpenses.AsQueryable();

//                        var deList = await Id
//                            .OrderBy(x => x.Id)
//                            .ProjectTo<DailyExpenseVm>(_mapper.ConfigurationProvider)
//                            .ToListAsync(cancellationToken);

//                        return new DailyExpenseVm
//                        {
//                            DailyExpenseDetails = (ICollection<DailyExpenseDetailVm>)deList
//                        };
//                    }
//                    if (request.isActive == true)
//                    {
//                        var Id = _context.DailyExpenses.AsQueryable();

//                        var deList = await Id
//                            .OrderBy(x => x.Id)

//                            .ProjectTo<DailyExpenseVm>(_mapper.ConfigurationProvider)
//                            .ToListAsync(cancellationToken);

//                        return new DailyExpenseVm
//                        {
//                            DailyExpenseDetails = (ICollection<DailyExpenseDetailVm>)deList
//                        };
//                    }
//                    return null;
//                }
//                catch (Exception exception)
//                {
//                    _logger.LogError(exception.Message);
//                    throw new ApplicationException("List Error");
//                }
//            }
//        }
//    }

//}
