using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.BundleCategoriesItems.Models;
using Focus.Business.Interface;
using Focus.Business.PriceRecords;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Products.Queries.GetProductDetails
{
    public class GetProductDetailQuery:IRequest<ProductDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }



        public class Handler : IRequestHandler<GetProductDetailQuery, ProductDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<GetProductDetailQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProductDetailLookUpModel> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    Product product;
                    if (request.BarCode != null)
                    {
                        product = await _context.Products
                            .AsNoTracking()
                            .Include(x => x.ProductSizes)
                            .ThenInclude(x => x.Size)
                            .Include(x => x.BranchItems)
                            .FirstOrDefaultAsync(x=>x.BarCode == request.BarCode,cancellationToken);
                       
                    }
                    else
                    {
                        product = await _context.Products
                            .AsNoTracking()
                            .Include(x => x.PriceRecords)
                            .Include(x => x.ProductSizes)
                            .ThenInclude(x => x.Size)
                            .Include(x => x.BranchItems)
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                    }

                    var branchesList = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                    var productDetails = new ProductDetailLookUpModel
                    {
                        Id = product.Id,
                        Code = product.Code,
                        CostPrice = product.CostPrice ?? 0,
                        CostValue = product.CostValue ?? 0,
                        CostSign = string.IsNullOrEmpty(product.CostSign) ? "F" : product.CostSign,
                        HsCode = product.HsCode,
                        ProductMasterId = product.ProductMasterId,
                        EnglishName = product.EnglishName,
                        ArabicName = product.ArabicName,
                        CategoryId = product.CategoryId,
                        SubCategoryId = product.SubCategoryId,
                        BrandId = product.BrandId,
                        UnitId = product.UnitId,
                        SizeId = product.SizeId,
                        ColorId = product.ColorId,
                        Barcode = request.BarCode != null ? request.BarCode : product.BarCode,
                        Length = product.Length,
                        Width = product.Width,
                        TaxRateId = product.TaxRateId,
                        TaxMethod = product.TaxMethod,
                        SalePrice = product.SalePrice,
                        WholesalePrice = product.WholesalePrice,
                        OriginId = product.OriginId,
                        StockLevel = product.StockLevel,
                        SaleReturnDays = product.SaleReturnDays,
                        Description = product.Description,
                        Shelf = product.Shelf,
                        IsExpire = product.IsExpire,
                        IsActive = product.IsActive,
                        //BundelOffer = request.BundleCategory == null ? null : request.BundleCategory.Offer,
                        //BundelBuy = request.BundleCategory == null ? 0M : request.BundleCategory.Buy,
                        //BundelGet = request.BundleCategory == null ? 0M : request.BundleCategory.Get,
                        //BundelisActive = request.BundleCategory == null ? false : request.BundleCategory.isActive,
                        //PromotionsOffer = request.PromotionOffer == null ? null : request.PromotionOffer.Offer,
                        //PromotionsFromDate = request.PromotionOffer == null ? null : request.PromotionOffer.FromDate,
                        //PromotionsToDate = request.PromotionOffer == null ? null : request.PromotionOffer.ToDate,
                        Image = product.Image,
                        IsRaw = product.IsRaw,
                        LevelOneUnit = product.LevelOneUnit,
                        BasicUnit = product.BasicUnit,
                        UnitPerPack = product.UnitPerPack,
                        SalePriceUnit = product.SalePriceUnit,
                        Assortment = product.Assortment,
                        StyleNumber = product.StyleNumber,
                        Serial = product.Serial,
                        Guarantee = product.Guarantee,
                        ServiceItem = product.ServiceItem,
                        HighUnitPrice = product.HighUnitPrice,
                        WholesaleQuantity = product.WholesaleQuantity,
                        SchemeQuantity = product.SchemeQuantity,
                        Scheme = product.Scheme,
                        PurchasePrice = product.PurchasePrice,
                        ProductGroupId = product.ProductGroupId,
                        DisplayName = product.DisplayName,
                        SizeIdList = product.ProductSizes.Select(x => x.SizeId).ToList(),
                        SKU = product.SKU,
                        PartNumber = product.PartNumber,
                        DisplayNameForPrint = product.DisplayNameForPrint,
                        PriceRecords = product.PriceRecords.Select(x => new PriceRecordModelForProduct
                        {
                            PriceLabelingId = x.PriceLabelingId,
                            Price = x.Price,
                            NewPrice = x.NewPrice,
                            IsActive = x.IsActive
                        }).ToList(),

                        BranchesIdList = product.BranchItems.Count > 0 ? product.BranchItems.Select(x => new BundlesOffersBranchesLookupModel
                        {
                            Id = Guid.Parse(x.BranchId.ToString()),
                            Name = branchesList.Count > 0 ? branchesList.FirstOrDefault(y => y.Id == x.BranchId)?.Code + " " + branchesList.FirstOrDefault(y => y.Id == x.BranchId)?.BranchName : null,
                        }).ToList() : new List<BundlesOffersBranchesLookupModel>(),
                    };

                    return productDetails;


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
