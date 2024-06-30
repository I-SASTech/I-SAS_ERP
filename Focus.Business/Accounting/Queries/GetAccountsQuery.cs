using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Focus.Business.Accounting.Queries
{
    public class GetAccountsQuery : IRequest<AccountTemplateListModel>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }
        public Guid? CompanyId { get; set; }

        public class GetMembersListQueryHandler : IRequestHandler<GetAccountsQuery, AccountTemplateListModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            public GetMembersListQueryHandler(IApplicationDbContext context, IMapper mapper, 
                IUserHttpContextProvider httpContextProvider,
                IMemoryCache memoryCache)
            {
                _context = context;
                _mapper = mapper;
                _httpContextProvider = httpContextProvider;
                _memoryCache = memoryCache;
            }

            public async Task<AccountTemplateListModel> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
            {
                var accountTypeList = new List<AccountTypeLookupModel>();

                var accountType = _context.AccountTypes.AsQueryable();
                if (request.CompanyId != null)
                {
                    _context.DisableTenantFilter = true;
                    accountType = _context.AccountTypes.Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();
                }
               
                accountTypeList = await accountType.OrderBy(x => x.Name)
               .ProjectTo<AccountTypeLookupModel>(_mapper.ConfigurationProvider).AsNoTracking()
               .ToListAsync(cancellationToken);
                return new AccountTemplateListModel
                {
                    AccountTypes = accountTypeList
                };
            }
        }
    }
}
