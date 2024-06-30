using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BundleCategoriesItems.Models;
using Focus.Business.Categories.Command.AddUpdateCategory;
using Focus.Business.Categories.Queries;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.PriceRecords;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Products.Commands.AddUpdateProduct
{
    public class AddUpdateProductCommand : IRequest<Message>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public string BarCode { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public decimal SalePrice { get; set; }
        public Guid? OriginId { get; set; }
        public string StockLevel { get; set; }
        public string SaleReturnDays { get; set; }
        public string Description { get; set; }
        public string Shelf { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActive { get; set; }
        public string DisplayNameForPrint { get; set; }
        public string Image { get; set; }
        public bool IsRaw { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SalePriceUnit { get; set; }
        public string Assortment { get; set; }
        public string StyleNumber { get; set; }
        public Guid? ProductMasterId { get; set; }
        public bool Guarantee { get; set; }
        public bool Serial { get; set; }
        public bool QuickProduct { get; set; }
        public Guid? CategoryIdQuick { get; set; }
        public bool ServiceItem { get; set; }
        public bool IsBarCodeGenerated { get; set; }
        public decimal WholesalePrice { get; set; }
        public bool HighUnitPrice { get; set; }
        public ICollection<Guid> SizeIdList { get; set; }
        public ICollection<PriceRecordModelForProduct> PriceRecords { get; set; }
        public string HsCode { get; set; }
        public decimal? WholesaleQuantity { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid? ProductGroupId { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? CostValue { get; set; }
        public string CostSign { get; set; }
        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }
        public string SKU { get; set; }
        public string PartNumber { get; set; }
        public string DisplayName { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateProductCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateProductCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateProductCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    var itemsBarCodeDisplayName = await _context.ListOrderSetups.Where(x => x.DocumentName == "BarCode").ToListAsync();

                    string barCodeDisplayName= "";

                    if (itemsBarCodeDisplayName.Count > 0)
                    {
                        var categories = await _context.Categories.ToListAsync();
                        var brand = await _context.Brands.ToListAsync();
                        var origin = await _context.Origins.ToListAsync();
                        var size = await _context.Sizes.ToListAsync();
                        var unit = await _context.Units.ToListAsync();
                        var color = await _context.Colors.ToListAsync();


                        string barCodeDisplayNameValue = "";

                        foreach (var item in itemsBarCodeDisplayName)
                        {
                            var categoryName = categories.FirstOrDefault(x => x.Id == request.CategoryId)?.Name;

                            if (item.Name == "Item Code")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + request.Code + " ";
                            }
                            else if (item.Name == "Item Name (English)")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + request.EnglishName + " ";
                            }
                            else if (item.Name == "Item Name (Arabic)")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + request.ArabicName + " ";
                            }
                            else if (item.Name == "Item Description")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + request.Description + " ";
                            }
                            else if (item.Name == "Item Category")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + categoryName + " ";
                            }
                            else if (item.Name == "Item Barcode")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + request.BarCode + " ";
                            }
                            else if (item.Name == "Item Style/Model")
                            {
                                barCodeDisplayNameValue = barCodeDisplayNameValue + request.StyleNumber + " ";
                            }
                            else if (item.Name == "Item Brand")
                            {
                                if (request.BrandId != Guid.Empty)
                                {
                                    var brandName = brand.FirstOrDefault(x => x.Id == request.BrandId)?.Name;

                                    barCodeDisplayNameValue = barCodeDisplayNameValue + brandName + " ";
                                }
                            }
                            else if (item.Name == "Item Origin")
                            {
                                if (request.OriginId != Guid.Empty)
                                {
                                    var OriginName = origin.FirstOrDefault(x => x.Id == request.OriginId)?.Name;

                                    barCodeDisplayNameValue = barCodeDisplayNameValue + OriginName + " ";
                                }
                            }
                            else if (item.Name == "Item Size")
                            {
                                if (request.SizeId != Guid.Empty)
                                {
                                    var sizeName = size.FirstOrDefault(x => x.Id == request.SizeId)?.Name;

                                    barCodeDisplayNameValue = barCodeDisplayNameValue + sizeName + " ";
                                }
                            }
                            else if (item.Name == "Item Color")
                            {
                                if (request.ColorId != Guid.Empty)
                                {
                                    var colorName = color.FirstOrDefault(x => x.Id == request.ColorId)?.Name;

                                    barCodeDisplayNameValue = barCodeDisplayNameValue + colorName + " ";
                                }
                            }
                            else if (item.Name == "Item Unit")
                            {
                                if (request.UnitId != Guid.Empty)
                                {
                                    var unitName = unit.FirstOrDefault(x => x.Id == request.UnitId)?.Name;

                                    barCodeDisplayNameValue = barCodeDisplayNameValue + unitName + " ";
                                }
                            }
                        }

                        barCodeDisplayName = barCodeDisplayNameValue.Length >= 25 ? barCodeDisplayNameValue.Substring(0, 25) : barCodeDisplayNameValue;
                    }



                    if (request.QuickProduct)
                    {
                        var isProductExist = await _context.Products.AnyAsync(x => x.Code == request.Code, cancellationToken);

                        if (isProductExist)
                            throw new ObjectAlreadyExistsException("Product Code" + request.Code + " Already Exist");

                        Guid? categoryId;
                        if (request.ServiceItem && (request.CategoryIdQuick == null || request.CategoryIdQuick == Guid.Empty))
                        {
                            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Service", cancellationToken: cancellationToken);
                            if (category == null)
                            {
                                var autoNo = await _mediator.Send(new GetCategoryCodeQuery(), cancellationToken);
                                var catId = await _mediator.Send(new AddUpdateCategoryCommand
                                {
                                    Id = new Guid(),
                                    Name = "Service",
                                    NameArabic = "خدمة",
                                    Code = autoNo,
                                    Description = "Auto Generate Service Category",
                                    IsActive = true,
                                    IsService = true,
                                }, cancellationToken);
                                categoryId = catId;
                            }
                            else
                            {
                                categoryId = category.Id;
                            }
                        }
                        else
                        {
                            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == "Temporary Category", cancellationToken);
                            categoryId = category.Id;
                        }

                        

                        var product = new Product
                        {
                            Code = request.Code,
                            HsCode = request.HsCode,
                            EnglishName = request.EnglishName,
                            ArabicName = request.ArabicName,
                            DisplayName = request.DisplayName,
                            DisplayNameForPrint = request.DisplayNameForPrint,
                            CategoryId = categoryId.Value,
                            SubCategoryId = request.SubCategoryId,
                            BrandId = request.BrandId,
                            UnitId = request.UnitId,
                            SizeId = request.SizeId,
                            ColorId = request.ColorId,
                            BarCode = request.BarCode,
                            Length = request.Length,
                            Width = request.Width,
                            TaxRateId = request.TaxRateId,
                            TaxMethod = request.TaxMethod,
                            SalePrice = request.SalePrice,
                            OriginId = request.OriginId,
                            StockLevel = request.StockLevel,
                            SaleReturnDays = request.SaleReturnDays,
                            Description = request.Description,
                            Shelf = request.Shelf,
                            IsExpire = request.IsExpire,
                            IsActive = true,
                            Image = request.Image,
                            IsRaw = request.IsRaw,
                            LevelOneUnit = request.LevelOneUnit,
                            UnitPerPack = request.UnitPerPack == null || request.UnitPerPack == 0 ? string.IsNullOrEmpty(request.Width) ? 0 : decimal.Parse(request.Width) : request.UnitPerPack,
                            SalePriceUnit = request.SalePriceUnit,
                            Assortment = request.Assortment,
                            StyleNumber = request.StyleNumber,
                            ProductMasterId = request.ProductMasterId,
                            Serial = request.Serial,
                            Guarantee = request.Guarantee,
                            ServiceItem = request.ServiceItem,
                            WholesalePrice = request.WholesalePrice,
                            HighUnitPrice = request.HighUnitPrice,
                            WholesaleQuantity = request.WholesaleQuantity,
                            SchemeQuantity = request.SchemeQuantity,
                            Scheme = request.Scheme,
                            PurchasePrice = request.PurchasePrice,
                            ProductGroupId = request.ProductGroupId,
                            CostPrice = request.CostPrice,
                            CostValue = request.CostValue,
                            CostSign = request.CostSign,
                            BarCodeDisplayName = barCodeDisplayName,
                        };


                        //Add Product to database
                        await _context.Products.AddAsync(product, cancellationToken);

                        var accountCount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "11100001", cancellationToken: cancellationToken);
                        if (accountCount == null)
                            throw new NotFoundException("Account  Code 11100001 ", request.EnglishName);

                        var accountCogs = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "60000101", cancellationToken: cancellationToken);
                        if (accountCogs == null)
                            throw new NotFoundException("Account  Code 60000101 ", request.EnglishName);

                        var accountSale = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "42000001", cancellationToken: cancellationToken);
                        if (accountSale == null)
                            throw new NotFoundException("Account  Code 42000001 ", request.EnglishName);

                        var accounts = new List<LedgerAccount>
                        {
                            new()
                            {
                                Name = request.EnglishName,
                                NameArabic = request.ArabicName,
                                Description = request.EnglishName + " " + request.Code,
                                Code = request.Code,
                                AccountId = accountCount.Id,
                                AccountLedgerId = product.Id,
                                IsActive = true
                            },
                            new()
                            {
                                Name = request.EnglishName,
                                NameArabic = request.ArabicName,
                                Description = request.EnglishName + " " + request.Code,
                                Code = request.Code,
                                AccountId = accountCogs.Id,
                                AccountLedgerId = product.Id,
                                IsActive = true
                            },
                            new()
                            {
                                Name = request.EnglishName,
                                NameArabic = request.ArabicName,
                                Description = request.EnglishName + " " + request.Code,
                                Code = request.Code,
                                AccountId = accountSale.Id,
                                AccountLedgerId = product.Id,
                                IsActive = true
                            }
                        };
                        await _context.LedgerAccounts.AddRangeAsync(accounts, cancellationToken);

                        product.SaleAccountId = accounts[2].Id;
                        product.CogsAccountId = accounts[1].Id;
                        product.InventoryAccountId = accounts[0].Id;

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }

                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //BranchId = item.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = product.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                    }

                    if (request.Id != Guid.Empty)
                    {
                        var productDetail = await _context.Products.FindAsync(request.Id);
                        if (productDetail == null)
                            throw new NotFoundException("Product Name", request.EnglishName);

                        _context.ProductSizes.RemoveRange(productDetail.ProductSizes);
                        _context.PriceRecords.RemoveRange(productDetail.PriceRecords);
                        _context.BranchItems.RemoveRange(productDetail.BranchItems);

                        var log = _context.SyncLog();
                        var list = new List<SyncRecordModel>();
                        if (productDetail.BranchItems.Any())
                        {
                            foreach (var branch in productDetail.BranchItems)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.BranchId,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //BranchId = branch.BranchId,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);


                        var product = await _context.Products.FindAsync(request.Id);
                        if (product == null)
                            throw new NotFoundException("Product Name", request.EnglishName);

                        var ledgerAccounts = await _context.LedgerAccounts.Where(x => x.AccountLedgerId == product.Id).ToListAsync(cancellationToken: cancellationToken);
                        foreach (var account in ledgerAccounts)
                        {
                            account.Name = request.EnglishName;
                            account.NameArabic = request.ArabicName;
                        }

                        string barCodeType = "";
                        if (string.IsNullOrEmpty(request.BarCode))
                        {
                            barCodeType = "None";
                        }
                        else
                        {
                            if(product.BarCode == request.BarCode)
                            {
                                barCodeType = "Generated";
                            }
                            else if (request.IsBarCodeGenerated)
                            {
                                barCodeType = "Generated";
                            }
                            else
                            {
                                barCodeType = "Scanned";
                            }
                        }

                        product.EnglishName = request.EnglishName;
                        product.HsCode = request.HsCode;
                        product.ArabicName = request.ArabicName;
                        product.DisplayName =request.DisplayName;
                        product.DisplayNameForPrint = request.DisplayNameForPrint;
                        product.CategoryId = request.CategoryId;
                        product.SubCategoryId = request.SubCategoryId;
                        product.BrandId = request.BrandId;
                        product.UnitId = request.UnitId;
                        product.BasicUnit = _context.Units.FirstOrDefault(x => x.Id == request.UnitId)?.Name;
                        product.SizeId = request.SizeId;
                        product.ColorId = request.ColorId;
                        product.BarCode = request.BarCode;
                        product.BarCodeType = barCodeType;
                        product.Length = request.Length;
                        product.Width = request.Width;
                        product.TaxRateId = request.TaxRateId;
                        product.TaxMethod = request.TaxMethod;
                        product.SalePrice = request.SalePrice;
                        product.OriginId = request.OriginId;
                        product.StockLevel = request.StockLevel;
                        product.SaleReturnDays = request.SaleReturnDays;
                        product.Description = request.Description;
                        product.Shelf = request.Shelf;
                        product.IsExpire = request.IsExpire;
                        product.IsActive = request.IsActive;
                        product.Image = request.Image;
                        //product.BasicUnit = Context.Units.FirstOrDefault(x => x.Id == request.UnitId).Name;
                        product.IsRaw = request.IsRaw;
                        product.LevelOneUnit = request.LevelOneUnit;
                        product.BasicUnit = request.BasicUnit;
                        product.UnitPerPack = request.UnitPerPack == null || request.UnitPerPack == 0 ? string.IsNullOrEmpty(request.Width) ? 0 : decimal.Parse(request.Width) : decimal.Parse(request.Width);
                        product.SalePriceUnit = request.SalePriceUnit;
                        product.Assortment = request.Assortment;
                        product.StyleNumber = request.StyleNumber;
                        product.ProductMasterId = request.ProductMasterId;
                        product.Serial = request.Serial;
                        product.Guarantee = request.Guarantee;
                        product.ServiceItem = request.ServiceItem;
                        product.WholesalePrice = request.WholesalePrice;
                        product.HighUnitPrice = request.HighUnitPrice;
                        product.WholesaleQuantity = request.WholesaleQuantity;
                        product.SchemeQuantity = request.SchemeQuantity;
                        product.Scheme = request.Scheme;
                        product.PurchasePrice = request.PurchasePrice;
                        product.ProductGroupId = request.ProductGroupId;
                        product.CostPrice = request.CostPrice;
                        product.CostValue = request.CostValue;
                        product.CostSign = request.CostSign;
                        product.SKU = request.SKU;
                        product.PartNumber = request.PartNumber;
                        product.BarCodeDisplayName = barCodeDisplayName;

                        if (request.SizeIdList != null && request.SizeIdList.Any())
                        {
                            foreach (var size in request.SizeIdList)
                            {
                                var productSize = new ProductSize()
                                {
                                    ProductId = product.Id,
                                    SizeId = size
                                };
                                await _context.ProductSizes.AddAsync(productSize, cancellationToken);
                            }
                        }


                        if (request.PriceRecords != null && request.PriceRecords.Any())
                        {
                            foreach (var size in request.PriceRecords)
                            {
                                var priceRexord = new PriceRecord()
                                {
                                    ProductId = product.Id,
                                    CostPrice = product.CostPrice ?? 0,
                                    PurchasePrice = product.PurchasePrice,
                                    SalePrice = product.SalePrice,
                                    PriceLabelingId = size.PriceLabelingId,
                                    Price = size.Price,
                                    NewPrice = size.NewPrice,
                                };
                                await _context.PriceRecords.AddAsync(priceRexord, cancellationToken);
                            }
                        }
                        

                        var branchItems = new List<BranchItems>();
                        if (request.BranchesIdList.Count > 0)
                        {
                            foreach (var item in request.BranchesIdList)
                            {
                                branchItems.Add(new BranchItems
                                {
                                    BranchId = item.Id,
                                    ProductId = product.Id
                                });
                            }
                            await _context.BranchItems.AddRangeAsync(branchItems, cancellationToken);
                        }

                        var log1 = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log1.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log1.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);


                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return new Message
                        {
                            Id = product.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };

                    }
                    else
                    {
                        var isProductExist = await _context.Products
                            .AnyAsync(x => x.Code == request.Code, cancellationToken);

                        if (isProductExist)
                            throw new ObjectAlreadyExistsException("Product Code" + request.Code + " Already Exist");

                        if (request.ServiceItem && request.CategoryId == Guid.Empty)
                        {
                            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Service", cancellationToken: cancellationToken);

                            if (category == null)
                            {
                                var autoNo = await _mediator.Send(new GetCategoryCodeQuery(), cancellationToken);
                                var categoryId = await _mediator.Send(new AddUpdateCategoryCommand
                                {
                                    Id = new Guid(),
                                    Name = "Service",
                                    NameArabic = "خدمة",
                                    Code = autoNo,
                                    Description = "Auto Generate Service Category",
                                    IsActive = true,
                                    IsService = true,
                                }, cancellationToken);
                                request.CategoryId = categoryId;
                            }
                            else
                            {
                                request.CategoryId = category.Id;
                            }
                        }

                        string barCodeType = "";
                        if(string.IsNullOrEmpty(request.BarCode))
                        {
                            barCodeType = "None";
                        }
                        else
                        {
                            if (request.IsBarCodeGenerated)
                            {
                                barCodeType = "Generated";
                            }
                            else
                            {
                                barCodeType = "Scanned";
                            }
                        }

                        var product = new Product
                        {
                            Code = request.Code,
                            HsCode = request.HsCode,
                            EnglishName = request.EnglishName,
                            ArabicName = request.ArabicName,
                            DisplayName = request.DisplayName,
                            DisplayNameForPrint = request.DisplayNameForPrint,
                            CategoryId = request.CategoryId,
                            SubCategoryId = request.SubCategoryId,
                            BrandId = request.BrandId,
                            UnitId = request.UnitId,
                            SizeId = request.SizeId,
                            ColorId = request.ColorId,
                            BarCode = request.BarCode,
                            BarCodeType = barCodeType,
                            Length = request.Length,
                            Width = request.Width,
                            TaxRateId = request.TaxRateId,
                            TaxMethod = request.TaxMethod,
                            SalePrice = request.SalePrice,
                            OriginId = request.OriginId,
                            StockLevel = request.StockLevel,
                            SaleReturnDays = request.SaleReturnDays,
                            Description = request.Description,
                            Shelf = request.Shelf,
                            IsExpire = request.IsExpire,
                            IsActive = request.IsActive,
                            Image = request.Image,
                            IsRaw = request.IsRaw,
                            LevelOneUnit = request.LevelOneUnit,
                            //BasicUnit = Context.Units.FirstOrDefault(x => x.Id == request.UnitId).Name,
                            UnitPerPack = request.UnitPerPack == null || request.UnitPerPack == 0 ? string.IsNullOrEmpty(request.Width) ? 0 : decimal.Parse(request.Width) : request.UnitPerPack,
                            SalePriceUnit = request.SalePriceUnit,
                            Assortment = request.Assortment,
                            StyleNumber = request.StyleNumber,
                            ProductMasterId = request.ProductMasterId,
                            Serial = request.Serial,
                            Guarantee = request.Guarantee,
                            ServiceItem = request.ServiceItem,
                            WholesalePrice = request.WholesalePrice,
                            HighUnitPrice = request.HighUnitPrice,
                            WholesaleQuantity = request.WholesaleQuantity,
                            SchemeQuantity = request.SchemeQuantity,
                            Scheme = request.Scheme,
                            PurchasePrice = request.PurchasePrice,
                            ProductGroupId = request.ProductGroupId,
                            CostPrice = request.CostPrice,
                            CostValue = request.CostValue,
                            CostSign = request.CostSign,
                            SKU = request.SKU,
                            PartNumber = request.PartNumber,
                            BarCodeDisplayName = barCodeDisplayName
                        };


                        //Add Product to database
                        await _context.Products.AddAsync(product, cancellationToken);

                        var bundleOfferBranches = new List<BranchItems>();
                        if (request.BranchesIdList.Count > 0)
                        {
                            foreach (var item in request.BranchesIdList)
                            {
                                bundleOfferBranches.Add(new BranchItems
                                {
                                    BranchId = item.Id,
                                    ProductId = product.Id
                                });
                            }
                            await _context.BranchItems.AddRangeAsync(bundleOfferBranches, cancellationToken);
                        }



                        var accountCount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "11100001", cancellationToken: cancellationToken);
                        if (accountCount == null)
                            throw new NotFoundException("Account  Code 11100001 ", request.EnglishName);

                        var accountCogs = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "60000101", cancellationToken: cancellationToken);
                        if (accountCogs == null)
                            throw new NotFoundException("Account  Code 60000101 ", request.EnglishName);

                        var accountSale = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "42000001", cancellationToken: cancellationToken);
                        if (accountSale == null)
                            throw new NotFoundException("Account  Code 42000001 ", request.EnglishName);

                        var accounts = new List<LedgerAccount>
                        {
                            new()
                            {
                                Name = request.EnglishName,
                                NameArabic = request.ArabicName,
                                Description = request.EnglishName + " " + request.Code,
                                Code = request.Code,
                                AccountId = accountCount.Id,
                                AccountLedgerId = product.Id,
                                IsActive = request.IsActive
                            },
                            new()
                            {
                                Name = request.EnglishName,
                                NameArabic = request.ArabicName,
                                Description = request.EnglishName + " " + request.Code,
                                Code = request.Code,
                                AccountId = accountCogs.Id,
                                AccountLedgerId = product.Id,
                                IsActive = request.IsActive
                            },
                            new()
                            {
                                Name = request.EnglishName,
                                NameArabic = request.ArabicName,
                                Description = request.EnglishName + " " + request.Code,
                                Code = request.Code,
                                AccountId = accountSale.Id,
                                AccountLedgerId = product.Id,
                                IsActive = request.IsActive
                            }
                        };
                        await _context.LedgerAccounts.AddRangeAsync(accounts, cancellationToken);

                        product.SaleAccountId = accounts[2].Id;
                        product.CogsAccountId = accounts[1].Id;
                        product.InventoryAccountId = accounts[0].Id;

                        if (request.SizeIdList != null && request.SizeIdList.Any())
                        {
                            foreach (var size in request.SizeIdList)
                            {
                                var productSize = new ProductSize()
                                {
                                    ProductId = product.Id,
                                    SizeId = size
                                };
                                await _context.ProductSizes.AddAsync(productSize, cancellationToken);
                            }
                        }

                        if (request.PriceRecords != null && request.PriceRecords.Any())
                        {
                            foreach (var size in request.PriceRecords)
                            {
                                var priceRexord = new PriceRecord()
                                {
                                    ProductId = product.Id,
                                    CostPrice = product.CostPrice??0,
                                    PurchasePrice = product.PurchasePrice,
                                    SalePrice = product.SalePrice,
                                    PriceLabelingId = size.PriceLabelingId,
                                    Price = size.Price,
                                    NewPrice = size.NewPrice
                                };
                                await _context.PriceRecords.AddAsync(priceRexord, cancellationToken);
                            }
                        }

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }

                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //BranchId = item.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = product.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
