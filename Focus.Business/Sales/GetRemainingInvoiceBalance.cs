using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Contacts.Queries.GetContactDetails;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales
{
    public class GetRemainingInvoiceBalance : IRequest<decimal>
    {
        public Guid InvoiceId { get; set; }

        public class Handler : IRequestHandler<GetRemainingInvoiceBalance, decimal>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetContactDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<decimal> Handle(GetRemainingInvoiceBalance request, CancellationToken cancellationToken)
            {
                try
                {

                    var paymentVoucherAmount =  await _context.PaymentVouchers
                                                .Where(x => (x.SaleInvoice == request.InvoiceId 
                                                       || x.PurchaseInvoice == request.InvoiceId 
                                                       || x.DocumentId == request.InvoiceId) 
                                                       && x.ApprovalStatus == ApprovalStatus.Approved
                                                      )
                                                .SumAsync(x => x.Amount, cancellationToken: cancellationToken);

                    decimal remainingAmount = 0;

                    var paymentVoucherItems = await _context.PaymentVouchers.Include(x => x.PaymentVoucherItems).Where(x => x.PurchaseInvoice == null || x.SaleInvoice == null).ToListAsync();
                    decimal total = 0;
                    if (paymentVoucherItems.Count > 0)
                    {
                        foreach (var item in paymentVoucherItems)
                        {
                            var pur = item.PaymentVoucherItems.Where(x => x.PurchaseInvoiceId == request.InvoiceId || x.SaleInvoiceId == request.InvoiceId).Sum(x => x.PayAmount);
                            total = total + pur;
                        }
                    }

                    remainingAmount = paymentVoucherAmount + total;

                    return remainingAmount;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
