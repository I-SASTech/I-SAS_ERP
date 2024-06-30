using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.FinancialYearTemporaryClosing.Models;
using Focus.Business.Interface;
using Focus.Business.JournalVouchers.Commands.AddUpdateJournalVoucher;
using Focus.Business.JournalVouchers.Queries.GetJournalVoucherNumber;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.FinancialYearTemporaryClosing.Commands
{
    public class AddFinancialYearClosingBalanceCommand : IRequest<Message>
    {
        public FinancialYearClosingLookUpModel FinancialYearClosing { get; set; }
        public class Handler : IRequestHandler<AddFinancialYearClosingBalanceCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddFinancialYearClosingBalanceCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            
            public async Task<Message> Handle(AddFinancialYearClosingBalanceCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Check Previous Period is closed
                    var lastPeriodDate = request.FinancialYearClosing.FromDate.AddDays(-1);
                    var isClose = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x =>
                        x.PeriodStart.Year == lastPeriodDate.Year && x.PeriodStart.Month == lastPeriodDate.Month, cancellationToken: cancellationToken);

                    if (isClose is { IsPeriodClosed: false })
                    {
                        throw new ApplicationException("Close Previous Period First");
                    }

                    var financialYearClosingDetail = await _context.FinancialYearClosings
                        .AsNoTracking()
                        .Where(x => x.FromDate.Date >= request.FinancialYearClosing.FromDate.Date
                                    && x.ToDate.Date <= request.FinancialYearClosing.ToDate.Date)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (financialYearClosingDetail != null)
                    {
                        financialYearClosingDetail.Date = DateTime.Now;
                        financialYearClosingDetail.Description = request.FinancialYearClosing.Description;

                        if (request.FinancialYearClosing.CostCenterList != null)
                        {
                            var financialYearClosingBalances = new List<FinancialYearClosingBalance>();
                            foreach (var costCenter in request.FinancialYearClosing.CostCenterList)
                            {
                                var list = costCenter.AccountList.Select(x =>
                                    new FinancialYearClosingBalance()
                                    {
                                        Date = DateTime.Now,
                                        Debit = x.Debit,
                                        Credit = x.Credit,
                                        Balance = x.Balance,
                                        AccountId = x.AccountId,
                                        FinancialYearClosingId = financialYearClosingDetail.Id
                                    }).ToList();
                                financialYearClosingBalances.AddRange(list);
                            }
                            
                            await _context.FinancialYearClosingBalances.AddRangeAsync(financialYearClosingBalances, cancellationToken);

                            var periods = await _context.CompanySubmissionPeriods.Where(x =>
                                x.PeriodStart.Date >= request.FinancialYearClosing.FromDate.Date &&
                                x.PeriodEnd.Date <= request.FinancialYearClosing.ToDate.Date).ToListAsync(cancellationToken: cancellationToken);
                            foreach (var period in periods)
                            {
                                period.IsPeriodClosed = true;
                            }

                            await _context.SaveChangesAsync(cancellationToken);


                            var yearlyPeriodId = periods.FirstOrDefault()?.YearlyPeriodId;

                            var isAllClose = await _context.CompanySubmissionPeriods.AnyAsync(x =>
                                x.YearlyPeriodId == yearlyPeriodId && !x.IsPeriodClosed, cancellationToken: cancellationToken);

                            if (!isAllClose)
                            {
                                var yearlyPeriod = await _context.YearlyPeriods.FirstOrDefaultAsync(x => x.Id == yearlyPeriodId, cancellationToken: cancellationToken);
                                if (yearlyPeriod != null)
                                {
                                    yearlyPeriod.IsYearlyPeriodClosed = true;

                                    //Create Opening Cash Document
                                    var jvNumber = await _mediator.Send(new GetJournalVoucherNumberQuery
                                    {
                                        formName = "OpeningCash",
                                        //BranchId = branchId

                                    }, cancellationToken);

                                    var journalVoucherItem = new List<JournalVoucherItem>();
                                    foreach (var item in financialYearClosingBalances)
                                    {
                                        var jvItem = new JournalVoucherItem
                                        {
                                            Id = item.Id,
                                            Debit = item.Debit,
                                            Credit = item.Credit,
                                            AccountId = item.AccountId,
                                            PaymentMode = SalePaymentType.Default,
                                            PaymentMethod = PaymentMethod.Default,
                                        };
                                        journalVoucherItem.Add(jvItem);
                                    }

                                    var message = await _mediator.Send(new AddUpdateJournalVoucherCommand
                                    {
                                        Id = Guid.Empty,
                                        VoucherNumber = jvNumber,
                                        Date = DateTime.Now,
                                        OpeningCash = true,
                                        JournalVoucherItems = journalVoucherItem,
                                        ApprovalStatus = ApprovalStatus.Draft,
                                        //BranchId = journalVoucher.BranchId,
                                    }, cancellationToken);
                                }
                            }
                        }
                    }
                    else
                    {
                        var financialYearClosing = new FinancialYearClosing
                        {
                            Date = DateTime.Now,
                            PeriodName = request.FinancialYearClosing.PeriodName,
                            FromDate = request.FinancialYearClosing.FromDate,
                            ToDate = request.FinancialYearClosing.ToDate,
                            Description = request.FinancialYearClosing.Description,
                        };
                        await _context.FinancialYearClosings.AddAsync(financialYearClosing, cancellationToken);


                        if (request.FinancialYearClosing.CostCenterList != null)
                        {
                            var financialYearClosingBalances = new List<FinancialYearClosingBalance>();
                            foreach (var costCenter in request.FinancialYearClosing.CostCenterList)
                            {
                                var list = costCenter.AccountList.Select(x =>
                                    new FinancialYearClosingBalance()
                                    {
                                        Date = DateTime.Now,
                                        Debit = x.Debit,
                                        Credit = x.Credit,
                                        Balance = x.Balance,
                                        AccountId = x.AccountId,
                                        FinancialYearClosingId = financialYearClosing.Id
                                    }).ToList();
                                financialYearClosingBalances.AddRange(list);
                            }
                            

                            await _context.FinancialYearClosingBalances.AddRangeAsync(financialYearClosingBalances, cancellationToken);

                            var periods = await _context.CompanySubmissionPeriods.Where(x =>
                                x.PeriodStart.Date >= request.FinancialYearClosing.FromDate.Date &&
                                x.PeriodEnd.Date <= request.FinancialYearClosing.ToDate.Date).ToListAsync(cancellationToken: cancellationToken);
                            foreach (var period in periods)
                            {
                                period.IsPeriodClosed = true;
                            }

                            await _context.SaveChangesAsync(cancellationToken);


                            var yearlyPeriodId = periods.FirstOrDefault()?.YearlyPeriodId;

                            var isAllClose = await _context.CompanySubmissionPeriods.AnyAsync(x =>
                                x.YearlyPeriodId == yearlyPeriodId && !x.IsPeriodClosed, cancellationToken: cancellationToken);

                            if (!isAllClose)
                            {
                                var yearlyPeriod = await _context.YearlyPeriods.FirstOrDefaultAsync(x => x.Id == yearlyPeriodId, cancellationToken: cancellationToken);
                                if (yearlyPeriod != null)
                                {
                                    yearlyPeriod.IsYearlyPeriodClosed = true;

                                    //Create Opening Cash Document
                                    var jvNumber = await _mediator.Send(new GetJournalVoucherNumberQuery
                                    {
                                        formName = "OpeningCash",
                                        //BranchId = branchId

                                    }, cancellationToken);

                                    var journalVoucherItem = new List<JournalVoucherItem>();
                                    foreach (var item in financialYearClosingBalances)
                                    {
                                        var jvItem = new JournalVoucherItem
                                        {
                                            Id = item.Id,
                                            Debit = item.Debit,
                                            Credit = item.Credit,
                                            AccountId = item.AccountId,
                                            PaymentMode = SalePaymentType.Default,
                                            PaymentMethod = PaymentMethod.Default,
                                        };
                                        journalVoucherItem.Add(jvItem);
                                    }

                                    var message = await _mediator.Send(new AddUpdateJournalVoucherCommand
                                    {
                                        Id = Guid.Empty,
                                        VoucherNumber = jvNumber,
                                        Date = DateTime.Now,
                                        OpeningCash = true,
                                        JournalVoucherItems = journalVoucherItem,
                                        ApprovalStatus = ApprovalStatus.Draft,
                                        //BranchId = journalVoucher.BranchId,
                                    }, cancellationToken);
                                }
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
                        IsAddUpdate = "Data has been added successfully"
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
