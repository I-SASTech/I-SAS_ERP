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
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.HoldTypeSetup.Queries
{
    public class GetHoldSetupDetailsQuery : IRequest<HoldSetupLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetHoldSetupDetailsQuery, HoldSetupLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context,
                ILogger<GetHoldSetupDetailsQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<HoldSetupLookupModel> Handle(GetHoldSetupDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var hold = await _context.HoldSetups.FirstOrDefaultAsync();
                    if(hold == null)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var holdSetup = new HoldSetup
                        {
                            HoldRecordType = "5 Months",
                            IsActive = true,
                        };

                        await _context.HoldSetups.AddAsync(holdSetup);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

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
