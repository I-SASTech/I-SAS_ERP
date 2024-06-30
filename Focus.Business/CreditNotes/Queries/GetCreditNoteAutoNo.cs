using Focus.Business.Interface;
using Focus.Business.Prefix.Quries;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.CreditNotes.Queries
{
    public class GetCreditNoteAutoNo : IRequest<string>
    {
        public bool IsCreditNote { get; set; }
        public string TerminalId { get; set; }
        public string InvoicePrefix { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetCreditNoteAutoNo, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(IApplicationDbContext context, ILogger<GetCreditNoteAutoNo> logger, IMediator mediator, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }

            public async Task<string> Handle(GetCreditNoteAutoNo request, CancellationToken cancellationToken)
            {
                try
                {
                    CreditNote creditNote;
                    string branchCode = "";
                    if (request.BranchId != null)
                    {
                        if (request.InvoicePrefix == "MachineWisePrefix")
                        {
                            creditNote = await _context.CreditNotes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(x => x.IsCreditNote == request.IsCreditNote && x.BranchId == request.BranchId && x.TerminalId == Guid.Parse(request.TerminalId), cancellationToken);
                        }
                        else if (request.InvoicePrefix == "EmployeeWisePrefix")
                        {
                            creditNote = await _context.CreditNotes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(x => x.IsCreditNote == request.IsCreditNote && x.BranchId == request.BranchId && x.CreatedBy == _contextProvider.GetUserId(), cancellationToken);
                        }
                        else
                        {
                            creditNote = await _context.CreditNotes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(x => x.IsCreditNote == request.IsCreditNote && x.BranchId == request.BranchId, cancellationToken);
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
                            creditNote = await _context.CreditNotes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(x => x.IsCreditNote == request.IsCreditNote && x.TerminalId == Guid.Parse(request.TerminalId), cancellationToken);
                        }
                        else if (request.InvoicePrefix == "EmployeeWisePrefix")
                        {
                            creditNote = await _context.CreditNotes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(x => x.IsCreditNote == request.IsCreditNote && x.CreatedBy == _contextProvider.GetUserId(), cancellationToken);
                        }
                        else
                        {
                            creditNote = await _context.CreditNotes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(x => x.IsCreditNote == request.IsCreditNote, cancellationToken);
                        }
                    }


                    var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);


                    string middleCode = string.Empty;
                    if (string.Equals(request.InvoicePrefix, "MachineWisePrefix"))
                    {
                        var terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.TerminalId.ToString()), cancellationToken: cancellationToken);
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
                            string code = null;
                            if (userForCode != null)
                            {
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

                    if (creditNote != null)
                    {
                        if (string.IsNullOrEmpty(creditNote.Code))
                        {
                            if (request.IsCreditNote)
                            {
                                return branchCode + prefixRecord.CreditNote + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                            }
                            return branchCode + prefixRecord.DebitNote + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        else
                        {
                            string fetchNo = creditNote.Code.Substring(creditNote.Code.Length - 5);
                            //fetchNo = (string.IsNullOrEmpty(middleCode) ? fetchNo : fetchNo.Substring(3));
                            Int32 autoNo = Convert.ToInt32((fetchNo));
                            var format = "00000";
                            autoNo++;
                            if (request.IsCreditNote)
                            {
                                var newCode = prefixRecord.CreditNote + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                                return branchCode + newCode;
                            }
                            else
                            {
                                return branchCode + prefixRecord.DebitNote + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                            }
                        }
                    }

                    if (request.IsCreditNote)
                    {
                        return branchCode + prefixRecord.CreditNote + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }
                    else
                    {
                        return branchCode + prefixRecord.DebitNote + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";

                    }
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
