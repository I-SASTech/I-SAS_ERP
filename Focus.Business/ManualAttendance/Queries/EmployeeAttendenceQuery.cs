using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Focus.Business.ManualAttendance.Queries
{
  public  class EmployeeAttendenceQuery :  IRequest<List<ManualAttendenceLookUpModel>>
    {
      
        public Guid? EmployeeId { get; set; }

        public class Handler : IRequestHandler<EmployeeAttendenceQuery, List<ManualAttendenceLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<EmployeeAttendenceQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<ManualAttendenceLookUpModel>> Handle(EmployeeAttendenceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                   


                 var   query = _context.ManualAttendances
                        .AsNoTracking()
                        .Include(x=>x.EmployeeRegistration)
                        .Where(x => x.EmployeeId == request.EmployeeId)
                        .ToList();
                    if(query!=null)
                    {
                        var holiday = _context.Holidays.AsNoTracking()
                      .Include(x => x.GuestedHolidays)
                      .Include(x => x.WeekHolidays)
                      .FirstOrDefault();

                        var earlyTime = holiday.InTime?.ToString("HH:mm");

                        var manuals = new List<ManualAttendenceLookUpModel>();
                        foreach (var color in query)
                        {
                            decimal hour=0;
                            decimal minute=0;
                            decimal officeHour=0;
                            decimal officeMinute=0;
                            if(color.CheckIn!=null && color.CheckOut!=null)
                            {
                                hour = (color.CheckOut - color.CheckIn).Value.Hours;
                                minute = (color.CheckOut - color.CheckIn).Value.Minutes;
                                hour = (hour * 60) + minute;
                            }

                             if(holiday.InTime != null && holiday.OutTime!=null)
                            {
                                officeHour = (holiday.OutTime - holiday.InTime).Value.Hours;
                                officeMinute = (holiday.OutTime - holiday.InTime).Value.Minutes;
                                officeHour = (officeHour * 60) + officeMinute;
                            }

                            if (color.IsOnLeave)
                            {
                                manuals.Add(new ManualAttendenceLookUpModel()
                                {
                                    CheckType = "On Leave",
                                    IsOnLeave=true,
                                    Description = "Leave",
                                    TotalHour = 9 * 60,
                                    TotalMinute = 0,
                                    OfficeMinute = 0,
                                    OfficeHour = 9 * 60,
                                    WorkingPercentage = 100,
                                    EmployeeName = color.EmployeeRegistration?.EnglishName,
                                    EmployeeNameAr = color.EmployeeRegistration?.ArabicName,



                                });
                            }
                            else
                            {
                                manuals.Add(new ManualAttendenceLookUpModel()
                                {
                                    Id = color.Id,
                                    CheckIn = color.CheckIn,
                                    IsOnLeave = color.IsOnLeave,
                                    IsAbsent = color.IsAbsent,
                                    CheckOut = color.CheckOut,
                                    EmployeeName = color.EmployeeRegistration?.EnglishName,
                                    EmployeeNameAr = color.EmployeeRegistration?.ArabicName,
                                    CompanyTimeIn = holiday.InTime,
                                    CompanyTimeOut = holiday.OutTime,
                                    TotalHour = hour,
                                    TotalMinute = minute,
                                    OfficeMinute = officeMinute,
                                    OfficeHour = officeHour,

                                });
                            }

                           

                        }


                        return manuals;

                    }
                    else

                    {
                        return null;
                    }


                }


                
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}