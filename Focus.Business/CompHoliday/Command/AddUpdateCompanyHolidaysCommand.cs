using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.CompHoliday.Models;
using Focus.Business.Exceptions;
using Focus.Business.Holidays.Commands.AddHoliday;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.CompHoliday.Command
{
    public class AddUpdateCompanyHolidaysCommand : IRequest<Message>
    {
        public CompanyHolidaysLookupModel companyHoliday { get; set; }
        public class Handler : IRequestHandler<AddUpdateCompanyHolidaysCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            public readonly IMediator _mediator;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateCompanyHolidaysCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(AddUpdateCompanyHolidaysCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.companyHoliday.Id != Guid.Empty)
                    {
                        var company =  _context.CompanyHolidays.FirstOrDefault(x => x.Id == request.companyHoliday.Id);
                        if (company == null)
                            throw new NotFoundException("Data Not Found", request.companyHoliday.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        company.Date = request.companyHoliday.Date;
                        company.HolidayType = request.companyHoliday.HolidayType;
                        company.Year = request.companyHoliday.Year;
                        company.Description = request.companyHoliday.Description;
                        company.PaidStatus = false;
                        company.IsActive = request.companyHoliday.IsActive;
                        company.Color = request.companyHoliday.Color;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message()
                        {
                            Id = company.Id,
                            IsAddUpdate = ""
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var company = new CompanyHolidays()
                        {
                            HolidayType = request.companyHoliday.HolidayType,
                            Date = request.companyHoliday.Date,
                            Year = request.companyHoliday.Year,
                            Description = request.companyHoliday.Description,
                            PaidStatus = false,
                            IsActive = request.companyHoliday.IsActive,
                            Color = request.companyHoliday.Color
                        };

                        await _context.CompanyHolidays.AddAsync(company, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message()
                        {
                            Id = company.Id,
                            IsAddUpdate = ""
                        };
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
