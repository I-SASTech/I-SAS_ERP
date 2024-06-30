using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Attachments.Commands;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.Sales.Queries.CashVoucherQuery;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Business.Users;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Focus.Business.Sales.Queries.GetSaleDetailForEmail
{
    public class GetSaleDetailEmailQuery : IRequest<SaleDetailEmailLookupModel>
    {
        public Guid Id { get; set; }
        public bool IsReturn { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public string InvoiceNo { get; set; }
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetSaleDetailEmailQuery, SaleDetailEmailLookupModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            public readonly IMapper Mapper;
            public readonly UserManager<ApplicationUser> UserManager;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;
            private readonly IHostingEnvironment _hostingEnvironment;

            public Handler(IApplicationDbContext context,
                ILogger<GetSaleDetailEmailQuery> logger,
                IMapper mapper, UserManager<ApplicationUser> userManager, IMediator mediator, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
            {
                UserManager = userManager;
                Context = context;
                Logger = logger;
                Mapper = mapper;
                _mediator = mediator;
                _configuration = configuration;
                _hostingEnvironment = hostingEnvironment;
            }
            public async Task<SaleDetailEmailLookupModel> Handle(GetSaleDetailEmailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string counterCode = null;
                    string userName = null;
                    Context.DisableTenantFilter = true;
                    using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        var counterUserId =
                            conn.Query<CountUserIdLookUp>("select CounterId, UserId from Sales where Id='" +
                                                          request.Id + "' ").FirstOrDefault();
                        if (counterUserId != null)
                        {
                            counterCode =
                                conn.Query<string>("select Code from Terminals where Id='" +
                                                   counterUserId.CounterId + "' ").FirstOrDefault();

                            userName =
                                conn.Query<string>("select UserName from AspNetUsers where Id='" +
                                                   counterUserId.UserId + "'").FirstOrDefault();
                        }


                        conn.Close();
                    }

                    var sale = await Context.Sales
                        .Include(x => x.Employee)
                        .Include(x => x.Area)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        //.ThenInclude(x => x.PromotionOffer)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        //.ThenInclude(x => x.BundleCategory)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Inventories)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.WareHouse)
                        .Include(x => x.Customer)
                        .Include(x => x.CashCustomer)
                        .Include(x => x.SalePayments)
                        .ThenInclude(x => x.PaymentOption)
                        .Include(x => x.SaleOrder)
                        .ThenInclude(x => x.SaleOrderItems)
                        .Include(x => x.Logistic)
                        .FirstOrDefaultAsync(x => (x.Id == request.Id || x.RegistrationNo == request.InvoiceNo) && EF.Property<Guid>(x, "CompanyId") == request.CompanyId, cancellationToken);

                    

                    IQueryable<Inventory> inventoryData = null;
                    if (request.IsFifo)
                    {

                        inventoryData = Context.Inventories.Where(x =>
                                x.IsActive && x.IsOpen && (x.TransactionType == TransactionType.PurchasePost ||
                                                           x.TransactionType == TransactionType.StockIn) && EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();
                        
                    }
                    if (sale == null)
                        throw new NotFoundException("Sale Invoice Cannot be found", request.Id);

                    if (!string.IsNullOrEmpty(request.InvoiceNo) && sale.InvoiceType == InvoiceType.Hold)
                    {
                        throw new NotFoundException(request.InvoiceNo + " Sale Invoice Cannot be found", request.Id);
                    }

                    var attachments = Context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == request.Id && EF.Property<Guid>(x, "CompanyId") == request.CompanyId)
                        .AsQueryable();

                    var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
                    {
                        Id = x.Id,
                        DocumentId = x.DocumentId,
                        Date = x.Date,
                        Description = x.Description,
                        FileName = x.FileName,
                        Path = x.Path
                    }).ToListAsync(cancellationToken: cancellationToken);

                    var paymentVoucher = new List<PaymentVoucher>();
                    if (sale.IsCredit)
                    {
                        paymentVoucher = await Context.PaymentVouchers.AsNoTracking()
                            .Where(x => x.SaleInvoice == sale.Id && EF.Property<Guid>(x, "CompanyId") == request.CompanyId).ToListAsync(cancellationToken: cancellationToken);
                    }
                    IQueryable<SaleItem> itemList = null;
                    //var itemList = new IQueryable<SaleItem>();
                    if (request.IsReturn)
                    {
                        itemList = Context.SaleItems.AsNoTracking()
                            .Where(x => x.SaleId == sale.SaleInvoiceId && EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();
                    }

                    var cashVoucher = new CashVoucherLookUpModel();
                    var saleItems = new List<SaleItemLookupModel>();
                    var salePayments = new SalePaymentLookupModel();
                    var voucher = await Context.CashVouchers.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.SaleReturnId == sale.Id && EF.Property<Guid>(x, "CompanyId") == request.CompanyId, cancellationToken: cancellationToken);
                    if (voucher != null)
                    {
                        cashVoucher.Id = voucher.Id;
                        cashVoucher.VoucherNo = voucher.VoucherNo;
                        cashVoucher.Date = voucher.Date;
                        cashVoucher.Amount = voucher.Amount;
                    }

                    var products = Context.Products.AsNoTracking()
                        .Include(x => x.Category)
                        .Where(x=> EF.Property<Guid>(x, "CompanyId") == request.CompanyId)
                        .AsQueryable();



                    decimal amountWithDiscount = 0;
                    foreach (var item in sale.SaleItems)
                    {
                        decimal discount = 0;
                        var productList = await products.FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken: cancellationToken);
                        List<InventoryBatchLookUpModel> inventoryBatchDetail = null;
                        if (request.IsFifo)
                        {
                            inventoryBatchDetail = inventoryData.Where(x => x.ProductId == item.ProductId ).Take(request.OpenBatch)
                                .Select(p =>
                                    new InventoryBatchLookUpModel()
                                    {
                                        ExpiryDate = p.ExpiryDate,
                                        AutoNumbering = p.AutoNumbering,
                                        BatchNumber = p.BatchNumber,
                                        IsOpen = p.IsOpen,
                                        ProductId = p.ProductId,
                                        Price = p.Price,
                                        RemainingQuantity = p.RemainingQuantity
                                    }).ToList();
                        }
                        discount += item.Discount == 0 ? item.FixDiscount * item.OfferQuantity : (item.UnitPrice * item.OfferQuantity * item.Discount) / 100;
                        amountWithDiscount += discount;
                        if (item.Product.TaxRateId != null)
                            saleItems.Add(new SaleItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId.Value,
                                Code = productList.Code,
                                ProductName = productList.EnglishName,
                                Description = item.Description,
                                IsFree = item.IsFree,
                                ReturnQuantity = 0,
                                SoQty = sale.SaleOrder?.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.QuantityOut ?? 0,
                                ArabicName = productList.ArabicName,
                                SaleReturnDays = productList.SaleReturnDays,
                                CategoryName = productList.Category.Code + "--" + productList.Category.Name,
                                Discount = item.Discount,
                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                Quantity = item.Quantity,
                                OfferQuantity = item.OfferQuantity,
                                TaxRateId = item.TaxRateId,
                                TaxRate = item.TaxRate.Rate,
                                UnitPrice = item.UnitPrice,
                                Total = item.Quantity * item.UnitPrice,
                                TaxMethod = sale.TaxMethod,
                                PromotionId = item.PromotionId,
                                BundleId = item.BundleId,
                                BatchNo = item.BatchNo,
                                BatchExpiry = item.BatchExpiry,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                ServiceItem = item.ServiceItem,
                                RemainingQuantity = request.IsReturn == false ? item.RemainingQuantity : itemList?.FirstOrDefault(x => x.ProductId == item.ProductId)?.RemainingQuantity ?? 0,
                                BundleAmount = item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0,

                                DiscountAmount = (item.OfferQuantity == 0 ?
                                    (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                        : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                        : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100),

                                InclusiveVat =
                                    (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                        ? ((item.Quantity * item.UnitPrice) -
                                           ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                            (item.OfferQuantity == 0
                                                ?
                                                (item.Discount == 0
                                                    ? item.Quantity * item.FixDiscount
                                                    : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                                : item.Discount == 0
                                                    ? item.OfferQuantity * item.FixDiscount
                                                    : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                        item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                        : 0,

                                ExclusiveVat = item.TaxRate.Rate == 0
                                    ? 0
                                    : ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                        ? ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                            ?
                                            (item.Discount == 0
                                                ? item.Quantity * item.FixDiscount
                                                : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                            : item.Discount == 0
                                                ? item.OfferQuantity * item.FixDiscount
                                                : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                        item.TaxRate.Rate / 100
                                        : 0),

                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    StyleNumber = item.Product.StyleNumber,
                                    Length = item.Product.Length,
                                    Width = item.Product.Width,
                                    UnitPerPack = item.Product.UnitPerPack,
                                    InventoryBatch = inventoryBatchDetail,
                                    
                                    Code = item.Product.Code,
                                    SaleReturnDays = item.Product.SaleReturnDays,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    LevelOneUnit = item.Product.LevelOneUnit,
                                    BasicUnit = item.Product.Unit?.Name,
                                    IsExpire = item.Product.IsExpire,
                                    ServiceItem = item.Product.ServiceItem,
                                    Serial = item.Product.Serial,
                                    Guarantee = item.Product.Guarantee,
                                    //PromotionOffer = item.Product.PromotionOffer == null
                                    //    ? null
                                    //    : new PromotionOffer
                                    //    {
                                    //        FromDate = item.Product.PromotionOffer.FromDate,
                                    //        isActive = item.Product.PromotionOffer.isActive,
                                    //        Offer = item.Product.PromotionOffer.Offer,
                                    //        StockLimit = item.Product.PromotionOffer.StockLimit,
                                    //        QuantityOut = item.Product.PromotionOffer.QuantityOut,
                                    //        //DiscountPercentage = item.Product.PromotionOffer.DiscountPercentage,
                                    //        //FixedDiscount = item.Product.PromotionOffer.FixedDiscount,
                                    //        //Quantity = item.Product.PromotionOffer.Quantity,
                                    //        ToDate = item.Product.PromotionOffer.ToDate
                                    //    },
                                    //BundleCategory = item.Product.BundleCategory == null
                                    //    ? null
                                    //    : new BundleCategory
                                    //    {
                                    //        Buy = item.Product.BundleCategory.Buy,
                                    //        Get = item.Product.BundleCategory.Get,
                                    //        Offer = item.Product.BundleCategory.Offer,
                                    //        isActive = item.Product.BundleCategory.isActive,
                                    //        QuantityLimit = item.Product.BundleCategory.QuantityLimit,
                                    //        StockLimit = item.Product.BundleCategory.StockLimit
                                    //    },
                                    TaxRateId = item.Product.TaxRateId.Value,
                                    Inventory = item.Product.Inventories == null
                                        ? null
                                        : request.IsFifo? new Inventory()
                                        {
                                            CurrentQuantity = inventoryBatchDetail.Count>0?inventoryBatchDetail.FirstOrDefault(x=>x.BatchNumber==item.BatchNo)==null?0: inventoryBatchDetail.FirstOrDefault(x => x.BatchNumber == item.BatchNo).RemainingQuantity:0,
                                        }: new Inventory()
                                        {
                                            CurrentQuantity = _mediator.Send(new GetLatestInventoryQuery
                                            {
                                                ProductId = item.ProductId.Value,
                                                WareHouseId = item.WareHouseId
                                            }, cancellationToken).Result.CurrentQuantity,
                                        }

                                }
                            });
                    }

                    var paymentTypes = new List<PaymentTypeLookupModel>();

                    if (sale.InvoiceType == InvoiceType.Paid || sale.InvoiceType == InvoiceType.SaleReturn)
                    {
                        foreach (var payment in sale.SalePayments)
                        {
                            paymentTypes.Add(new PaymentTypeLookupModel
                            {
                                Id = payment.Id,
                                Amount = payment.Received,
                                PaymentType = payment.PaymentType,
                                Name = payment.Name,
                                Rate = payment.Rate,
                                AmountCurrency = payment.Amount
                                //PaymentOptionName = payment.PaymentType == SalePaymentType.Bank ? payment.PaymentOption.Name : null
                            });
                        }

                        salePayments.DueAmount = sale.SalePayments.FirstOrDefault()?.DueAmount ?? 0;
                        salePayments.Received = 0;
                        salePayments.Balance = sale.SalePayments.FirstOrDefault()?.Balance ?? 0;
                        salePayments.Change = sale.SalePayments.FirstOrDefault()?.Change ?? 0;
                        salePayments.PaymentOptionId = sale.SalePayments.FirstOrDefault()?.PaymentOptionId;
                        salePayments.PaymentType = sale.SalePayments.FirstOrDefault()?.PaymentType ?? 0;
                        salePayments.PaymentTypes = paymentTypes;


                    }
                    //var saleReturnData = await Context.SalePayments.LastOrDefaultAsync(x => x.SaleId == request.Id, cancellationToken);

                    return new SaleDetailEmailLookupModel
                    {
                        Id = sale.Id,
                        AttachmentList = attachmentList,
                        TaxMethod = sale.TaxMethod,
                        LogisticId = sale.LogisticId,
                        Mobile = sale.Mobile,
                        LogisticNameEn = sale.Logistic?.EnglishName,
                        LogisticNameAr = sale.Logistic?.ArabicName,
                        Date = sale.Date,
                        BarCode = sale.BarCode,
                        CounterCode = counterCode,
                        UserName = userName,
                        Description = sale.Description,
                        CustomerAddressWalkIn = sale.CustomerAddressWalkIn,
                        SaleOrderId = sale.SaleOrderId,
                        SaleOrderNo = sale.SaleOrder?.RegistrationNo,
                        SaleInvoiceId = sale.SaleInvoiceId,
                        InvoiceNo = sale.SaleInvoiceId != null ? Context.Sales.FirstOrDefault(x => x.Id == sale.SaleInvoiceId && EF.Property<Guid>(x, "CompanyId") == request.CompanyId)?.RegistrationNo : null,
                        RefrenceNo = sale.RefrenceNo,
                        EmployeeId = sale.EmployeeId,
                        AreaId = sale.AreaId,
                        IsSaleReturnPost = sale.IsSaleReturnPost,
                        Change = sale.Change.ToString("0.00"),
                        InvoiceType = sale.InvoiceType,
                        PaymentAmount = paymentTypes.Sum(x => x.Amount).ToString("0.00"),
                        PaymentTypes = paymentTypes,
                        RegistrationNo = sale.RegistrationNo,
                        CustomerId = sale.CustomerId,
                        DiscountAmount = amountWithDiscount,
                        CreditCustomer = sale.CustomerId == null ? null : sale.Customer.Code + "--" + sale.Customer.EnglishName,
                        DueDate = sale.DueDate,
                        CashCustomer = sale.CashCustomer?.Name,
                        CustomerCode = sale.Customer?.Code,
                        CustomerCategory = sale.Customer?.Category,
                        CustomerNameEn = sale.Customer?.EnglishName,
                        CustomerNameAr = sale.Customer?.ArabicName,
                        CustomerTelephone = sale.Customer?.ContactNo1,
                        CustomerVat = sale.Customer?.VatNo,
                        CustomerAddress = sale.Customer?.Address,
                        Code = sale.CashCustomer?.Code,
                        Email = sale.CashCustomer?.Email,
                        CashCustomerId = sale.CashCustomer?.CustomerId,
                        WareHouseId = sale.WareHouseId,
                        WareHouseName = sale.SaleItems.FirstOrDefault()?.WareHouse.Name,
                        WareHouseNameAr = sale.SaleItems.FirstOrDefault()?.WareHouse.NameArabic,
                        SaleItems = saleItems,
                        SalePayment = salePayments,
                        IsCredit = sale.IsCredit,
                        TermAndCondition = sale.TermAndCondition,
                        TermAndConditionAr = sale.TermAndConditionAr,
                        WarrantyLogoPath =  await GetBaseImage(sale.WarrantyLogoPath),
                        CashVoucher = cashVoucher,
                        PaymentVoucher = paymentVoucher,
                        PartiallyInvoice = sale.PartiallyInvoices.ToString(),
                        CustomerName = sale.CustomerId != null && sale.CustomerId != Guid.Empty ? sale.Customer.EnglishName : null,
                        //ReturnInvoiceAmount = saleReturnData?.Received ?? 0,
                        //ReturnInvoiceRegNo = saleReturnData != null? saleReturnData.Name:""

                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    Context.DisableTenantFilter = false;
                }
            }
            public async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
