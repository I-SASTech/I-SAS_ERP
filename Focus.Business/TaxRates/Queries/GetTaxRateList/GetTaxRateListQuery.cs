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
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.TaxRates.Queries.GetTaxRateList
{
    public class GetTaxRateListQuery : IRequest<TaxRateListModel>
    {
        public bool isActive;

        public bool IsEmail { get; set; }

        public class Handler : IRequestHandler<GetTaxRateListQuery, TaxRateListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context,
                ILogger<GetTaxRateListQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<TaxRateListModel> Handle(GetTaxRateListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.IsEmail)
                        _context.DisableTenantFilter = true;
                    if (request.isActive == true)
                    {
                        var TaxRate = _context.TaxRates.AsNoTracking().Where(x=>x.isActive).AsQueryable();

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
                        var TaxRate = _context.TaxRates.AsNoTracking().AsQueryable();
                       if(TaxRate.All(x=>x.Rate!=0))
                        {
                            var autoNo = await _mediator.Send(new GetTaxRateCodeQuery());
                            var taxRate = new Domain.Entities.TaxRate
                            {
                                Name = "Vat Exempted",
                                NameArabic = "معفاة من ضريبة القيمة المضافة",
                                Description = "Vat Exempted",
                                Code = autoNo,
                                Rate = 0,
                                isActive = true
                            };

                            //Add Department to database
                            await _context.TaxRates.AddAsync(taxRate, cancellationToken);
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);

                        }
                        var companyAccountDetail = _context.CompanyAccountSetups.AsNoTracking().FirstOrDefault();

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
