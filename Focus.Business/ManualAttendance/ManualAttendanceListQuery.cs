using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Holidays;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ManualAttendance
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, string lastDay)
        {
            DayOfWeek startOfWeek = DayOfWeek.Monday;
            switch (lastDay)
            {
                case "Monday":
                    startOfWeek = DayOfWeek.Tuesday;
                    break;
                case "Tuesday":
                    startOfWeek = DayOfWeek.Wednesday;
                    break;
                case "Wednesday":
                    startOfWeek = DayOfWeek.Thursday;
                    break;
                case "Thursday":
                    startOfWeek = DayOfWeek.Friday;
                    break;
                case "Friday":
                    startOfWeek = DayOfWeek.Saturday;
                    break;
                case "Saturday":
                    startOfWeek = DayOfWeek.Sunday;
                    break;
                case "Sunday":
                    startOfWeek = DayOfWeek.Monday;
                    break;
            }

            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
    public class ManualAttendanceListQuery : IRequest<ManualAttendanceListModel>
    {
        public Guid? Id { get; set; }

        public class Handler : IRequestHandler<ManualAttendanceListQuery, ManualAttendanceListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ManualAttendanceListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<ManualAttendanceListModel> Handle(ManualAttendanceListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //Holidays
                    var holiday = _context.Holidays.AsNoTracking()
                        .Include(x => x.GuestedHolidays)
                        .Include(x => x.WeekHolidays)
                        .FirstOrDefault();
                    if(holiday==null)
                    {
                        throw new ObjectAlreadyExistsException("First Select Week Holidays,Office Timing from Holidays Setup");
                    }

                    var offDays = holiday.WeekHolidays.Select(x =>
                                 new WeekHolidaysLookupModel()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     NameArabic = x.NameArabic,

                                 }).ToList();
                    var lastDay = offDays.LastOrDefault()?.Name;

                    if(lastDay==null)
                    {
                        lastDay = "Sunday";
                    }

                    var GuestedHolidays = holiday.GuestedHolidays.Select(x =>
                                new GuestedHolidaysLookupModel()
                                {
                                    Id = x.Id,
                                    Date = x.Date,
                                    Description = x.Description,

                                }).ToList();




                    var currentMonth = DateTime.Now.Month.ToString();
                    var CurrentYear = DateTime.Now.Year.ToString();
                    var now = DateTime.Now;
                    now = now.AddDays(-6);
                    var currentDay = now.DayOfWeek;
                    int days = (int)currentDay;
                    DateTime sunday = now;
                    var daysThisWeek = Enumerable.Range(0, 7)
                        .Select(d => sunday.AddDays(d))
                        .ToList();

                    var holidayList = new List<DayOfWeekLookUpModel>();

                    foreach (var day in daysThisWeek)
                    {
                        var inActive = false;
                        var holidayType = "";
                        if (day < DateTime.Now.Date)
                        {
                            inActive = false;
                            holidayType = "Prev Day";
                        }
                        if (offDays.Any(x => x.Name == day.Date.DayOfWeek.ToString()))
                        {
                            inActive = true;
                            holidayType = "Week Holiday";
                        }
                        if (GuestedHolidays.Any(x => x.Date.Date == day.Date))
                        {
                            inActive = true;
                            holidayType = "Guested Holidays";
                        }
                        var lookUpModel = new DayOfWeekLookUpModel
                        {
                            WeekDate = day.Date,
                            Disable = inActive,
                            HolidayType = holidayType,
                            DayName = day.Date.DayOfWeek.ToString(),
                        };
                        holidayList.Add(lookUpModel);

                    }

                    var query = _context.EmployeeRegistrations
                           .AsNoTracking()
                           .Where(x=>!x.IsActive)
                           .ToList();
                    var manualAttendence = _context.ManualAttendances.AsNoTracking().AsQueryable();
                    var employeeList = new List<EmployeeManualAttendence>();


                    foreach (var employee in query)
                    {

                        var checkInDate = new List<Guid>();

                        var attendence = new List<ManualAttendenceLookUpModel>();

                        for (int i = 0; i < holidayList.Count(); i++)
                        {
                            var manuals = manualAttendence.Where(x => x.EmployeeId == employee.Id && x.Date.Date == holidayList[i].WeekDate).FirstOrDefault();
                            if(manuals!=null)
                            {
                                attendence.Add(new ManualAttendenceLookUpModel{
                                    Id= manuals.Id,
                                    IsCheckIn=manuals.IsCheckIn,
                                    IsCheckOut=manuals.IsCheckOut,
                                    CheckIn=manuals.CheckIn,
                                    CheckOut=manuals.CheckOut,
                                    EmployeeId=manuals.EmployeeId,
                                    IsOnLeave=manuals.IsOnLeave,
                                    IsAbsent = manuals.IsAbsent,
                                    Date=manuals.Date,
                                    Description=manuals.Description,


                                });

                            }
                            else
                            {
                                attendence.Add(new ManualAttendenceLookUpModel());
                            }
                           
                           

                        };

                        var lookUpModel = new EmployeeManualAttendence
                        {
                            Id = employee.Id,
                            EnglishName = employee.EnglishName,
                            ArabicName = employee.ArabicName,
                            Attendence= attendence
                        };
                        employeeList.Add(lookUpModel);

                    }

                    return new ManualAttendanceListModel
                    {

                        CurrentMonth = currentMonth,
                        CurrentYear = CurrentYear,
                        DayOfWeekLookUpModel = holidayList,
                        EmployeeManualAttendence= employeeList
                    };

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
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
