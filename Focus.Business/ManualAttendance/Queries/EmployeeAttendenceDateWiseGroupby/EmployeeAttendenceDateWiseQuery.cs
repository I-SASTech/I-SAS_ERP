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

namespace Focus.Business.ManualAttendance.Queries.EmployeeAttendenceDateWiseGroupby
{
   public class EmployeeAttendenceDateWiseQuery : IRequest<List<ManualAttendenceLookUpModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<Guid?> Employee { get; set; }

        public class Handler : IRequestHandler<EmployeeAttendenceDateWiseQuery, List<ManualAttendenceLookUpModel>>
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

            public async Task<List<ManualAttendenceLookUpModel>> Handle(EmployeeAttendenceDateWiseQuery request, CancellationToken cancellationToken)
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
                     .Where(x => !x.IsActive)
                     .ToList();
                    request.ToDate = request.ToDate.AddDays(1);

                    var offDays = new List<WeekHolidaysLookupModel>();
                    var GuestedHolidays = new List<GuestedHolidaysLookupModel>();
                    if (holiday!=null)
                    {
                         offDays = holiday.WeekHolidays.Select(x =>
                                new WeekHolidaysLookupModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    NameArabic = x.NameArabic,

                                }).ToList();
                         GuestedHolidays = holiday.GuestedHolidays.Select(x =>
                             new GuestedHolidaysLookupModel()
                             {
                                 Id = x.Id,
                                 Date = x.Date,
                                 Description = x.Description,

                             }).ToList();
                    }
                    
                   
                    
                    var employeeAttendence = new List<ManualAttendenceLookUpModel>();
                    if(request.Employee!=null && request.Employee.Count!=0 )
                    {
                        employees = employees.Where(x => request.Employee.Contains(x.Id)).ToList();

                    }
                    if (request.FromDate != null && request.ToDate != null && holiday != null)
                    {
                        while (request.FromDate.Date < request.ToDate.Date)
                        {



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
                                    Description = chkOffDay.Name,
                                    TotalHour = 9 * 60,
                                    TotalMinute = 0,
                                    OfficeMinute = 0,
                                    OfficeHour = 9*60,
                                    WorkingPercentage = 100,
                                    Id = Guid.NewGuid(),




                                });

                            }
                            else if (isGuestedDay)
                            {
                                employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                {
                                    CheckType = "Guested Holiday",
                                    Description = chkGuestedDay.Description,
                                    TotalHour = 9 * 60,
                                    TotalMinute = 0,
                                    OfficeMinute = 0,
                                    OfficeHour = 9 * 60,
                                    WorkingPercentage = 100,
                                    Id = Guid.NewGuid(),


                                }); ;
                            }




                            else if (request.FromDate.Date <= DateTime.Now)
                            {
                                foreach (var employee in employees)
                                {
                                    decimal hour = 0;
                                    decimal minute = 0;
                                    decimal officeHour = 0;
                                    decimal officeMinute = 0;
                                    
                                    var recordMatch = query.Where(x => x.EmployeeId == employee.Id && x.Date.Date == request.FromDate.Date).FirstOrDefault();
                                    if (recordMatch != null)
                                    {
                                        if (recordMatch.CheckIn != null && recordMatch.CheckOut != null)
                                        {
                                            hour = (recordMatch.CheckOut - recordMatch.CheckIn).Value.Hours;
                                            minute = (recordMatch.CheckOut - recordMatch.CheckIn).Value.Minutes;
                                            hour= (hour * 60) + minute;
                                        }

                                        if (holiday.InTime != null && holiday.OutTime != null)
                                        {
                                            officeHour = (holiday.OutTime - holiday.InTime).Value.Hours;
                                          
                                            officeMinute = (holiday.OutTime - holiday.InTime).Value.Minutes;
                                            officeHour = (officeHour * 60)+ officeMinute;
                                        }

                                        if(recordMatch.IsOnLeave)
                                        {
                                            employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                            {
                                                CheckType = "On Leave",
                                                Description = "Leave",
                                                TotalHour = 9*60,
                                                TotalMinute = 0,
                                                OfficeMinute = 0,
                                                OfficeHour = 9*60,
                                                WorkingPercentage = 100,
                                                EmployeeName = recordMatch.EmployeeRegistration?.EnglishName,
                                                EmployeeNameAr = recordMatch.EmployeeRegistration?.ArabicName,
                                                Id = Guid.NewGuid(),



                                            });
                                        }

                                        else
                                        {
                                            employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                            {
                                                Id = recordMatch.Id,
                                                Date = recordMatch.Date,
                                                CheckIn = recordMatch.CheckIn,
                                                CheckInTime = recordMatch.CheckIn?.ToString("d"),
                                                CheckOutTime = recordMatch.CheckOut?.ToString("d"),
                                                CheckOut = recordMatch.CheckOut,
                                                IsAbsent = recordMatch.IsAbsent,
                                                EmployeeName = recordMatch.EmployeeRegistration?.EnglishName,
                                                EmployeeNameAr = recordMatch.EmployeeRegistration?.ArabicName,
                                                CompanyTimeIn = holiday.InTime,
                                                CompanyTimeOut = holiday.OutTime,
                                                TotalHour = hour,
                                                TotalMinute = minute,
                                                OfficeMinute = officeMinute,
                                                OfficeHour = officeHour,
                                                WorkingPercentage = hour == 0 ? 0 : (hour / officeHour) * 100,

                                            });
                                        }
                                        

                                    }
                                    //else
                                    //{
                                    //    employeeAttendence.Add(new ManualAttendenceLookUpModel()
                                    //    {
                                    //        EmployeeName = employee.EnglishName,
                                    //        EmployeeNameAr = employee.EnglishName,

                                    //    });
                                    //}

                                }







                            }
                            request.FromDate = request.FromDate.AddDays(1);
                        }

                    }




                    return employeeAttendence;

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
