using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Interface;
using Focus.Business.Interface;
using Focus.Business.Prefix.Quries;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Sales.Queries.GetSaleRegistrationNo
{
    public class GetSaleRegistrationNoQuery : IRequest<RegistrationNoLookUp>
    {
        public Guid? BranchId { get; set; }
        public string UserId { get; set; }
        public string TerminalId { get; set; }
        public string InvoicePrefix { get; set; }
        public class Handler : IRequestHandler<GetSaleRegistrationNoQuery, RegistrationNoLookUp>
        {
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(ILogger<GetSaleRegistrationNoQuery> logger, IUserHttpContextProvider contextProvider, IApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager)
            {
                _logger = logger;
                _contextProvider = contextProvider;
                _context = context;
                _mediator = mediator;
                _userManager = userManager;
            }
            public async Task<RegistrationNoLookUp> Handle(GetSaleRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string saleHold;
                    string saleInvoice;
                    string saleReturn;
                    string saleProformaInvoice;
                    string purchaseOrder;
                    string globalInvoice;
                    string receiptInvoice;
                    string branchCode = "";

                    if (request.BranchId != null)
                    {
                        if (request.InvoicePrefix == "MachineWisePrefix")
                        {
                            saleReturn = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleReturn && x.BranchId == request.BranchId && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleHold = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoiceHold && x.BranchId == request.BranchId && x.InvoiceType == InvoiceType.Hold && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoice && x.BranchId == request.BranchId && !x.IgnoreCashCreditForNumber && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleProformaInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ProformaInvoice && x.BranchId == request.BranchId && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            purchaseOrder = await _context.Sales.Where(x => x.DocumentType == DocumentType.PurchaseOrder && x.BranchId == request.BranchId && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            globalInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.GlobalInvoice && x.BranchId == request.BranchId && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            receiptInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ReceiptInvoice && x.BranchId == request.BranchId && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                        else if (request.InvoicePrefix == "EmployeeWisePrefix")
                        {
                            saleReturn = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleReturn && x.BranchId == request.BranchId && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleHold = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoiceHold && x.BranchId == request.BranchId && x.InvoiceType == InvoiceType.Hold && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoice && x.BranchId == request.BranchId && !x.IgnoreCashCreditForNumber && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleProformaInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ProformaInvoice && x.BranchId == request.BranchId && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            purchaseOrder = await _context.Sales.Where(x => x.DocumentType == DocumentType.PurchaseOrder && x.BranchId == request.BranchId && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            globalInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.GlobalInvoice && x.BranchId == request.BranchId && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            receiptInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ReceiptInvoice && x.BranchId == request.BranchId && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                        else
                        {
                            saleReturn = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleReturn && x.BranchId == request.BranchId).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleHold = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoiceHold && x.BranchId == request.BranchId && x.InvoiceType == InvoiceType.Hold).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoice && x.BranchId == request.BranchId && !x.IgnoreCashCreditForNumber).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleProformaInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ProformaInvoice && x.BranchId == request.BranchId).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            purchaseOrder = await _context.Sales.Where(x => x.DocumentType == DocumentType.PurchaseOrder && x.BranchId == request.BranchId).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            globalInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.GlobalInvoice && x.BranchId == request.BranchId).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            receiptInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ReceiptInvoice && x.BranchId == request.BranchId).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        }

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
                        if (request.InvoicePrefix == "MachineWisePrefix")
                        {
                            saleReturn = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleReturn && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleHold = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoiceHold && x.InvoiceType == InvoiceType.Hold && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoice && !x.IgnoreCashCreditForNumber && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleProformaInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ProformaInvoice && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            purchaseOrder = await _context.Sales.Where(x => x.DocumentType == DocumentType.PurchaseOrder && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            globalInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.GlobalInvoice && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            receiptInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ReceiptInvoice && x.TerminalId == Guid.Parse(request.TerminalId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                        else if (request.InvoicePrefix == "EmployeeWisePrefix")
                        {
                            saleReturn = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleReturn && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleHold = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoiceHold && x.InvoiceType == InvoiceType.Hold && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoice && !x.IgnoreCashCreditForNumber && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleProformaInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ProformaInvoice && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            purchaseOrder = await _context.Sales.Where(x => x.DocumentType == DocumentType.PurchaseOrder && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            globalInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.GlobalInvoice && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            receiptInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ReceiptInvoice && x.CreatedBy == Guid.Parse(request.UserId)).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                        else
                        {
                            saleReturn = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleReturn).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleHold = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoiceHold && x.InvoiceType == InvoiceType.Hold).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.SaleInvoice && !x.IgnoreCashCreditForNumber).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            saleProformaInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ProformaInvoice).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            purchaseOrder = await _context.Sales.Where(x => x.DocumentType == DocumentType.PurchaseOrder).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            globalInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.GlobalInvoice).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);

                            receiptInvoice = await _context.Sales.Where(x => x.DocumentType == DocumentType.ReceiptInvoice).Select(x => x.RegistrationNo).OrderBy(x => x).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                    }



                    var registration = new RegistrationNoLookUp();
                    var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);


                    string middleCode = string.Empty;
                    if (string.Equals(request.InvoicePrefix, "MachineWisePrefix"))
                    {
                        var terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.TerminalId), cancellationToken: cancellationToken);
                        if (terminal == null)
                            throw new ApplicationException("Terminal Id not null");
                        middleCode = terminal.Code[0] + terminal.Code[^1].ToString();
                    }
                    else if (string.Equals(request.InvoicePrefix, "EmployeeWisePrefix"))
                    {
                        var user = await _userManager.FindByIdAsync(_contextProvider.GetUserId().ToString());
                        if (user == null)
                            throw new ApplicationException("User Id not null");
                        if (user.Code == null)
                        {
                            var userForCode = _userManager.Users.OrderBy(x => x.Code).LastOrDefault();
                            if (userForCode != null)
                            {
                                string code;
                                if (string.IsNullOrEmpty(userForCode.Code))
                                {
                                    code = "U" + "-" + "00001";
                                }
                                else
                                {
                                    string fetchNo = userForCode.Code.Substring(userForCode.Code.Length - 5);
                                    //fetchNo =  fetchNo.Substring(fetchNo.Length-5);
                                    Int32 autoNo = Convert.ToInt32((fetchNo));
                                    var format = "00000";
                                    autoNo++;
                                    code = "U" + "-" + autoNo.ToString(format);
                                }
                                user.Code = code;
                                await _userManager.UpdateAsync(user);
                                middleCode = user.Code[0] + user.Code[^1].ToString();

                            }
                        }
                        else
                        {
                            middleCode = user.Code[0] + user.Code[^1].ToString();
                        }
                    }


                    //Global Invoice Auto Number
                    if (globalInvoice != null && !string.IsNullOrEmpty(globalInvoice))
                    {
                        string fetchNo = globalInvoice.Substring(globalInvoice.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        var newCode = prefixRecord.GlobalInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        registration.GlobalInvoice = branchCode + newCode;
                    }
                    else
                    {
                        registration.GlobalInvoice = branchCode + prefixRecord.GlobalInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }


                    //Receipt Invoice
                    if (receiptInvoice != null && !string.IsNullOrEmpty(receiptInvoice))
                    {
                        string fetchNo = receiptInvoice.Substring(receiptInvoice.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        var newCode = prefixRecord.ReceiptInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        registration.ReceiptInvoice = branchCode + newCode;
                    }
                    else
                    {
                        registration.ReceiptInvoice = branchCode + prefixRecord.ReceiptInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }


                    //Sale Return Auto Number
                    if (saleReturn != null && !string.IsNullOrEmpty(saleReturn))
                    {

                        string fetchNo = saleReturn.Substring(saleReturn.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        var newCode = prefixRecord.SReturn + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        registration.SaleReturn = branchCode + newCode;
                    }
                    else
                    {
                        registration.SaleReturn = branchCode + prefixRecord.SReturn + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }


                    //Sale Hold Auto Number
                    if (saleHold != null && !string.IsNullOrEmpty(saleHold))
                    {
                        string fetchNo = saleHold.Substring(saleHold.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        var newCode = prefixRecord.SaleInvoiceHold + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        registration.Hold = branchCode + newCode;
                    }
                    else
                    {
                        registration.Hold = branchCode + prefixRecord.SaleInvoiceHold + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }


                    //Sale Invoice Auto Number
                    if (saleInvoice != null && !string.IsNullOrEmpty(saleInvoice))
                    {

                        string fetchNo = saleInvoice.Substring(saleInvoice.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        registration.Paid = branchCode + prefixRecord.SInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                    }
                    else
                    {
                        registration.Paid = branchCode + prefixRecord.SInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }


                    //Sale ProformaInvoice Auto Number
                    if (saleProformaInvoice != null && !string.IsNullOrEmpty(saleProformaInvoice))
                    {
                        string fetchNo = saleProformaInvoice.Substring(saleProformaInvoice.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        int autoNo = Convert.ToInt32((fetchNo));
                        const string format = "00000";
                        autoNo++;
                        var newCode = prefixRecord.ProformaSaleInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        registration.ProformaInvoice = branchCode + newCode;
                    }
                    else
                    {
                        registration.ProformaInvoice = branchCode + prefixRecord.ProformaSaleInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }


                    //Customer Purchase Order
                    if (purchaseOrder != null && !string.IsNullOrEmpty(purchaseOrder))
                    {
                        string fetchNo = purchaseOrder.Substring(purchaseOrder.Length - 5);
                        //fetchNo = string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3);
                        int autoNo = Convert.ToInt32((fetchNo));
                        const string format = "00000";
                        autoNo++;
                        var newCode = prefixRecord.POrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        registration.PurchaseOrder = branchCode + newCode;
                    }
                    else
                    {
                        registration.PurchaseOrder = branchCode + prefixRecord.POrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }

                    return new RegistrationNoLookUp()
                    {
                        GlobalInvoice = registration.GlobalInvoice,
                        ReceiptInvoice = registration.ReceiptInvoice,
                        Paid = registration.Paid,
                        Hold = registration.Hold,
                        SaleReturn = registration.SaleReturn,
                        ProformaInvoice = registration.ProformaInvoice,
                        PurchaseOrder = registration.PurchaseOrder,
                        prefixiesLookupModel = prefixRecord
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}