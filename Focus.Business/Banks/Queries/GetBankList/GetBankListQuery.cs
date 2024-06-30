using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Banks.Queries.GetBankList
{
    public class GetBankListQuery : IRequest<BankListModel>
    {
        public bool isActive { get; set; }

        public class Handler : IRequestHandler<GetBankListQuery, BankListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBankListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<BankListModel> Handle(GetBankListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                   
                    {
                        if (request.isActive)
                        {
                            var banks = _context.Banks.AsNoTracking().Include(x => x.Currency).Where(x=>x.Active).AsQueryable();

                            var banksList = await banks
                                .OrderBy(x => x.Code)
                                .ProjectTo<BankLookUpModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

                            return new BankListModel
                            {
                                Banks = banksList
                            };
                        }
                        else
                        {
                            var banks = _context.Banks.AsNoTracking().Include(x => x.Currency).AsQueryable();

                            var banksList = await banks
                                .OrderBy(x => x.Code)
                                .ProjectTo<BankLookUpModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

                            return new BankListModel
                            {
                                Banks = banksList
                            };
                        }
                       
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
