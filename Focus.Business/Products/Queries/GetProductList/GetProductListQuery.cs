using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class GetProductListQuery : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }
        public Guid? WareHouseId { get; set; }
        public string Status { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsRaw { get; set; }
        public bool IsActive { get; set; }
        public bool IsProductList { get; set; }
        public bool? IsService { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public bool IsEmail { get; set; }
        public bool ColorVariants { get; set; }
        public List<Guid?> productMasterId { get; set; }
        public List<Guid?> SubCategoryId { get; set; }
        public List<Guid?> ProductMasterId { get; set; }

        public List<Guid?> ColorId { get; set; }

        public List<Guid?> SizeId { get; set; }

        public List<Guid?> OriginId { get; set; }
        public List<Guid?> Categories { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetProductListQuery, PagedResult<ProductListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetProductListQuery> logger, IConfiguration configuration, IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _mediator = mediator;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsEmail)
                        _context.DisableTenantFilter = true;


                    var query = await (from product in _context.Products.Include(x => x.Category)
                                       select new
                                       {
                                           product.Id,
                                           product.Code,
                                           product.Description,
                                           EnglishName = product.EnglishName ?? "",
                                           ArabicName = product.ArabicName ?? "",
                                           DisplayName = product.DisplayName ?? "",
                                           product.TaxMethod,
                                           TaxRate = product.TaxRate.Name,
                                           TaxRateValue = product.TaxRate.Rate,
                                           product.SalePrice,
                                           product.PurchasePrice,
                                           product.StyleNumber,
                                           BarCode = product.BarCode ?? "",

                                           product.CategoryId,
                                           product.SubCategoryId,
                                           product.SizeId,
                                           product.ColorId,
                                           product.OriginId,
                                           product.ServiceItem,
                                           product.IsActive,
                                           product.IsRaw,
                                           UnitName = product.Unit.Name,
                                           product.UnitPerPack
                                       }).ToListAsync(cancellationToken: cancellationToken);




                    if (request.WareHouseId != Guid.Empty && request.WareHouseId != null)
                    {
                        query = query.Where(x => x.IsRaw == request.IsRaw).ToList();
                        if (request.IsService == true)
                        {
                            query = query.Where(x => x.ServiceItem).ToList();
                        }
                    }
                    else
                    {
                        if (request.IsDropdown && !request.IsRaw)
                        {
                            query = query.Where(x => x.IsActive && !x.IsRaw).ToList();

                        }
                        else if (request.IsRaw)
                        {
                            query = query.Where(x => x.IsActive && x.IsRaw).ToList();

                        }
                        else if (request.IsProductList)
                        {
                            query = query.Where(x => x.IsActive && x.IsRaw == request.IsRaw).ToList();
                        }
                    }


                    if (request.Categories != null && request.Categories.Count != 0)
                    {
                        query = query.Where(x => request.Categories.Contains(x.CategoryId)).ToList();
                    }


                    if (request.CategoryId != Guid.Empty && request.CategoryId != null)
                    {
                        query = query.Where(x => x.CategoryId == request.CategoryId).ToList();
                    }


                    if (request.SubCategoryId != null && request.SubCategoryId.Count != 0)
                    {
                        query = query.Where(x => request.SubCategoryId.Contains(x.SubCategoryId)).ToList();
                    }


                    if (request.SizeId != null && request.SizeId.Count != 0)
                    {
                        query = query.Where(x => request.SizeId.Contains(x.SizeId)).ToList();
                    }


                    if (request.ColorId != null && request.ColorId.Count != 0)
                    {
                        query = query.Where(x => request.ColorId.Contains(x.ColorId)).ToList();
                    }


                    if (request.OriginId != null && request.OriginId.Count != 0)
                    {
                        query = query.Where(x => request.OriginId.Contains(x.OriginId)).ToList();
                    }

                    //if (request.BranchId != null)
                    //{
                    //    query = query.Where(x => x.BranchItems.Any(y => y.BranchId == request.BranchId)).ToList();
                    //}

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.DisplayName.ToLower().Contains(searchTerm) ||
                                                 x.BarCode.ToLower().Contains(searchTerm) || x.Code.ToLower().Contains(searchTerm)).ToList();

                    }

                    var productLookUpModel = new List<ProductLookUpModel>();
                    if (request.IsDropdown)
                    {
                        if (request.IsService != true)
                        {
                            query = query.Where(x => !x.ServiceItem).ToList();
                        }
                        
                        foreach (var product in query)
                        {
                            productLookUpModel.Add(new ProductLookUpModel()
                            {
                                ArabicName = product.ArabicName,
                                BarCode = product.BarCode,
                                Code = product.Code,
                                Description = product.Description,
                                EnglishName = product.EnglishName,
                                DisplayName = product.DisplayName,
                                Id = product.Id,
                                SalePrice = product.SalePrice,
                                StyleNumber = product.StyleNumber,
                                TaxMethod = product.TaxMethod,
                                TaxRate = product.TaxRate,
                                TaxRateValue = product.TaxRateValue,
                                PurchasePrices = product.PurchasePrice,
                                UnitPerPack= product.UnitPerPack,
                                UnitNameEn = product.UnitName,
                            });
                        }

                        return new PagedResult<ProductListModel>
                        {
                            Results = new ProductListModel
                            {
                                Products = productLookUpModel
                            }
                        };
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    return new PagedResult<ProductListModel>
                    {
                        Results = new ProductListModel
                        {
                            Products = productLookUpModel
                        },
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = count / request.PageSize
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
