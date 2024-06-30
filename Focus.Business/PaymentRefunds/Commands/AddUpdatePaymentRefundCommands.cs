using Focus.Business.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;
using Focus.Business.PaymentRefunds.Models;
using Focus.Business.PaymentRefunds.Queries;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.PaymentRefunds.Commands
{
    public class AddUpdatePaymentRefundCommands : IRequest<Message>
    {
        public PaymentRefundModel PaymentRefundModel { get; set; }
        public class Handler : IRequestHandler<AddUpdatePaymentRefundCommands, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdatePaymentRefundCommands> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdatePaymentRefundCommands request, CancellationToken cancellationToken)
            {
                try
                {
                    var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.PaymentRefundModel.Date.Date && x.PeriodEnd >= request.PaymentRefundModel.Date.Date, cancellationToken: cancellationToken);
                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (request.PaymentRefundModel.Id != Guid.Empty)
                    {
                        return new Message
                        {
                            //Id = paymentVoucher.Id,
                            IsAddUpdate = "Data Added Successfully"
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        //var isExistVoucherNumber = await _context.PaymentRefunds.AnyAsync(x => x.VoucherNumber == request.PaymentRefundModel.VoucherNumber, cancellationToken: cancellationToken);
                        //if (isExistVoucherNumber)
                        //    throw new ObjectAlreadyExistsException("Voucher Number: " + request.PaymentRefundModel.VoucherNumber + " Already Exist");

                        request.PaymentRefundModel.VoucherNumber = await _mediator.Send(new GetPaymentRefundNumberQuery(), cancellationToken);

                        var paymentVoucher = new PaymentRefund
                        {
                            PaymentVoucherId = request.PaymentRefundModel.PaymentVoucherId,
                            Date = request.PaymentRefundModel.Date,
                            VoucherNumber = request.PaymentRefundModel.VoucherNumber,
                            Narration = request.PaymentRefundModel.Narration,
                            ChequeNumber = request.PaymentRefundModel.ChequeNumber,
                            NatureOfPayment = request.PaymentRefundModel.NatureOfPayment,
                            Amount = request.PaymentRefundModel.Amount,
                            ApprovalStatus = request.PaymentRefundModel.ApprovalStatus,
                            PaymentMode = request.PaymentRefundModel.PaymentMode,
                            PaymentMethod = request.PaymentRefundModel.PaymentMethod,
                            PurchaseInvoice = request.PaymentRefundModel.PurchaseInvoice,
                            SaleInvoice = request.PaymentRefundModel.SaleInvoice,
                            BankCashAccountId = request.PaymentRefundModel.BankCashAccountId,
                            ContactAccountId = request.PaymentRefundModel.ContactAccountId,
                            TaxMethod = request.PaymentRefundModel.TaxMethod,
                            TaxRateId = request.PaymentRefundModel.TaxRateId,

                        };
                        await _context.PaymentRefunds.AddAsync(paymentVoucher, cancellationToken);

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        Guid? branchId = null;
                        if (request.PaymentRefundModel.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            if (paymentVoucher.PaymentVoucherId != null)
                            {
                                var voucher = await _context.PaymentVouchers.FindAsync(paymentVoucher.PaymentVoucherId);
                                if (voucher != null)
                                {
                                    voucher.IsRefund = true;
                                    //paymentVoucher.BranchId = voucher.BranchId;
                                    branchId= voucher.BranchId;
                                }

                                var transactions = new List<Transaction>
                                {
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = paymentVoucher.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = paymentVoucher.Date,
                                        Description = paymentVoucher.Narration,
                                        AccountId = paymentVoucher.BankCashAccountId,
                                        TransactionType = TransactionType.PaymentRefund,
                                        Credit = paymentVoucher.Amount,
                                        Debit = 0M,
                                        Year = period.Year,
                                        PeriodId = period.Id,
                                        BranchId = voucher?.BranchId,
                                    },
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = paymentVoucher.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = paymentVoucher.Date,
                                        Description = paymentVoucher.Narration,
                                        AccountId = paymentVoucher.ContactAccountId,
                                        TransactionType = TransactionType.PaymentRefund,
                                        Credit = 0M,
                                        Debit = paymentVoucher.Amount,
                                        Year = period.Year,
                                        PeriodId = period.Id,
                                        BranchId = voucher?.BranchId,
                                    }
                                };

                                await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }
                            
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = branchId,
                            Code = _code,
                        }, cancellationToken);
                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);
                        scope.Complete();

                        return new Message
                        {
                            Id = paymentVoucher.Id,
                            IsAddUpdate = "Data Added Successfully"
                        };

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
