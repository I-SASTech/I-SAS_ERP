using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using AutoMapper;
using Focus.Business.CompanyActionProcess.Commands;

namespace Focus.Business.CompanyActionProcess.Queries
{
    public class GetProcessListQuery : PagedRequest, IRequest<PagedResult<List<ProcessLookUpModel>>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }
        public string Document { get; set; }

        public class Handler : IRequestHandler<GetProcessListQuery, PagedResult<List<ProcessLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProcessListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<List<ProcessLookUpModel>>> Handle(GetProcessListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.CompanyProcess.AsQueryable();
                    if (request.IsDropdown)
                    {
                        query = query.Where(x => x.Type == request.Document);
                        var process1 = query.Select(x =>
                            new ProcessLookUpModel()
                            {
                                Id = x.Id,
                                ProcessName = x.ProcessName,
                                ProcessNameArabic = x.ProcessNameArabic,
                                Type = x.Type,
                                Status = x.Status,
                            }).ToList();
                        return new PagedResult<List<ProcessLookUpModel>>
                        {
                            Results = process1
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;



                        query = query.Where(x => x.ProcessName.ToLower().Contains(searchTerm.ToLower()) ||
                                                 x.ProcessNameArabic.ToLower().Contains(searchTerm.ToLower()) ||
                                                 x.Type.ToLower().Contains(searchTerm.ToLower()));


                    }
                    query = query.OrderByDescending(x => x.ProcessName);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var process = query.Select(x =>
                        new ProcessLookUpModel()
                        {
                            Id = x.Id,
                            ProcessName = x.ProcessName,
                            ProcessNameArabic = x.ProcessNameArabic,
                            Type = x.Type,
                            Status = x.Status,
                        }).ToList();

                    return new PagedResult<List<ProcessLookUpModel>>
                    {
                        Results = process,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = process.Count / request.PageSize
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
