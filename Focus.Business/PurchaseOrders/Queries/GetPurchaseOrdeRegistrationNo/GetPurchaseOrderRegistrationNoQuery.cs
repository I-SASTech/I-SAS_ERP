using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Prefix.Quries;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseOrders.Queries.GetPurchaseOrdeRegistrationNo
{
    public class GetPurchaseOrderRegistrationNoQuery : IRequest<string>
    {
        public Guid? TerminalId { get; set; }
        public Guid? UserId { get; set; }
        public string InvoicePrefix { get; set; }
        public string DocumentType { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPurchaseOrderRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly UserManager<ApplicationUser> _userManager;



            public Handler(IApplicationDbContext context, ILogger<GetPurchaseOrderRegistrationNoQuery> logger, IMediator mediator, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _userManager = userManager;



            }

            public async Task<string> Handle(GetPurchaseOrderRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string branchCode = "";
                    string purchaseOrder;

                    if (request.DocumentType == "SupplierQuotation")
                    {
                        if (request.BranchId != null)
                        {
                            purchaseOrder = await _context.PurchaseOrders
                                .AsNoTracking()
                                .Where(x => x.DocumentType == "SupplierQuotation" && x.BranchId == request.BranchId)
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
                            purchaseOrder = await _context.PurchaseOrders
                                .AsNoTracking()
                                .Where(x => x.DocumentType == "SupplierQuotation")
                                .Select(x => x.RegistrationNo)
                                .OrderBy(x => x)
                                .LastOrDefaultAsync(cancellationToken);
                        }

                        if (purchaseOrder != null)
                        {
                            if (string.IsNullOrEmpty(purchaseOrder))
                            {
                                return GenerateCodeFirstTime(branchCode);
                            }

                            return GenerateNewCode(purchaseOrder, branchCode);
                        }

                        return GenerateCodeFirstTime(branchCode);

                    }



                    if (request.BranchId != null)
                    {
                        purchaseOrder = await _context.PurchaseOrders
                            .Where(x => (x.DocumentType == "" || x.DocumentType == null) && x.BranchId == request.BranchId)
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
                        purchaseOrder = await _context.PurchaseOrders
                            .Where(x => x.DocumentType == "" || x.DocumentType == null)
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

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder))
                        {
                            return branchCode + prefixRecord.POrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";

                        }

                        string fetchNo = purchaseOrder.Substring(purchaseOrder.Length - 5);
                        Int32 autoNo = Convert.ToInt32(fetchNo);
                        var format = "00000";
                        autoNo++;
                        var newCode1 = prefixRecord.POrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        return branchCode + newCode1;
                    }

                    return branchCode + prefixRecord.POrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            private string GenerateCodeFirstTime(string branchCode)
            {
                return branchCode + "SQ-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "SQ-" + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }
    }
}

