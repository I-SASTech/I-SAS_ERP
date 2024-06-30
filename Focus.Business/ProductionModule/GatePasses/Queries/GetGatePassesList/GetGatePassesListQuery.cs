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

namespace Focus.Business.ProductionModule.GatePasses.Queries.GetGatePassesList
{
    public class GetGatePassesListQuery : PagedRequest, IRequest<PagedResult<List<GatePassesLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetGatePassesListQuery, PagedResult<List<GatePassesLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetGatePassesListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<GatePassesLookUpModel>>> Handle(GetGatePassesListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown == true)
                    {
                        var po = _context.GatePasses
                            .AsNoTracking()
                            .AsQueryable();



                        var gatepassesList = new List<GatePassesLookUpModel>();
                        foreach (var item in po)
                        {
                            gatepassesList.Add(new GatePassesLookUpModel()
                            {
                                Id = item.Id,
                                RegistrationNo = item.RegistrationNo,
                                ToDate = item.ToDate,
                                FromDate=item.FromDate,
                                IsActive = item.IsActive,
                               
                               
                            });
                        }


                        return new PagedResult<List<GatePassesLookUpModel>>
                        {
                            Results = gatepassesList
                        };

                    }
                    else
                    {
                        var query = _context.GatePasses
                            .AsNoTracking()
                            .AsQueryable();
                        /*if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }*/
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                                x.RegistrationNo.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                        }
                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var gatepassesList = new List<GatePassesLookUpModel>();
                        foreach (var item in query)
                        {
                            gatepassesList.Add(new GatePassesLookUpModel()
                            {
                                Id = item.Id,
                                RegistrationNo = item.RegistrationNo,
                                FromDate = item.FromDate,
                                ToDate = item.ToDate,
                                Date = item.Date,
                                
                            });
                        }



                        return new PagedResult<List<GatePassesLookUpModel>>
                        {
                            Results = gatepassesList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = gatepassesList.Count / request.PageSize
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
