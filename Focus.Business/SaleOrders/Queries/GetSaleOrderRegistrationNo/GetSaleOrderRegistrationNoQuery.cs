using Focus.Business.Interface;
using Focus.Business.Prefix.Quries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Sales.Queries.GetSaleRegistrationNo;

namespace Focus.Business.SaleOrders.Queries.GetSaleOrderRegistrationNo
{
    public class GetSaleOrderRegistrationNoQuery : IRequest<RegistrationNoLookUp>
    {
        public bool IsQuotation { get; set; }
        public bool IsSaleOrderTracking { get; set; }
        public string QuotationType { get; set; }
        public Guid? TerminalId { get; set; }
        public string InvoicePrefix { get; set; }
        public string DocumentNo { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetSaleOrderRegistrationNoQuery, RegistrationNoLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(IApplicationDbContext context, ILogger<GetSaleOrderRegistrationNoQuery> logger, IMediator mediator, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }

            public async Task<RegistrationNoLookUp> Handle(GetSaleOrderRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    SaleOrder saleOrder;
                    string branchCode = "";

                    if (request.BranchId != null)
                    {
                        if (request.InvoicePrefix == "MachineWisePrefix")
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsQuotation == request.IsQuotation && x.BranchId==request.BranchId && x.TerminalId == request.TerminalId, cancellationToken);
                        }
                        else if (request.InvoicePrefix == "EmployeeWisePrefix")
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsQuotation == request.IsQuotation && x.BranchId == request.BranchId && x.CreatedBy == _contextProvider.GetUserId(), cancellationToken);
                        }
                        else if (request.IsSaleOrderTracking)
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsSaleOrderTracking && x.BranchId == request.BranchId, cancellationToken);
                        }
                        else
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsQuotation == request.IsQuotation && x.BranchId == request.BranchId && !x.IsSaleOrderTracking, cancellationToken);
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
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsQuotation == request.IsQuotation && x.TerminalId == request.TerminalId, cancellationToken);
                        }
                        else if (request.InvoicePrefix == "EmployeeWisePrefix")
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsQuotation == request.IsQuotation && x.CreatedBy == _contextProvider.GetUserId(), cancellationToken);
                        }
                        else if (request.IsSaleOrderTracking)
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsSaleOrderTracking, cancellationToken);
                        }
                        else
                        {
                            saleOrder = await _context.SaleOrders.AsNoTracking().OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(x => x.IsQuotation == request.IsQuotation && !x.IsSaleOrderTracking, cancellationToken);
                        }
                    }


                    var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);


                    string middleCode = string.Empty;
                    if (string.Equals(request.InvoicePrefix, "MachineWisePrefix"))
                    {
                        var terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.TerminalId.ToString()), cancellationToken: cancellationToken);
                        if (terminal == null)
                            throw new ApplicationException("Terminal Id not null");
                        middleCode = terminal.Code.Substring(0, 1) + Convert.ToInt32(terminal.Code.Substring(5));

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
                                middleCode = user.Code.Substring(0, 1) + Convert.ToInt32(user.Code.Substring(5));

                            }
                        }
                        else
                        {
                            middleCode = user.Code.Substring(0, 1) + Convert.ToInt32(user.Code.Substring(5));
                        }
                    }

                    if (saleOrder != null)
                    {
                        if (string.IsNullOrEmpty(saleOrder.RegistrationNo))
                        {
                            if (request.IsQuotation)
                            {
                                if (request.QuotationType == "Proposal")
                                {
                                    request.DocumentNo = branchCode + "PR" + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                                }
                                request.DocumentNo = branchCode + prefixRecord.SQutation + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                            }
                            request.DocumentNo = branchCode + prefixRecord.SOrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        else
                        {
                            string fetchNo = saleOrder.RegistrationNo.Substring(saleOrder.RegistrationNo.Length - 5);
                            //fetchNo = (string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3));
                            Int32 autoNo = Convert.ToInt32((fetchNo));
                            var format = "00000";
                            autoNo++;
                            string newCode1;
                            if (request.IsQuotation)
                            {
                                if (request.QuotationType == "Proposal")
                                {
                                    var newCode = "PR" + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                                    request.DocumentNo = branchCode + newCode;
                                }
                                else
                                {
                                    var newCode = prefixRecord.SQutation + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                                    request.DocumentNo = branchCode + newCode;
                                }

                            }

                            else if (request.IsSaleOrderTracking)
                            {
                                newCode1 = prefixRecord.SOrdeTracking + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                                request.DocumentNo = branchCode + newCode1;
                            }
                            else
                            {
                                newCode1 = prefixRecord.SOrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                                request.DocumentNo = branchCode + newCode1;
                            }

                        }
                    }
                    else
                    {
                        if (request.IsQuotation)
                        {
                            if (request.QuotationType == "Proposal")
                            {
                                request.DocumentNo = branchCode + "PR" + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                            }
                            request.DocumentNo = branchCode + prefixRecord.SQutation + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        else if (request.IsSaleOrderTracking)
                        {
                            request.DocumentNo = branchCode + prefixRecord.SOrdeTracking + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        else
                        {
                            request.DocumentNo = branchCode + prefixRecord.SOrder + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";

                        }
                    }


                    return new RegistrationNoLookUp()
                    {
                        DocumentNo = request.DocumentNo,
                        prefixiesLookupModel = prefixRecord
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

