using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.FinancialYearTemporaryClosing.Models;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DateTime = System.DateTime;

namespace Focus.Business.FinancialYearTemporaryClosing.Commands
{
    public class ChangeFinancialYearStatusCommand : IRequest<Message>
    {
        public FinancialYearClosingLookUpModel FinancialYearClosing { get; set; }
        public class Handler : IRequestHandler<ChangeFinancialYearStatusCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<ChangeFinancialYearStatusCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(ChangeFinancialYearStatusCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    //Financial Year Settings
                    var financialYearSetting = await _context.FinancialYearSettings
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    if (financialYearSetting == null)
                    {
                        return new Message
                        {
                            IsAddUpdate = "Financial Year Settings"
                        };
                    }

                    if (financialYearSetting.ClosingType == "Month")
                    {
                        var currentDate = DateTime.Now;
                        var lastDate = DateTime.Now.AddMonths(-1);

                        var companySubmissionPeriods = await _context.CompanySubmissionPeriods
                            .Where(x => x.PeriodStart.Date >= request.FinancialYearClosing.FromDate.Date && x.PeriodEnd.Date <= request.FinancialYearClosing.ToDate.Date)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (!companySubmissionPeriods.Any(x => (x.PeriodStart.Year == currentDate.Year && x.PeriodStart.Month == currentDate.Month) || (x.PeriodStart.Year == lastDate.Year && x.PeriodStart.Month == lastDate.Month)))
                        {
                            return new Message
                            {
                                IsAddUpdate = "You can only Re-Open last or current Financial period"
                            };
                        }
                    }
                    else if (financialYearSetting.ClosingType == "Quarterly")
                    {
                        var currentDate = DateTime.Now;
                        var lastDate = DateTime.Now.AddMonths(-3);

                        var companySubmissionPeriods = await _context.CompanySubmissionPeriods
                            .Where(x => x.PeriodStart.Date >= request.FinancialYearClosing.FromDate.Date && x.PeriodEnd.Date <= request.FinancialYearClosing.ToDate.Date)
                            .ToListAsync(cancellationToken: cancellationToken);
                        
                        if (!companySubmissionPeriods.Any(x => (x.PeriodStart.Year == currentDate.Year && x.PeriodStart.Month == currentDate.Month) || (x.PeriodStart.Year == lastDate.Year && x.PeriodStart.Month == lastDate.Month)))
                        {
                            return new Message
                            {
                                IsAddUpdate = "You can only Re-Open last or current Financial period"
                            };
                        }
                    }
                    else if (financialYearSetting.ClosingType == "6 Months")
                    {
                        var currentDate = DateTime.Now;
                        var lastDate = DateTime.Now.AddMonths(-6);

                        var companySubmissionPeriods = await _context.CompanySubmissionPeriods
                            .Where(x => x.PeriodStart.Date >= request.FinancialYearClosing.FromDate.Date && x.PeriodEnd.Date <= request.FinancialYearClosing.ToDate.Date)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (!companySubmissionPeriods.Any(x => (x.PeriodStart.Year == currentDate.Year && x.PeriodStart.Month == currentDate.Month) || (x.PeriodStart.Year == lastDate.Year && x.PeriodStart.Month == lastDate.Month)))
                        {
                            return new Message
                            {
                                IsAddUpdate = "You can only Re-Open last or current Financial period"
                            };
                        }
                    }
                    else if (financialYearSetting.ClosingType == "Year")
                    {
                        var date = request.FinancialYearClosing.FromDate;
                        var lastYear = DateTime.Now.AddYears(-1);

                        if (date.Year != lastYear.Year && date.Year != DateTime.Now.Year)
                        {
                            return new Message
                            {
                                IsAddUpdate = "You can only Re-Open last or current Financial period"
                            };
                        }
                    }

                    var periods = await _context.CompanySubmissionPeriods.Where(x =>
                        x.PeriodStart.Date >= request.FinancialYearClosing.FromDate.Date &&
                        x.PeriodEnd.Date <= request.FinancialYearClosing.ToDate.Date).ToListAsync(cancellationToken: cancellationToken);

                    foreach (var period in periods)
                    {
                        period.IsPeriodClosed = false;
                    }


                    var yearlyPeriodId = periods.FirstOrDefault()?.YearlyPeriodId;

                    var yearlyPeriod = await _context.YearlyPeriods.FirstOrDefaultAsync(x => x.Id == yearlyPeriodId, cancellationToken: cancellationToken);
                    if (yearlyPeriod != null)
                    {
                        yearlyPeriod.IsYearlyPeriodClosed = false;
                    }

                    // Fetch FinancialYearClosing along with its FinancialYearClosingBalances in one go
                    var financialYearClosing = await _context.FinancialYearClosings
                        .Include(fyc => fyc.FinancialYearClosingBalances)
                        .Where(x => x.FromDate.Date >= request.FinancialYearClosing.FromDate.Date
                                    && x.ToDate.Date <= request.FinancialYearClosing.ToDate.Date
                                    && x.FinancialYearClosingBalances.Any(z => z.ReOpen == 0))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (financialYearClosing != null)
                    {
                        financialYearClosing.ReOpen += 1;

                        foreach (var period in financialYearClosing.FinancialYearClosingBalances)
                        {
                            if (period.ReOpen == 0) // Additional check if needed
                            {
                                period.IsSoftDelete = true;
                                period.ReOpen = financialYearClosing.ReOpen;
                            }
                        }

                    }


                    //await _mediator.Send(new AddUpdateSyncRecordCommand()
                    //{
                    //    SyncRecordModels = _context.SyncLog(),
                    //    BranchId = goodReceive.BranchId,
                    //    Code = _code,
                    //    DocumentNo = goodReceive.RegistrationNo
                    //}, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);

                    return new Message
                    {
                        Id = Guid.NewGuid(),
                        IsAddUpdate = "Status has been changed successfully"
                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
