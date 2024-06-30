using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalList
{
    public class GetBankPosTerminalListQuery : IRequest<BankPosTerminalListModel>
    {
        public bool isActive { get; set; }
        public Guid? BankId { get; set; }

        public class Handler : IRequestHandler<GetBankPosTerminalListQuery, BankPosTerminalListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBankPosTerminalListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<BankPosTerminalListModel> Handle(GetBankPosTerminalListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isActive == false)
                    {
                        var bankPosTerminals = _context.BankPosTerminals.AsNoTracking().Include(x=>x.Account).AsQueryable();

                        var bankPosTerminalsList = await bankPosTerminals
                            .OrderBy(x => x.Id)
                            .ProjectTo<BankPosTerminalLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new BankPosTerminalListModel
                        {
                            BankPosTerminals = bankPosTerminalsList
                        };
                    }
                    if (request.isActive == true)
                    {
                        var bankPosTerminals = _context.BankPosTerminals.AsNoTracking().Include(x => x.Account).Where(x=>x.BankId==request.BankId).AsQueryable();

                        var bankPosTerminalsList = await bankPosTerminals
                            .OrderBy(x => x.Id)
                            .Where(x => x.isActive == true)
                            .ProjectTo<BankPosTerminalLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new BankPosTerminalListModel
                        {
                            BankPosTerminals = bankPosTerminalsList
                        };
                    }
                    return null;
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
