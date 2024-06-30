using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PaymentOptions.Queries.GetPaymentOptionsDetails
{
    public class GetPaymentOptionsDetailQuery : IRequest<PaymentOptionsDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPaymentOptionsDetailQuery, PaymentOptionsDetailsLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetPaymentOptionsDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PaymentOptionsDetailsLookUpModel> Handle(GetPaymentOptionsDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var paymentOptions = await _context.PaymentOptions.FindAsync(request.Id);

                    if (paymentOptions != null)
                    {
                        return PaymentOptionsDetailsLookUpModel.Create(paymentOptions);
                    }
                    throw new NotFoundException(nameof(paymentOptions), request.Id);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw;
                }

            }
        }
    }
}
