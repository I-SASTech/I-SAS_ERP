using Focus.Business.BranchSetups.Model;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BranchSetups.Commands
{
    public class AddUpdateBranchSetupCommand : IRequest<Guid>
    {
        public BranchSetupLookupModel Pre { get; set; }

        public class Handler : IRequestHandler<AddUpdateBranchSetupCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateBranchSetupCommand> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Guid> Handle(AddUpdateBranchSetupCommand request, CancellationToken cancellationToken)
            {
                try
                {


                    


                    var prefix = _context.BranchSetups.FirstOrDefault(x=>x.LocationId==request.Pre.LocationId);
                    if (prefix != null)
                    {
                        prefix.StartingNumber = request.Pre.StartingNumber;
                        prefix.Prefix = request.Pre.Prefix;
                        prefix.EndNumber = request.Pre.EndNumber;
                        prefix.LocationId = request.Pre.LocationId;
                       

                        await _context.SaveChangesAsync(cancellationToken);
                        return prefix.Id;
                    }
                    else
                    {
                        var branchsetup = new BranchSetup
                        {
                            StartingNumber = request.Pre.StartingNumber,
                            EndNumber = request.Pre.EndNumber,
                            LocationId = request.Pre.LocationId,
                           
                        };
                        await _context.BranchSetups.AddAsync(branchsetup);
                        await _context.SaveChangesAsync(cancellationToken);
                        return branchsetup.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
