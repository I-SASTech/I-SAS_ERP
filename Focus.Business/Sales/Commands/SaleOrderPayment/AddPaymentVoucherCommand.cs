using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherNumber;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.Sales.Commands.SaleOrderPayment
{
    public class AddPaymentVoucherCommand : IRequest<Message>
    {
        public Guid Id { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public Guid ContactAccountId { get; set; }
        public string UserName { get; set; }
        public bool IsPurchase { get; set; }
        public bool IsSaleOrder { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public ICollection<Domain.Entities.SaleOrderPayment> SaleOrderPayments { get; set; }
        public decimal Payment { get; set; }
        public decimal DueAmount { get; set; }
        public Guid? ContactId { get; set; }
        public Guid ContactAdvanceAccountId { get; set; }
        public bool IsCredit { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public SalePaymentType PaymentType { get; set; }
        public bool CashInvoice { get; set; }
        public ICollection<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AddPaymentVoucherCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;

            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddPaymentVoucherCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddPaymentVoucherCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (!request.IsPurchase)
                    {
                        var pvNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery { PaymentVoucherType = PaymentVoucherType.BankReceipt }, cancellationToken);

                        decimal amount;
                        if (!request.IsCredit)
                        {
                            if (request.CashInvoice)
                            {
                                var paymentVoucher = new PaymentVoucher
                                {
                                    Date = DateTime.UtcNow,
                                    VoucherNumber = pvNumber,
                                    Narration = "Narration",
                                    ChequeNumber = "",
                                    ApprovalStatus = ApprovalStatus.Approved,
                                    PaymentVoucherType = PaymentVoucherType.BankReceipt,
                                    BankCashAccountId = request.BankCashAccountId ?? Guid.Empty,
                                    ContactAccountId = request.ContactAccountId,
                                    Amount = request.DueAmount,
                                    PettyCash = 0,
                                    SaleInvoice = request.SaleInvoice,
                                    PurchaseInvoice = request.PurchaseInvoice,
                                    ApprovedBy = request.UserName,
                                    ApprovedDate = DateTime.UtcNow,
                                    PaymentMethod = PaymentMethod.Transfer,
                                    PaymentMode = request.PaymentType,
                                    BranchId = request.BranchId,
                                };
                                await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);
                                var transactions = new List<Transaction>
                            {
                                new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = pvNumber,
                                    Date = DateTime.UtcNow,
                                    Description = "Bank Account",
                                    Year = DateTime.UtcNow.Year.ToString("yyyy"),
                                    AccountId = request.BankCashAccountId?? Guid.Empty,
                                    TransactionType = TransactionType.BankReceipt,
                                    Credit = 0M,
                                    Debit = request.DueAmount,
                                    BranchId = request.BranchId,
                                },
                                new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = pvNumber,
                                    Date = DateTime.UtcNow,
                                    Description = "Contact Account",
                                    Year = DateTime.UtcNow.Year.ToString("yyyy"),
                                    AccountId = request.ContactAccountId,
                                    TransactionType = TransactionType.BankReceipt,
                                    Debit = 0M,
                                    Credit = request.DueAmount,
                                    BranchId = request.BranchId,
                                }
                            };
                                using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                                foreach (var transaction in transactions)
                                {
                                    await _context.Transactions.AddAsync(transaction, cancellationToken);
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    DocumentNo = paymentVoucher.VoucherNumber,
                                    Code = _code,
                                }, cancellationToken);

                                //Save changes to database
                                await _context.SaveChangesAsync(cancellationToken);

                                scope.Complete();
                            }
                            else
                            {
                                var totalPay = request.SaleOrderPayments.Sum(x => x.Amount);
                                amount = request.DueAmount + (totalPay - request.Payment);
                                var paymentVoucher = new PaymentVoucher
                                {
                                    Date = DateTime.UtcNow,
                                    VoucherNumber = pvNumber,
                                    Narration = "Narration",
                                    ChequeNumber = "",
                                    ApprovalStatus = ApprovalStatus.Approved,
                                    PaymentVoucherType = PaymentVoucherType.BankReceipt,
                                    BankCashAccountId = request.BankCashAccountId ?? Guid.Empty,
                                    ContactAccountId = request.ContactAccountId,
                                    Amount = amount,
                                    PettyCash = 0,
                                    SaleInvoice = request.SaleInvoice,
                                    PurchaseInvoice = request.PurchaseInvoice,
                                    ApprovedBy = request.UserName,
                                    ApprovedDate = DateTime.UtcNow,
                                    PaymentMethod = PaymentMethod.Transfer,
                                    PaymentMode = request.PaymentType,
                                    BranchId = request.BranchId,
                                };
                                await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);
                                var transactions = new List<Transaction>
                            {
                                new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = pvNumber,
                                    Date = DateTime.UtcNow,
                                    DocumentDate =DateTime.UtcNow,
                                    ApprovalDate = DateTime.UtcNow,
                                    Description = "Bank Account",
                                    Year = DateTime.UtcNow.Year.ToString("yyyy"),
                                    AccountId = request.BankCashAccountId?? Guid.Empty,
                                    TransactionType = TransactionType.BankReceipt,
                                    Credit = 0M,
                                    Debit = amount,
                                    BranchId = request.BranchId,
                                },
                                new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = pvNumber,
                                    Date = DateTime.UtcNow,
                                    DocumentDate =DateTime.UtcNow,
                                    ApprovalDate = DateTime.UtcNow,
                                    Description = "Contact Account",
                                    Year = DateTime.UtcNow.Year.ToString("yyyy"),
                                    AccountId = request.ContactAccountId,
                                    TransactionType = TransactionType.BankReceipt,
                                    Debit = 0M,
                                    Credit = amount,
                                    BranchId = request.BranchId,
                                }
                            };
                                using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                                foreach (var transaction in transactions)
                                {
                                    await _context.Transactions.AddAsync(transaction, cancellationToken);
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    DocumentNo = paymentVoucher.VoucherNumber,
                                    Code = _code,
                                }, cancellationToken);

                                //Save changes to database
                                await _context.SaveChangesAsync(cancellationToken);

                                scope.Complete();
                            }
                        }
                        else
                        {
                            var totalPay = request.SaleOrderPayments.Sum(x => x.Amount);
                            var result = request.DueAmount - (totalPay - request.Payment);
                            if (result <= 0)
                            {
                                amount = Math.Abs(request.DueAmount);
                            }
                            else if (totalPay - request.Payment > 0)
                            {
                                amount = Math.Abs(totalPay - request.Payment);
                            }
                            else
                            {
                                amount = Math.Abs(totalPay - request.Payment);
                            }
                            var paymentVoucher = new PaymentVoucher
                            {
                                Date = DateTime.UtcNow,
                                VoucherNumber = pvNumber,
                                Narration = "Narration",
                                ChequeNumber = "",
                                ApprovalStatus = ApprovalStatus.Approved,
                                PaymentVoucherType = PaymentVoucherType.BankReceipt,
                                BankCashAccountId = request.ContactAdvanceAccountId,
                                ContactAccountId = request.ContactAccountId,
                                Amount = amount,
                                PettyCash = 0,
                                SaleInvoice = request.SaleInvoice,
                                PurchaseInvoice = request.PurchaseInvoice,
                                ApprovedBy = request.UserName,
                                ApprovedDate = DateTime.UtcNow,
                                PaymentMethod = PaymentMethod.Transfer,
                                PaymentMode = SalePaymentType.Advance,
                                BranchId = request.BranchId,
                            };
                            await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);

                            var transactions = new List<Transaction>
                    {
                        new Transaction()
                        {
                            DocumentId = paymentVoucher.Id,
                            DocumentNumber = pvNumber,
                            Date = DateTime.UtcNow,
                            DocumentDate =DateTime.UtcNow,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank Account",
                            Year = DateTime.UtcNow.Year.ToString("yyyy"),
                            AccountId = request.ContactAdvanceAccountId,
                            TransactionType = TransactionType.BankReceipt,
                            Credit = 0M,
                            Debit = amount,
                            BranchId = request.BranchId,
                        },
                        new Transaction()
                        {
                            DocumentId = paymentVoucher.Id,
                            DocumentNumber = pvNumber,
                            Date = DateTime.UtcNow,
                            DocumentDate =DateTime.UtcNow,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Contact Account",
                            Year = DateTime.UtcNow.Year.ToString("yyyy"),
                            AccountId = request.ContactAccountId,
                            TransactionType = TransactionType.BankReceipt,
                            Debit = 0M,
                            Credit = amount,
                            BranchId = request.BranchId,
                        }
                    };


                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            foreach (var transaction in transactions)
                            {
                                await _context.Transactions.AddAsync(transaction, cancellationToken);
                            }
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                DocumentNo = paymentVoucher.VoucherNumber
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            scope.Complete();
                        }
                    }
                    else
                    {
                        var pvNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery { PaymentVoucherType = PaymentVoucherType.BankPay }, cancellationToken);
                        decimal amount;
                        var totalPay = request.PurchaseOrderPayments.Sum(x => x.Amount);
                        var result = request.DueAmount - (totalPay - request.Payment);
                        if (result <= 0)
                        {
                            amount = Math.Abs(request.DueAmount);
                        }
                        else if (totalPay - request.Payment > 0)
                        {
                            amount = Math.Abs(totalPay - request.Payment);
                        }
                        else
                        {
                            amount = Math.Abs(totalPay - request.Payment);
                        }
                        var paymentVoucher = new PaymentVoucher
                        {
                            Date = DateTime.UtcNow,
                            VoucherNumber = pvNumber,
                            Narration = "Narration",
                            ChequeNumber = "",
                            ApprovalStatus = ApprovalStatus.Approved,
                            PaymentVoucherType = PaymentVoucherType.BankPay,
                            BankCashAccountId = request.ContactAdvanceAccountId,
                            ContactAccountId = request.ContactAccountId,
                            Amount = amount,
                            PettyCash = 0,
                            SaleInvoice = request.SaleInvoice,
                            PurchaseInvoice = request.PurchaseInvoice,
                            ApprovedBy = request.UserName,
                            ApprovedDate = DateTime.UtcNow,
                            PaymentMethod = PaymentMethod.Transfer,
                            PaymentMode = SalePaymentType.Advance,
                            BranchId = request.BranchId,
                        };
                        await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);

                        var transactions = new List<Transaction>
                    {
                        new Transaction()
                        {
                            DocumentId = paymentVoucher.Id,
                            DocumentNumber = pvNumber,
                            Date = DateTime.UtcNow,
                            DocumentDate =DateTime.UtcNow,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank Account",
                            Year = DateTime.UtcNow.Year.ToString("yyyy"),
                            AccountId = request.ContactAdvanceAccountId,
                            TransactionType = TransactionType.BankPay,
                            Debit = 0M,
                            Credit = amount,
                            BranchId = request.BranchId,
                        },
                        new Transaction()
                        {
                            DocumentId = paymentVoucher.Id,
                            DocumentNumber = pvNumber,
                            Date = DateTime.UtcNow,
                            DocumentDate =DateTime.UtcNow,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Contact Account",
                            Year = DateTime.UtcNow.Year.ToString("yyyy"),
                            AccountId = request.ContactAccountId,
                            TransactionType = TransactionType.BankPay,
                            Debit = amount,
                            Credit = 0M,
                            BranchId = request.BranchId,
                        }
                    };


                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        foreach (var transaction in transactions)
                        {
                            await _context.Transactions.AddAsync(transaction, cancellationToken);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo = paymentVoucher.VoucherNumber
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();
                    }


                    var message = new Message
                    {
                        IsAddUpdate = "Data Added Successfully"
                    };
                    return message;

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
