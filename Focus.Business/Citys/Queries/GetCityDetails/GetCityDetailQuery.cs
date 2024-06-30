using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Citys.Queries.GetCityDetails
{
    public class GetCityDetailQuery : IRequest<CityDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCityDetailQuery, CityDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCityDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CityDetailsLookUpModel> Handle(GetCityDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var city = await _context.Cities.FindAsync(request.Id);

                    if (city != null)
                    {
                        return CityDetailsLookUpModel.Create(city);
                    }
                    throw new NotFoundException(nameof(city), request.Id);
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
