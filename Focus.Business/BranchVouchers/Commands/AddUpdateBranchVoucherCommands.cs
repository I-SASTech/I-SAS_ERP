using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.BranchVouchers.Models;
using Focus.Business.BranchVouchers.Queries;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.BranchVouchers.Commands
{
    public class AddUpdateBranchVoucherCommands : IRequest<Message>
    {
        public BranchVoucherModel PaymentRefundModel { get; set; }
        public class Handler : IRequestHandler<AddUpdateBranchVoucherCommands, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateBranchVoucherCommands> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateBranchVoucherCommands request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.PaymentRefundModel.Date.Date && x.PeriodEnd >= request.PaymentRefundModel.Date.Date, cancellationToken: cancellationToken);
                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (request.PaymentRefundModel.Id != Guid.Empty)
                    {
                        var paymentVoucher = await _context.BranchVouchers.FirstOrDefaultAsync(x => x.Id == request.PaymentRefundModel.Id, cancellationToken: cancellationToken);
                        if (paymentVoucher == null)
                            throw new NotFoundException("Branch Voucher Not Found", "");


                        paymentVoucher.Date = request.PaymentRefundModel.Date;
                        paymentVoucher.Narration = request.PaymentRefundModel.Narration;
                        paymentVoucher.ChequeNumber = request.PaymentRefundModel.ChequeNumber;
                        paymentVoucher.NatureOfPayment = request.PaymentRefundModel.NatureOfPayment;
                        paymentVoucher.Amount = request.PaymentRefundModel.Amount;
                        paymentVoucher.ApprovalStatus = request.PaymentRefundModel.ApprovalStatus;
                        paymentVoucher.PaymentMode = request.PaymentRefundModel.PaymentMode;
                        paymentVoucher.PaymentMethod = request.PaymentRefundModel.PaymentMethod;
                        paymentVoucher.BankCashAccountId = request.PaymentRefundModel.BankCashAccountId;
                        paymentVoucher.ContactAccountId = request.PaymentRefundModel.ContactAccountId;
                        paymentVoucher.TaxMethod = request.PaymentRefundModel.TaxMethod;
                        paymentVoucher.TaxRateId = request.PaymentRefundModel.TaxRateId;
                        

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        if (request.PaymentRefundModel.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            if (request.PaymentRefundModel.IsSupplier)
                            {
                                var transactions = new List<Transaction>
                                {
                                    // Bank Cash Account
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
                                        BranchId = paymentVoucher.BranchId
                                    },
                                    // Contact Account
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
                                        BranchId = paymentVoucher.BranchId
                                    }
                                };
                                await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }
                            else
                            {
                                var transactions = new List<Transaction>
                                {
                                    // Bank Cash Account
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = paymentVoucher.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = paymentVoucher.Date,
                                        Description = paymentVoucher.Narration,
                                        AccountId = paymentVoucher.BankCashAccountId,
                                        TransactionType = TransactionType.PaymentRefund,
                                        Credit = 0,
                                        Debit = paymentVoucher.Amount,
                                        Year = period.Year,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId
                                    },
                                    // Contact Account
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = paymentVoucher.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = paymentVoucher.Date,
                                        Description = paymentVoucher.Narration,
                                        AccountId = paymentVoucher.ContactAccountId,
                                        TransactionType = TransactionType.PaymentRefund,
                                        Credit = paymentVoucher.Amount,
                                        Debit = 0,
                                        Year = period.Year,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId
                                    }
                                };
                                await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = paymentVoucher.BranchId,
                            Code = _code
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
                    else
                    {
                        var isExistVoucherNumber = await _context.BranchVouchers.AsNoTracking().AnyAsync(x => x.VoucherNumber == request.PaymentRefundModel.VoucherNumber, cancellationToken: cancellationToken);
                        if (isExistVoucherNumber)
                            request.PaymentRefundModel.VoucherNumber = await _mediator.Send(new GetBranchVoucherNumberQuery()
                            {
                                BranchId = request.PaymentRefundModel.BranchId
                            }, cancellationToken);
                        
                        var paymentVoucher = new BranchVoucher
                        {
                            Date = request.PaymentRefundModel.Date,
                            VoucherNumber = request.PaymentRefundModel.VoucherNumber,
                            Narration = request.PaymentRefundModel.Narration,
                            ChequeNumber = request.PaymentRefundModel.ChequeNumber,
                            NatureOfPayment = request.PaymentRefundModel.NatureOfPayment,
                            Amount = request.PaymentRefundModel.Amount,
                            ApprovalStatus = request.PaymentRefundModel.ApprovalStatus,
                            PaymentMode = request.PaymentRefundModel.PaymentMode,
                            PaymentMethod = request.PaymentRefundModel.PaymentMethod,
                            BankCashAccountId = request.PaymentRefundModel.BankCashAccountId,
                            ContactAccountId = request.PaymentRefundModel.ContactAccountId,
                            TaxMethod = request.PaymentRefundModel.TaxMethod,
                            TaxRateId = request.PaymentRefundModel.TaxRateId,
                            BranchId = request.PaymentRefundModel.BranchId,
                        };
                        await _context.BranchVouchers.AddAsync(paymentVoucher, cancellationToken);

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        if (request.PaymentRefundModel.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            if (request.PaymentRefundModel.IsSupplier)
                            {
                                var transactions = new List<Transaction>
                                {
                                    // Bank Cash Account
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
                                        BranchId = paymentVoucher.BranchId
                                    },
                                    // Contact Account
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
                                        BranchId = paymentVoucher.BranchId
                                    }
                                };
                                await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }
                            else
                            {
                                var transactions = new List<Transaction>
                                {
                                    // Bank Cash Account
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = paymentVoucher.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = paymentVoucher.Date,
                                        Description = paymentVoucher.Narration,
                                        AccountId = paymentVoucher.BankCashAccountId,
                                        TransactionType = TransactionType.PaymentRefund,
                                        Credit = 0,
                                        Debit = paymentVoucher.Amount,
                                        Year = period.Year,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId
                                    },
                                    // Contact Account
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = paymentVoucher.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = paymentVoucher.Date,
                                        Description = paymentVoucher.Narration,
                                        AccountId = paymentVoucher.ContactAccountId,
                                        TransactionType = TransactionType.PaymentRefund,
                                        Credit = paymentVoucher.Amount,
                                        Debit = 0,
                                        Year = period.Year,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId
                                    }
                                };
                                await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = paymentVoucher.BranchId,
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
