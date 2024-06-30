using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Sales.Queries.GetSaleRegistrationNo;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Prefix.Quries;

namespace Focus.Business.Purchases.Queries.GetPurchaseRegistrationNo
{
    public class GetPurchaseRegistrationNoQuery : IRequest<RegistrationNoLookUp>
    {
        public Guid? TerminalId { get; set; }
        public Guid? UserId { get; set; }
        public string InvoicePrefix { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPurchaseRegistrationNoQuery, RegistrationNoLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetPurchaseRegistrationNoQuery> logger, IMediator mediator, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _userManager = userManager;
            }

            public async Task<RegistrationNoLookUp> Handle(GetPurchaseRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string purchaseDraft;
                    string purchasePost;
                    string purchaseReturn;
                    string branchCode = "";
                    if (request.BranchId != null)
                    {
                        purchaseDraft = await _context.PurchasePosts
                            .Where(x => !x.IsPurchasePost && !x.IsPurchaseReturn && x.BranchId == request.BranchId)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                           .LastOrDefaultAsync(cancellationToken);

                        purchasePost = await _context.PurchasePosts
                            .Where(x => x.IsPurchasePost && !x.IsPurchaseReturn && x.BranchId == request.BranchId)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                           .LastOrDefaultAsync(cancellationToken);

                        purchaseReturn = await _context.PurchasePosts
                            .Where(x => x.IsPurchaseReturn && x.BranchId == request.BranchId)
                            .Select(x => x.RegistrationNo)
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
                        purchaseDraft = await _context.PurchasePosts
                            .Where(x => !x.IsPurchasePost && !x.IsPurchaseReturn)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);

                        purchasePost = await _context.PurchasePosts
                            .Where(x => x.IsPurchasePost && !x.IsPurchaseReturn)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);

                        purchaseReturn = await _context.PurchasePosts
                            .Where(x => x.IsPurchaseReturn)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                    var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);
                    string middleCode = string.Empty;
                    if (string.Equals(request.InvoicePrefix, "MachineWisePrefix"))
                    {
                        var terminal = await _context.Terminals.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.TerminalId.ToString()), cancellationToken: cancellationToken);
                        if (terminal == null)
                            throw new ApplicationException("Terminal Id not null");
                        middleCode = terminal.Code[0] + terminal.Code[^1].ToString();
                    }
                    if (string.Equals(request.InvoicePrefix, "EmployeeWisePrefix"))
                    {
                        var user = _userManager.Users.FirstOrDefaultAsync(x => x.Id == (request.UserId.ToString()), cancellationToken: cancellationToken);
                        if (user == null)
                            throw new ApplicationException("User not null");
                        middleCode = user.Result.Code[0] + user.Result.Code[^1].ToString();
                    }
                    var registration = new RegistrationNoLookUp();

                    //Purchase Draft Auto Number
                    if (purchaseDraft != null)
                    {
                        if (string.IsNullOrEmpty(purchaseDraft))
                        {
                            registration.Draft = branchCode + prefixRecord.PurchaseInvoiceDraft + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        string fetchNo = purchaseDraft.Substring(purchaseDraft.Length - 5);
                        Int32 autoNo = Convert.ToInt32(fetchNo);
                        var format = "00000";
                        autoNo++;
                        registration.Draft = branchCode + prefixRecord.PurchaseInvoiceDraft + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                    }
                    else
                    {
                        registration.Draft = branchCode + prefixRecord.PurchaseInvoiceDraft + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }

                    //Purchase Post Auto Number
                    if (purchasePost != null)
                    {
                        if (string.IsNullOrEmpty(purchasePost))
                        {
                            registration.Post = branchCode + prefixRecord.PInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        string fetchNo = purchasePost.Substring(purchasePost.Length - 5);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        registration.Post = branchCode + prefixRecord.PInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                    }
                    else
                    {
                        registration.Post = branchCode + prefixRecord.PInvoice + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";

                    }

                    //Purchase Return Auto Number
                    if (purchaseReturn != null)
                    {
                        if (string.IsNullOrEmpty(purchaseReturn))
                        {
                            registration.PurchaseReturn = branchCode + prefixRecord.PReturn + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        string fetchNo = purchaseReturn.Substring(purchaseReturn.Length - 5);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        registration.PurchaseReturn = branchCode + prefixRecord.PReturn + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                    }
                    else
                    {
                        registration.PurchaseReturn = branchCode + prefixRecord.PReturn + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }

                    return new RegistrationNoLookUp()
                    {
                        Draft = registration.Draft,
                        Post = registration.Post,
                        PurchaseReturn = registration.PurchaseReturn
                    };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}

