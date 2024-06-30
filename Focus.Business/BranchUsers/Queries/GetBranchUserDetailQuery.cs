using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchUsers.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.BranchUsers.Queries
{
    public class GetBranchUserDetailQuery : IRequest<BranchUserModel>
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<GetBranchUserDetailQuery, BranchUserModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBranchUserDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<BranchUserModel> Handle(GetBranchUserDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var branch = await _context.BranchUsers
                        .Where(x => x.UserId == request.Id)
                        .Select(x => new
                        {
                            x.UserId,
                            x.BranchId,
                            x.Branch.BranchName,
                            x.Branch.Code,
                        }).ToListAsync(cancellationToken: cancellationToken);

                    if (branch == null)
                    {
                        throw new NotFoundException("Branches", request.Id);
                    }


                    return new BranchUserModel()
                    {
                        UserId = request.Id,
                        BranchId = branch.Select(x => new BranchModel
                        {
                            Id = x.BranchId,
                            Name = x.Code + " " + x.BranchName,
                        }).ToList(),
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
