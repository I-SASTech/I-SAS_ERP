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

namespace Focus.Business.Transactions.JVTransaction
{
    public class GetTransactionQueryForLedger : PagedRequest, IRequest<AccountLedgerReportLookUp>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid AccountID { get; set; }
        public bool isLedger { get; set; }
        public string DateType { get; set; }

        public class Handler : IRequestHandler<GetTransactionQueryForLedger, AccountLedgerReportLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetTransactionQueryForLedger> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<AccountLedgerReportLookUp> Handle(GetTransactionQueryForLedger request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isLedger)
                    {
                        var query = _context.Transactions
                            .Include(x => x.Account)
                            .AsNoTracking()
                            .Where(x=>x.AccountId==request.AccountID)
                            .AsQueryable();

                        var opening = query.Where(x => x.Date.Date < request.FromDate.Date)
                            .Sum(x => x.Debit - x.Credit);

                        //if(request.DateType== "Document Date")
                        //{
                        //    query = query.Where(x => x.DocumentDate >= request.FromDate.Date && x.DocumentDate <= request.ToDate.Date);
                        //    query = query.OrderBy(x => x.DocumentDate);
                        //} 
                        //else if(request.DateType== "Approval Date")
                        //{
                        //    query = query.Where(x => x.ApprovalDate >= request.FromDate.Date && x.ApprovalDate <= request.ToDate.Date);
                        //    query = query.OrderBy(x => x.ApprovalDate);
                        //}
                        //else if(request.DateType== "Transaction Date")
                        //{
                        //    query = query.Where(x => x.Date >= request.FromDate.Date && x.Date <= request.ToDate.Date);
                        //    query = query.OrderBy(x => x.Date);
                        //}
                       
                            query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                            query = query.OrderBy(x => x.Date);
                     
                        
                        var result = new List<TransactionDto>();
                        decimal runningBalance = opening;
                        foreach (var tr in query)
                        {
                            if (tr.Debit>0 || tr.Credit>0)
                            {
                                result.Add(new TransactionDto()
                                {
                                    Id = tr.Id,
                                    AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                    DocumentId = tr.DocumentId,
                                    Date = tr.Date.ToString("d"),
                                    DocumentDate = tr.DocumentDate?.ToString("d"),
                                    ApprovalDate = tr.ApprovalDate?.ToString("d"),
                                    DocumentNumber = tr.DocumentNumber,
                                    Description = tr.Description == "" || tr.Description ==null ? tr.TransactionType.ToString() : tr.Description,
                                    DebitAmount = tr.Debit,
                                    CreditAmount = tr.Credit,
                                    TransactionType = tr.TransactionType.ToString(),
                                    OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                                }); 
                               
                                runningBalance = runningBalance + (tr.Debit - tr.Credit);
                            }
                            
                        }
                        

                        return new AccountLedgerReportLookUp
                        {
                            TransactionList = result,
                            TotalDebit = result.Sum(x=>x.DebitAmount),
                            TotalCredit = result.Sum(x=>x.CreditAmount),
                            OpeningBalance = opening,
                            RunningBalance = runningBalance
                        };
                    }

                    return null;
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
