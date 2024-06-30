using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using Focus.Business.TaxRates.Queries.GetTaxRateList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales.Queries.GetSaleDetailForEmail
{
    public class GettaxRateListForEmail : IRequest<TaxRateListModel>
    {
        public bool isActive;
        public Guid CompanyId;

        public class Handler : IRequestHandler<GettaxRateListForEmail, TaxRateListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context,
                ILogger<GettaxRateListForEmail> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<TaxRateListModel> Handle(GettaxRateListForEmail request, CancellationToken cancellationToken)
            {

                try
                {
                    _context.DisableTenantFilter = true;
                    if (request.isActive == true)
                    {
                        var TaxRate = _context.TaxRates.AsNoTracking().Where(x => x.isActive && EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();

                        var TaxRateList = await TaxRate
                            .OrderBy(x => x.Id)
                            .ProjectTo<TaxRateLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new TaxRateListModel
                        {
                            TaxRates = TaxRateList
                        };
                    }
                    if (request.isActive == false)
                    {
                        var TaxRate = _context.TaxRates.AsNoTracking().Where(x=> EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();

                        var companyAccountDetail = _context.CompanyAccountSetups.AsNoTracking().FirstOrDefault(x=> EF.Property<Guid>(x, "CompanyId") == request.CompanyId);

                        var TaxRateList = await TaxRate
                            .OrderBy(x => x.Id)
                            .ProjectTo<TaxRateLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new TaxRateListModel
                        {
                            TaxRates = TaxRateList,
                            TaxMethod = companyAccountDetail?.TaxMethod
                        };
                    }
                    return null;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
