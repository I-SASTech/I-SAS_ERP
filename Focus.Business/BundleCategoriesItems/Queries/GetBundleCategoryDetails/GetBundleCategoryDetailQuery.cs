using AutoMapper;
using Focus.Business.BundleCategoriesItems.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryDetails
{
    public class GetBundleCategoryDetailQuery : IRequest<BundleCategoryDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBundleCategoryDetailQuery, BundleCategoryDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBundleCategoryDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<BundleCategoryDetailsLookUpModel> Handle(GetBundleCategoryDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var promotionOffer = await _context.BundleCategories.FindAsync(request.Id);
                    var branchesList = await _context.Branches.AsNoTracking().ToListAsync();

                    if (promotionOffer == null)
                        throw new NotFoundException(nameof(promotionOffer), request.Id);

                    var brand = new BundleCategoryDetailsLookUpModel
                    {
                        Id = promotionOffer.Id,
                        Offer = promotionOffer.Offer,
                        Buy = promotionOffer.Buy,
                        Get = promotionOffer.Get,
                        isActive = promotionOffer.IsActive,
                        QuantityLimit = promotionOffer.QuantityLimit,
                        StockLimit = promotionOffer.StockLimit,
                        FromDate = promotionOffer.FromDate,
                        ToDate = promotionOffer.ToDate,
                        ProductId = promotionOffer.ProductId,
                        Name = promotionOffer.Product.DisplayName,
                        BranchesIdList = promotionOffer.BundleOfferBranches.Select(x => new BundlesOffersBranchesLookupModel
                        {
                            Id = Guid.Parse(x.BranchId.ToString()),
                            Name = branchesList.Count > 0  ? branchesList.FirstOrDefault(y => y.Id == x.BranchId).Code + " " + branchesList.FirstOrDefault(y => y.Id == x.BranchId).BranchName : null,
                        }).ToList(),
                    };
                   return brand;


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
