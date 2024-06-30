using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList
{
    public class GetPaymentVoucherListQuery : PagedRequest, IRequest<PagedResult<PaymentVoucherLookUpWithCalculation>>
    {
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string SearchTerm { get; set; }
        public string UserId { get; set; }
        public int? Month { get; set; }
        public string NatureOfPayment { get; set; }
        public string NewNatureOfPayment { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMode { get; set; }
        public int? Year { get; set; }
        public bool IsDashboard { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPaymentVoucherListQuery, PagedResult<PaymentVoucherLookUpWithCalculation>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<GetPaymentVoucherListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public string UserId { get; set; }

            public async Task<PagedResult<PaymentVoucherLookUpWithCalculation>> Handle(GetPaymentVoucherListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<PaymentVoucher> query;
                    _context.DisableTenantFilter = true;
                    //it is use in Sale Dashboard Report
                    if (request.IsDashboard)
                    {
                        if (request.PaymentVoucherType == PaymentVoucherType.BankReceipt)
                        {
                            query =await _context.PaymentVouchers.AsNoTracking()
                           .Include(x => x.ContactAccount)
                           .Include(x => x.Account)
                           .Where(x => x.PaymentVoucherType == PaymentVoucherType.BankReceipt).ToListAsync();


                            if (request.BranchId != null)
                            {
                                query = query.Where(x => x.BranchId == request.BranchId).ToList();
                            }

                            if (request.FromDate != null)
                            {
                                query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date).ToList();
                            }

                            if (request.ToDate != null)
                            {
                                query = query.Where(x => x.Date.Date <= request.ToDate.Value.Date).ToList();
                            }
                            if (request.ContactId != null)
                            {
                                query = query.Where(x => x.ContactAccountId == request.ContactId).ToList();
                            }

                            var paymentList = new List<PaymentVoucherLookUpModel>();
                            foreach (var item in query)
                            {

                                paymentList.Add(new PaymentVoucherLookUpModel
                                {
                                    Id = item.Id,
                                    Date = item.Date,
                                    PaymentDate = item.PaymentDate,
                                    VoucherNumber = item.VoucherNumber,
                                    Narration = item.Narration,
                                    ChequeNumber = item.ChequeNumber,
                                    ApprovalStatus = item.ApprovalStatus,
                                    Amount = item.Amount,
                                    PaymentVoucherType = item.PaymentVoucherType,
                                    BankCashAccountId = item.BankCashAccountId,
                                    BankCashAccountName = item.Account.Name,
                                    ContactAccountName = item.ContactAccount.Name,
                                    DraftBy = item.DraftBy,
                                    ApprovedBy = item.ApprovedBy,
                                    RejectBy = item.RejectBy,
                                    VoidBy = item.VoidBy,
                                    Reason = item.Reason,
                                    DraftDate = item.DraftDate,
                                    ApprovedDate = item.ApprovedDate,
                                    RejectDate = item.RejectDate,
                                    VoidDate = item.VoidDate,
                                    PaymentMode = item.PaymentMode
                                });
                            }
                            return new PagedResult<PaymentVoucherLookUpWithCalculation>
                            {
                                Results = new PaymentVoucherLookUpWithCalculation
                                {
                                    PaymentVouchers = paymentList,


                                },

                            };

                        }

                    }
                    if (request.PaymentVoucherType == PaymentVoucherType.BankPay)
                    {
                        query = await _context.PaymentVouchers.AsNoTracking()
                            .Include(x => x.ContactAccount)
                            .Include(x => x.Account)
                            .Where(x => (x.PaymentVoucherType == PaymentVoucherType.BankPay 
                                         || x.PaymentVoucherType == PaymentVoucherType.AdvancePay
                                         || x.PaymentVoucherType == PaymentVoucherType.AdvanceExpense)).ToListAsync();
                    }
                    else if (request.PaymentVoucherType == PaymentVoucherType.AdvancePurchase)
                    {
                        query = _context.PaymentVouchers.AsNoTracking()
                            .Include(x => x.ContactAccount)
                            .Include(x => x.Account)
                            .Where(x => (x.PaymentVoucherType == PaymentVoucherType.AdvancePurchase)).ToList();
                    } else if (request.PaymentVoucherType == PaymentVoucherType.PettyCash)
                    {
                        query = _context.PaymentVouchers.AsNoTracking()
                            .Include(x => x.ContactAccount)
                            .Include(x => x.Account)
                            .Where(x => (x.PaymentVoucherType == PaymentVoucherType.PettyCash)).ToList();
                    }
                    else if (request.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)
                    {
                        query = _context.PaymentVouchers.AsNoTracking()
                            .Include(x => x.ContactAccount)
                            .Include(x => x.Account)
                            .Where(x => (x.PaymentVoucherType == PaymentVoucherType.AdvanceReceipt)).ToList();
                    }
                    else if (request.PaymentVoucherType == PaymentVoucherType.AdvancePurchase)
                    {
                        query = _context.PaymentVouchers.AsNoTracking()
                            .Include(x => x.ContactAccount)
                            .Include(x => x.Account)
                            .Where(x => (x.PaymentVoucherType == PaymentVoucherType.AdvancePurchase)).ToList();
                    }
                    else
                    {
                        query = _context.PaymentVouchers.AsNoTracking()
                            .Include(x => x.ContactAccount)
                            .Include(x => x.Account)
                            .Where(x => (x.PaymentVoucherType == PaymentVoucherType.BankReceipt)).ToList();

                    }

                    if (request.BranchId != null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId).ToList();
                    }

                    var lookUp = new PaymentVoucherLookUpWithCalculation();
                    //lookUp.Posted = query.Count(x =>
                    //   x.ApprovalStatus == ApprovalStatus.Approved);

                    //lookUp.Draft = query.Count(x =>
                    //    x.ApprovalStatus == ApprovalStatus.Draft);

                    //lookUp.Rejected = query.Count(x =>
                    //    x.ApprovalStatus == ApprovalStatus.Rejected);

                    //lookUp.TotalReceipt = query.Count();
                    //lookUp.CashReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Cash);
                    //lookUp.TotalCashReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Cash).Sum(x => x.Amount);
                    //lookUp.BankReceipt = query.Count(x => x.PaymentMode == SalePaymentType.Bank);
                    //lookUp.TotalBankReceipt = query.Where(x => x.PaymentMode == SalePaymentType.Bank).Sum(x => x.Amount);
                    //lookUp.TotalCashBank = query.Sum(x => x.Amount);
                    //lookUp.Normal = query.Where(x => x.NatureOfPayment == "Normal").Sum(x => x.Amount);
                    //lookUp.Advance = query.Where(x => x.NatureOfPayment == "Advance Payment").Sum(x => x.Amount);
                    //lookUp.Security = query.Where(x => x.NatureOfPayment == "Security Deposit").Sum(x => x.Amount);
                    //lookUp.TotalNormal = query.Where(x => x.NatureOfPayment == "Security Deposit" || x.NatureOfPayment == "Advance Payment" || x.NatureOfPayment == "Normal").Sum(x => x.Amount);

                    query = query.Where(x => (x.ApprovalStatus == request.ApprovalStatus)).ToList();

                    if (request.FromDate != null)
                    {
                        query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date).ToList();
                    }
                    if (request.ToDate != null)
                    {
                        query = query.Where(x => x.Date.Date <= request.ToDate.Value.Date).ToList();
                    }
                    if (request.PaymentMode == "Cash")
                    {
                        query = query.Where(x => x.PaymentMode == SalePaymentType.Cash).ToList();

                    }
                    else if (request.PaymentMode == "Bank")
                    {
                        query = query.Where(x => x.PaymentMode == SalePaymentType.Bank).ToList();

                    }
                    else if (request.PaymentMode == "Advance")
                    {
                        query = query.Where(x => x.PaymentMode == SalePaymentType.Advance).ToList();

                    }

                    if(request.PaymentMethod == "Card")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.Card).ToList();
                    }
                    else if(request.PaymentMethod == "Cheque")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.Cheque).ToList();
                    }
                    else if(request.PaymentMethod == "Credit Card")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.CreditCard).ToList();
                    }
                    else if(request.PaymentMethod == "Debit Card")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.DebitCard).ToList();
                    }
                    else if(request.PaymentMethod == "Credit Card Master")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.CreditCardMaster).ToList();
                    }
                    else if(request.PaymentMethod == "Transfer")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.Transfer).ToList();
                    }
                    else if(request.PaymentMethod == "American Express")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.AmericanExpress).ToList();
                    }
                    else if(request.PaymentMethod == "Credit Card Visa")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.CreditCardVisa).ToList();
                    }
                    else if(request.PaymentMethod == "Int. Transfer")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.IntTransfer).ToList();
                    }
                    else if(request.PaymentMethod == "Deposit")
                    {
                        query = query.Where(x => x.PaymentMethod == Focus.Domain.Enum.PaymentMethod.Deposit).ToList();
                    }


                    if (!string.IsNullOrEmpty(request.NatureOfPayment) && request.NatureOfPayment.Equals("Created Date", StringComparison.OrdinalIgnoreCase))
                    {
                        if (request.FromDate != null)
                        {
                            DateTime fromDate = request.FromDate.Value.Date;
                            query = query.Where(x => x.Date.Date >= fromDate).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            DateTime toDate = request.ToDate.Value.Date;
                            query = query.Where(x => x.Date.Date <= toDate).ToList();
                        }
                    }
                    else if (!string.IsNullOrEmpty(request.NatureOfPayment) && request.NatureOfPayment.Equals("Voucher Date", StringComparison.OrdinalIgnoreCase))
                    {
                        if (request.FromDate != null)
                        {
                            DateTime fromDate = request.FromDate.Value.Date;
                            query = query.Where(x => x.PaymentDate.Date >= fromDate).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            DateTime toDate = request.ToDate.Value.Date;
                            query = query.Where(x => x.PaymentDate.Date <= toDate).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(request.NewNatureOfPayment) && request.NewNatureOfPayment != "null")
                    {
                        query = query.Where(x => x.NatureOfPayment == request.NewNatureOfPayment).ToList();

                    }


                    if (request.Year != 0 && request.Year != null)
                    {
                        query = query.Where(x => x.Date.Year == request.Year).ToList();

                    }
                    if (request.Month != 0 && request.Month != null)
                    {
                        query = query.Where(x => x.Date.Month == request.Month).ToList();

                    }
                    if (!string.IsNullOrEmpty(request.UserId) && request.UserId != "null" && request.UserId != "undefined")
                    {
                        query = query.Where(x => EF.Property<Guid>(x, "UserId") == Guid.Parse(request.UserId)).ToList();

                    }


                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.VoucherNumber.Contains(searchTerm) 
                                           || x.ContactAccount.Name.Contains(searchTerm) 
                                           || x.Account.Name.Contains(searchTerm) 
                                           || x.Amount.ToString().Contains(searchTerm)
                                           || x.Date.ToString().Contains(searchTerm)).ToList();

                    }
                    query = query.OrderByDescending(x => x.VoucherNumber).ToList();
                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();
                    var branchList = _context.Branches.AsNoTracking().ToList();

                    var pvouchers = new List<PaymentVoucherLookUpModel>();
                    foreach (var item in query)
                    {

                        pvouchers.Add(new PaymentVoucherLookUpModel
                        {
                            Id = item.Id,
                            Date = item.Date,
                            VoucherNumber = item.VoucherNumber,
                            Narration = item.Narration,
                            ChequeNumber = item.ChequeNumber,
                            ApprovalStatus = item.ApprovalStatus,
                            Amount = item.Amount,
                            PaymentVoucherType = item.PaymentVoucherType,
                            BankCashAccountId = item.BankCashAccountId,
                            ContactAccountId = item.ContactAccountId,
                            BankCashAccountName = item.Account.Name,
                            ContactAccountName = item.ContactAccount.Name,
                            DraftBy = item.DraftBy,
                            ApprovedBy = item.ApprovedBy,
                            RejectBy = item.RejectBy,
                            VoidBy = item.VoidBy,
                            Reason = item.Reason,
                            DraftDate = item.DraftDate,
                            ApprovedDate = item.ApprovedDate,
                            RejectDate = item.RejectDate,
                            VoidDate = item.VoidDate,
                            NatureOfPayment = item.NatureOfPayment,
                            PaymentMode = item.PaymentMode,
                            PaymentMethods = item.PaymentMethod.ToString(),
                            PaymentModes = item.PaymentMode.ToString(),
                            BranchCode = branchList.FirstOrDefault(x=>x.Id==item.BranchId)?.Code,
                            IsRefund = item.IsRefund,
                            PaymentDate = item.PaymentDate,
                            InvoiceAmount = item.InvoiceAmount,
                            RemainingBalance = item.RemainingBalance,
                        });
                    }

                    return new PagedResult<PaymentVoucherLookUpWithCalculation>
                    {
                        Results = new PaymentVoucherLookUpWithCalculation
                        {
                            PaymentVouchers = pvouchers,
                            //Posted = lookUp.Posted,
                            //Draft = lookUp.Draft,
                            //Rejected = lookUp.Rejected,
                            //TotalReceipt = lookUp.TotalReceipt,
                            //CashReceipt = lookUp.CashReceipt,
                            //TotalCashReceipt = lookUp.TotalCashReceipt,
                            //BankReceipt = lookUp.BankReceipt,
                            //TotalBankReceipt = lookUp.TotalBankReceipt,
                            //TotalCashBank = lookUp.TotalCashBank,
                            //Normal = lookUp.Normal,
                            //Advance = lookUp.Advance,
                            //Security = lookUp.Security,
                            //TotalNormal = lookUp.TotalNormal,

                        },
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = pvouchers.Count / request.PageSize

                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }

            }
        }
    }
}
