using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrders.AdvancePayment
{
  public  class AdvancePaymentListQuery : IRequest<List<Domain.Entities.PaymentVoucher>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<AdvancePaymentListQuery, List<Domain.Entities.PaymentVoucher>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<AdvancePaymentListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<Domain.Entities.PaymentVoucher>> Handle(AdvancePaymentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PaymentVouchers
                        .AsNoTracking()
                        .AsQueryable();



                    var payment = await query.Select(x =>
                        new Domain.Entities.PaymentVoucher()
                        {
                            Id = x.Id,
                            Date = x.Date,
                            VoucherNumber = x.VoucherNumber,
                            PaymentMode = x.PaymentMode,
                            PaymentMethod = x.PaymentMethod,
                            ChequeNumber = x.ChequeNumber,
                            PaymentVoucherType = x.PaymentVoucherType,
                            BankCashAccountId = x.BankCashAccountId,
                            ContactAccountId = x.ContactAccountId,
                            Narration = x.Narration,
                            ApprovalStatus = x.ApprovalStatus,
                            Amount = x.Amount,
                        }).ToListAsync(cancellationToken: cancellationToken);

                    return payment;


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }

    }
}
