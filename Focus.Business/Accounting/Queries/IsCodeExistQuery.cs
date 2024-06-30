using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Accounting.Queries
{
    public class IsCodeExistQuery : IRequest<bool>
    {
        public string Code { get; set; }
        public class IsCodeExistQueryHandler : IRequestHandler<IsCodeExistQuery, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;

            public IsCodeExistQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetAccountsQuery> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;

            }

            public async Task<bool> Handle(IsCodeExistQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var accounts = _context.Accounts.AsQueryable();
                    var isExist = await accounts
                        .ProjectTo<AccountLookUpModel>(_mapper.ConfigurationProvider).AsNoTracking()
                        .AnyAsync(x => x.Code == request.Code, cancellationToken);

                    return isExist;
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