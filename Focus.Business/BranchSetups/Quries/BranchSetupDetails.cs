using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchSetups.Model;
using Focus.Domain.Entities;

namespace Focus.Business.BranchSetups.Quries
{
    public class BranchSetupDetails : IRequest<BranchSetupLookupModel>
    {
        public Guid BranchId { get; set; }

        public class Handler : IRequestHandler<BranchSetupDetails, BranchSetupLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<BranchSetupDetails> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<BranchSetupLookupModel> Handle(BranchSetupDetails request, CancellationToken cancellationToken)
            {
                try
                {
                    var prefix = await _context.BranchSetups.Where(x=>x.LocationId==request.BranchId).FirstOrDefaultAsync(cancellationToken);
                    if (prefix == null)
                    {
                       

                        return new BranchSetupLookupModel
                        {
                            Prefix = "B",
                            StartingNumber = "01",

                        };
                    }
                    else
                    {
                        return new BranchSetupLookupModel
                        {
                            Id = prefix.Id,
                            Prefix = prefix.Prefix,
                            StartingNumber = prefix.StartingNumber,
                           
                        };
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Something went wrong " + ex.Message);
                }
            }
        }
    }
}
