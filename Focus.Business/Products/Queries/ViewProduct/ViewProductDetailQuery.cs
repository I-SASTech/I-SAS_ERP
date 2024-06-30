using AutoMapper;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Products.Queries.ViewProduct
{
  public  class ViewProductDetailQuery : IRequest<ViewProductLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<ViewProductDetailQuery, ViewProductLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ViewProductDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ViewProductLookUpModel> Handle(ViewProductDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = _context.Products
                        .Include(x => x.Category)
                        .Include(x => x.Brand)
                        .Include(x => x.Unit)
                        .Include(x => x.Size)
                        .Include(x => x.Color)
                        .Include(x => x.Origin)
                        .AsNoTracking().AsQueryable();

                  
                  
                   var product = await products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                var   subCategories = new SubCategory();
                    if(product.SubCategoryId!=null)
                    {
                        subCategories = _context.SubCategories.AsNoTracking().FirstOrDefault(x => x.Id == product.SubCategoryId);

                    }



                    return new ViewProductLookUpModel
                    {
                        Id = product.Id,
                        Code = product.Code,
                        EnglishName = product.EnglishName,
                        ArabicName = product.ArabicName,
                        CategoryNameEn = product.Category == null ? "" : product.Category.Name,
                        CategoryNameAr = product.Category == null ? "" : product.Category.NameArabic,
                        SubCategoryNameEn = subCategories==null?"":subCategories.Name,
                        SubCategoryNameAr= subCategories==null?"":subCategories.NameArabic,
                        BrandNameEn = product.Brand == null ? "" : product.Brand.Name,
                        BrandNameAr = product.Brand == null ? "" : product.Brand.NameArabic,
                        UnitNameEn = product.Unit == null ? "" : product.Unit.Name,
                        UnitNameAr = product.Unit == null ? "" : product.Unit.NameArabic,
                        SizeNameEn = product.Size == null ? "" : product.Size.Name,
                        SizeNameAr = product.Size == null ? "" : product.Size.NameArabic,
                        ColorNameEn = product.Color == null ? "" : product.Color.Name,
                        ColorNameAr = product.Color == null ? "" : product.Color.NameArabic,
                        OriginNameEn = product.Origin == null ? "" : product.Origin.Name,
                        OriginNameAr = product.Origin == null ? "" : product.Origin.NameArabic,
                        Barcode = product.BarCode,
                        Length = product.Length,
                        Width = product.Width,
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
                        Image = product.Image,
                        IsRaw = product.IsRaw,
                        LevelOneUnit = product.LevelOneUnit,
                        BasicUnit = product.BasicUnit,
                        UnitPerPack = product.UnitPerPack,
                        SalePriceUnit = product.SalePriceUnit,
                        Assortment = product.Assortment,
                        StyleNumber = product.StyleNumber

                    };

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
