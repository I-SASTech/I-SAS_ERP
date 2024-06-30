using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.JournalVouchers.Queries.GetJournalVoucherList
{
    public class GetJournalVoucherListQuery : PagedRequest, IRequest<PagedResult<List<JournalVoucherLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsOpeningCash { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetJournalVoucherListQuery, PagedResult<List<JournalVoucherLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetJournalVoucherListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;
            }

            public async Task<PagedResult<List<JournalVoucherLookUpModel>>> Handle(GetJournalVoucherListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    int count;
                    _context.DisableTenantFilter = true;

                    var query = await _context.JournalVouchers
                        .Where(x => x.OpeningCash == request.IsOpeningCash && !x.IsStockTransfer)
                        .Select(x => new
                        {
                            x.Id,
                            x.Date,
                            x.Narration,
                            x.Comments,
                            x.OneTimeEntry,
                            x.OpeningCash,
                            x.VoucherNumber,
                            x.ApprovalStatus,
                            x.BranchId,
                            TotalDr = x.JournalVoucherItems.Sum(z => z.Debit),
                            TotalCr = x.JournalVoucherItems.Sum(z => z.Credit),
                            CreatedById = EF.Property<string>(x, "CreatedById")
                        }).OrderBy(x => x.VoucherNumber)
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (request.BranchId != null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId).ToList();
                    }

                    //query = query.Where(x => x.OpeningCash == request.IsOpeningCash);

                    if (request.IsDropDown)
                    {
                        var journalVoucherList12 = query
                            .Select(x => new JournalVoucherLookUpModel
                            {
                                Id = x.Id,
                                Date = x.Date,
                                Narration = x.Narration,
                                Comments = x.Comments,
                                OneTimeEntry = x.OneTimeEntry,
                                VoucherNumber = x.VoucherNumber,
                                ApprovalStatus = x.ApprovalStatus.ToString(),
                                TotalDr = x.TotalDr,
                                TotalCr = x.TotalCr,
                                CreatedUser = _userManager.Users.FirstOrDefault(z => z.Id == x.CreatedById)?.UserName,
                            }).ToList();

                        return new PagedResult<List<JournalVoucherLookUpModel>>
                        {
                            Results = journalVoucherList12
                        };
                    }



                    if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                    {
                        query = query.Where(x => x.ApprovalStatus == request.Status).ToList();
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.VoucherNumber.Contains(searchTerm)).ToList();
                    }

                    count = query.Count();

                    //var usersList = _userManager.Users.ToList();
                    var branchList = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                    query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var journalVoucherList = query
                        .Select(journalVoucher => new JournalVoucherLookUpModel
                        {
                            Id = journalVoucher.Id,
                            Date = journalVoucher.Date,
                            Narration = journalVoucher.Narration,
                            Comments = journalVoucher.Comments,
                            CreatedUser = _userManager.Users.FirstOrDefault(z => z.Id == journalVoucher.CreatedById)?.UserName,
                            VoucherNumber = journalVoucher.VoucherNumber,
                            TotalDr = journalVoucher.TotalDr,
                            TotalCr = journalVoucher.TotalCr,
                            ApprovalStatus = journalVoucher.ApprovalStatus.ToString(),
                            OneTimeEntry = journalVoucher.OneTimeEntry,
                            BranchCode = branchList.FirstOrDefault(x => x.Id == journalVoucher.BranchId)?.Code,
                        }).ToList();

                    return new PagedResult<List<JournalVoucherLookUpModel>>
                    {
                        Results = journalVoucherList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = count / request.PageSize
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
