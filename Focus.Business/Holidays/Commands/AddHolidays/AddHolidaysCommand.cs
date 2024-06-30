using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Holidays.Commands.AddHoliday
{
    public class AddHolidayCommand : IRequest<Guid>
    {

        public HolidayLookupModel Holiday { get; set; }
        public class Handler : IRequestHandler<AddHolidayCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            public readonly IMediator _mediator;
            private string _code;
            //Create logger interface variable for log error
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AddHolidayCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddHolidayCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Holiday.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var holiday = new Holiday()
                        {
                            Name = request.Holiday.Name,
                            InTime = request.Holiday.InTime,
                            OutTime = request.Holiday.OutTime,
                            NameArabic = request.Holiday.NameArabic,
                            Month = request.Holiday.Month,
                            Year = request.Holiday.Year,
                            Description = request.Holiday.Description,

                            GuestedHolidays = request.Holiday.GuestedHolidays.Select(x =>
                               new GuestedHoliday()
                               {
                                   Date = x.Date,
                                   Description = x.Description,


                               }).ToList(),

                        };
                        await _context.Holidays.AddAsync(holiday, cancellationToken);

                        var weekHoliday = _context.WeekHolidays
                         .AsNoTracking().ToList();

                        foreach( var x in request.Holiday.WeekHolidays)
                        {
                            var chk = weekHoliday.FirstOrDefault(y => y.Name == x.Name);
                            if (chk!=null)
                            {
                                chk.HolidayId = holiday.Id;
                                _context.WeekHolidays.Update(chk);
                            }
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                          
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);
                        // Return Product id after successfully updating data
                        return holiday.Id;
                    }
                    else
                    {
                        var purchase = await _context.Holidays
                          .FindAsync(request.Holiday.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        purchase.Name = request.Holiday.Name;
                        purchase.InTime = request.Holiday.InTime;
                        purchase.OutTime = request.Holiday.OutTime;
                        purchase.NameArabic = request.Holiday.NameArabic;
                        purchase.Month = request.Holiday.Month;
                        purchase.Year = request.Holiday.Year;
                        purchase.Description = request.Holiday.Description;


                      
                        _context.GuestedHolidays.RemoveRange(purchase.GuestedHolidays);

                        purchase.GuestedHolidays = request.Holiday.GuestedHolidays.Select(x =>
                               new GuestedHoliday()
                               {
                                   Date = x.Date,
                                   Description = x.Description,


                               }).ToList();

                        _context.WeekHolidays.RemoveRange(purchase.WeekHolidays);
                        var weekHoliday = _context.WeekHolidays
                        .AsNoTracking().ToList();

                        foreach (var x in request.Holiday.WeekHolidays)
                        {
                            var chk = weekHoliday.FirstOrDefault(y => y.Name == x.Name);
                            if (chk != null)
                            {
                                chk.HolidayId = purchase.Id;
                                _context.WeekHolidays.Update(chk);
                            }
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return purchase.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
