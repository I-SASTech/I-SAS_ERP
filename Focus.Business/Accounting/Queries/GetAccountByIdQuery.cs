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
    public class GetAccountByIdQuery : IRequest<AccountLookUpModel>
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }

        public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;

            public GetAccountByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetAccountByIdQuery> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<AccountLookUpModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var accounts = _context.Accounts.AsNoTracking()
                        .Include(x=>x.CostCenter)
                        .ThenInclude(x=>x.Accounts)
                        .AsQueryable();

                    if (request.CompanyId != null)
                    {
                        _context.DisableTenantFilter = true;
                        accounts = _context.Accounts.Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsNoTracking()
                            .Include(x => x.CostCenter)
                            .ThenInclude(x => x.Accounts)
                            .AsQueryable();
                       
                    }
                    var account = await accounts
                        .ProjectTo<AccountLookUpModel>(_mapper.ConfigurationProvider).AsNoTracking()
                        .Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

                    return account;
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