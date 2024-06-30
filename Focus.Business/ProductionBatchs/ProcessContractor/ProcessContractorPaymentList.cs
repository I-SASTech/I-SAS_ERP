using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ProductionBatchs.ProcessContractor
{
   public class ProcessContractorPaymentList : IRequest<List<Domain.Entities.ContractorPayment>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<ProcessContractorPaymentList, List<Domain.Entities.ContractorPayment>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ProcessContractorPaymentList> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<Domain.Entities.ContractorPayment>> Handle(ProcessContractorPaymentList request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.ContractorPayments
                        .AsNoTracking()
                        .Include(x=>x.PaymentVoucher)
                        .Where(x => x.BatchProcessContractorId == request.Id)
                        .AsQueryable();


                    query = query.OrderBy(x => x.Id);

                    var payment = await query.Select(x =>
                        new Domain.Entities.ContractorPayment()
                        {
                            Id = x.Id,
                            PaymentVoucherId = x.PaymentVoucherId,
                            Date = x.Date,
                            Narration = x.Narration,
                            PaymentMode = x.PaymentMode,
                            Amount = x.Amount,
                            VoucherNumber=x.VoucherNumber
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
