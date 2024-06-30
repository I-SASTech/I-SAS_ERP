//using AutoMapper;
//using Focus.Business.Common;
//using Focus.Business.Interface;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Focus.Domain.Enum;

//namespace Focus.Business.Holidays.Queries.GetHolidayList
//{
//    public class GetHolidayListQuery : PagedRequest, IRequest<PagedResult<List<HolidaysLookupModel>>>
//    {
//        public string SearchTerm { get; set; }
//        public ApprovalStatus Status { get; set; }
//        public bool IsDropDown { get; set; }

//        public class Handler : IRequestHandler<GetHolidayListQuery, PagedResult<List<HolidaysLookupModel>>>
//        {
//            public readonly IApplicationDbContext _context;
//            public readonly ILogger _logger;
//            private readonly IMapper _mapper;
//            public Handler(IApplicationDbContext context, ILogger<GetHolidayListQuery> logger, IMapper mapper)
//            {
//                _context = context;
//                _logger = logger;
//                _mapper = mapper;
//            }

//            public async Task<PagedResult<List<HolidaysLookupModel>>> Handle(GetHolidayListQuery request, CancellationToken cancellationToken)
//            {
//                try
//                {
//                    if (request.IsDropDown)
//                    {
//                        var po = _context.Holidays
//                            .AsNoTracking()
//                            .AsQueryable();

//                        var holidayList = new List<HolidaysLookupModel>();
//                        foreach (var holiday in po)
//                        {
//                            var sOrder = HolidaysLookupModel.Create(holiday);
//                            holidayList.Add(sOrder);
//                        }
//                        return new PagedResult<List<HolidaysLookupModel>>
//                        {
//                            Results = holidayList
//                        };

//                    }
//                    else
//                    {
//                        var query = _context.Holidays
//                            .AsNoTracking()
//                            .Include(x => x.BillerAccount)
//                            .Include(x => x.HolidayItems)
//                            .AsQueryable();
//                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
//                        {
//                            query = query.Where(x => x.ApprovalStatus == request.Status);
//                        }
//                        if (!string.IsNullOrEmpty(request.SearchTerm))
//                        {
//                            var searchTerm = request.SearchTerm;
                           
                           
//                                query = query.Where(x =>
//                                    x.RegistrationNo.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));
                            
//                        }
//                        query = query.OrderByDescending(x => x.RegistrationNo);
//                        var count = await query.CountAsync(cancellationToken: cancellationToken);
//                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
//                        var holidayList = new List<HolidaysLookupModel>();
//                        foreach (var holiday in query)
//                        {
//                            var sOrder = HolidaysLookupModel.Create(holiday);
//                            holidayList.Add(sOrder);
//                        }

//                        return new PagedResult<List<HolidaysLookupModel>>
//                        {
//                            Results = holidayList,
//                            RowCount = count,
//                            PageSize = request.PageSize,
//                            CurrentPage = request.PageNumber,
//                            PageCount = holidayList.Count / request.PageSize
//                        };
//                    }

//                }
//                catch (Exception exception)
//                {
//                    _logger.LogError(exception.Message);
//                    throw new ApplicationException(exception.Message);
//                }
//            }
//        }
//    }
//}
