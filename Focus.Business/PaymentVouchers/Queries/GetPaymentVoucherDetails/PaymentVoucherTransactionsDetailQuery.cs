using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherDetails
{
    public class PaymentVoucherTransactionsDetailQuery : IRequest<List<PaymentVoucherLookUpModel>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<PaymentVoucherTransactionsDetailQuery, List<PaymentVoucherLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentVoucherTransactionsDetailQuery> logger)
            {
                _context = context;
                Logger = logger;
            }

            public async Task<List<PaymentVoucherLookUpModel>> Handle(PaymentVoucherTransactionsDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //var paymentVoucherTransactionDetailQuery =
                    //    (
                    //         from tr in _context.Transactions
                    //         join acc in _context.Accounts on tr.AccountId equals acc.Id
                    //         join pv in _context.PaymentVouchers on tr.DocumentId equals pv.Id
                    //         where tr.DocumentId == request.Id
                    //         select new PaymentVoucherLookUpModel
                    //         {
                    //             Id = tr.Id,
                    //             Date = pv.Date,
                    //             VoucherNumber = pv.VoucherNumber,
                    //             VoucherType = tr.Description,
                    //             DetailDate = tr.Date,
                    //             AccountName = acc.Code + "--" + acc.Name,
                    //             DetailDescription = tr.Description,
                    //             Debit = tr.Debit,
                    //             Credit = tr.Credit,
                    //             ChequeNumber = pv.ChequeNumber
                    //         }
                    //    ).ToList();

                    //return paymentVoucherTransactionDetailQuery;
                    return null;
                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}