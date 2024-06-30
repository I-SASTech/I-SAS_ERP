using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Branches.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Branches.Queries
{
    public class GetBranchesDetailQuery : IRequest<BranchesModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBranchesDetailQuery, BranchesModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBranchesDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<BranchesModel> Handle(GetBranchesDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var branch = await _context.Branches.FindAsync(request.Id);

                    if (branch == null)
                    {
                        throw new NotFoundException("Branches", request.Id);
                    }
                    

                    return new BranchesModel()
                    {
                        Id = branch.Id,
                        Code = branch.Code,
                        BranchName = branch.BranchName,
                        BranchType = branch.BranchType,
                        ContactNo = branch.ContactNo,
                        Address = branch.Address,
                        City = branch.City,
                        State = branch.State,
                        PostalCode = branch.PostalCode,
                        Country = branch.Country,
                        IsActive = branch.IsActive,
                        MainBranch = branch.MainBranch,
                        IsOnline = branch.IsOnline,
                        IsApproval = branch.IsApproval,
                        IsCentralized = branch.IsCentralized
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
