using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Transactions.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.CustomerAdvancesReport
{
    public class GetCustomerAdvancesQuery : PagedRequest, IRequest<CustomerAdavanceReportLookUp>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? AccountId { get; set; }
        public string DocumentName { get; set; }
        public class Handler : IRequestHandler<GetCustomerAdvancesQuery, CustomerAdavanceReportLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCustomerAdvancesQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<CustomerAdavanceReportLookUp> Handle(GetCustomerAdvancesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                        var lookUp = new CustomerAdavanceReportLookUp();
                        var advanceAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == (request.DocumentName == "CustomerAdvances" ? "Customers Advance" : "Supplier Advance"));
                        if (advanceAccount != null || advanceAccount?.Id != Guid.Empty)
                        {
                            var transactions = await _context.Transactions
                                .AsNoTracking()
                                .Where(x => x.AccountId == advanceAccount.Id && x.ContactId == request.AccountId && (x.TransactionType == Domain.Enum.TransactionType.AdvanceBankPay || x.TransactionType == Domain.Enum.TransactionType.AdvanceBankReceipt || x.TransactionType == Domain.Enum.TransactionType.AdvanceCashPay || x.TransactionType == Domain.Enum.TransactionType.AdvanceCashReceipt)).ToListAsync();
                            var opningBalance = transactions.Where(x => x.Date.Date < request.FromDate.Date).Sum(x => x.Debit - x.Credit);
                            var runningBalance = transactions.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date).ToList();
                            var closingBalace = transactions.Where(x => x.Date.Date >= request.ToDate.Date).Sum(x => x.Debit - x.Credit);
                            var accountInfo = await _context.Contacts.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.AccountId == request.AccountId, cancellationToken: cancellationToken);
                            var currentbalance = 0;
                            var transactionDtos = runningBalance.Select(x => new TransactionDto()
                            {
                                Id = x.Id,
                                AccountName = x.Account != null ? x.Account.Code + "--" + x.Account.Name : "N/A",
                                DocumentId = x.DocumentId,
                                DocumentDate = x.DocumentDate == null ? string.Empty : x.DocumentDate.Value.ToString("d"),
                                Date = x.Date.ToString("d"),
                                DocumentNumber = x.DocumentNumber,
                                Description = x.Description,
                                DebitAmount = x.Debit,
                                CreditAmount = x.Credit,
                                TransactionType = x.TransactionType.ToString(),
                                OpeningBalance = currentbalance + (x.Debit - x.Credit),
                            }).ToList();
                            return new CustomerAdavanceReportLookUp
                            {
                                Transactions = transactionDtos,
                                OpeningBalance = opningBalance,
                                RunningBalance = currentbalance,
                                ClosingBalance = closingBalace,


                                CustomerNameEn = accountInfo?.EnglishName,
                                CustomerNameAr = accountInfo?.ArabicName,
                                Address = accountInfo?.Address,
                                CustomerNo = accountInfo?.Code,
                                CustomerVat = accountInfo?.VatNo,
                                PhoneNumber = accountInfo?.ContactNo1

                            };

                        }
                    

                    return new CustomerAdavanceReportLookUp();
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
