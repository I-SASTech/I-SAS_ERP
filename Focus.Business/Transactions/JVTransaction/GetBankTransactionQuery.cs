using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Transactions.JVTransaction
{
  public  class GetBankTransactionQuery : IRequest<BankTransactionLookUpModel>
    {
        public Guid AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public class Handler : IRequestHandler<GetBankTransactionQuery, BankTransactionLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetBankTransactionQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<BankTransactionLookUpModel> Handle(GetBankTransactionQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var contact = _context.Transactions.AsNoTracking().Where(x => x.AccountId == request.AccountId)
                        .AsQueryable();
                    var salPayment = await _context.SalePayments.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                    
                    var opening = contact.Where(x => x.Date.Date < request.FromDate.Date).Sum(x=>x.Debit-x.Credit);

                    var runningBalanceList = contact
                        .Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date).AsQueryable();


                    var runningBalance = opening;
                    var bankList = new List<BankLookUpModel>();
                    foreach (var x in runningBalanceList)
                    {
                        if (x.Debit > 0 || x.Credit > 0)
                        {
                            bankList.Add(new BankLookUpModel()
                            {
                                Date = x.Date.ToString("d"),
                                TransactionType = x.TransactionType.ToString(),
                                DocumentNo = x.DocumentNumber,
                                Description = x.Description,
                                Debit = x.Debit,
                                Credit = x.Credit,
                                Amount = runningBalance + (x.Debit - x.Credit),
                                BankName = salPayment.FirstOrDefault(y=>y.SaleId==x.DocumentId)?.Name.ToString(),
                            });
                            runningBalance = runningBalance + (x.Debit - x.Credit);
                        }
                       
                    }
                    return new BankTransactionLookUpModel
                    {
                        BankLook = bankList,
                        Opening = opening,
                        RunningBalance = runningBalance,
                        TotalDebit = bankList.Sum(x=>x.Debit),
                        TotalCredit = bankList.Sum(x=>x.Credit),
                    };

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