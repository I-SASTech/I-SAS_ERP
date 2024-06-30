using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchVouchers.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.BranchVouchers.Queries
{
    public class GetBranchVoucherDetailQuery : IRequest<BranchVoucherModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBranchVoucherDetailQuery, BranchVoucherModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBranchVoucherDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<BranchVoucherModel> Handle(GetBranchVoucherDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var paymentRefund = await _context.BranchVouchers
                        .Select(x => new BranchVoucherModel
                        {
                            Id = x.Id,
                            Date = x.Date,
                            VoucherNumber = x.VoucherNumber,
                            Narration = x.Narration,
                            ChequeNumber = x.ChequeNumber,
                            NatureOfPayment = x.NatureOfPayment,
                            Amount = x.Amount,
                            ApprovalStatus = x.ApprovalStatus,
                            PaymentMode = x.PaymentMode,
                            PaymentMethod = x.PaymentMethod,
                            BankCashAccountId = x.BankCashAccountId,
                            ContactAccountId = x.ContactAccountId,
                            TaxMethod = x.TaxMethod,
                            TaxRateId = x.TaxRateId,
                        }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (paymentRefund == null)
                        throw new NotFoundException("Payment Refund", "was not found");


                    return paymentRefund;
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
