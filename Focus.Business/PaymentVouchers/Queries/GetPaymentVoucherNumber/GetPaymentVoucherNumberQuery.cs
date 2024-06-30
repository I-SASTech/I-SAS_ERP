using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using Focus.Business.Interface;
using Focus.Business.Prefix.Quries;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherNumber
{
    public class GetPaymentVoucherNumberQuery : IRequest<string>
    {
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPaymentVoucherNumberQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetPaymentVoucherNumberQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<string> Handle(GetPaymentVoucherNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string autoCode;
                    string branchCode = "";

                    var prefixes = await _mediator.Send(new PrefixiesDetails(), cancellationToken);
                    
                    if (request.BranchId != null)
                    {
                        autoCode = await _context.PaymentVouchers
                            .Where(x => x.PaymentVoucherType == request.PaymentVoucherType && x.BranchId == request.BranchId)
                            .Select(x => x.VoucherNumber)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);

                        var branch = await _context.Branches
                            .Where(x => x.Id == request.BranchId)
                            .Select(x => x.Code)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        string firstLetter = branch.Substring(0, 1);
                        var fetchNo = Convert.ToInt32(branch.Substring(2));
                        branchCode = firstLetter + fetchNo + "-";
                    }
                    else
                    {
                        autoCode = await _context.PaymentVouchers
                            .Where(x => x.PaymentVoucherType == request.PaymentVoucherType)
                            .Select(x => x.VoucherNumber)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode))
                        {
                            return GenerateCodeFirstTime(request.PaymentVoucherType, branchCode, prefixes.SupplierPayReceipt, prefixes.AdvancePurchaseReceipt, prefixes.CustomerPayReceipt,prefixes.AdvanceSaleReceipt);
                        }
                        return GenerateNewCode(autoCode, request.PaymentVoucherType, branchCode, prefixes.SupplierPayReceipt, prefixes.AdvancePurchaseReceipt, prefixes.CustomerPayReceipt, prefixes.AdvanceSaleReceipt);
                    }

                    return GenerateCodeFirstTime(request.PaymentVoucherType, branchCode, prefixes.SupplierPayReceipt, prefixes.AdvancePurchaseReceipt, prefixes.CustomerPayReceipt, prefixes.AdvanceSaleReceipt);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
            private string GenerateCodeFirstTime(PaymentVoucherType type, string branchCode, string SupplierPayReceipt, string AdvancePurchaseReceipt, string CustomerPayReceipt, string AdvanceSaleReceipt)
            {
                if (PaymentVoucherType.BankPay == type)
                {
                    return branchCode+ SupplierPayReceipt + "-00001";
                }
                if (PaymentVoucherType.CashReceipt == type)
                {
                    return branchCode + "CR-00001";
                }

                if (PaymentVoucherType.AdvanceReceipt == type)
                {
                    return branchCode + AdvanceSaleReceipt + "-00001";
                }
                if (PaymentVoucherType.AdvancePurchase == type)
                {
                    return branchCode + AdvancePurchaseReceipt +  "-00001";
                }

                if (PaymentVoucherType.AdvancePay == type)
                {
                    return branchCode + "AP-00001";
                }
                if (PaymentVoucherType.ContractorAdvancePay == type)
                {
                    return branchCode + "CA-00001";
                }
                if (PaymentVoucherType.AdvanceExpense == type)
                {
                    return branchCode + "AE-00001";
                }
                if (PaymentVoucherType.PettyCash == type)
                {
                    return branchCode + "PC-00001";
                }

                return branchCode + CustomerPayReceipt + "-00001";
            }

            private string GenerateNewCode(string soNumber, PaymentVoucherType type, string branchCode, string SupplierPayReceipt, string AdvancePurchaseReceipt, string CustomerPayReceipt, string AdvanceSaleReceip)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                string prefix;
                if (PaymentVoucherType.BankPay == type)
                {
                    prefix = SupplierPayReceipt + "-";
                }
                else if (PaymentVoucherType.CashReceipt == type)
                {
                    prefix = "CR-";
                }
                else if (PaymentVoucherType.AdvanceReceipt == type)
                {
                    prefix = AdvanceSaleReceip + "-";
                }
                else if (PaymentVoucherType.AdvancePay == type)
                {
                    prefix = "AP-";
                }
                else if (PaymentVoucherType.ContractorAdvancePay == type)
                {
                    prefix = "CA-";
                }
                else if (PaymentVoucherType.PettyCash == type)
                {
                    prefix = "PC-";
                }
                else if (PaymentVoucherType.AdvanceExpense == type)
                {
                    prefix = "AE-";
                } else if (PaymentVoucherType.AdvancePurchase == type)
                {
                    prefix = AdvancePurchaseReceipt + "-";
                }
                else
                {
                    prefix = CustomerPayReceipt + "-";
                }
                var newCode = prefix + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }
    }
}
