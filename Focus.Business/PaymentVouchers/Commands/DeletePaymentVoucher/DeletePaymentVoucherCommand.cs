using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PaymentVouchers.Commands.DeletePaymentVoucher
{
    public class DeletePaymentVoucherCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeletePaymentVoucherCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<DeletePaymentVoucherCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(DeletePaymentVoucherCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var paymentVoucher = await _context.PaymentVouchers.FindAsync(request.Id);

                    _context.PaymentVouchers.Remove(paymentVoucher);

                    var paymentVoucherDetails = _context.PaymentVoucherDetails.FirstOrDefault(x=> x.PaymentVoucherId == request.Id);

                    _context.PaymentVoucherDetails.Remove(paymentVoucherDetails);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    var message = new Message
                    {
                        Id = request.Id,
                        IsDeleted = "Data has been deleted successfully"
                    };
                    return message;
                }
               
                catch (Exception exception)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsDeleted = "Some Error Occurred /n " + exception.Message
                    };
                    _logger.LogError(exception.Message);

                    return message;
                }
            }
        }
    }
}
