using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;
using System.Collections.Generic;

namespace Focus.Business.Terminals.Queries.GetTerminalList
{
    public class GetTerminalListQuery : IRequest<TerminalListModel>
    {
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
        public bool EditSelected { get; set; }
        public TerminalType TerminalType { get; set; }
        public bool AllShown { get; set; }
        public Guid? CompanyId { get; set; }

        public class Handler : IRequestHandler<GetTerminalListQuery, TerminalListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetTerminalListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<TerminalListModel> Handle(GetTerminalListQuery request, CancellationToken cancellationToken)
            {
                try
                { 
                    var terminals  = _context.Terminals.AsQueryable(); 
                    if (request.CompanyId != null )
                    {
                        _context.DisableTenantFilter = true;
                        terminals = _context.Terminals.Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();
                    }
                        

                    if (request.IsActive)
                    {
                        if (request.AllShown)
                        {
                            terminals = terminals.Where(x => x.TerminalType == request.TerminalType && x.IsActive);

                        }
                        else
                        {
                            terminals = terminals.Where(x =>  x.IsActive);
                            if (!request.IsSelected)
                            {
                                var dayStartList = _context.DayStarts.Where(x => x.IsActive && !x.IsDayStart)
                                    .Select(x => x.CounterId);
                                foreach (var y in dayStartList)
                                {
                                    terminals = terminals.Where(x => x.Id != y).AsQueryable();

                                }
                            }
                        }


                    }
                    else if (request.IsSelected)
                    {
                        terminals = terminals.Where(x => x.IsActive).AsQueryable();
                    }
                    var list = terminals.OrderBy(x=>x.Code).ToList();

                    var terminalsList = new List<TerminalLookUpModel>();
                    foreach(var data in list)
                    {
                        if(data.TerminalUserType== 0)
                        {
                            data.TerminalUserType = TerminalUserType.Offline;

                        }
                        terminalsList.Add(new TerminalLookUpModel
                        {
                            Id = data.Id,
                            MACAddress = data.MACAddress,
                            Code = data.Code,
                            IPAddress = data.IPAddress,
                            isActive = data.IsActive,
                            PrinterName = data.PrinterName,
                            TerminalUserType = data.TerminalUserType.ToString()
                        });
                    }
                   

                    return new TerminalListModel
                    {
                        Terminals = terminalsList
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
