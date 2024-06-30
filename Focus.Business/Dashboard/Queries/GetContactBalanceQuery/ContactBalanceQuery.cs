using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Transactions.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Dashboard.Queries.GetContactBalanceQuery
{
    public class ContactBalanceQuery : PagedRequest, IRequest<ContactLookUpModel>
    {
        public Guid AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsReport { get; set; }
        public string BranchId { get; set; }


        public class Handler : IRequestHandler<ContactBalanceQuery, ContactLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ContactBalanceQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<ContactLookUpModel> Handle(ContactBalanceQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var contact = await _context.Transactions
                        .AsNoTracking()
                        .Include(x => x.Account)
                        .Where(y => y.AccountId == request.AccountId)
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        contact = contact.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    var openingBalance = contact.Where(x => x.Date.Date < request.FromDate.Date)
                        .Sum(x => x.Debit - x.Credit);

                    var runningBalanceList = contact
                        .OrderBy(x => x.Date)
                        .Where(x => x.Date.Date >= request.FromDate && x.Date.Date <= request.ToDate.Date).AsQueryable();

                    var customer = await _context.Contacts.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.AccountId == request.AccountId, cancellationToken: cancellationToken);

                    var result = new List<TransactionDto>();
                    decimal runningBalance = openingBalance;
                    foreach (var tr in runningBalanceList)
                    {
                        if (tr.Debit > 0 || tr.Credit > 0)
                        {
                            result.Add(new TransactionDto()
                            {
                                Id = tr.Id,
                                AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                DocumentId = tr.DocumentId,
                                DocumentDate = tr.DocumentDate == null ? string.Empty : tr.DocumentDate.Value.ToString("d"),
                                Date = tr.Date.ToString("d"),
                                DocumentNumber = tr.DocumentNumber,
                                Description = tr.Description,
                                DebitAmount = tr.Debit,
                                CreditAmount = tr.Credit,
                                TransactionType = tr.TransactionType.ToString(),
                                OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                            });
                            runningBalance = runningBalance + (tr.Debit - tr.Credit);
                        }

                    }

                    return new ContactLookUpModel
                    {
                        Transactions = result,
                        OpeningBalance = openingBalance,
                        RunningBalance = runningBalance,
                        TotalDebit = result.Sum(x => x.DebitAmount),
                        TotalCredit = result.Sum(x => x.CreditAmount),

                        CustomerNameEn = customer?.EnglishName,
                        CustomerNameAr = customer?.ArabicName,
                        Address = customer?.Address,
                        CustomerNo = customer?.Code,
                        CustomerVat = customer?.VatNo,
                        PhoneNumber = customer?.ContactNo1,
                        TotalBalance = result.Sum(x => x.DebitAmount - x.CreditAmount),
                    };
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
