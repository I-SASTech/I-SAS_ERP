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

namespace Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeList
{
    public class GetAllowanceTypeListQuery : IRequest<AllowanceTypeListModel>
    {
        public bool IsDropdown { get; set; }

        public class Handler : IRequestHandler<GetAllowanceTypeListQuery, AllowanceTypeListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetAllowanceTypeListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AllowanceTypeListModel> Handle(GetAllowanceTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                   
                        var allowanceTypes = _context.AllowanceTypes.AsQueryable();

                        if (request.IsDropdown)
                        {
                            var allowanceTypesLists = await allowanceTypes
                                .OrderBy(x => x.Id)
                                .Where(x=>x.IsActive)
                                .ProjectTo<AllowanceTypeLookUpModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

                            return new AllowanceTypeListModel
                            {
                                AllowanceTypes = allowanceTypesLists
                            };
                        }
                        var allowanceTypesList = await allowanceTypes
                            .OrderBy(x => x.Id)
                            .ProjectTo<AllowanceTypeLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new AllowanceTypeListModel
                        {
                            AllowanceTypes = allowanceTypesList
                        };
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
