using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.PriceRecordChanges.Model;
using Focus.Business.PriceRecords;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PriceRecordChanges.Queries
{
    public class PriceLabelProductQuery : IRequest<List<PriceLabelsProuctsListLookupModel>>
    {
        public Guid? PriceLebelId { get; set; }
        public Guid? CustomerId { get; set; }

        public class Handler : IRequestHandler<PriceLabelProductQuery, List<PriceLabelsProuctsListLookupModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<PriceLabelProductQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<PriceLabelsProuctsListLookupModel>> Handle(PriceLabelProductQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PriceRecords
                            .AsNoTracking()
                            .Include(x => x.Product)
                            .Include(x => x.PriceLabeling)
                            .AsQueryable();

                    if(request.PriceLebelId != Guid.Empty)
                    {
                        query = query.Where(x => x.PriceLabelingId == request.PriceLebelId);
                    }

                    if (request.CustomerId != Guid.Empty)
                    {
                        query = query.Where(x => x.CustomerId == request.CustomerId);
                    }

                    var priceRecordList = new List<PriceLabelsProuctsListLookupModel>();
                    foreach (var x in query)
                    {
                        priceRecordList.Add(new PriceLabelsProuctsListLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Price = x.Price,
                            Code = x.Product.Code,
                            DisplayName = string.IsNullOrEmpty(x.Product.DisplayName) ? x.Product.Code + " " + x.Product.EnglishName : x.Product.DisplayName,
                            IsActive = x.IsActive,
                            ProductName = x.Product.EnglishName != null ? x.Product.EnglishName : x.Product.ArabicName,
                            ProductId = x.ProductId,
                            PriceLabelingId = x.PriceLabelingId,
                            PriceLabelName = x.PriceLabeling == null ? "": x.PriceLabeling.Name,
                            SalePrice = x.SalePrice,
                            PurchasePrice = x.PurchasePrice,
                            CostPrice = x.CostPrice,
                            NewPrice = x.NewPrice,
                            CustomerId = x.CustomerId,
                            IsCustomer = x.IsCustomer
                        });
                    }

                    return priceRecordList;
             
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
