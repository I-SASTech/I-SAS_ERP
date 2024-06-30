using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Holidays.Queries.GetHolidayDetails
{
    public class GetHolidayDetailQuery : IRequest<HolidayLookupModel>
    {
        public Guid? Id { get; set; }

        public class Handler : IRequestHandler<GetHolidayDetailQuery, HolidayLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetHolidayDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<HolidayLookupModel> Handle(GetHolidayDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var holiday =  _context.Holidays.AsNoTracking()
                        .Include(x=>x.GuestedHolidays)
                        .Include(x=>x.WeekHolidays)
                        .FirstOrDefault();

                    if (holiday != null)
                    {


                      

                        return new HolidayLookupModel
                        {
                            Id = holiday.Id,
                            Name = holiday.Name,
                            NameArabic = holiday.NameArabic,
                            InTime = holiday.InTime,
                            OutTime = holiday.OutTime,
                            Month = holiday.Month,
                            Year = holiday.Year,
                            Description = holiday.Description,
                            WeekHolidays = holiday.WeekHolidays.Select(x =>
                                new WeekHolidaysLookupModel()
                                {
                                    Id=x.Id,
                                    Name = x.Name,
                                    NameArabic = x.NameArabic,

                                }).ToList(),
                            GuestedHolidays = holiday.GuestedHolidays.Select(x =>
                                new GuestedHolidaysLookupModel()
                                {
                                    Id=x.Id,
                                    Date = x.Date,
                                    Description = x.Description,

                                }).ToList(),
                        };
                    }
                    else
                    {
                        return null;
                    }
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
