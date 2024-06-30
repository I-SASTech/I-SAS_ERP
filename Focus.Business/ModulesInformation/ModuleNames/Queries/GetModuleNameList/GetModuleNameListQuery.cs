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

namespace Focus.Business.ModulesInformation.ModuleNames.Queries.GetModuleNameList
{
    public class GetModuleNameListQuery : IRequest<List<ModuleNameLookUpModel>>
    {
        public bool IsActive { get; set; }

        public class Handler : IRequestHandler<GetModuleNameListQuery, List<ModuleNameLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetModuleNameListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<ModuleNameLookUpModel>> Handle(GetModuleNameListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var moduleNames = _context.NobleModules.AsQueryable();

                    var moduleNamesList = from mn in moduleNames
                        select new ModuleNameLookUpModel
                        {
                            NewId = mn.Id,
                            Name = mn.ModuleName
                        };
                    return moduleNamesList.ToList();
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
