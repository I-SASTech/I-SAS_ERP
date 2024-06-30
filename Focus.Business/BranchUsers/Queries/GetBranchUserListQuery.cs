using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchUsers.Models;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.BranchUsers.Queries
{
    public class GetBranchUserListQuery : PagedRequest, IRequest<PagedResult<List<BranchUserModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }

        public class Handler : IRequestHandler<GetBranchUserListQuery, PagedResult<List<BranchUserModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetBranchUserListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;
            }
            public async Task<PagedResult<List<BranchUserModel>>> Handle(GetBranchUserListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.BranchUsers
                        .GroupBy(u => u.UserId)
                        .Select(x => new BranchUserModel
                        {
                            UserId = x.Key,
                            BranchId = x.Select(z => new BranchModel
                            {
                                Id = z.BranchId,
                                Name = z.Branch.Code + " " + z.Branch.BranchName,
                            }).ToList(),
                        }).ToListAsync(cancellationToken: cancellationToken);

                    var userIds = query.Select(x => x.UserId).ToList();
                    var users = await _userManager.Users.Where(z => userIds.Contains(z.Id))
                        .Select(z => new
                        {
                            z.Id,
                            z.UserName,
                        }).ToListAsync(cancellationToken: cancellationToken);


                    foreach (var item in query)
                    {
                        item.UserName = users.FirstOrDefault(x => x.Id == item.UserId)?.UserName;
                    }

                    if (request.IsDropdown)
                    {
                        return new PagedResult<List<BranchUserModel>>
                        {
                            Results = query.ToList(),
                        };

                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();

                            query = query.Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower())).ToList();
                        }

                        query = query.OrderBy(x => x.UserName).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                        return new PagedResult<List<BranchUserModel>>
                        {
                            Results = query.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = query.Count() / request.PageSize
                        };
                    }


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
