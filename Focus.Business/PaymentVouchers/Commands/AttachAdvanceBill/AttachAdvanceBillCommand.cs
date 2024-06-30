using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PaymentVouchers.Commands.AttachAdvanceBill
{
    public class AttachAdvanceBillCommand : IRequest<Message>
    {
        public Guid BillsId { get; set; }
        public Guid PurchaseId { get; set; }
        public bool IsInvoice { get; set; }

        public class Handler : IRequestHandler<AttachAdvanceBillCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AttachAdvanceBillCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AttachAdvanceBillCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var bill = await _context.PurchaseBills.FindAsync(request.BillsId);
                    if (bill==null)
                    {
                        throw new NotFoundException("Bill Not Found", "");
                    }

                    if (request.IsInvoice)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var purchasePostExpense = new PurchasePostExpense()
                        {
                            Date = DateTime.UtcNow,
                            VoucherNumber = bill.RegistrationNo,
                            ExpenseType = "Bill",
                            BillId = bill.Id,
                            ChequeNumber = "",
                            Amount = bill.TotalAmount,
                            Narration = bill.RegistrationNo + "Bill Attachment",
                            BankCashAccountId = null,
                            ContactAccountId = null,
                            PurchasePostId = request.PurchaseId,
                            PaymentVoucherId = null,
                            VatAccountId = null,
                            TaxMethod = "",
                            TaxRateId = null,
                        };
                        await _context.PurchasePostExpenses.AddAsync(purchasePostExpense, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = purchasePostExpense.Id,
                            IsAddUpdate = "Data Added Successfully"
                        };
                        return message;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var purchasePostExpense = new PurchaseOrderExpense()
                        {
                            Date = DateTime.UtcNow,
                            VoucherNumber = bill.RegistrationNo,
                            //ExpenseType = "Bill",
                            BillId = bill.Id,
                            ChequeNumber = "",
                            Amount = bill.TotalAmount,
                            Narration = bill.RegistrationNo + "Bill Attachment",
                            BankCashAccountId = null,
                            ContactAccountId = null,
                            PurchaseOrderId = request.PurchaseId,
                            PaymentVoucherId = null,
                            VatAccountId = null,
                            TaxMethod = "",
                            TaxRateId = null,
                        };
                        await _context.PurchaseOrderExpenses.AddAsync(purchasePostExpense, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = purchasePostExpense.Id,
                            IsAddUpdate = "Data Added Successfully"
                        };
                        return message;
                    }

                    


                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
