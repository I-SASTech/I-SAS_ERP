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
using Focus.Domain.Enum;
using Focus.Domain.Enums;

namespace Focus.Business.PriceRecords.Queries.GetPriceRecordList
{
    public class GetPriceRecordListQuery : PagedRequest, IRequest<PagedResult<List<PriceRecordLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string IsActiveValue { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? OriginId { get; set; }
        public Guid? ProdutMasterId { get; set; }
        public Guid? CategoryId { get; set; }

        public class Handler : IRequestHandler<GetPriceRecordListQuery, PagedResult<List<PriceRecordLookupModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetPriceRecordListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<PriceRecordLookupModel>>> Handle(GetPriceRecordListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.PriceRecords
                        .AsNoTracking()
                        .Include(x => x.Product)
                        .Include(x => x.PriceLabeling)
                        .Where(x => (request.CategoryId == null || x.Product.CategoryId == request.CategoryId.Value) &&
                                    (request.SubCategoryId == null || x.Product.SubCategoryId == request.SubCategoryId) &&
                                    (request.SizeId == null || x.Product.SizeId == request.SizeId) &&
                                    (request.ProdutMasterId == null || x.Product.ProductMasterId == request.ProdutMasterId) &&
                                    (string.IsNullOrEmpty(request.IsActiveValue) || x.IsActive == (request.IsActiveValue == "Active"))
                        )
                        .Where(x => string.IsNullOrEmpty(request.SearchTerm) ||
                                    x.Product.EnglishName.Contains(request.SearchTerm) ||
                                    x.Product.ArabicName.Contains(request.SearchTerm) ||
                                    x.Product.Code.Contains(request.SearchTerm)
                        )
                        .OrderBy(x => x.Id)
                        .Select(x => new PriceRecordLookupModel
                        {
                            Id = x.Id,
                            Code = x.Product.Code,
                            ProductId = x.ProductId,
                            EnglishName = x.Product.EnglishName,
                            ArabicName = x.Product.ArabicName,
                            DisplayName = x.Product.DisplayName,
                            SalePrice = x.Product.SalePrice,
                            PurchasePrice = x.Product.PurchasePrice,
                            CostPrice = x.Product.CostPrice ?? 0,
                            Name = x.Name,
                            Price = x.Price,
                            NewPrice = x.NewPrice,
                            IsActive = x.IsActive,
                            PriceLabelingId = x.PriceLabelingId,
                            PriceLabelName = x.PriceLabeling == null ? "" : x.PriceLabeling.Name
                        });

                    var count = query.Count();
                    var pagedQuery = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
                    var priceRecordList = await pagedQuery.ToListAsync();

                    return new PagedResult<List<PriceRecordLookupModel>>
                    {
                        Results = priceRecordList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = (int)Math.Ceiling((double)count / request.PageSize)
                    };

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
