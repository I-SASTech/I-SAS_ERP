using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList
{
    public class PaymentVoucherDashboard : IRequest<PaymentVoucherLookUpWithCalculation>
    {
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<PaymentVoucherDashboard, PaymentVoucherLookUpWithCalculation>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentVoucherDashboard> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PaymentVoucherLookUpWithCalculation> Handle(PaymentVoucherDashboard request, CancellationToken cancellationToken)
            {
                try
                {
                    var lookUp = new PaymentVoucherLookUpWithCalculation();

                    if (request.PaymentVoucherType == PaymentVoucherType.BankPay)
                    {
                        var query = await _context.PaymentVouchers.AsNoTracking()
                              .Where(x => (x.PaymentVoucherType == PaymentVoucherType.BankPay || x.PaymentVoucherType == PaymentVoucherType.AdvancePay
                                  || x.PaymentVoucherType == PaymentVoucherType.AdvanceExpense)
                                       ).Select(x => new
                                       {
                                           x.Id,
                                           x.ApprovalStatus,
                                           x.PaymentMode,
                                           x.Amount,
                                           x.PaymentVoucherType,
                                           x.NatureOfPayment,
                                           x.BranchId
                                       }).ToListAsync(cancellationToken: cancellationToken);

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        lookUp.Posted = query.Count(x => x.ApprovalStatus == ApprovalStatus.Approved);

                        lookUp.Draft = query.Count(x => x.ApprovalStatus == ApprovalStatus.Draft);

                        lookUp.Rejected = query.Count(x => x.ApprovalStatus == ApprovalStatus.Rejected);

                        lookUp.TotalReceipt = query.Count();
                        lookUp.CashReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Cash);
                        lookUp.TotalCashReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Cash).Sum(x => x.Amount);
                        lookUp.BankReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Bank);
                        lookUp.TotalBankReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Bank).Sum(x => x.Amount);
                        lookUp.TotalCashBank = query.Sum(x => x.Amount);
                        lookUp.Normal = query.Where(x => x.NatureOfPayment == "Normal").Sum(x => x.Amount);
                        lookUp.Advance = query.Where(x => x.NatureOfPayment == "Advance Payment").Sum(x => x.Amount);
                        lookUp.Security = query.Where(x => x.NatureOfPayment == "Security Deposit").Sum(x => x.Amount);
                        lookUp.TotalNormal = query.Where(x => x.NatureOfPayment == "Security Deposit" || x.NatureOfPayment == "Advance Payment" || x.NatureOfPayment == "Normal").Sum(x => x.Amount);
                    }
                    else if (request.PaymentVoucherType == PaymentVoucherType.PettyCash)
                    {
                        var query =await _context.PaymentVouchers.AsNoTracking()
                              .Where(x => (x.PaymentVoucherType == PaymentVoucherType.PettyCash))
                              .Select(x => new
                              {
                                  x.Id,
                                  x.ApprovalStatus,
                                  x.PaymentMode,
                                  x.Amount,
                                  x.PaymentVoucherType,
                                  x.NatureOfPayment,
                                  x.BranchId

                              }).ToListAsync(cancellationToken: cancellationToken);

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        lookUp.Posted = query.Count(x => x.ApprovalStatus == ApprovalStatus.Approved);

                        lookUp.Draft = query.Count(x => x.ApprovalStatus == ApprovalStatus.Draft);

                        lookUp.Rejected = query.Count(x => x.ApprovalStatus == ApprovalStatus.Rejected);

                        lookUp.TotalReceipt = query.Count();
                        lookUp.CashReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Cash);
                        lookUp.TotalCashReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Cash).Sum(x => x.Amount);
                        lookUp.BankReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Bank);
                        lookUp.TotalBankReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Bank).Sum(x => x.Amount);
                        lookUp.TotalCashBank = query.Sum(x => x.Amount);
                        lookUp.Normal = query.Where(x => x.NatureOfPayment == "Normal").Sum(x => x.Amount);
                        lookUp.Advance = query.Where(x => x.NatureOfPayment == "Advance Payment").Sum(x => x.Amount);
                        lookUp.Security = query.Where(x => x.NatureOfPayment == "Security Deposit").Sum(x => x.Amount);
                        lookUp.TotalNormal = query.Where(x => x.NatureOfPayment == "Security Deposit" || x.NatureOfPayment == "Advance Payment" || x.NatureOfPayment == "Normal").Sum(x => x.Amount);
                    }
                    else
                    {
                        var query =await _context.PaymentVouchers.AsNoTracking()
                              .Where(x => (x.PaymentVoucherType == PaymentVoucherType.BankReceipt ||
                                           x.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)).Select(x => new
                                           {
                                               x.Id,
                                               x.ApprovalStatus,
                                               x.PaymentMode,
                                               x.Amount,
                                               x.PaymentVoucherType,
                                               x.NatureOfPayment,
                                               x.BranchId
                                           }).ToListAsync(cancellationToken: cancellationToken);

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        lookUp.Posted = query.Count(x => x.ApprovalStatus == ApprovalStatus.Approved);

                        lookUp.Draft = query.Count(x => x.ApprovalStatus == ApprovalStatus.Draft);

                        lookUp.Rejected = query.Count(x => x.ApprovalStatus == ApprovalStatus.Rejected);

                        lookUp.TotalReceipt = query.Count();
                        lookUp.CashReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Cash);
                        lookUp.TotalCashReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Cash).Sum(x => x.Amount);
                        lookUp.BankReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Bank);
                        lookUp.TotalBankReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Bank).Sum(x => x.Amount);
                        lookUp.TotalCashBank = query.Sum(x => x.Amount);
                        lookUp.Normal = query.Where(x => x.NatureOfPayment == "Normal").Sum(x => x.Amount);
                        lookUp.Advance = query.Where(x => x.NatureOfPayment == "Advance Payment").Sum(x => x.Amount);
                        lookUp.Security = query.Where(x => x.NatureOfPayment == "Security Deposit").Sum(x => x.Amount);
                        lookUp.TotalNormal = query.Where(x => x.NatureOfPayment == "Security Deposit" || x.NatureOfPayment == "Advance Payment" || x.NatureOfPayment == "Normal").Sum(x => x.Amount);

                        var accountId = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Customers Advance");

                        var transactions = _context.Transactions.Where(x => x.AccountId == accountId.Id).Sum(x => x.Debit - x.Credit);

                        lookUp.Advancebalacne = transactions * -1;

                    }

                    
                    return new PaymentVoucherLookUpWithCalculation
                    {
                        Posted = lookUp.Posted,
                        Draft = lookUp.Draft,
                        Rejected = lookUp.Rejected,
                        TotalReceipt = lookUp.TotalReceipt,
                        CashReceipt = lookUp.CashReceipt,
                        TotalCashReceipt = lookUp.TotalCashReceipt,
                        BankReceipt = lookUp.BankReceipt,
                        TotalBankReceipt = lookUp.TotalBankReceipt,
                        TotalCashBank = lookUp.TotalCashBank,
                        Normal = lookUp.Normal,
                        Advance = lookUp.Advance,
                        Security = lookUp.Security,
                        TotalNormal = lookUp.TotalNormal,
                        Advancebalacne =lookUp.Advancebalacne
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
