using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Holidays.Queries.GetHolidaysList
{
  public  class GetWeekDaysListQuery : PagedRequest, IRequest<PagedResult<List<WeekHolidaysLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetWeekDaysListQuery, PagedResult<List<WeekHolidaysLookupModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetWeekDaysListQuery> logger, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator= mediator;
            }

            public async Task<PagedResult<List<WeekHolidaysLookupModel>>> Handle(GetWeekDaysListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var countAllowances = _context.WeekHolidays.Count();


                    if (countAllowances == 0)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var allowanceTypes = new List<WeekHoliday>()
                        {
                            new WeekHoliday
                            {
                                Name="Friday",
                                NameArabic= " ",


                            },
                            new WeekHoliday
                            {
                                Name="Saturday",
                                NameArabic= " "


                            },
                            new WeekHoliday
                            {
                                Name="Sunday",
                                NameArabic= " "


                            },
                            new WeekHoliday
                            {
                                  Name="Monday",
                                  NameArabic= " "

                            },
                            new WeekHoliday
                            {
                                  Name="Tuesday",
                                  NameArabic= " "


                            },
                            new WeekHoliday
                            {
                                  Name="Wednesday",
                                  NameArabic= " "


                            },
                            new WeekHoliday
                            {
                                  Name="Thrusday",
                                  NameArabic= " "


                            },

                        };

                        await _context.WeekHolidays.AddRangeAsync(allowanceTypes, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                    }


                    var po = _context.WeekHolidays
                            .AsNoTracking()
                            .AsQueryable();

                    

                        var purchaseBillList = new List<WeekHolidaysLookupModel>();
                        foreach (var item in po)
                        {
                        purchaseBillList.Add(new WeekHolidaysLookupModel
                        {
                            Id= item.Id,
                            Name=item.Name,
                            NameArabic=item.NameArabic
                        });
                           
                        }
                        return new PagedResult<List<WeekHolidaysLookupModel>>
                        {
                            Results = purchaseBillList
                        };

                    

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
