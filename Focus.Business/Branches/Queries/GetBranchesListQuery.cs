using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Branches.Models;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Branches.Queries
{
    public class GetBranchesListQuery : PagedRequest, IRequest<PagedResult<List<BranchesModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }
        public bool IsLayout { get; set; }
        public Guid LocationId { get; set; }

        public class Handler : IRequestHandler<GetBranchesListQuery, PagedResult<List<BranchesModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(IApplicationDbContext context, ILogger<GetBranchesListQuery> logger, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;
            }
            public async Task<PagedResult<List<BranchesModel>>> Handle(GetBranchesListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<BranchesModel> query;

                    if (request.IsLayout)
                    {
                        var userId = _contextProvider.GetUserId();
                        var branchIds = await _context.BranchUsers
                            .Where(x => x.UserId.ToLower() == userId.ToString().ToLower())
                            .Select(x => x.BranchId).ToListAsync(cancellationToken: cancellationToken);

                        query =await _context.Branches
                            .AsNoTracking()
                            .Where(x => branchIds.Contains(x.Id))
                            .Select(branch => new BranchesModel
                            {
                                Id = branch.Id,
                                BusinessId = branch.BusinessId,
                                Code = branch.Code,
                                BranchName = branch.BranchName,
                                BranchType = branch.BranchType,
                                ContactNo = branch.ContactNo,
                                Address = branch.Address,
                                City = branch.City,
                                State = branch.State,
                                PostalCode = branch.PostalCode,
                                Country = branch.Country,
                                MainBranch = branch.MainBranch,
                                IsActive = branch.IsActive,
                                IsApproval = branch.IsApproval,
                                IsCentralized = branch.IsCentralized,
                                IsOnline = branch.IsOnline,
                            }).ToListAsync(cancellationToken: cancellationToken);
                    }
                    else
                    {


                         query = (
                            from branch in _context.Branches.AsNoTracking()
                            where branch.LocationId == request.LocationId
                            select new BranchesModel
                            {
                                Id = branch.Id,
                                BusinessId = branch.BusinessId,
                                Code = branch.Code,
                                BranchName = branch.BranchName,
                                BranchType = branch.BranchType,
                                ContactNo = branch.ContactNo,
                                Address = branch.Address,
                                City = branch.City,
                                State = branch.State,
                                PostalCode = branch.PostalCode,
                                Country = branch.Country,
                                MainBranch = branch.MainBranch,
                                IsActive = branch.IsActive,
                                IsApproval = branch.IsApproval,
                                IsCentralized = branch.IsCentralized,
                                IsOnline = branch.IsOnline,
                            }
                        ).ToList();


                      
                    }



                    if (request.IsDropdown)
                    {
                        return new PagedResult<List<BranchesModel>>
                        {
                            Results = query,
                        };

                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();

                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.BranchName.ToLower().Contains(searchTerm.ToLower())).ToList();
                        }

                        query = query.OrderBy(x => x.Code).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                        return new PagedResult<List<BranchesModel>>
                        {
                            Results = query,
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
