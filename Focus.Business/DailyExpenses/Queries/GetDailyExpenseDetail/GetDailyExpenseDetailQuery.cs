using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.DailyExpenses.Queries.GetDailyExpenseDetail
{
   public class GetDailyExpenseDetailQuery : IRequest<DailyExpenseVm>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDailyExpenseDetailQuery, DailyExpenseVm>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDailyExpenseDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DailyExpenseVm> Handle(GetDailyExpenseDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var dailyExpense = await _context.DailyExpenses.FindAsync(request.Id);


                    if (dailyExpense != null)
                    {
                        var poItems = new List<DailyExpenseDetailVm>();
                        foreach (var item in dailyExpense.DailyExpenseDetails)
                        {
                            poItems.Add(new DailyExpenseDetailVm
                            {
                                Id = item.Id,
                                Amount = item.Amount,
                                AccountName = item.ExpenseAccount?.Name,
                                NameArabic = item.ExpenseAccount?.NameArabic,
                                TaxName = item.TaxRate?.Name,
                                ExpenseAccountId = item.ExpenseAccountId,
                                VatId = item.VatId,
                                TaxMethod = item.TaxMethod,
                                InclusiveTotalVat =  item.TaxRate==null?0: 
                                item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ?
                                (item.Amount * item.TaxRate.Rate) / (100 + item.TaxRate.Rate):0,

                                ExclusiveTotalVat =  item.TaxRate==null?0: (item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل") ?
                                        (item.Amount * item.TaxRate.Rate / 100):0,
                               

                               
                                Description = item.Description,
                                DailyExpenseId = dailyExpense.Id

                            });
                        }

                        var attachments = _context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == dailyExpense.Id)
                            .AsQueryable();

                        var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
                        {
                            Id = x.Id,
                            DocumentId = x.DocumentId,
                            Date = x.Date,
                            Description = x.Description,
                            FileName = x.FileName,
                            Path = x.Path
                        }).ToListAsync(cancellationToken: cancellationToken);

                        return new DailyExpenseVm
                        {
                            Id = dailyExpense.Id,
                            ExpenseAttachment = dailyExpense.DailyExpenseAttachments.Select(x =>
                                new DailyExpenseAttachmentLookUpModel()
                                {
                                    Path = x.Path,
                                    Name = x.Name,
                                    Date = x.Date,
                                    Description = x.Description

                                }).ToList(),
                            Date = dailyExpense.Date,
                            IsExpenseAccount = dailyExpense.IsExpenseAccount,
                            AccountName= dailyExpense.Account?.Name,
                            NameArabic = dailyExpense.Account?.NameArabic,
                            ReferenceNo = dailyExpense.ReferenceNo,
                            TaxId = dailyExpense.TaxId,
                            Name = dailyExpense.Name,
                            Description = dailyExpense.Description,
                            VoucherNo = dailyExpense.VoucherNo,
                            Reason=dailyExpense.Reason,
                            ApprovalStatus=dailyExpense.ApprovalStatus,
                            DailyExpenseDetails = poItems,
                            AccountId = dailyExpense.AccountId,
                            BillerAccountId = dailyExpense.BillerAccountId,
                            PaymentMode = dailyExpense.PaymentMode,
                            TotalAmount = dailyExpense.TotalAmount,
                            TotalVat = dailyExpense.TotalVat,
                            GrossAmount = dailyExpense.GrossAmount,
                            AttachmentList = attachmentList,
                            ExpenseDate = dailyExpense.ExpenseDate
                        };
                    }
                    throw new NotFoundException(nameof(dailyExpense), request.Id);
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
