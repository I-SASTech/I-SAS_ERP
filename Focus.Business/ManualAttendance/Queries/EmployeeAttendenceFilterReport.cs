using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Holidays;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ManualAttendance.Queries
{
   public class EmployeeAttendenceFilterReport : IRequest<List<DateLookUpModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public class Handler : IRequestHandler<EmployeeAttendenceFilterReport, List<DateLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<EmployeeAttendenceFilterReport> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<List<DateLookUpModel>> Handle(EmployeeAttendenceFilterReport request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.ManualAttendances
                       .AsNoTracking()
                       .Include(x => x.EmployeeRegistration)
                       .ToList();
                    var holiday = _context.Holidays.AsNoTracking()
                     .Include(x => x.GuestedHolidays)
                     .Include(x => x.WeekHolidays)
                     .FirstOrDefault();
                    var employees = _context.EmployeeRegistrations
                     .AsNoTracking()
                     .Where(x=>!x.IsActive)
                     .ToList();

                    var offDays = holiday.WeekHolidays.Select(x =>
                                new WeekHolidaysLookupModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    NameArabic = x.NameArabic,

                                }).ToList();
                    var GuestedHolidays = holiday.GuestedHolidays.Select(x =>
                              new GuestedHolidaysLookupModel()
                              {
                                  Id = x.Id,
                                  Date = x.Date,
                                  Description = x.Description,

                              }).ToList();

                    var dateWiseAttendenceList = new List<DateLookUpModel>();
                    if (request.FromDate != null && request.ToDate != null)
                    {
                        
                        while (request.FromDate.Date< request.ToDate.Date )
                        {
                            var employeeAttendence = new List<ManualAttendenceLookUpModel>();
                            string dayof = request.FromDate.Date.DayOfWeek.ToString();
                            bool isOffDay = false;
                            bool isGuestedDay = false;
                            var chkOffDay = offDays.FirstOrDefault(a => a.Name == dayof);
                            if (chkOffDay != null)
                            {
                                isOffDay = true;

                            }
                            var chkGuestedDay = GuestedHolidays.FirstOrDefault(a => a.Date == request.FromDate.Date);
                            if (chkGuestedDay != null)
                            {
                                isGuestedDay = true;

                            }
                            if (isOffDay)
                            {
                                employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                {
                                    CheckType = "Week Holiday",
                                    Description = chkOffDay.Name


                                });

                            }
                            else if (isGuestedDay)
                            {
                                employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                {
                                    CheckType = "Guested Holiday",
                                    Description = chkGuestedDay.Description

                                }); 
                            }




                            if (request.FromDate.Date<= DateTime.Now)
                            {
                                foreach (var employee in employees)
                                {
                                    var recordMatch = query.Where(x => x.EmployeeId == employee.Id && x.Date.Date == request.FromDate.Date).FirstOrDefault();
                                    if (recordMatch != null)
                                    {
                                        employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                        {
                                            Id = recordMatch.Id,
                                            Date = recordMatch.Date,
                                            CheckIn = recordMatch.CheckIn,
                                            CheckInTime = recordMatch.CheckIn?.ToString("d"),
                                            CheckOutTime = recordMatch.CheckOut?.ToString("d"),
                                            CheckOut = recordMatch.CheckOut,
                                            EmployeeName = recordMatch.EmployeeRegistration?.EnglishName,
                                            EmployeeNameAr = recordMatch.EmployeeRegistration?.ArabicName,
                                            CompanyTimeIn = holiday.InTime,
                                            CompanyTimeOut = holiday.OutTime,

                                        });

                                    }
                                    else
                                    {
                                        employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                        {
                                            EmployeeName = employee.EnglishName,
                                            EmployeeNameAr = employee.EnglishName,

                                        });
                                    }

                                }

                                if (employeeAttendence.Count != 0)
                                {
                                    dateWiseAttendenceList.Add(new DateLookUpModel()
                                    {
                                        Date = request.FromDate,
                                        ManualAttendenceLookUpModel = employeeAttendence


                                    });
                                }
                            }
                           

                           

                            request.FromDate = request.FromDate.AddDays(1);
                        }


                       
                    }
                   
                    


                    return dateWiseAttendenceList;

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
