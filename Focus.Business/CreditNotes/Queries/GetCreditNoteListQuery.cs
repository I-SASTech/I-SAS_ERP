using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.CreditNotes.Model;
using Focus.Domain.Enum;

namespace Focus.Business.CreditNotes.Queries
{
    public class GetCreditNoteListQuery : PagedRequest, IRequest<PagedResult<List<CreditNotesModel>>>
    {
        public bool IsDropdown;
        public bool IsCreditNote;
        public string SearchTerm { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid? BranchId { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? TerminalId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? UserId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }

        public class Handler : IRequestHandler<GetCreditNoteListQuery, PagedResult<List<CreditNotesModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCreditNoteListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<CreditNotesModel>>> Handle(GetCreditNoteListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                    var query = await _context.CreditNotes
                        .Where(x => x.IsCreditNote == request.IsCreditNote && x.ApprovalStatus == request.ApprovalStatus)
                        .Select(x => new CreditNotesModel
                        {
                            Id = x.Id,
                            Code = x.Code,
                            Date = x.Date,
                            EnglishName = x.Contact.EnglishName??"",
                            ArabicName = x.Contact.ArabicName ?? "",
                            CustomerDisplayName = x.Contact.CustomerDisplayName ?? "",
                            IsDefault = x.Contact.IsAllowEmail,
                            CustomerCode = x.Contact.Code,
                            VatAmount = x.VatAmount,
                            Amount = x.Amount,
                            BranchId = x.BranchId,
                            CustomerEmail = x.Contact.Email,
                            BranchCode = branches.Count > 0 ? branches.FirstOrDefault(z => z.Id == x.BranchId).Code : "",
                        }).ToListAsync(cancellationToken: cancellationToken);

                    if (request.BranchId != null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId).ToList();
                    }

                    if (request.IsDropdown)
                    {
                        return new PagedResult<List<CreditNotesModel>>
                        {
                            Results = query,
                        };
                    }
                    if (request.FromDate != null)
                    {
                        query = query.Where(x => x.Date.Date >= request.FromDate).ToList();
                    }

                    if (request.ToDate != null)
                    {
                        query = query.Where(x => x.Date.Date <= request.ToDate).ToList();
                    }
                    if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                    {
                        query = query.Where(x => x.ContactId == request.CustomerId).ToList();
                    }

                    if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                    {
                        query = query.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year).ToList();

                    }

                    if (request.UserId != null)
                    {
                        query = query.Where(x => x.CreatedBy == request.UserId).ToList();
                    }
                    if (request.TerminalId != null)
                    {
                        query = query.Where(x => x.TerminalId == request.TerminalId).ToList();
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                            {
                                var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower())
                                            || x.CustomerDisplayName.ToLower().Contains(searchTerm.ToLower())
                                            || x.EnglishName.ToLower().Contains(searchTerm.ToLower())
                                            || x.ArabicName.ToLower().Contains(searchTerm)
                                            || x.CustomerCode.ToLower().Contains(searchTerm.ToLower())
                                            || x.Amount.ToString().Contains(searchTerm)
                                            ).ToList();

                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    return new PagedResult<List<CreditNotesModel>>
                    {
                        Results = query,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = query.Count() / request.PageSize
                    };


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
