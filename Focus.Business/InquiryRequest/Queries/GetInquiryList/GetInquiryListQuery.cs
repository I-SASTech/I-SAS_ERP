using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Interface;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryList
{
    public class GetInquiryListQuery : PagedRequest, IRequest<PagedResult<List<InquiryLookUpModel>>>
    {
        public Guid? ProcessId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetInquiryListQuery, PagedResult<List<InquiryLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetInquiryListQuery> logger, UserManager<ApplicationUser> userManager, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;
                _contextProvider = contextProvider;
            }
            public async Task<PagedResult<List<InquiryLookUpModel>>> Handle(GetInquiryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var loginPermissions = await _context.LoginPermissions.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == _contextProvider.GetUserId(), cancellationToken: cancellationToken);

                    var totalStatus = _context.InquiryStatusDynamics.Count();
                    if (totalStatus < 3)
                    {
                        var list = new List<string> { "UnAssigned", "Assigned", "TransferTo" };
                        var statusDynamicList = list.Select(status => new InquiryStatusDynamic() { Name = status }).ToList();

                        await _context.InquiryStatusDynamics.AddRangeAsync(statusDynamicList, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    var query = _context.Inquiries.Include(x => x.InquiryProcess).AsQueryable();

                    if (request.BranchId!=null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId);
                    }

                    if (!loginPermissions.AllowViewAllData)
                    {
                        query = query.Where(x => x.CreatedBy == _contextProvider.GetUserId());
                    }

                    if (request.ProcessId != null && request.ProcessId != Guid.Empty)
                    {
                        query = query.Where(x => x.InquiryProcessId == request.ProcessId);
                    }

                    query = query.OrderBy(x => x.InquiryNumber);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                    var inquiryList = new List<InquiryLookUpModel>();

                    foreach (var q in query)
                    {

                        var userName = await _userManager.FindByIdAsync(q.ReceiverId.ToString());
                        inquiryList.Add(new InquiryLookUpModel()
                        {
                            Id = q.Id,
                            InquiryNumber = q.InquiryNumber,
                            Process = q.InquiryProcess?.Name,
                            Status = q.InquiryStatus.LastOrDefault()?.InquiryStatusDynamic.Name,
                            ReceivedBy = userName.UserName,
                            CreatedOn = q.DateTime.ToString("d")
                        });
                    }

                    return new PagedResult<List<InquiryLookUpModel>>
                    {
                        Results = inquiryList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = inquiryList.Count / request.PageSize
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
