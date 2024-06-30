using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.HoldTypeSetup.Models;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading; 
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.HoldTypeSetup.Queries
{
    public class GetPermanentDeleteHoldSetupDetailsQuery : IRequest<HoldSetupLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPermanentDeleteHoldSetupDetailsQuery, HoldSetupLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetHoldSetupDetailsQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<HoldSetupLookupModel> Handle(GetPermanentDeleteHoldSetupDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var hold = await _context.PermanentDeleteHoldSetups.FirstOrDefaultAsync();
                    if(hold == null)
                    {
                        var holdSetup = new PermanentDeleteHoldSetup
                        {
                            HoldRecordType = "1 Month",
                            IsActive = true,
                        };

                        await _context.PermanentDeleteHoldSetups.AddAsync(holdSetup);
                        await _context.SaveChangesAsync();

                        return new HoldSetupLookupModel
                        {
                            Id = holdSetup.Id,
                            HoldRecordType = holdSetup.HoldRecordType,
                            IsActive = holdSetup.IsActive,
                        };
                    }
                    else
                    {
                        return new HoldSetupLookupModel
                        {
                            Id = hold.Id,
                            HoldRecordType = hold.HoldRecordType,
                            IsActive = hold.IsActive,
                        };
                    }
                    throw new NotFoundException(nameof(hold), request.Id);
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
