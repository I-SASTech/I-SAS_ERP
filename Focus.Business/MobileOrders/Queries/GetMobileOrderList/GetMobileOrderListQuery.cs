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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enums;

namespace Focus.Business.MobileOrders.Queries.GetMobileOrderList
{
    public class GetMobileOrderListQuery : PagedRequest, IRequest<PagedResult<List<MobileOrderLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public Guid CustomerId { get;  set; }

        public class Handler : IRequestHandler<GetMobileOrderListQuery, PagedResult<List<MobileOrderLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetMobileOrderListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<MobileOrderLookUpModel>>> Handle(GetMobileOrderListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    
                    {
                        var query = _context.MobileOrders
                            .AsNoTracking()
                            .Where(x=>x.CustomerId==request.CustomerId)
                            .Include(x=>x.MobileOrderItems)
                            .ThenInclude(x=>x.Product)
                        
                            .AsQueryable();
                    
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                           
                           
                                query = query.Where(x =>
                                    x.OrderNo.Contains(searchTerm));
                            
                        }
                        query = query.OrderByDescending(x => x.OrderNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
                        var purchaseOrderList = new List<MobileOrderLookUpModel>();
                        foreach (var purchaseOrder in query)
                        {
                            var sOrder = MobileOrderLookUpModel.Create(purchaseOrder);
                            purchaseOrderList.Add(sOrder);
                        }

                        return new PagedResult<List<MobileOrderLookUpModel>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseOrderList.Count / request.PageSize
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
