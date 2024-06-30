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

namespace Focus.Business.ManualAttendance.Queries.EmployeeTodayAttendence
{
  public class EmployeeTodayAttendence : IRequest<List<TodayAttendenceLookUpModel>>
    {

        public class Handler : IRequestHandler<EmployeeTodayAttendence, List<TodayAttendenceLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<EmployeeTodayAttendence> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<List<TodayAttendenceLookUpModel>> Handle(EmployeeTodayAttendence request, CancellationToken cancellationToken)
            {
                try
                {
                    //Holidays
                    var holiday = _context.Holidays.AsNoTracking()
                        .Include(x => x.GuestedHolidays)
                        .Include(x => x.WeekHolidays)
                        .FirstOrDefault();
                    if (holiday == null)
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
                    var currentDay = now.DayOfWeek;
                  


                    
                    var query = _context.EmployeeRegistrations
                           .AsNoTracking()
                           .Include(x=>x.Department)
                           .Where(x => !x.IsActive)
                           .ToList();
                    var manualAttendence = _context.ManualAttendances.AsNoTracking()
                        .Where(x=>x.Date.Date== DateTime.Now.Date)
                        .ToList();
                    var employeeList = new List<TodayAttendenceLookUpModel>();


                    foreach (var employee in query)
                    {
                        bool IsCheckIn = false;
                        bool IsCheckOut = false;
                        bool IsOnLeave = false;
                        bool IsAbsent = false;
                        Guid AttendenceId = Guid.Empty;
                        var record = manualAttendence.FirstOrDefault(x => x.EmployeeId == employee.Id);
                        if(record!=null)
                        {
                            IsCheckIn = record.IsCheckIn;
                            IsCheckOut = record.IsCheckOut;
                            IsOnLeave = record.IsOnLeave;
                            IsAbsent = record.IsAbsent;
                            AttendenceId = record.Id;
                        }
                       
                       

                        var lookUpModel = new TodayAttendenceLookUpModel
                        {
                            EmployeeId = employee.Id,
                            CheckIn = record==null?null: record.CheckIn,
                            CheckOut = record == null ? null : record.CheckOut,
                            AttendenceId = AttendenceId,
                            EnglishName = employee.EnglishName,
                            ArabicName = employee.ArabicName,
                            DepartmentEng = employee.Department?.Name,
                            DepartmentAr = employee.Department?.NameArabic,
                            IsCheckIn= IsCheckIn,
                            IsCheckOut = IsCheckOut,
                            IsOnLeave = IsOnLeave,
                            IsAbsent = IsAbsent,


                        };
                        employeeList.Add(lookUpModel);

                    }

                    return employeeList;

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
