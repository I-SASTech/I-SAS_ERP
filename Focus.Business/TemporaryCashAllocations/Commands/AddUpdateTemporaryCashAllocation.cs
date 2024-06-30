using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Attachments.Commands;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Guid = System.Guid;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.TemporaryCashAllocations.Commands
{
    public class AddUpdateTemporaryCashAllocation : IRequest<Message>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid BankCashAccountId { get; set; }
        public Guid ContactAccountId { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ChequeNumber { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid UserEmployeeId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AddUpdateTemporaryCashAllocation, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateTemporaryCashAllocation> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateTemporaryCashAllocation request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var period = await _context.CompanySubmissionPeriods
                        .FirstOrDefaultAsync(x => x.PeriodStart <= request.Date.Date
                                                  && x.PeriodEnd >= request.Date.Date, cancellationToken: cancellationToken);

                    if (request.Id != Guid.Empty)
                    {
                        var paymentVoucher = await _context.TemporaryCashAllocations.FindAsync(request.Id);
                        if (paymentVoucher == null)
                            throw new NotFoundException("Voucher Number ", request.VoucherNumber);


                        paymentVoucher.Date = request.Date;
                        paymentVoucher.VoucherNumber = request.VoucherNumber;
                        paymentVoucher.Narration = request.Narration;
                        paymentVoucher.ChequeNumber = request.ChequeNumber;
                        paymentVoucher.ApprovalStatus = request.ApprovalStatus;
                        paymentVoucher.PaymentVoucherType = request.PaymentVoucherType;
                        paymentVoucher.BankCashAccountId = request.BankCashAccountId;
                        paymentVoucher.UserEmployeeId = request.UserEmployeeId;
                        paymentVoucher.Amount = request.Amount;
                        paymentVoucher.ApprovedBy = request.UserName;
                        paymentVoucher.RejectBy = request.UserName;
                        paymentVoucher.DraftBy = request.UserName;
                        paymentVoucher.ApprovedDate = DateTime.UtcNow;
                        paymentVoucher.PaymentMethod = request.PaymentMethod;
                        paymentVoucher.PaymentMode = request.PaymentMode;
                        paymentVoucher.PeriodId = period.Id;
                        paymentVoucher.BranchId = request.BranchId;


                        if (request.AttachmentList != null)
                        {
                            var attachments = _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == paymentVoucher.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }
                            foreach (var item in request.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = paymentVoucher.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }

                        Guid temporaryCashAccount = default;
                        var temporaryCash = await _context.Accounts
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Name == "Temporary Cash" || x.NameArabic == "النقدية المؤقتة", cancellationToken: cancellationToken);

                        if (temporaryCash == null)
                        {
                            var costCenter = await _context.CostCenters
                                    .AsNoTracking()
                                    .Include(x => x.Accounts)
                                    .FirstOrDefaultAsync(x => x.Code == "101000", cancellationToken: cancellationToken);
                            if (costCenter != null)
                            {
                                var newAccount = new Account
                                {
                                    Code = "10100002",
                                    Name = "Temporary Cash",
                                    NameArabic = "النقدية المؤقتة",
                                    Description = "Temporary Cash",
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                temporaryCashAccount = newAccount.Id;
                            }

                        }
                        else
                        {
                            temporaryCashAccount = temporaryCash.Id;
                        }

                        if (request.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var transactions = new List<Transaction>
                                {
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        Description = request.Narration,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        AccountId = request.BankCashAccountId,
                                        TransactionType = TransactionType.TemporaryCashAllocation,
                                        Debit = 0M,
                                        Credit = request.Amount,
                                        PeriodId = period.Id,
                                        BranchId = request.BranchId,
                                    },
                                    new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = DateTime.UtcNow,
                                        Description = request.Narration,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        AccountId = temporaryCashAccount,
                                        TransactionType = TransactionType.TemporaryCashAllocation,
                                        Debit = request.Amount,
                                        Credit = 0M,
                                        PeriodId = period.Id,
                                        BranchId = request.BranchId,
                                    }
                                };
                            foreach (var transaction in transactions)
                            {
                                await _context.Transactions.AddAsync(transaction, cancellationToken);
                            }
                        }

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                           
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        var success = await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        if (success > 0)
                        {
                            var message = new Message
                            {
                                Id = paymentVoucher.Id,
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
                    else
                    {
                        var isExistVoucherNumber =
                            _context.TemporaryCashAllocations.Any(x => x.VoucherNumber == request.VoucherNumber);
                        if (isExistVoucherNumber)
                            throw new ObjectAlreadyExistsException("Payment Voucher Number: " + request.VoucherNumber + " Already Exist");


                        var paymentVoucher = new TemporaryCashAllocation()
                        {
                            Date = request.Date,
                            VoucherNumber = request.VoucherNumber,
                            Narration = request.Narration,
                            ChequeNumber = request.ChequeNumber,
                            ApprovalStatus = request.ApprovalStatus,
                            PaymentVoucherType = request.PaymentVoucherType,
                            BankCashAccountId = request.BankCashAccountId,
                            Amount = request.Amount,
                            UserEmployeeId = request.UserEmployeeId,
                            ApprovedBy = request.UserName,
                            RejectBy = request.UserName,
                            DraftBy = request.UserName,
                            ApprovedDate = DateTime.UtcNow,
                            PaymentMethod = request.PaymentMethod,
                            PaymentMode = request.PaymentMode,
                            PeriodId = period.Id,
                            BranchId = request.BranchId
                        };
                        await _context.TemporaryCashAllocations.AddAsync(paymentVoucher, cancellationToken);

                        if (request.AttachmentList != null && request.AttachmentList.Count > 0)
                        {
                            foreach (var item in request.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = paymentVoucher.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }

                        Guid temporaryCashAccount = default;
                        var temporaryCash = await _context.Accounts
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Name == "Temporary Cash" || x.NameArabic == "النقدية المؤقتة", cancellationToken: cancellationToken);

                        if (temporaryCash == null)
                        {
                            var costCenter = await _context.CostCenters
                                .AsNoTracking()
                                .Include(x => x.Accounts)
                                .FirstOrDefaultAsync(x => x.Code == "101000", cancellationToken: cancellationToken);
                            if (costCenter != null)
                            {
                                var newAccount = new Account
                                {
                                    Code = "10100002",
                                    Name = "Temporary Cash",
                                    NameArabic = "النقدية المؤقتة",
                                    Description = "Temporary Cash",
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                temporaryCashAccount = newAccount.Id;
                            }

                        }
                        else
                        {
                            temporaryCashAccount = temporaryCash.Id;
                        }


                        if (request.ApprovalStatus == ApprovalStatus.Approved)
                        {

                            var transactions = new List<Transaction>
                            {
                                new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = request.VoucherNumber,
                                    Date = DateTime.UtcNow,
                                    Description = request.Narration,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    AccountId = request.BankCashAccountId,
                                    TransactionType = TransactionType.TemporaryCashAllocation,
                                    Debit = 0M,
                                    Credit = request.Amount,
                                    PeriodId = period.Id,
                                    BranchId = request.BranchId
                                },
                                new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = request.VoucherNumber,
                                    Date = DateTime.UtcNow,
                                    Description = request.Narration,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    AccountId = temporaryCashAccount,
                                    TransactionType = TransactionType.TemporaryCashAllocation,
                                    Debit = request.Amount,
                                    Credit = 0M,
                                    PeriodId = period.Id,
                                    BranchId = request.BranchId
                                }
                            };

                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            foreach (var transaction in transactions)
                            {
                                await _context.Transactions.AddAsync(transaction, cancellationToken);
                            }

                            scope.Complete();
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                        
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = paymentVoucher.Id,
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
