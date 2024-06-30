using AutoMapper;
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
using Focus.Business.BomSetup.Models;

namespace Focus.Business.BomSetup.Queries
{
    public class GetBomListQuery : PagedRequest, IRequest<PagedResult<BomsLookupModel>>
    {
        public bool IsDropdown { get; set; }
        public string SearchTerm { get; set; }
        public string ApprovalStatus { get; set; }
        public class Handler : IRequestHandler<GetBomListQuery, PagedResult<BomsLookupModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBomListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<BomsLookupModel>> Handle(GetBomListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.Boms.Where(x => x.ApprovalStatus == request.ApprovalStatus).ToListAsync(cancellationToken: cancellationToken);
                    var saleOrders = await _context.SaleOrders.ToListAsync(cancellationToken: cancellationToken);
                    if (request.IsDropdown == false)
                    {

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower())).ToList();

                        }
                        query = query.OrderBy(x => x.Code).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                        var bomList = new List<BomsLookupModel>();

                        foreach (var item in query)
                        {
                            var saleOrderCode = saleOrders.FirstOrDefault(x => x.Id == item.SaleOrderId)?.RegistrationNo;
                            bomList.Add(new BomsLookupModel
                            {
                                Id = item.Id,
                                Code = item.Code,
                                SaleOrderId = item.SaleOrderId,
                                Date= item.Date,
                                WareHouseId = item.WareHouseId,
                                CreatedDate = item.CreatedDate,
                                Status = item.Status,
                                SaleOrder = saleOrderCode,
                            });
                        }

                        return new PagedResult<BomsLookupModel>
                        {


                            Results = new BomsLookupModel
                            {
                                BomsList = bomList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = bomList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        var bomList = new List<BomsLookupModel>();

                        foreach (var item in query)
                        {
                            var saleOrderCode = saleOrders.FirstOrDefault(x => x.Id == item.SaleOrderId)?.RegistrationNo;
                            bomList.Add(new BomsLookupModel
                            {
                                Id = item.Id,
                                Code = item.Code,
                                SaleOrderId = item.SaleOrderId,
                                Date = item.Date,
                                WareHouseId = item.WareHouseId,
                                CreatedDate = item.CreatedDate,
                                SaleOrder = saleOrderCode,
                                Status = item.Status,
                            });
                        }


                        return new PagedResult<BomsLookupModel>
                        {
                            Results = new BomsLookupModel
                            {
                                BomsList = bomList
                            },
                        };
                    }


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
