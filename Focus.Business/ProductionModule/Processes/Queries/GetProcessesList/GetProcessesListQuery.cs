using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.ProductionModule.Processes.Queries.GetProcessesList
{
    public class GetProcessesListQuery : PagedRequest, IRequest<PagedResult<List<ProcessesLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetProcessesListQuery, PagedResult<List<ProcessesLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetProcessesListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<ProcessesLookUpModel>>> Handle(GetProcessesListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown == true)
                    {
                        var po =await _context.Processes
                            .AsNoTracking()
                            .Include(x => x.ProcessContractors)
                            .ThenInclude(x => x.Contractor)
                            .ToListAsync();



                        var processList = new List<ProcessesLookUpModel>();
                        foreach (var item in po)
                        {
                            processList.Add(new ProcessesLookUpModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                Color = item.Color,
                                EnglishName = item.EnglishName,
                                Description=item.Description,
                                IsActive = item.IsActive,
                                ProcessContractors = item.ProcessContractors.Select(x =>
                                    new ProcessContractorLookUpModel()
                                    {
                                        Id = x.Id,
                                        ContractorId = x.ContractorId,
                                        ProcessId = x.ProcessId,
                                        ContractorNameEn = x.Contractor.EnglishName,
                                        ContractorNameAr = x.Contractor.ArabicName,
                                    }).ToList(),
                               
                            });
                        }


                        return new PagedResult<List<ProcessesLookUpModel>>
                        {
                            Results = processList
                        };

                    }
                    else
                    {
                        var query = _context.Processes
                            .AsNoTracking()
                            .Include(x=>x.ProcessContractors)
                            .ThenInclude(x=>x.Contractor)
                            .AsQueryable();
                        /*if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }*/
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                                x.Code.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                        }
                        query = query.OrderByDescending(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var processList = new List<ProcessesLookUpModel>();
                        foreach (var item in query)
                        {
                            processList.Add(new ProcessesLookUpModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                Description = item.Description,
                                EnglishName = item.EnglishName,
                                Date = item.Date,
                                ProcessContractors = item.ProcessContractors.Select(x =>
                                    new ProcessContractorLookUpModel()
                                    {
                                        Id = x.Id,
                                        ContractorNameEn = x.Contractor.EnglishName,
                                        ContractorNameAr = x.Contractor.ArabicName,
                                    }).ToList(),
                            });
                        }



                        return new PagedResult<List<ProcessesLookUpModel>>
                        {
                            Results = processList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = processList.Count / request.PageSize
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
