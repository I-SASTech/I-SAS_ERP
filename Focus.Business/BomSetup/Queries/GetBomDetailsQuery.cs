using Focus.Business.BomSetup.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SaleOrders;
using Focus.Business.Sales.Queries.GetSaleDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BomSetup.Queries
{
    public class GetBomDetailsQuery : IRequest<BomsLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBomDetailsQuery, BomsLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;



            public Handler(IApplicationDbContext context,
                ILogger<GetBomDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;

            }
            public async Task<BomsLookupModel> Handle(GetBomDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var boms = await _context.Boms.FindAsync(request.Id);

                    var wareHouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == boms.WareHouseId);
                    var saleOder = await _context.SaleOrderItems.AsNoTracking().Include(x => x.SaleOrder).FirstOrDefaultAsync(x => x.SaleOrderId == boms.SaleOrderId);
                    if(boms== null)
                    {
                        throw new NotFoundException("No Data Found", "");
                    }

                    var bomDetails = new BomsLookupModel
                    {
                        Id = boms.Id,
                        Code = boms.Code,
                        Date = boms.Date,
                        SaleOrderId = boms.SaleOrderId,
                        CreatedDate = boms.CreatedDate,
                        WareHouseId = boms.WareHouseId,
                        SaleOrder = saleOder.SaleOrder.RegistrationNo,
                        WareHouseName = wareHouse != null ? wareHouse.Name : "",
                        BomSaleOrderItem = boms.BomSaleOrderItems.Select(x => new SaleOrderItemLookupModel
                        {
                            Id = x.Id,
                            ProductId = x.ProductId,
                            Code = x.Products.Code,
                            ProductName = string.IsNullOrEmpty(x.Products.EnglishName) ? x.Products.ArabicName : x.Products.EnglishName,
                            ProductNameEn = x.Products.EnglishName,
                            ProductNameAr = x.Products.ArabicName,
                            Quantity = x.Quantity,
                            CurrentQuantity = x.Quantity,
                            SaleOrderId = x.SaleOrderId,
                            IsExpire = x.Products.IsExpire,
                            DisplayName = x.Products.DisplayName,
                            UnitPrice = saleOder.UnitPrice,
                            UnitName = x.Products?.Unit?.Name,
                            UnitPerPack = x.Products.UnitPerPack,
                            Product = new ProductDropdownLookUpModel
                            {
                                Id = x.Products.Id,
                                BarCode = x.Products.BarCode,
                                StyleNumber = x.Products.StyleNumber,
                                Width = x.Products.Width,
                                UnitPerPack = x.Products.UnitPerPack,
                                ServiceItem = x.Products.ServiceItem,
                                WholesaleQuantity = x.Products.WholesaleQuantity,
                                WholesalePrice = x.Products.WholesalePrice,
                                Code = x.Products.Code,
                                EnglishName = x.Products.EnglishName,
                                ArabicName = x.Products.ArabicName,
                                SalePrice = x.Products.SalePrice,
                                LevelOneUnit = x.Products.LevelOneUnit,
                                BasicUnit = x.Products.Unit?.Name,

                                TaxRateId = x.Products.TaxRateId.Value,
                                TaxMethod = x.Products.TaxMethod,
                                Serial = x.Products.Serial,
                                HighUnitPrice = x.Products.HighUnitPrice,
                                Guarantee = x.Products.Guarantee,
                            },
                            BomRawProducts = x.BomRawProducts.Select(y => new BomRawProductsLookupModel
                            {
                                Id = y.Id,
                                salePrice = y.Price,
                                SaleOrderId = y.SaleOrderId,
                                BomSaleOrderItemId = y.BomSaleOrderItemId,
                                ProductId = y.ProductId,
                                Quantity= y.Quantity,
                                CurrentQuantity = y.CurrentQuantity,
                                UnitPerPack = y.Products?.UnitPerPack,
                                UnitName = y.Products?.Unit?.Name,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = y.Products.Id,
                                    BarCode = y.Products.BarCode,
                                    StyleNumber = y.Products.StyleNumber,
                                    Width = y.Products.Width,
                                    UnitPerPack = y.Products.UnitPerPack,
                                    ServiceItem = y.Products.ServiceItem,
                                    WholesaleQuantity = y.Products.WholesaleQuantity,
                                    WholesalePrice = y.Products.WholesalePrice,
                                    Code = y.Products.Code,
                                    EnglishName = y.Products.EnglishName,
                                    ArabicName = y.Products.ArabicName,
                                    SalePrice = y.Products.SalePrice,
                                    LevelOneUnit = y.Products.LevelOneUnit,
                                    BasicUnit = y.Products.Unit?.Name,

                                    TaxRateId = y.Products.TaxRateId.Value,
                                    TaxMethod = y.Products.TaxMethod,
                                    Serial = y.Products.Serial,
                                    HighUnitPrice = y.Products.HighUnitPrice,
                                    Guarantee = y.Products.Guarantee,
                                },
                            }).ToList(),

                        }).ToList(),
                    };


                    return bomDetails;


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
