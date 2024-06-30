using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.CompaniesOption.Commands.AddUpdateOption;

namespace Focus.Business.CompaniesOption.Queries
{
    public class GetCompanyOptionListQuery : IRequest<List<CompanyOptionLookUp>>
    {
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetCompanyOptionListQuery, List<CompanyOptionLookUp>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCompanyOptionListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<CompanyOptionLookUp>> Handle(GetCompanyOptionListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.CompanyOptions.AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        query = query.Where(x =>
                            x.Label.Contains(request.SearchTerm));

                    }
                    var list = new List<CompanyOptionLookUp>();

                    foreach (var item in query)
                    {
                        list.Add(new CompanyOptionLookUp()
                        {
                            Id = item.Id,
                            LocationId = item.LocationId,
                            Label = item.Label,
                            Value = item.Value,
                            OptionValue = item.OptionValue,
                        });
                    }


                    return list;




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
