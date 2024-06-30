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
using Focus.Business.PurchaseOrders.Commands;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;
using Focus.Business.Sales;
using Focus.Business.Sales.Queries.SaleAmountDetailQuery;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.PaymentVouchers.Commands.AddUpdatePaymentVoucher
{
    public class AddUpdatePaymentVoucherCommand : IRequest<Message>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public PettyCash? PettyCash { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid BankCashAccountId { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public Guid? ReparingOrderId { get; set; }
        public Guid ContactAccountId { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Path { get; set; }
        public string ChequeNumber { get; set; }
        public bool IsPurchase { get; set; }
        public bool IsView { get; set; }
        public bool IsSaleOrder { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public bool IsPurchaseOrderExpense { get; set; }
        public Guid? ExpenseTypeId { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? VatAccountId { get; set; }
        public Guid? BillsId { get; set; }
        public bool IsPurchasePostExpense { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<PaymentVoucherItemLookUp> PaymentVoucherItems { get; set; }
        public List<Guid?> MultipleDocument { get; set; }

        public Guid? TemporaryCashIssueId { get; set; }
        public Guid? BatchProcessContractorId { get; set; }
        public bool IsContractor { get; set; }
        public bool FromCreditInvoice { get; set; }
        public string TransactionId { get; set; }
        public string NatureOfPayment { get; set; }
        public string Prefix { get; set; }
        public Guid? BranchId { get; set; }
        public string DocumentType { get; set; }
        public Guid? DocumentId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public bool PremiumAdvance { get; set; }
        public class Handler : IRequestHandler<AddUpdatePaymentVoucherCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdatePaymentVoucherCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdatePaymentVoucherCommand request, CancellationToken cancellationToken)
            {
                var newAccount = new Account();
                var vatAccount = new Account();

                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.Date.Year && x.PeriodStart.Month == request.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    

                    if (request.Id != Guid.Empty)
                    {

                        var paymentVoucher = await _context.PaymentVouchers.FindAsync(request.Id);
                        if (paymentVoucher == null)
                            throw new NotFoundException("Voucher Number ", request.VoucherNumber);

                        if (request.IsView)
                        {
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
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = request.BranchId,
                                DocumentNo = request.VoucherNumber,
                            }, cancellationToken);

                            var success = await _context.SaveChangesAsync(cancellationToken);
                            if (success > 0)
                            {
                                var message = new Message
                                {
                                    Id = paymentVoucher.Id,
                                    IsAddUpdate = "Status Reject Updated Successfully"
                                };
                                return message;
                            }
                            else
                            {
                                var message = new Message
                                {
                                    Id = Guid.Empty,
                                    IsAddUpdate = "Status Reject Not Updated Successfully"
                                };
                                return message;
                            }
                        }


                        if (request.PaymentVoucherItems != null)
                        {
                            var attachments = _context.PaymentVoucherItems
                                .AsNoTracking()
                                .Where(x => x.PaymentVoucherId == paymentVoucher.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.PaymentVoucherItems.RemoveRange(attachments);
                            }
                            foreach (var item in request.PaymentVoucherItems)
                            {
                                {
                                    var attachment = new PaymentVoucherItem()
                                    {
                                        PaymentVoucherId = paymentVoucher.Id,
                                        Description = item.Description,
                                        PartiallyInvoices = item.PartiallyInvoices,
                                        Amount = item.Amount,
                                        ExtraAmount = item.ExtraAmount,
                                        PayAmount = item.PayAmount,
                                        Total = item.Total,
                                        PurchaseInvoiceId = item.PurchaseInvoiceId,
                                        SaleInvoiceId = item.SaleInvoiceId,
                                        ChequeNumber = item.ChequeNumber,
                                        Date = System.DateTime.Now,
                                        IsActive = item.IsActive,
                                        IsAging = item.IsAging,
                                        DueAmount = item.DueAmount
                                    };
                                    //Add Attachments to database
                                    await _context.PaymentVoucherItems.AddAsync(attachment, cancellationToken);
                                }
                            }
                        }


                        if (paymentVoucher.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var transactions = await _context.Transactions.AsNoTracking().Where(x => x.DocumentId == paymentVoucher.Id).ToListAsync(cancellationToken: cancellationToken);
                            _context.Transactions.RemoveRange(transactions);
                        }

                        if (request.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            paymentVoucher.Date = request.Date;
                            paymentVoucher.VoucherNumber = request.VoucherNumber;
                            paymentVoucher.Narration = request.Narration;
                            paymentVoucher.TransactionId = request.TransactionId;
                            paymentVoucher.NatureOfPayment = request.NatureOfPayment;
                            paymentVoucher.Prefix = request.Prefix;
                            paymentVoucher.ChequeNumber = request.ChequeNumber;
                            paymentVoucher.ApprovalStatus = request.ApprovalStatus;
                            paymentVoucher.PaymentVoucherType = request.PaymentVoucherType;
                            paymentVoucher.BankCashAccountId = request.BankCashAccountId;
                            paymentVoucher.ContactAccountId = request.ContactAccountId;
                            paymentVoucher.Amount = request.Amount;
                            paymentVoucher.SaleInvoice = request.SaleInvoice;
                            paymentVoucher.PurchaseInvoice = request.PurchaseInvoice;
                            paymentVoucher.ApprovedBy = request.UserName;
                            paymentVoucher.PettyCash = request.PettyCash ?? 0;
                            paymentVoucher.ApprovedDate = DateTime.UtcNow;
                            paymentVoucher.PaymentMethod = request.PaymentMethod;
                            paymentVoucher.PaymentMode = request.PaymentMode;
                            paymentVoucher.TaxMethod = request.TaxMethod;
                            paymentVoucher.TaxRateId = request.TaxRateId;
                            paymentVoucher.BillsId = request.BillsId;
                            paymentVoucher.PeriodId = period.Id;
                            paymentVoucher.DocumentType = request.DocumentType;
                            paymentVoucher.DocumentId = request.DocumentId;
                            paymentVoucher.PaymentDate = request.PaymentDate;
                            paymentVoucher.InvoiceAmount = request.InvoiceAmount;
                            paymentVoucher.RemainingBalance = request.RemainingBalance;


                            request.ApprovalDate = DateTime.UtcNow;

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
                            var accounts = _context.Accounts.AsNoTracking().Include(x => x.CostCenter).AsQueryable();
                            var vats = _context.TaxRates.AsNoTracking().AsQueryable();
                            var transactions = new List<Transaction>();
                            {
                                var accountType = await _context.AccountTypes.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Name == "Expenses" || x.NameArabic == "المصاريف", cancellationToken: cancellationToken);
                                var costCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "610000");
                                if (costCenter == null)
                                {
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "610000",
                                        Name = "Expense Adjustment",
                                        NameArabic = "تعديل المصاريف",
                                        Description = "Expense Adjustment",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);
                                    newAccount = new Account
                                    {
                                        Code = "61000001",
                                        Name = "Expense Adjustment",
                                        NameArabic = "تعديل المصاريف",
                                        Description = "Expense Adjustment",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                }
                                var vatExpense = accounts.FirstOrDefault(x => x.Code == "1300002");
                                if (vatExpense == null)
                                {
                                    var expenseCostCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "130000");
                                    vatAccount = new Account
                                    {
                                        Code = "1300002",
                                        Name = "VAT Paid On Expense",
                                        NameArabic = "دفع ضريبة القيمة المضافة على المصاريف",
                                        Description = "VAT Paid On Expense",
                                        IsActive = true,
                                        CostCenterId = expenseCostCenter.CostCenterId
                                    };
                                    await _context.Accounts.AddAsync(vatAccount, cancellationToken);
                                }



                            }

                            if (TransactionType.BankPay == (TransactionType)request.PaymentVoucherType || TransactionType.CashPay == (TransactionType)request.PaymentVoucherType || TransactionType.PettyCash == (TransactionType)request.PaymentVoucherType)
                            {
                                TransactionType transactionType;
                                if (request.PaymentMode == SalePaymentType.Cash && request.PaymentVoucherType == PaymentVoucherType.AdvancePay)
                                    transactionType = TransactionType.AdvanceCashPay;
                                else if (request.PaymentMode == SalePaymentType.Bank && request.PaymentVoucherType == PaymentVoucherType.AdvancePay)
                                    transactionType = TransactionType.AdvanceBankPay;
                                else if (request.PaymentVoucherType == PaymentVoucherType.AdvanceExpense)
                                    transactionType = TransactionType.AdvanceExpense;
                                else if (request.PaymentMode == SalePaymentType.Cash)
                                    transactionType = TransactionType.CashPay;
                                else if (request.PaymentMode == SalePaymentType.Bank)
                                    transactionType = TransactionType.BankPay;
                                else if (request.PaymentMode == SalePaymentType.Advance)
                                    transactionType = TransactionType.AdvancePay;
                                else
                                {
                                    transactionType = TransactionType.CashPay;
                                }

                                var list = new List<Transaction>();
                                if (request.IsPurchaseOrderExpense || request.IsPurchasePostExpense)
                                {
                                    var vatIds = vats.FirstOrDefault(x => x.Id == request.TaxRateId);
                                    if (request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل")
                                    {
                                        if (vatIds == null)
                                        {
                                            throw new NotFoundException("You Select Tax Method but not Select VAT", null);
                                        }




                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId : null,

                                        });
                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });


                                    }
                                    else if (request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل")
                                    {
                                        if (vatIds == null)
                                        {
                                            throw new NotFoundException("You Select Tax Method but not Select VAT", null);
                                        }

                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = request.Amount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                        });
                                        list.Add(new Transaction
                                        {

                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = request.Amount,
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        list.Add(new Transaction
                                        {

                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = Math.Round((request.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                            Credit = 0m,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = Math.Round((request.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                    }
                                    else
                                    {
                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0M,
                                            Credit = request.Amount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                        });
                                        list.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = request.Amount,
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                    }

                                }
                                else
                                {
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.BankCashAccountId,
                                        TransactionType = transactionType,
                                        Debit = 0M,
                                        Credit = request.Amount,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                        ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                    });
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.ContactAccountId,
                                        TransactionType = transactionType,
                                        Debit = request.Amount,
                                        Credit = 0M,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                    });
                                }
                            }
                            else
                            {
                                TransactionType transactionType;
                                if (request.PaymentMode == SalePaymentType.Cash && request.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)
                                    transactionType = TransactionType.AdvanceCashReceipt;
                                else if (request.PaymentMode == SalePaymentType.Bank && request.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)
                                    transactionType = TransactionType.AdvanceBankReceipt;
                                else if (request.PaymentMode == SalePaymentType.Cash && request.PaymentVoucherType == PaymentVoucherType.PettyCash)
                                    transactionType = TransactionType.PettyCash;
                                else if (request.PaymentMode == SalePaymentType.Cash)
                                    transactionType = TransactionType.CashReceipt;
                                else if (request.PaymentMode == SalePaymentType.Bank)
                                    transactionType = TransactionType.BankReceipt;
                                else if (request.PaymentMode == SalePaymentType.Advance)
                                    transactionType = TransactionType.AdvanceReceipt;
                                else
                                {
                                    transactionType = TransactionType.CashReceipt;
                                }
                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = request.VoucherNumber,
                                    Date = request.PaymentDate,
                                    DocumentDate = request.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    Description = request.Narration,
                                    Year = period.Year,
                                    AccountId = request.BankCashAccountId,
                                    TransactionType = transactionType,
                                    Credit = 0M,
                                    Debit = request.Amount,
                                    PeriodId = period.Id,
                                    BranchId = paymentVoucher.BranchId,
                                    ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                });
                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = request.VoucherNumber,
                                    Date = request.PaymentDate,
                                    DocumentDate = request.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    Description = request.Narration,
                                    Year = period.Year,
                                    AccountId = request.ContactAccountId,
                                    TransactionType = transactionType,
                                    Debit = 0M,
                                    Credit = request.Amount,
                                    PeriodId = period.Id,
                                    BranchId = paymentVoucher.BranchId,
                                });

                            }
                            if (request.PaymentVoucherItems != null)
                            {
                                if (request.PaymentVoucherItems.Count > 0)
                                {
                                    foreach (var item in request.PaymentVoucherItems)
                                    {
                                        if (item.IsActive && item.SaleInvoiceId != null)
                                        {
                                            var sale = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == item.SaleInvoiceId);
                                            if (sale != null)
                                            {
                                                //var saleAmount = sale.TotalAmount;
                                                //var paidAmount =  _context.PaymentVoucherItems.Where(x =>x.SaleInvoiceId == sale.Id).Sum(x=>x.PayAmount);




                                                if (item.Total == 0)
                                                {
                                                    sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                                    _context.Sales.Update(sale);
                                                }
                                                else if (item.Total > 0)
                                                {
                                                    sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                                    _context.Sales.Update(sale);
                                                }

                                                var creditPayment = await _context.SalePayments.FirstOrDefaultAsync(x => x.SaleId == item.SaleInvoiceId && x.Name == "Credit", cancellationToken: cancellationToken);
                                                if (creditPayment != null)
                                                {
                                                    creditPayment.DueAmount -= item.PayAmount;
                                                    creditPayment.Received -= item.PayAmount;
                                                    _context.SalePayments.Update(creditPayment);
                                                }
                                                var salePayment = new SalePayment
                                                {
                                                    DueAmount = item.PayAmount,
                                                    Received = item.PayAmount,
                                                    Balance = 0,
                                                    Change = 0,
                                                    PaymentType = request.PaymentMode,
                                                    BankCashAccountId = request.BankCashAccountId,
                                                    Name = request.PaymentMode == SalePaymentType.Cash ? "Cash" : "Bank",
                                                    SaleId = sale.Id,
                                                    Rate = 0,
                                                    Amount = 0
                                                };
                                                _context.SalePayments.Add(salePayment);
                                            }

                                        }
                                        else if (item.IsActive && item.PurchaseInvoiceId != null)
                                        {
                                            var purchase = _context.PurchasePosts.AsNoTracking().FirstOrDefault(x => x.Id == item.PurchaseInvoiceId);
                                            if (purchase != null)
                                            {




                                                if (item.Total == 0)
                                                {
                                                    purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                                                    purchase.ReceivedAmount = purchase.ReceivedAmount + item.PayAmount;
                                                    _context.PurchasePosts.Update(purchase);
                                                }
                                                else if (item.Total > 0)
                                                {
                                                    purchase.PartiallyInvoices = PartiallyInvoices.Partially;
                                                    purchase.ReceivedAmount = purchase.ReceivedAmount + item.PayAmount;
                                                    _context.PurchasePosts.Update(purchase);
                                                }


                                            }

                                        }


                                    }


                                }
                            }

                            else if (request.SaleInvoice != Guid.Empty && request.SaleInvoice != null)
                            {
                                var sale = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == request.SaleInvoice);
                                if (sale != null)
                                {
                                    var saleAmount = await _mediator.Send(new SaleAmountDetailQuery { Id = sale.Id }, cancellationToken);
                                    var paidAmount = await _context.PaymentVouchers.Where(x => (x.SaleInvoice == request.SaleInvoice.Value || x.PurchaseInvoice == request.SaleInvoice.Value) && x.Id != paymentVoucher.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount, cancellationToken: cancellationToken);


                                    if (saleAmount <= request.Amount + paidAmount)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                        _context.Sales.Update(sale);

                                    }
                                    else if (saleAmount > request.Amount + paidAmount)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                        _context.Sales.Update(sale);

                                    }


                                    var creditPayment = await _context.SalePayments.FirstOrDefaultAsync(x => x.SaleId == sale.Id && x.Name == "Credit", cancellationToken: cancellationToken);
                                    if (creditPayment != null)
                                    {
                                        creditPayment.DueAmount -= request.Amount;
                                        creditPayment.Received -= request.Amount;
                                        _context.SalePayments.Update(creditPayment);
                                    }
                                    var salePayment = new SalePayment
                                    {
                                        DueAmount = request.Amount,
                                        Received = request.Amount,
                                        Balance = 0,
                                        Change = 0,
                                        PaymentType = request.PaymentMode,
                                        BankCashAccountId = request.BankCashAccountId,
                                        Name = request.PaymentMode == SalePaymentType.Cash ? "Cash" : "Bank",
                                        SaleId = sale.Id,
                                        Rate = 0,
                                        Amount = 0
                                    };
                                    _context.SalePayments.Add(salePayment);
                                }


                            }

                            if (request.PurchaseInvoice != Guid.Empty && request.PurchaseInvoice != null)
                            {

                                var purchase = _context.PurchasePosts.AsNoTracking().FirstOrDefault(x => x.Id == request.PurchaseInvoice);

                                if (purchase != null)
                                {

                                    var purchaseAmount = await _mediator.Send(new PurchasePostDetailQuery { Id = purchase.Id }, cancellationToken);
                                    var paidAmount = await _context.PaymentVouchers.Where(x => (x.SaleInvoice == request.PurchaseInvoice.Value || x.PurchaseInvoice == request.PurchaseInvoice.Value) && x.Id != paymentVoucher.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount, cancellationToken: cancellationToken);


                                    if (purchaseAmount != null)
                                    {
                                        if (purchaseAmount.NetAmount <= request.Amount + paidAmount)
                                        {
                                            purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                                            _context.PurchasePosts.Update(purchase);
                                        }
                                        else if (purchaseAmount.NetAmount > request.Amount + paidAmount)
                                        {
                                            purchase.PartiallyInvoices = PartiallyInvoices.Partially;
                                            _context.PurchasePosts.Update(purchase);
                                        }
                                    }
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
                                DocumentNo = request.VoucherNumber,
                                BranchId = request.BranchId,
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
                                    Id = paymentVoucher.Id,
                                    IsAddUpdate = "Data Updated Successfully"
                                };
                                return message;
                            }
                        }

                        else if (request.ApprovalStatus == ApprovalStatus.Rejected)
                        {
                            paymentVoucher.ApprovalStatus = request.ApprovalStatus;
                            paymentVoucher.RejectBy = request.UserName;
                            paymentVoucher.RejectDate = DateTime.UtcNow;

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

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                DocumentNo = request.VoucherNumber,
                                BranchId = request.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            var success = await _context.SaveChangesAsync(cancellationToken);
                            if (success > 0)
                            {
                                var message = new Message
                                {
                                    Id = paymentVoucher.Id,
                                    IsAddUpdate = "Status Reject Updated Successfully"
                                };
                                return message;
                            }
                            else
                            {
                                var message = new Message
                                {
                                    Id = Guid.Empty,
                                    IsAddUpdate = "Status Reject Not Updated Successfully"
                                };
                                return message;
                            }
                        }
                        else if (request.ApprovalStatus == ApprovalStatus.Void)
                        {
                            paymentVoucher.VoidBy = request.UserName;
                            paymentVoucher.VoidDate = DateTime.UtcNow;
                            paymentVoucher.ApprovalStatus = request.ApprovalStatus;

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

                            var transactions = new List<Transaction>();

                            if (TransactionType.BankPay == (TransactionType)paymentVoucher.PaymentVoucherType || TransactionType.CashPay == (TransactionType)paymentVoucher.PaymentVoucherType || TransactionType.PettyCash == (TransactionType)request.PaymentVoucherType)
                            {

                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = request.PaymentDate,
                                    DocumentDate = request.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    Description = paymentVoucher.Narration,
                                    Year = paymentVoucher.Date.Year.ToString("yyyy"),
                                    AccountId = paymentVoucher.BankCashAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Debit = paymentVoucher.Amount,
                                    Credit = 0M,
                                    PeriodId = period.Id,
                                    BranchId = paymentVoucher.BranchId,
                                });
                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = request.PaymentDate,
                                    DocumentDate = request.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    Description = paymentVoucher.Narration,
                                    Year = paymentVoucher.Date.Year.ToString("yyyy"),
                                    AccountId = paymentVoucher.ContactAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Debit = 0M,
                                    Credit = paymentVoucher.Amount,
                                    PeriodId = period.Id,
                                    BranchId = paymentVoucher.BranchId,
                                });

                            }
                            else
                            {
                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = request.PaymentDate,
                                    DocumentDate = request.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    Description = paymentVoucher.Narration,
                                    Year = paymentVoucher.Date.Year.ToString("yyyy"),
                                    AccountId = paymentVoucher.BankCashAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Credit = paymentVoucher.Amount,
                                    Debit = 0M,
                                    PeriodId = period.Id,
                                    BranchId = paymentVoucher.BranchId,
                                    ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                });
                                transactions.Add(new Transaction()
                                {
                                    DocumentId = paymentVoucher.Id,
                                    DocumentNumber = paymentVoucher.VoucherNumber,
                                    Date = request.PaymentDate,
                                    DocumentDate = request.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    Description = paymentVoucher.Narration,
                                    Year = paymentVoucher.Date.Year.ToString("yyyy"),
                                    AccountId = paymentVoucher.ContactAccountId,
                                    TransactionType = (TransactionType)paymentVoucher.PaymentVoucherType,
                                    Debit = paymentVoucher.Amount,
                                    Credit = 0M,
                                    PeriodId = period.Id,
                                    BranchId = paymentVoucher.BranchId,
                                });

                            }
                            if (paymentVoucher.SaleInvoice != Guid.Empty || paymentVoucher.SaleInvoice != null)
                            {

                                var sale = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == paymentVoucher.SaleInvoice);
                                if (sale != null)
                                {


                                    var saleAmount = await _mediator.Send(new SaleAmountDetailQuery { Id = sale.Id }, cancellationToken);
                                    var paidAmount = await _mediator.Send(
                                        new GetRemainingInvoiceBalance
                                        {
                                            InvoiceId = request.SaleInvoice.Value
                                        }, cancellationToken);

                                    if (saleAmount <= paymentVoucher.Amount + paidAmount)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.UnPaid;
                                        _context.Sales.Update(sale);

                                    }
                                    else if (saleAmount > paymentVoucher.Amount)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                        _context.Sales.Update(sale);

                                    }
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
                                DocumentNo = request.VoucherNumber,
                                BranchId = request.BranchId,
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
                                    IsAddUpdate = "Status Void Updated Successfully"
                                };
                                return message;
                            }
                            else
                            {
                                var message = new Message
                                {
                                    Id = Guid.Empty,
                                    IsAddUpdate = "Status Void Not Updated Successfully"
                                };
                                return message;
                            }
                        }
                        else
                        {
                            paymentVoucher.Date = request.Date;
                            paymentVoucher.VoucherNumber = request.VoucherNumber;
                            paymentVoucher.Narration = request.Narration;
                            paymentVoucher.TransactionId = request.TransactionId;
                            paymentVoucher.NatureOfPayment = request.NatureOfPayment;
                            paymentVoucher.Prefix = request.Prefix;
                            paymentVoucher.PettyCash = request.PettyCash == null ? 0 : request.PettyCash.Value;
                            paymentVoucher.ChequeNumber = request.ChequeNumber;
                            paymentVoucher.ApprovalStatus = request.ApprovalStatus;
                            paymentVoucher.PaymentVoucherType = request.PaymentVoucherType;
                            paymentVoucher.BankCashAccountId = request.BankCashAccountId;
                            paymentVoucher.ContactAccountId = request.ContactAccountId;
                            paymentVoucher.Amount = request.Amount;
                            paymentVoucher.SaleInvoice = request.SaleInvoice;
                            paymentVoucher.PurchaseInvoice = request.PurchaseInvoice;
                            paymentVoucher.DraftBy = request.UserName;
                            paymentVoucher.DraftDate = DateTime.UtcNow;
                            paymentVoucher.PaymentMethod = request.PaymentMethod;
                            paymentVoucher.PaymentMode = request.PaymentMode;
                            paymentVoucher.BillsId = request.BillsId;
                            paymentVoucher.DocumentType = request.DocumentType;
                            paymentVoucher.DocumentId = request.DocumentId;
                            paymentVoucher.PaymentDate = request.PaymentDate;
                            paymentVoucher.InvoiceAmount = request.InvoiceAmount;
                            paymentVoucher.RemainingBalance = request.RemainingBalance;


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

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                DocumentNo = request.VoucherNumber,
                                BranchId = request.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            var success = await _context.SaveChangesAsync(cancellationToken);
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
                                    Id = paymentVoucher.Id,
                                    IsAddUpdate = "Data Updated Successfully"
                                };
                                return message;
                            }
                        }
                    }
                    else
                    {
                        var isExistVoucherNumber = _context.PaymentVouchers.Any(x => x.VoucherNumber == request.VoucherNumber);
                        if (isExistVoucherNumber)
                            throw new ObjectAlreadyExistsException("Payment Voucher Number: " + request.VoucherNumber + " Already Exist");


                        if (request.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            request.ApprovalDate = DateTime.UtcNow;
                            var paymentVoucher = new PaymentVoucher
                            {
                                Date = request.Date,
                                VoucherNumber = request.VoucherNumber,
                                Narration = request.Narration,
                                TransactionId = request.TransactionId,
                                NatureOfPayment = request.NatureOfPayment,
                                Prefix = request.Prefix,
                                ChequeNumber = request.ChequeNumber,
                                ApprovalStatus = request.ApprovalStatus,
                                PaymentVoucherType = request.PaymentVoucherType,
                                BankCashAccountId = request.BankCashAccountId,
                                ContactAccountId = request.ContactAccountId,
                                Amount = request.Amount,
                                PettyCash = request.PettyCash ?? 0,
                                SaleInvoice = request.SaleInvoice,
                                PurchaseInvoice = request.PurchaseInvoice,
                                ApprovedBy = request.UserName,
                                ApprovedDate = DateTime.UtcNow,
                                PaymentMethod = request.PaymentMethod,
                                PaymentMode = request.PaymentMode,
                                TaxMethod = request.TaxMethod,
                                TaxRateId = request.TaxRateId,
                                VatAccountId = request.VatAccountId,
                                BillsId = request.BillsId,
                                BranchId = request.BranchId,
                                DocumentType = request.DocumentType,
                                DocumentId = request.DocumentId,
                                PeriodId = period.Id,
                                PaymentDate = request.PaymentDate,
                                InvoiceAmount = request.InvoiceAmount,
                                RemainingBalance = request.RemainingBalance,
                            };
                            await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);

                            if (request.TemporaryCashIssueId != null)
                            {
                                var temporaryCashIssueExpense = new TemporaryCashIssueExpense()
                                {
                                    TemporaryCashIssueId = request.TemporaryCashIssueId.Value,
                                    DocumentId = paymentVoucher.Id,
                                    DocumentType = "Supplier Payment"
                                };
                                await _context.TemporaryCashIssueExpenses.AddAsync(temporaryCashIssueExpense, cancellationToken);
                            }
                            if (request.IsContractor)
                            {
                                var contractorPayment = new ContractorPayment
                                {
                                    Date = request.Date,
                                    VoucherNumber = paymentVoucher.VoucherNumber,
                                    Narration = request.Narration,
                                    ChequeNumber = request.ChequeNumber,
                                    Amount = request.Amount,
                                    PaymentMethod = request.PaymentMethod,
                                    PaymentMode = request.PaymentMode,
                                    BatchProcessContractorId = request.BatchProcessContractorId,
                                    PaymentVoucherId = paymentVoucher.Id
                                };
                                await _context.ContractorPayments.AddAsync(contractorPayment, cancellationToken);

                            }

                            if (request.PaymentVoucherItems is { Count: > 0 })
                            {
                                foreach (var item in request.PaymentVoucherItems)
                                {
                                    var attachment = new PaymentVoucherItem()
                                    {
                                        PaymentVoucherId = paymentVoucher.Id,
                                        Description = item.Description,
                                        Amount = item.Amount,
                                        PayAmount = item.PayAmount,
                                        Total = item.Total,
                                        ExtraAmount = item.ExtraAmount,
                                        PartiallyInvoices = item.PartiallyInvoices,
                                        PurchaseInvoiceId = item.PurchaseInvoiceId,
                                        SaleInvoiceId = item.SaleInvoiceId,
                                        ChequeNumber = item.ChequeNumber,
                                        Date = System.DateTime.Now,
                                        IsActive = item.IsActive,
                                        IsAging = item.IsAging,
                                        DueAmount = item.DueAmount
                                    };
                                    //Add Attachments to database
                                    await _context.PaymentVoucherItems.AddAsync(attachment, cancellationToken);
                                }
                            }

                            if (request.AttachmentList is { Count: > 0 })
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

                                var paymentVoucherAttachment = new PaymentVoucherAttachment()
                                {
                                    PaymentVoucherId = paymentVoucher.Id,
                                    Path = request.Path,
                                };
                                await _context.PaymentVoucherAttachments.AddAsync(paymentVoucherAttachment, cancellationToken);
                            }

                            if (request.PaymentVoucherItems != null)
                            {
                                if (request.PaymentVoucherItems.Count > 0)
                                {
                                    foreach (var item in request.PaymentVoucherItems)
                                    {
                                        if (item.IsActive && item.SaleInvoiceId != null)
                                        {
                                            var sale = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == item.SaleInvoiceId);
                                            if (sale != null)
                                            {
                                                //var saleAmount = sale.TotalAmount;
                                                //var paidAmount =  _context.PaymentVoucherItems.Where(x =>x.SaleInvoiceId == sale.Id).Sum(x=>x.PayAmount);




                                                if (item.Total == 0)
                                                {
                                                    sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                                    _context.Sales.Update(sale);
                                                }
                                                else if (item.Total > 0)
                                                {
                                                    sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                                    _context.Sales.Update(sale);
                                                }

                                                var creditPayment = await _context.SalePayments.FirstOrDefaultAsync(x => x.SaleId == item.SaleInvoiceId && x.Name == "Credit", cancellationToken: cancellationToken);
                                                if (creditPayment != null)
                                                {
                                                    creditPayment.DueAmount -= item.PayAmount;
                                                    creditPayment.Received -= item.PayAmount;
                                                    _context.SalePayments.Update(creditPayment);
                                                }
                                                var salePayment = new SalePayment
                                                {
                                                    DueAmount = item.PayAmount,
                                                    Received = item.PayAmount,
                                                    Balance = 0,
                                                    Change = 0,
                                                    PaymentType = request.PaymentMode,
                                                    BankCashAccountId = request.BankCashAccountId,
                                                    Name = request.PaymentMode == SalePaymentType.Cash ? "Cash" : "Bank",
                                                    SaleId = sale.Id,
                                                    Rate = 0,
                                                    Amount = 0
                                                };
                                                _context.SalePayments.Add(salePayment);
                                            }

                                        }
                                        else if (item.IsActive && item.PurchaseInvoiceId != null)
                                        {
                                            var purchase = _context.PurchasePosts.FirstOrDefault(x => x.Id == item.PurchaseInvoiceId);
                                            if (purchase != null)
                                            {
                                                if (item.Total == 0)
                                                {
                                                    purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                                                    purchase.ReceivedAmount += item.PayAmount;
                                                    _context.PurchasePosts.Update(purchase);
                                                }
                                                else if (item.Total > 0)
                                                {
                                                    purchase.PartiallyInvoices = PartiallyInvoices.Partially;
                                                    purchase.ReceivedAmount += item.PayAmount;

                                                    _context.PurchasePosts.Update(purchase);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (request.SaleInvoice != Guid.Empty && request.SaleInvoice != null && !request.FromCreditInvoice)
                            {
                                var sale = _context.Sales.FirstOrDefault(x => x.Id == request.SaleInvoice);
                                if (sale != null)
                                {
                                    var saleAmount = await _mediator.Send(new SaleAmountDetailQuery { Id = sale.Id }, cancellationToken);
                                    var paidAmount = await _mediator.Send(new GetRemainingInvoiceBalance
                                    {
                                        InvoiceId = request.SaleInvoice.Value
                                    }, cancellationToken);


                                    if (saleAmount <= request.Amount + paidAmount)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                        _context.Sales.Update(sale);
                                    }
                                    else if (saleAmount > request.Amount + paidAmount)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                        _context.Sales.Update(sale);
                                    }

                                    var creditPayment = await _context.SalePayments.FirstOrDefaultAsync(x => x.SaleId == sale.Id && x.Name == "Credit", cancellationToken: cancellationToken);
                                    if (creditPayment != null)
                                    {
                                        creditPayment.DueAmount -= request.Amount;
                                        creditPayment.Received -= request.Amount;
                                        _context.SalePayments.Update(creditPayment);
                                    }
                                    var salePayment = new SalePayment
                                    {
                                        DueAmount = request.Amount,
                                        Received = request.Amount,
                                        Balance = 0,
                                        Change = 0,
                                        PaymentType = request.PaymentMode,
                                        BankCashAccountId = request.BankCashAccountId,
                                        Name = request.PaymentMode == SalePaymentType.Cash ? "Cash" : "Bank",
                                        SaleId = sale.Id,
                                        Rate = 0,
                                        Amount = 0
                                    };
                                    _context.SalePayments.Add(salePayment);
                                }
                            }

                            if (request.PurchaseInvoice != Guid.Empty && request.PurchaseInvoice != null)
                            {

                                var purchase = _context.PurchasePosts.FirstOrDefault(x => x.Id == request.PurchaseInvoice);
                                if (purchase != null)
                                {
                                    var purchaseAmount = await _mediator.Send(new PurchasePostDetailQuery { Id = purchase.Id }, cancellationToken);
                                    var paidAmount = await _mediator.Send(new GetRemainingInvoiceBalance
                                    {
                                        InvoiceId = request.PurchaseInvoice.Value
                                    }, cancellationToken);

                                    if (purchaseAmount != null)
                                    {
                                        if (purchaseAmount.NetAmount <= request.Amount + paidAmount)
                                        {
                                            purchase.PartiallyInvoices = PartiallyInvoices.Fully;

                                            _context.PurchasePosts.Update(purchase);
                                        }
                                        else if (purchaseAmount.NetAmount > request.Amount + paidAmount)
                                        {
                                            purchase.PartiallyInvoices = PartiallyInvoices.Partially;

                                            _context.PurchasePosts.Update(purchase);
                                        }
                                    }
                                }
                            }

                            var accounts = _context.Accounts.AsNoTracking().Include(x => x.CostCenter).AsQueryable();
                            var vats = _context.TaxRates.AsNoTracking().AsQueryable();
                            var transactions = new List<Transaction>();

                            var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Expenses", cancellationToken: cancellationToken);
                            var costCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "610000");
                            if (costCenter == null)
                            {
                                var newCostCenter = new CostCenter()
                                {
                                    Code = "610000",
                                    Name = "Expense Adjustment",
                                    NameArabic = "تعديل المصاريف",
                                    Description = "Expense Adjustment",
                                    IsActive = true,
                                    AccountTypeId = accountType.Id
                                };
                                await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);
                                newAccount = new Account
                                {
                                    Code = "61000001",
                                    Name = "Expense Adjustment",
                                    NameArabic = "تعديل المصاريف",
                                    Description = "Expense Adjustment",
                                    IsActive = true,
                                    CostCenterId = newCostCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                            }
                            var vatExpense = accounts.FirstOrDefault(x => x.Code == "1300002");
                            if (vatExpense == null)
                            {
                                var expenseCostCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "130000");
                                vatAccount = new Account
                                {
                                    Code = "1300002",
                                    Name = "VAT Paid On Expense",
                                    NameArabic = "دفع ضريبة القيمة المضافة على المصاريف",
                                    Description = "VAT Paid On Expense",
                                    IsActive = true,
                                    CostCenterId = expenseCostCenter.CostCenterId
                                };
                                await _context.Accounts.AddAsync(vatAccount, cancellationToken);
                            }

                            if (TransactionType.BankPay == (TransactionType)request.PaymentVoucherType || TransactionType.CashPay == (TransactionType)request.PaymentVoucherType || request.PaymentVoucherType == PaymentVoucherType.AdvancePay || request.PaymentVoucherType == PaymentVoucherType.AdvanceExpense)
                            {
                                TransactionType transactionType;
                                if (request.PaymentMode == SalePaymentType.Cash && request.PaymentVoucherType == PaymentVoucherType.AdvancePay)
                                    transactionType = TransactionType.AdvanceCashPay;
                                else if (request.PaymentMode == SalePaymentType.Bank && request.PaymentVoucherType == PaymentVoucherType.AdvancePay)
                                    transactionType = TransactionType.AdvanceBankPay;
                                else if (request.PaymentVoucherType == PaymentVoucherType.AdvanceExpense)
                                    transactionType = TransactionType.AdvanceExpense;
                                else if (request.PaymentMode == SalePaymentType.Cash)
                                    transactionType = TransactionType.CashPay;
                                else if (request.PaymentMode == SalePaymentType.Bank)
                                    transactionType = TransactionType.BankPay;
                                else if (request.PaymentMode == SalePaymentType.Advance)
                                    transactionType = TransactionType.AdvancePay;
                                else
                                {
                                    transactionType = TransactionType.CashPay;
                                }

                                if (request.IsPurchaseOrderExpense || request.IsPurchasePostExpense)
                                {
                                    var vatIds = vats.FirstOrDefault(x => x.Id == request.TaxRateId);
                                    if (request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل")
                                    {
                                        if (vatIds == null)
                                        {
                                            throw new NotFoundException("You Select Tax Method but not Select VAT", null);
                                        }

                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = request.Amount + Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId: null,

                                        });
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = request.Amount + Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,

                                        });
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = Math.Round((request.Amount * vatIds.Rate) / 100, 2),
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                    }
                                    else if (request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل")
                                    {
                                        if (vatIds == null)
                                        {
                                            throw new NotFoundException("You Select Tax Method but not Select VAT", null);
                                        }

                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = request.Amount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                        });
                                        transactions.Add(new Transaction
                                        {

                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = request.Amount,
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        transactions.Add(new Transaction
                                        {

                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = Math.Round((request.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                            Credit = 0m,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                            TransactionType = transactionType,
                                            Debit = 0m,
                                            Credit = Math.Round((request.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                    }
                                    else
                                    {
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0M,
                                            Credit = request.Amount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId : null,

                                        });
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = string.IsNullOrEmpty(request.Narration) ? transactionType.ToString() : request.Narration,
                                            Year = period.Year,
                                            AccountId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = request.Amount,
                                            Credit = 0M,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                    }
                                }
                                else
                                {
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.BankCashAccountId,
                                        TransactionType = transactionType,
                                        Debit = 0M,
                                        Credit = request.Amount,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                        ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                    });
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.ContactAccountId,
                                        TransactionType = transactionType,
                                        Debit = request.Amount,
                                        Credit = 0M,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                    });
                                }
                            }
                            else
                            {
                                TransactionType transactionType;
                                if (request.PaymentMode == SalePaymentType.Cash && request.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)
                                    transactionType = TransactionType.AdvanceCashReceipt;
                                else if (request.PaymentMode == SalePaymentType.Bank && request.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)
                                    transactionType = TransactionType.AdvanceBankReceipt;
                                else if (request.PaymentMode == SalePaymentType.Cash && request.PaymentVoucherType == PaymentVoucherType.PettyCash)
                                    transactionType = TransactionType.PettyCash;
                                else if (request.PaymentMode == SalePaymentType.Cash)
                                    transactionType = TransactionType.CashReceipt;
                                else if (request.PaymentMode == SalePaymentType.Bank)
                                    transactionType = TransactionType.BankReceipt;
                                else if (request.PaymentMode == SalePaymentType.Advance)
                                    transactionType = TransactionType.AdvanceReceipt;
                                else
                                {
                                    transactionType = TransactionType.CashReceipt;
                                }


                                if (request.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)
                                {
                                    if ((request.DocumentId != null && request.DocumentId != Guid.Empty ) ||  (request.MultipleDocument.Count>0 || request.MultipleDocument!=null))
                                    {
                                        if (request.DocumentType == "Proforma Invoice")
                                        {
                                            {
                                                var purcChase = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == request.DocumentId);


                                                if (purcChase != null)
                                                {

                                                    var paidAmount = _context.PaymentVouchers.Where(x => x.DocumentId == purcChase.Id && x.DocumentType == "Proforma Invoice" && x.ApprovalStatus == ApprovalStatus.Approved).Sum(x => x.Amount);





                                                    if (purcChase != null)
                                                    {
                                                        if (purcChase.TotalAmount <= request.Amount + paidAmount)
                                                        {
                                                            purcChase.PartiallyInvoices = PartiallyInvoices.Fully;
                                                            _context.Sales.Update(purcChase);
                                                        }
                                                        else if (purcChase.TotalAmount > request.Amount + paidAmount)
                                                        {
                                                            purcChase.PartiallyInvoices = PartiallyInvoices.Partially;
                                                            _context.Sales.Update(purcChase);
                                                        }
                                                    }
                                                }
                                            }


                                           

                                        }
                                        else if (request.DocumentType == "Sale Order" || request.DocumentType == "Quotation")
                                        {


                                            {
                                                var purchase = _context.SaleOrders.AsNoTracking().FirstOrDefault(x => x.Id == request.DocumentId);


                                                if (purchase != null)
                                                {

                                                    var paidAmount = _context.PaymentVouchers.Where(x => x.DocumentId == purchase.Id && x.ApprovalStatus == ApprovalStatus.Approved).Sum(x => x.Amount);


                                                    if (purchase != null)
                                                    {
                                                        if (purchase.TotalAmount <= request.Amount + paidAmount)
                                                        {
                                                            purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                                                            _context.SaleOrders.Update(purchase);
                                                        }
                                                        else if (purchase.TotalAmount > request.Amount + paidAmount)
                                                        {
                                                            purchase.PartiallyInvoices = PartiallyInvoices.Partially;
                                                            _context.SaleOrders.Update(purchase);
                                                        }
                                                    }
                                                }

                                            }

                                            

                                        }
                                    }

                                    var customerAdvanceAcc = new Account();

                                    var costCenterOfCashAdvance = accounts.Where(x => x.CostCenter.Code == "220000").ToList().LastOrDefault();


                                    if (costCenterOfCashAdvance != null)
                                    {
                                        var customerAdvanceAccount = accounts.FirstOrDefault(x => x.Name == "Customers Advance");
                                        if (customerAdvanceAccount == null)
                                        {

                                            var acc = new Account
                                            {
                                                Code = (int.Parse(costCenterOfCashAdvance.Code) + 1).ToString(),
                                                Name = "Customers Advance",
                                                NameArabic = "تقدم العملاء",
                                                Description = "Customers Advance",
                                                IsActive = true,
                                                CostCenterId = costCenterOfCashAdvance.CostCenterId
                                            };
                                            await _context.Accounts.AddAsync(acc, cancellationToken);
                                            customerAdvanceAcc.Id = acc.Id;


                                        }
                                        else
                                        {
                                            customerAdvanceAcc.Id = customerAdvanceAccount.Id;
                                        }


                                        transactions.Add(new Transaction()
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = request.Narration,
                                            Year = period.Year,
                                            AccountId = request.BankCashAccountId,
                                            TransactionType = transactionType,
                                            Credit = 0M,
                                            Debit = request.Amount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                            ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                        });
                                        transactions.Add(new Transaction()
                                        {
                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = request.VoucherNumber,
                                            Date = request.PaymentDate,
                                            DocumentDate = request.Date,
                                            ApprovalDate = request.ApprovalDate,
                                            Description = request.Narration,
                                            Year = period.Year,
                                            AccountId = customerAdvanceAcc.Id,
                                            ContactId = request.ContactAccountId,
                                            TransactionType = transactionType,
                                            Debit = 0M,
                                            Credit = request.Amount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });
                                    }







                                }


                                else if (request.PaymentVoucherType == PaymentVoucherType.AdvancePurchase)
                                {

                                    if (request.DocumentType == "Purchase Invoice")
                                    {
                                        var purchase = _context.PurchasePosts.FirstOrDefault(x => x.Id == request.DocumentId);


                                        if (purchase != null)
                                        {

                                            var paidAmount = _context.PaymentVouchers.Where(x => x.DocumentId == purchase.Id && x.DocumentType == "Purchase Invoice" && x.ApprovalStatus == ApprovalStatus.Approved).Sum(x => x.Amount);


                                            if (purchase != null)
                                            {
                                                if (purchase.TotalAmount <= request.Amount + paidAmount)
                                                {
                                                    purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                                                    _context.PurchasePosts.Update(purchase);
                                                }
                                                else if (purchase.TotalAmount > request.Amount + paidAmount)
                                                {
                                                    purchase.PartiallyInvoices = PartiallyInvoices.Partially;
                                                    _context.PurchasePosts.Update(purchase);
                                                }
                                            }
                                        }

                                    }

                                    else if (request.DocumentType == "Supplier Quotation" || request.DocumentType == "Purchase Order")
                                    {
                                        var purchase = _context.PurchaseOrders.AsNoTracking().FirstOrDefault(x => x.Id == request.DocumentId);


                                        if (purchase != null)
                                        {

                                            var paidAmount = _context.PaymentVouchers.Where(x => x.DocumentId == purchase.Id && x.DocumentType == "Supplier Quotation" && x.ApprovalStatus == ApprovalStatus.Approved).Sum(x => x.Amount);


                                            if (purchase != null)
                                            {
                                                if (purchase.TotalAmount <= request.Amount + paidAmount)
                                                {
                                                    purchase.PartiallyReceived = PartiallyInvoices.Fully;
                                                    _context.PurchaseOrders.Update(purchase);
                                                }
                                                else if (purchase.TotalAmount > request.Amount + paidAmount)
                                                {
                                                    purchase.PartiallyReceived = PartiallyInvoices.Partially;
                                                    _context.PurchaseOrders.Update(purchase);
                                                }
                                            }
                                        }

                                    }



                                    var customerAdvanceAcc = new Account();


                                    var supplierAdvance = accounts.FirstOrDefault(x => x.CostCenter.Code == "161000");
                                    if (supplierAdvance == null)
                                    {

                                        var assetsAccountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Assets", cancellationToken: cancellationToken);

                                        var supplierCostCenter = new CostCenter()
                                        {
                                            Code = "161000",
                                            Name = "Supplier Advance",
                                            NameArabic = "تقدم المورد",
                                            Description = "Supplier Advance",
                                            IsActive = true,
                                            AccountTypeId = assetsAccountType.Id
                                        };
                                        await _context.CostCenters.AddAsync(supplierCostCenter, cancellationToken);
                                        newAccount = new Account
                                        {
                                            Code = "1610001",
                                            Name = "Supplier Advance",
                                            NameArabic = "تقدم المورد",
                                            Description = "Supplier Advance",
                                            IsActive = true,
                                            CostCenterId = supplierCostCenter.Id
                                        };
                                        await _context.Accounts.AddAsync(newAccount, cancellationToken);

                                        customerAdvanceAcc.Id = newAccount.Id;
                                    }

                                    else
                                    {

                                        var costCenterOfCashAdvance = accounts.FirstOrDefault(x => x.Code == "1610001");
                                        if (costCenterOfCashAdvance != null)
                                        {
                                            customerAdvanceAcc.Id = costCenterOfCashAdvance.Id;

                                        }
                                    }
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.BankCashAccountId,
                                        TransactionType = transactionType,
                                        Credit = request.Amount,
                                        Debit =0 ,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                        ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                    });
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = customerAdvanceAcc.Id,
                                        ContactId = request.ContactAccountId,
                                        TransactionType = transactionType,
                                        Debit = request.Amount,
                                        Credit = 0,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                    });

                                }

                                else
                                {
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.BankCashAccountId,
                                        TransactionType = transactionType,
                                        Credit = 0M,
                                        Debit = request.Amount,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                        ContactId = request.PremiumAdvance ? request.ContactAccountId : null,
                                    });
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = paymentVoucher.Id,
                                        DocumentNumber = request.VoucherNumber,
                                        Date = request.PaymentDate,
                                        DocumentDate = request.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        Description = request.Narration,
                                        Year = period.Year,
                                        AccountId = request.ContactAccountId,
                                        TransactionType = transactionType,
                                        Debit = 0M,
                                        Credit = request.Amount,
                                        PeriodId = period.Id,
                                        BranchId = paymentVoucher.BranchId,
                                    });
                                }



                            }

                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            foreach (var transaction in transactions)
                            {
                                await _context.Transactions.AddAsync(transaction, cancellationToken);
                            }

                            // Purchase Order Payment
                            if (request.IsPurchase)
                            {
                                var purchaseOrderPayment = new PurchaseOrderPayment()
                                {
                                    Date = request.Date,
                                    VoucherNumber = request.VoucherNumber,
                                    PaymentMode = request.PaymentMode,
                                    PaymentMethod = request.PaymentMethod,
                                    ChequeNumber = request.ChequeNumber,
                                    Amount = request.Amount,
                                    Narration = request.Narration,
                                    PurchaseOrderId = request.PurchaseOrderId,
                                    PaymentVoucherId = paymentVoucher.Id,
                                };
                                await _context.PurchaseOrderPayments.AddAsync(purchaseOrderPayment, cancellationToken);

                                var action = _context.CompanyProcess.AsNoTracking()
                                    .Where(x => x.Type == "Purchase").AsQueryable();

                                var payment = await action.FirstOrDefaultAsync(x =>
                                    (x.ProcessName == "Add Payment" || x.ProcessName == "إضافة الدفع") && x.Type == "Purchase", cancellationToken: cancellationToken);
                                if (payment != null)
                                {
                                    var actions = new ActionLookUpModel()
                                    {
                                        PurchaseOrderId = request.PurchaseOrderId,
                                        ProcessId = payment.Id,
                                        Date = DateTime.UtcNow,
                                        Description = "Add Payment"
                                    };
                                    var id = await _mediator.Send(new AddUpdateActionCommand
                                    {
                                        Action = actions
                                    }, cancellationToken);
                                }
                            }

                            // Sale Order Payment
                            if (request.IsSaleOrder)
                            {
                                var saleOrderPayment = new SaleOrderPayment()
                                {
                                    Date = request.Date,
                                    VoucherNumber = request.VoucherNumber,
                                    PaymentMode = request.PaymentMode,
                                    PaymentMethod = request.PaymentMethod,
                                    ChequeNumber = request.ChequeNumber,
                                    Amount = request.Amount,
                                    Narration = request.Narration,
                                    SaleOrderId = request.PurchaseOrderId,
                                    PaymentVoucherId = paymentVoucher.Id,
                                };
                                await _context.SaleOrderPayments.AddAsync(saleOrderPayment, cancellationToken);
                            }
                            // Purchase Order Expense
                            if (request.IsPurchaseOrderExpense)
                            {
                                var purchaseOrderExpense = new PurchaseOrderExpense()
                                {
                                    Date = request.Date,
                                    VoucherNumber = request.VoucherNumber,
                                    PaymentMode = request.PaymentMode,
                                    PaymentMethod = request.PaymentMethod,
                                    ChequeNumber = request.ChequeNumber,
                                    Amount = request.Amount,
                                    Narration = request.Narration,
                                    BankCashAccountId = request.BankCashAccountId,
                                    ContactAccountId = request.ContactAccountId,
                                    PurchaseOrderId = request.PurchaseOrderId,
                                    PaymentVoucherId = paymentVoucher.Id,
                                    ExpenseTypeId = request.ExpenseTypeId,
                                    VatAccountId = request.VatAccountId,
                                    TaxMethod = request.TaxMethod,
                                    TaxRateId = request.TaxRateId,
                                };
                                await _context.PurchaseOrderExpenses.AddAsync(purchaseOrderExpense, cancellationToken);

                                var expense = await _context.CompanyProcess.FirstOrDefaultAsync(x => (x.ProcessName == "Add Expense" || x.ProcessName == "أضف المصاريف") && x.Type == "Purchase", cancellationToken: cancellationToken);
                                if (expense == null)
                                {
                                    var companyProcess = new CompanyProcess()
                                    {
                                        ProcessName = "Add Expense",
                                        ProcessNameArabic = "أضف المصاريف",
                                        Type = "Purchase",
                                        Status = true,
                                    };
                                    await _context.CompanyProcess.AddAsync(companyProcess, cancellationToken);
                                    var actions = new ActionLookUpModel()
                                    {
                                        PurchaseOrderId = request.PurchaseOrderId,
                                        ProcessId = companyProcess.Id,
                                        Date = DateTime.UtcNow,
                                        Description = "Add Expense"
                                    };
                                    var id = await _mediator.Send(new AddUpdateActionCommand
                                    {
                                        Action = actions
                                    }, cancellationToken);
                                }
                                else
                                {
                                    var actions = new ActionLookUpModel()
                                    {
                                        PurchaseOrderId = request.PurchaseOrderId,
                                        ProcessId = expense.Id,
                                        Date = DateTime.UtcNow,
                                        Description = "Add Expense"
                                    };
                                    await _mediator.Send(new AddUpdateActionCommand
                                    {
                                        Action = actions
                                    }, cancellationToken);
                                }
                            }

                            // Purchase Invoice Expense
                            if (request.IsPurchasePostExpense)
                            {
                                var purchasePostExpense = new PurchasePostExpense()
                                {
                                    Date = request.Date,
                                    VoucherNumber = request.VoucherNumber,
                                    PaymentMode = request.PaymentMode,
                                    PaymentMethod = request.PaymentMethod,
                                    ChequeNumber = request.ChequeNumber,
                                    Amount = request.Amount,
                                    Narration = request.Narration,
                                    BankCashAccountId = request.BankCashAccountId,
                                    ContactAccountId = request.ContactAccountId,
                                    PurchasePostId = request.PurchaseOrderId,
                                    PaymentVoucherId = paymentVoucher.Id,
                                    VatAccountId = request.VatAccountId,
                                    TaxMethod = request.TaxMethod,
                                    TaxRateId = request.TaxRateId,
                                };
                                await _context.PurchasePostExpenses.AddAsync(purchasePostExpense, cancellationToken);

                                var expense = await _context.CompanyProcess.FirstOrDefaultAsync(x =>
                                    (x.ProcessName == "Add Expense" || x.ProcessName == "أضف المصاريف") && x.Type == "Purchase", cancellationToken: cancellationToken);
                                if (expense == null)
                                {
                                    var companyProcess = new CompanyProcess()
                                    {
                                        ProcessName = "Add Expense",
                                        ProcessNameArabic = "أضف المصاريف",
                                        Type = "Purchase",
                                        Status = true,
                                    };
                                    await _context.CompanyProcess.AddAsync(companyProcess, cancellationToken);
                                    var actions = new ActionLookUpModel()
                                    {
                                        PurchaseOrderId = request.PurchaseOrderId,
                                        ProcessId = companyProcess.Id,
                                        Date = DateTime.UtcNow,
                                        Description = "Add Expense"
                                    };
                                    await _mediator.Send(new AddUpdateActionCommand
                                    {
                                        Action = actions,
                                        IsPurchasePost = true
                                    }, cancellationToken);
                                }
                                else
                                {
                                    var actions = new ActionLookUpModel()
                                    {
                                        PurchaseOrderId = request.PurchaseOrderId,
                                        ProcessId = expense.Id,
                                        Date = DateTime.UtcNow,
                                        Description = "Add Expense"
                                    };
                                    await _mediator.Send(new AddUpdateActionCommand
                                    {
                                        Action = actions,
                                        IsPurchasePost = true
                                    }, cancellationToken);
                                }
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                DocumentNo = request.VoucherNumber,
                                BranchId = request.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            scope.Complete();
                            var message = new Message
                            {
                                Id = paymentVoucher.Id,
                                IsAddUpdate = "Data Added Successfully"
                            };
                            return message;
                        }
                        else
                        {
                            var paymentVoucher = new PaymentVoucher
                            {
                                Date = request.Date,
                                VoucherNumber = request.VoucherNumber,
                                Narration = request.Narration,
                                ChequeNumber = request.ChequeNumber,
                                ApprovalStatus = request.ApprovalStatus,
                                PaymentVoucherType = request.PaymentVoucherType,
                                ContactAccountId = request.ContactAccountId,
                                BankCashAccountId = request.BankCashAccountId,
                                Amount = request.Amount,
                                PettyCash = request.PettyCash ?? 0,
                                SaleInvoice = request.SaleInvoice,
                                PurchaseInvoice = request.PurchaseInvoice,
                                DraftBy = request.UserName,
                                DraftDate = DateTime.UtcNow,
                                PaymentMethod = request.PaymentMethod,
                                TransactionId = request.TransactionId,
                                NatureOfPayment = request.NatureOfPayment,
                                Prefix = request.Prefix,
                                PaymentMode = request.PaymentMode,
                                BillsId = request.BillsId,
                                DocumentType = request.DocumentType,
                                DocumentId = request.DocumentId,
                                PaymentDate = request.PaymentDate,
                                InvoiceAmount = request.InvoiceAmount,
                                RemainingBalance = request.RemainingBalance,
                                
                            };


                            await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);


                            if (request.TemporaryCashIssueId != null)
                            {
                                var temporaryCashIssueExpense = new TemporaryCashIssueExpense()
                                {
                                    TemporaryCashIssueId = request.TemporaryCashIssueId.Value,
                                    DocumentId = paymentVoucher.Id,
                                    DocumentType = "Supplier Payment"
                                };
                                await _context.TemporaryCashIssueExpenses.AddAsync(temporaryCashIssueExpense, cancellationToken);
                            }

                            if (request.AttachmentList is { Count: > 0 })
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

                                
                                await _context.PaymentVoucherAttachments.AddAsync(new PaymentVoucherAttachment()
                                {
                                    PaymentVoucherId = paymentVoucher.Id,
                                    Path = request.Path,
                                }, cancellationToken);
                            }
                            if (request.PaymentVoucherItems is { Count: > 0 })
                            {
                                foreach (var item in request.PaymentVoucherItems)
                                {
                                    var attachment = new PaymentVoucherItem()
                                    {
                                        PaymentVoucherId = paymentVoucher.Id,
                                        Description = item.Description,
                                        Amount = item.Amount,
                                        ExtraAmount = item.ExtraAmount,
                                        PayAmount = item.PayAmount,
                                        Total = item.Total,
                                        PurchaseInvoiceId = item.PurchaseInvoiceId,
                                        SaleInvoiceId = item.SaleInvoiceId,
                                        ChequeNumber = item.ChequeNumber,
                                        Date = System.DateTime.Now,
                                        IsActive = item.IsActive,
                                        IsAging = item.IsAging,
                                        DueAmount = item.DueAmount
                                    };
                                    //Add Attachments to database
                                    await _context.PaymentVoucherItems.AddAsync(attachment, cancellationToken);
                                }
                            }
                            
                            await _context.PaymentVoucherAttachments.AddAsync(new PaymentVoucherAttachment()
                            {
                                PaymentVoucherId = paymentVoucher.Id,
                                Path = request.Path,
                            }, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                DocumentNo = request.VoucherNumber,
                                BranchId = request.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            var success = await _context.SaveChangesAsync(cancellationToken);
                            if (success > 0)
                            {
                                var message = new Message
                                {
                                    Id = paymentVoucher.Id,
                                    IsAddUpdate = "Data Added Successfully"
                                };
                                return message;
                            }
                            else
                            {
                                var message = new Message
                                {
                                    Id = paymentVoucher.Id,
                                    IsAddUpdate = "Data Added Successfully"
                                };
                                return message;
                            }
                        }
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    return new Message
                    {
                        IsAddUpdate = exception.Message
                    };
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    return new Message
                    {
                        IsAddUpdate = exception.Message
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    return new Message
                    {
                        IsAddUpdate = exception.Message
                    };
                }
            }
        }
    }
}