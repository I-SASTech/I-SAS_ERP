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

namespace Focus.Business.PrintSettings.Queries.GetPrintSettingsList
{
    public class GetPrintSettingListQuery : IRequest<PrintSettingListModel>
    {
        public bool IsActive;

        public class Handler : IRequestHandler<GetPrintSettingListQuery, PrintSettingListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetPrintSettingListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PrintSettingListModel> Handle(GetPrintSettingListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var printSettings = _context.PrintSettings.AsNoTracking().AsQueryable();
                    
                    var printSettingList = await printSettings.OrderBy(x => x.Id)
                     .ProjectTo<PrintSettingLookUpModel>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
                    return new PrintSettingListModel
                    {
                        PrintSettings = printSettingList
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

