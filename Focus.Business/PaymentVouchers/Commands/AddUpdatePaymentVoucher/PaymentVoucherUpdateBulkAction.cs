using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.SaleAmountDetailQuery;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.PaymentVouchers.Commands.AddUpdatePaymentVoucher
{
   public class PaymentVoucherUpdateBulkAction : IRequest<Message>
    {

        public ICollection<Guid> Id { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string UserName { get; set; }

        public class Handler : IRequestHandler<PaymentVoucherUpdateBulkAction, Message>
        {
            public IApplicationDbContext _context { get; set; }
            public ILogger Logger { get; set; }

            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<PaymentVoucherUpdateBulkAction> logger, IMediator mediator)
            {
                _context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(PaymentVoucherUpdateBulkAction request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var transactions = new List<Transaction>();
                    foreach (var id in request.Id)
                    {
                        var paymentVoucher = await _context.PaymentVouchers.
                            FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                        
                        if (paymentVoucher == null)
                            throw new NotFoundException("Not Found", request.Id);

                        paymentVoucher.ApprovalStatus = request.ApprovalStatus;
                        if (request.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            paymentVoucher.ApprovedBy = request.UserName;
                            paymentVoucher.ApprovedDate = DateTime.UtcNow;
                            if (TransactionType.BankPay == (TransactionType)paymentVoucher.PaymentVoucherType || TransactionType.CashPay == (TransactionType)paymentVoucher.PaymentVoucherType)
                            {

                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = paymentVoucher.Date,
                                    Description = paymentVoucher.Narration,
                                    Year = DateTime.UtcNow.ToString("yyyy"),
                                    AccountId = paymentVoucher.BankCashAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Debit = 0M,
                                    Credit = paymentVoucher.Amount,
                                    BranchId = paymentVoucher.BranchId,
                                });
                                transactions.Add(new Domain.Entities.Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = paymentVoucher.Date,
                                    Description = paymentVoucher.Narration,
                                    Year = DateTime.UtcNow.ToString("yyyy"),
                                    AccountId = paymentVoucher.ContactAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Debit = paymentVoucher.Amount,
                                    Credit = 0M,
                                    BranchId = paymentVoucher.BranchId,
                                });

                            }
                            else
                            {
                                transactions.Add(new Domain.Entities.Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = paymentVoucher.Date,
                                    Description = paymentVoucher.Narration,
                                    Year = DateTime.UtcNow.ToString("yyyy"),
                                    AccountId = paymentVoucher.BankCashAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Credit = 0M,
                                    Debit = paymentVoucher.Amount,
                                    BranchId = paymentVoucher.BranchId,
                                });
                                transactions.Add(new Domain.Entities.Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = paymentVoucher.Date,
                                    Description = paymentVoucher.Narration,
                                    Year = DateTime.UtcNow.ToString("yyyy"),
                                    AccountId = paymentVoucher.ContactAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Debit = 0M,
                                    Credit = paymentVoucher.Amount,
                                    BranchId = paymentVoucher.BranchId,
                                });

                            }
                            if (paymentVoucher.SaleInvoice != null)
                            {

                                var sale = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == paymentVoucher.SaleInvoice);
                                if (sale != null)
                                {
                                    var saleAmount = await _mediator.Send(new SaleAmountDetailQuery { Id = sale.Id }, cancellationToken);

                                        if (saleAmount <= paymentVoucher.Amount)
                                        {
                                            sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                            _context.Sales.Update(sale);

                                        }
                                        else if (saleAmount > paymentVoucher.Amount)
                                        {
                                            sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                            _context.Sales.Update(sale);

                                        }
                                }
                            }
                        }
                        else if(request.ApprovalStatus == ApprovalStatus.Rejected)
                        {
                            paymentVoucher.RejectBy = request.UserName;
                            paymentVoucher.RejectDate = DateTime.UtcNow;
                        }
                    }
                    using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                    foreach (var transaction in transactions)
                    {
                        await _context.Transactions.AddAsync(transaction, cancellationToken);
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    //Save changes to database
                    var success = await _context.SaveChangesAsync(cancellationToken);

                    scope.Complete();
                    //await Context.SaveChangesAsync(cancellationToken);
                    if (success > 0)
                    {
                        var message = new Message
                        {
                            Id = Guid.NewGuid(),
                            IsAddUpdate = "Data Updated Successfully"
                        };
                        return message;
                    }
                    else
                    {
                        var message = new Message
                        {
                            Id = Guid.Empty,
                            IsAddUpdate = "Data Not Updated Successfully"
                        };
                        return message;
                    }
                }
                catch (NotFoundException notFoundException)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "This Object not in Database " + notFoundException.Message
                    };
                    return message;
                }
                catch (Exception exception)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                    return message;
                }

            }
        }
    }
}