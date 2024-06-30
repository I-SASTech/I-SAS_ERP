using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Business.Sales.Commands.AddSale;
using System.Text;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail
{
    public class PurchasePostDetailQuery : IRequest<PurchasePostLookupModel>
    {
        public Guid Id { get; set; }
        public bool IsReturnView { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool SimpleQuery { get; set; }

        public class Handler : IRequestHandler<PurchasePostDetailQuery, PurchasePostLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<PurchasePostDetailQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<PurchasePostLookupModel> Handle(PurchasePostDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    if (request.SimpleQuery)
                    {

                        var purchasePost = await _context.PurchasePosts
                            .AsNoTracking()
                            .Include(x => x.Supplier)
                            .Include(x => x.PurchaseOrder)
                            .Include(x => x.GoodReceiveNote)
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Unit)
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);




                        var purchaseItems = new List<PurchasePostItemLookupModel>();
                        foreach (var item in purchasePost.PurchasePostItems)
                        {
                            if (item.ProductId == null)
                            {

                                purchaseItems.Add(new PurchasePostItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Description = item.Description,
                                        ProductName = item.Description,
                                        ProductNameArabic = item.Description,
                                        CategoryName = "",
                                        TaxRateId = item.TaxRateId,
                                        Discount = item.Discount,
                                        FixDiscount = item.FixDiscount,
                                        Quantity = item.Quantity,
                                        QuantityforCanvas = item.Quantity,
                                        HighQty = 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        RemainingQuantity = item.RemainingQuantity,
                                        UnitPrice = item.UnitPrice,
                                        TaxMethod = item.TaxMethod,
                                        PurchaseId = item.PurchasePostId,
                                        SerialNo = item.SerialNo,
                                        IsService = item.IsService,
                                        GuaranteeDate = item.GuaranteeDate,
                                        WarrantyTypeId = item.WarrantyTypeId,

                                        Total = item.Quantity * item.UnitPrice,
                                        DiscountAmount = item.DiscountAmount,
                                        VatAmount = item.VatAmount,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        Code = item.Product?.Code,
                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,



                                        BatchNo = !string.IsNullOrEmpty(item.Product?.Width) ? item.Product?.Width : "",
                                    ExpiryDate = item.ExpiryDate,
                                       



                                    });
                               
                            }
                            else
                            {
                                purchaseItems.Add(new PurchasePostItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Description = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Description : item.Product.DisplayNameForPrint,
                                        ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Description : item.Product.DisplayNameForPrint,
                                        TaxRateId = item.TaxRateId,
                                        Discount = item.Discount,
                                        FixDiscount = item.FixDiscount,
                                        QuantityforCanvas = item.Quantity,
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.RemainingQuantity,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        RemainingQuantity = item.RemainingQuantity,
                                        UnitPrice = item.UnitPrice,
                                        TaxMethod = item.TaxMethod,
                                        PurchaseId = item.PurchasePostId,
                                        SerialNo = item.SerialNo,
                                        IsService = item.IsService,
                                        GuaranteeDate = item.GuaranteeDate,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        ColorId = item.ColorId,
                                        Code = item.Product.Code,
                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        Total = item.TotalAmount,
                                        DiscountAmount = item.DiscountAmount,
                                        VatAmount = item.VatAmount,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,



                                        Product = new ProductDropdownLookUpModel
                                        {
                                           
                                            Id = item.Product.Id,
                                            SalePrice = item.Product.SalePrice,
                                            BarCode = item.Product.BarCode,
                                            DisplayName = item.Product.DisplayName,
                                            Serial = item.Product.Serial,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            StyleNumber = item.Product.StyleNumber,
                                            ServiceItem = item.Product.ServiceItem,
                                            HighUnitPrice = item.Product.HighUnitPrice,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                            IsExpire = item.Product.IsExpire,
                                          
                                        },
                                       
                                    });
                                
                            }

                        }


                        string purchseOrerValue12 = "";
                        if (purchasePost.GoodReceiveNoteId != null)
                        {
                            purchseOrerValue12 = purchasePost.GoodReceiveNote.RegistrationNo + " " +
                                                 purchasePost.GoodReceiveNote.Date.ToString("D") + " " +
                                                 purchasePost.GoodReceiveNote.TotalAmount;
                        }
                        else if (purchasePost.PurchaseOrderId != null)
                        {
                            purchseOrerValue12 = purchasePost.PurchaseOrder.RegistrationNo + " " +
                                                 purchasePost.PurchaseOrder.Date.ToString("D") + " " +
                                                 purchasePost.PurchaseOrder.TotalAmount;

                        }


                        if (purchasePost.BillingId != null)
                        {
                            var address = _context.DeliveryAddresses.AsNoTracking()
                                .FirstOrDefault(x => x.Id == purchasePost.BillingId);
                            if (address != null)
                            {
                                StringBuilder addressBuilder = new StringBuilder();

                                // Add customerAddress with a line break
                                if (!string.IsNullOrEmpty(address.Address))
                                {
                                    addressBuilder.AppendLine(address.Address);
                                }

                                // Add customerAddress2 with a line break if it has a value
                                if (!string.IsNullOrEmpty(address.Address2))
                                {
                                    addressBuilder.AppendLine(address.Address2);
                                }

                                // Combine zipCode, city, and country into a single line, if any of them have a value
                                string zipCode = string.IsNullOrEmpty(address.BillingZipCode) ? "" : address.BillingZipCode;
                                string city = string.IsNullOrEmpty(address.City) ? "" : address.City;
                                string country = string.IsNullOrEmpty(address.Country) ? "" : address.Country;

                                string lastLine = $"{zipCode}{(string.IsNullOrEmpty(zipCode) ? "" : ", ")}{city}{(string.IsNullOrEmpty(city) ? "" : ", ")}{country}".Trim();
                                if (!string.IsNullOrEmpty(lastLine))
                                {
                                    addressBuilder.AppendLine(lastLine);
                                }

                                // Set the Text property of Address2
                                purchasePost.Supplier.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }


                        }
                        else
                        {
                            {
                                StringBuilder addressBuilder = new StringBuilder();

                                string address1 = purchasePost.Supplier?.Address;
                                string address2 = purchasePost.Supplier?.DeliveryAddress;
                                string country1 = purchasePost.Supplier?.Country;
                                string zipCode1 = purchasePost.Supplier?.BillingZipCode;
                                string city1 = purchasePost.Supplier?.City;

                                if (!string.IsNullOrEmpty(address1))
                                {
                                    addressBuilder.AppendLine(address1);
                                }

                                if (!string.IsNullOrEmpty(address2))
                                {
                                    addressBuilder.AppendLine(address2);
                                }

                                string zipCode = string.IsNullOrEmpty(zipCode1) ? "" : zipCode1;
                                string city = string.IsNullOrEmpty(city1) ? "" : city1;
                                string country = string.IsNullOrEmpty(country1) ? "" : country1;

                                string lastLine = $"{zipCode}{(string.IsNullOrEmpty(zipCode) ? "" : ", ")}{city}{(string.IsNullOrEmpty(city) ? "" : ", ")}{country}".Trim();
                                if (!string.IsNullOrEmpty(lastLine))
                                {
                                    addressBuilder.AppendLine(lastLine);
                                }

                                purchasePost.Supplier.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }
                        }




                        return new PurchasePostLookupModel
                        {
                            PurchseOrderValue = purchseOrerValue12,
                            Id = purchasePost.Id,
                            PurchaseOrderId = purchasePost.PurchaseOrderId,
                            GoodReceiveNoteId = purchasePost.GoodReceiveNoteId,
                            RegistrationNo = purchasePost.RegistrationNo,
                            PurchaseOrderNo = purchasePost.PurchaseOrder?.RegistrationNo,
                            GoodReceiveNo = purchasePost.GoodReceiveNote?.RegistrationNo,
                            Date = purchasePost.Date,
                            SupplierId = purchasePost.SupplierId,
                            SupplierName = purchasePost.Supplier.CustomerDisplayName,
                            SupplierVAT = purchasePost.Supplier.VatNo,
                            InvoiceNo = purchasePost.InvoiceNo,
                            InvoiceDate = purchasePost.InvoiceDate,
                            IsPurchaseReturn = purchasePost.IsPurchaseReturn,
                            WareHouseId = purchasePost.WareHouseId,
                            TaxMethod = purchasePost.TaxMethod,
                            TaxRateId = purchasePost.TaxRateId,
                            IsRaw = purchasePost.Raw,
                            IsClose = purchasePost.IsClose,
                            IsCost = purchasePost.IsCost,
                            PurchasePostItems = purchaseItems,
                            IsPurchasePost = purchasePost.IsPurchasePost,
                            NetAmount = purchasePost.TotalAmount,
                            BankCashAccountId = purchasePost.BankCashAccountId,
                            PaymentType = purchasePost.PaymentType,
                            IsCredit = purchasePost.IsCredit,

                            Discount = purchasePost.Discount,
                            TransactionLevelDiscount = purchasePost.TransactionLevelDiscount,
                            IsDiscountOnTransaction = purchasePost.IsDiscountOnTransaction,
                            IsFixed = purchasePost.IsFixed,
                            IsBeforeTax = purchasePost.IsBeforeTax,

                            DiscountAmount = purchasePost.DiscountAmount,
                            VatAmount = purchasePost.VatAmount,
                            GrossAmount = purchasePost.TotalWithOutAmount,
                            TotalAmount = purchasePost.TotalAmount,
                            TotalAfterDiscount = purchasePost.TotalAfterDiscount,
                            SupplierQuotationId = purchasePost.SupplierQuotationId,

                            PoNumberAndDate = purchasePost.poNumberAndDate,
                            GoodsRecieveNumberAndDate = purchasePost.GoodsRecieveNumberAndDate,

                            Note = purchasePost.Note,
                            Reference = purchasePost.Reference,

                            SupplierAddress = purchasePost.Supplier.Address,
                            SupplierCNIC = purchasePost.Supplier.CommercialRegistrationNo,
                            SupplierCode = purchasePost.Supplier.Code,
                            SupplierContact = purchasePost.Supplier.ContactNo1,
                            SupplierCRN = purchasePost.Supplier.CommercialRegistrationNo,
                            SupplierRegNo = purchasePost.Supplier.VatNo,
                            SupplierPrefix = purchasePost.Supplier.Prefix,
                            SupplierArabicName = purchasePost.Supplier.ArabicName,
                            SupplierEnglishName = purchasePost.Supplier.EnglishName,

                        };



                    }




                    var po = await _context.PurchasePosts
                        .AsNoTracking()
                        .Include(x => x.GoodReceiveNote)
                        .Include(x => x.PurchaseInvoiceActions)
                        .ThenInclude(x => x.CompanyProcess)
                        .Include(x => x.PurchaseInvoiceAttachments)
                        .Include(x => x.PurchasePostExpenses)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Product)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Product)
                        //.ThenInclude(x => x.BundleCategory)

                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.ProductSizes)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.SaleSizeAssortments)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Color)
                        .Include(x => x.Supplier)
                        .Include(x => x.PurchaseOrder)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var wareHouseName = _context.Warehouses.FirstOrDefault(x => x.Id == po.WareHouseId)?.Name;
                    var taxRates = _context.TaxRates.FirstOrDefault(x => x.Id == po.TaxRateId);

                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == po.Id)
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

                    var purchaseInvoiceAttachments = new List<PurchaseInvoiceAttachment>();
                    if (po.PurchaseInvoiceAttachments != null && po.PurchaseInvoiceAttachments.Any())
                    {
                        foreach (var item in po.PurchaseInvoiceAttachments)
                        {
                            purchaseInvoiceAttachments.Add(new PurchaseInvoiceAttachment
                            {
                                Id = item.Id,
                                Date = item.Date,
                                PurchaseInvoicePostId = item.PurchaseInvoicePostId,
                                Description = item.Description,
                                Path = item.Path
                            });
                        }
                    }

                    var purchaseInvoiceActions = new List<PurchaseInvoiceActionLookUp>();
                    if (po.PurchaseInvoiceActions != null && po.PurchaseInvoiceActions.Any())
                    {
                        foreach (var action in po.PurchaseInvoiceActions)
                        {
                            purchaseInvoiceActions.Add(new PurchaseInvoiceActionLookUp()
                            {
                                Id = action.Id,
                                ProcessId = action.ProcessId,
                                ProcessName = action.CompanyProcess?.ProcessName,
                                ProcessNameArabic = action.CompanyProcess?.ProcessNameArabic,
                                PurchaseInvoicePostId = action.PurchaseInvoicePostId,
                                Date = action.Date,
                                Description = action.Description
                            });
                        }
                    }



                    var purchasePostExpenses = new List<PurchasePostExpense>();
                    if (po.PurchasePostExpenses != null && po.PurchasePostExpenses.Any())
                    {
                        foreach (var item in po.PurchasePostExpenses)
                        {
                            purchasePostExpenses.Add(new PurchasePostExpense()
                            {
                                Id = item.Id,
                                Date = item.Date,
                                BankCashAccountId = item.BankCashAccountId,
                                ContactAccountId = item.ContactAccountId,
                                VatAccountId = item.VatAccountId,
                                TaxRateId = item.TaxRateId,
                                TaxMethod = item.TaxMethod,
                                VoucherNumber = item.VoucherNumber,
                                Narration = item.Narration,
                                ChequeNumber = item.ChequeNumber,
                                Amount = item.Amount,
                                PaymentMode = item.PaymentMode,
                                PaymentMethod = item.PaymentMethod
                            });
                        }
                    }

                    var items = new List<PurchasePostItemLookupModel>();
                    var netAmount = new TotalAmount();
                    decimal discount = 0;
                    //decimal amountWithDiscount = 0;
                    var stocks = _context.Stocks.Where(x => x.WareHouseId == po.WareHouseId);
                    foreach (var item in po.PurchasePostItems)
                    {
                        if (item.ProductId == null)
                        {
                            discount +=  ((po.TaxMethod == "Inclusive" || po.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100)));
                            //amountWithDiscount += discount;

                            if (!request.IsReturnView)
                                items.Add(new PurchasePostItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Description = item.Description,
                                    ProductName = item.Description,
                                    ProductNameArabic = item.Description,
                                    CategoryName = "",
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate,
                                    Discount = item.Discount,
                                    FixDiscount = item.FixDiscount,
                                    Quantity = item.Quantity,
                                    QuantityforCanvas = item.Quantity,
                                    HighQty = 0,
                                    UnitPerPack = item.UnitPerPack ?? 0,
                                    RemainingQuantity = item.RemainingQuantity,
                                    UnitPrice = item.UnitPrice,
                                    TaxMethod = item.TaxMethod,
                                    PurchaseId = item.PurchasePostId,
                                    SerialNo = item.SerialNo,
                                    IsService = item.IsService,
                                    GuaranteeDate = item.GuaranteeDate,
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    
                                    Total = item.Quantity * item.UnitPrice,
                                    DiscountAmount = item.DiscountAmount,
                                    VatAmount = item.VatAmount,
                                    GrossAmount = item.TotalWithOutAmount,
                                    TotalAmount = item.TotalAmount,
                                    Code = item.Product?.Code,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                    //DiscountAmount = ((po.TaxMethod == "Inclusive" || po.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),




                                    BatchNo = item.BatchNo,
                                    ExpiryDate = item.ExpiryDate,
                                    //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                    //        ? ((item.Quantity * item.UnitPrice) -

                                    //            (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / (100 + item.TaxRate.Rate) : 0
                                    //,


                                    //IncludingVat =
                                    //    ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                    //        ? ((item.Quantity * item.UnitPrice) -
                                    //           (item.Discount == 0
                                    //               ? item.Quantity * item.FixDiscount
                                    //               : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / 100 : 0),



                                });
                            else
                                items.Add(new PurchasePostItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Description = item.Description,
                                    ProductName = item.Description,
                                    ProductNameArabic = item.Description,
                                    CategoryName = "",
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate,
                                    Discount = item.Discount,
                                    FixDiscount = item.FixDiscount,
                                    Quantity = item.Quantity,
                                    QuantityforCanvas = item.Quantity,
                                    HighQty = 0,
                                    UnitPerPack = item.UnitPerPack ?? 0,
                                    RemainingQuantity = item.RemainingQuantity,
                                    UnitPrice = item.UnitPrice,
                                    TaxMethod = item.TaxMethod,
                                    PurchaseId = item.PurchasePostId,
                                    SerialNo = item.SerialNo,
                                    IsService = item.IsService,
                                    GuaranteeDate = item.GuaranteeDate,
                                    Total = item.Quantity * item.UnitPrice,
                                    DiscountAmount = item.DiscountAmount,
                                    VatAmount = item.VatAmount,
                                    GrossAmount = item.TotalWithOutAmount,
                                    TotalAmount = item.TotalAmount,
                                    Code = item.Product?.Code,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                    //DiscountAmount = ((po.TaxMethod == "Inclusive" || po.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),


                                    BatchNo = item.BatchNo,
                                    ExpiryDate = item.ExpiryDate,
                                });
                        }
                        else
                        {
                            var prodcutList = await _context.Products.AsNoTracking()
                                .Include(x => x.Category)
                                .FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken: cancellationToken);

                            discount += item.Discount == 0 ? item.FixDiscount * item.Quantity : (item.UnitPrice * item.Quantity * item.Discount) / 100;
                            //amountWithDiscount += discount;
                            var total = item.IsService
                                ? item.Quantity * item.UnitPrice
                                : (item.Product.HighUnitPrice
                                    ? ((item.Quantity / item.Product.UnitPerPack ?? 0) * item.UnitPrice)
                                    : item.Quantity * item.UnitPrice);

                            //var discountCal = item.IsService
                            //    ? (item.Discount == 0
                            //        ? item.Quantity * item.FixDiscount
                            //        : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                            //    : (item.Product.HighUnitPrice
                            //        ? ((item.Discount == 0
                            //            ? (item.Quantity / item.Product.UnitPerPack ?? 0) * item.FixDiscount
                            //            : ((item.Quantity / item.Product.UnitPerPack ?? 0) * item.UnitPrice *
                            //               item.Discount) / 100))
                            //        : ((item.Discount == 0
                            //            ? item.Quantity * item.FixDiscount
                            //            : (item.Quantity * item.UnitPrice * item.Discount) / 100)));

                            if (item.Product.TaxRateId != null && !request.IsReturnView)
                                items.Add(new PurchasePostItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Description = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Description : item.Product.DisplayNameForPrint,
                                    ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Description : item.Product.DisplayNameForPrint,
                                    ProductNameArabic = prodcutList.ArabicName,
                                    CategoryName = prodcutList.Category.Name,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate,
                                    Discount = item.Discount,
                                    FixDiscount = item.FixDiscount,
                                    QuantityforCanvas = item.Quantity,
                                    Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.RemainingQuantity,
                                    HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                    UnitPerPack = item.UnitPerPack ?? 0,
                                    RemainingQuantity = item.RemainingQuantity,
                                    UnitPrice = item.UnitPrice,
                                    TaxMethod = item.TaxMethod,
                                    PurchaseId = item.PurchasePostId,
                                    SerialNo = item.SerialNo,
                                    IsService = item.IsService,
                                    GuaranteeDate = item.GuaranteeDate,
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    ColorId = item.ColorId,
                                    ColorName = item.Color?.Name,
                                    Code = item.Product.Code,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    Total = total,
                                    DiscountAmount = item.DiscountAmount,
                                    VatAmount = item.VatAmount,
                                    GrossAmount = item.TotalWithOutAmount,
                                    TotalAmount = item.TotalAmount,
                                    BatchNo = item.BatchNo,
                                    ExpiryDate = item.ExpiryDate,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,



                                    Product = new ProductDropdownLookUpModel
                                    {
                                        Inventory = new Inventory()
                                        {
                                            CurrentQuantity = stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.BranchId==po.BranchId)?.CurrentQuantity ?? 0
                                        },
                                        Id = item.Product.Id,
                                        SalePrice = item.Product.SalePrice,
                                        BarCode = item.Product.BarCode,
                                        DisplayName = item.Product.DisplayName,
                                        Serial = item.Product.Serial,
                                        Guarantee = item.Product.Guarantee,
                                        LevelOneUnit = item.Product.LevelOneUnit,
                                        BasicUnit = item.Product.Unit?.Name,
                                        StyleNumber = item.Product.StyleNumber,
                                        ProductSizes = item.Product.ProductSizes?.Select(x => new ProductSize
                                        {
                                            ProductId = x.ProductId,
                                            SizeId = x.SizeId,
                                        }).ToList(),
                                        ServiceItem = item.Product.ServiceItem,
                                        HighUnitPrice = item.Product.HighUnitPrice,

                                        Code = item.Product.Code,
                                        EnglishName = item.Product.EnglishName,
                                        ArabicName = item.Product.ArabicName,
                                        IsExpire = item.Product.IsExpire,
                                        //PromotionOffer = item.Product.PromotionOffer == null
                                        //    ? null
                                        //    : new PromotionOffer
                                        //    {
                                        //        FromDate = item.Product.PromotionOffer.FromDate,
                                        //        isActive = item.Product.PromotionOffer.isActive,
                                        //        Offer = item.Product.PromotionOffer.Offer,
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
                                        //        isActive = item.Product.BundleCategory.isActive
                                        //    },
                                        TaxRateId = item.Product.TaxRateId.Value
                                    },
                                    SaleSizeAssortment = item.SaleSizeAssortments?.Select(x => new SaleSizeAssortmentModel
                                    {
                                        SizeId = x.SizeId,
                                        PurchasePostItemId = x.PurchasePostItemId,
                                        ItemName = x.PurchasePostItem.Product?.EnglishName,
                                        Quantity = x.Quantity,
                                    }).ToList()
                                });
                            else
                                items.Add(new PurchasePostItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Description = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Description : item.Product.DisplayNameForPrint,
                                    ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Description : item.Product.DisplayNameForPrint,
                                    ProductNameArabic = prodcutList.ArabicName,
                                    CategoryName = prodcutList.Category.Name,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate,
                                    Discount = item.Discount,
                                    FixDiscount = item.FixDiscount,
                                    QuantityforCanvas = item.Quantity,
                                    Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                    HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                    UnitPerPack = item.UnitPerPack ?? 0,
                                    RemainingQuantity = item.RemainingQuantity,
                                    UnitPrice = item.UnitPrice,
                                    TaxMethod = item.TaxMethod,
                                    PurchaseId = item.PurchasePostId,
                                    SerialNo = item.SerialNo,
                                    IsService = item.IsService,
                                    GuaranteeDate = item.GuaranteeDate,
                                    Total = item.Quantity * item.UnitPrice,
                                    ColorId = item.ColorId,
                                    ColorName = item.Color?.Name,
                                    DiscountAmount = item.DiscountAmount,
                                    VatAmount = item.VatAmount,
                                    GrossAmount = item.TotalWithOutAmount,
                                    TotalAmount = item.TotalAmount,
                                    Code = item.Product.Code,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    BatchNo = item.BatchNo,
                                    ExpiryDate = item.ExpiryDate,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                    Product = new ProductDropdownLookUpModel
                                    {
                                        Inventory = new Inventory()
                                        {
                                            CurrentQuantity = stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.BranchId==po.BranchId)?.CurrentQuantity ?? 0
                                        },
                                        Id = item.Product.Id,
                                        StyleNumber = item.Product.StyleNumber,
                                        DisplayName = item.Product.DisplayName,
                                        ServiceItem = item.Product.ServiceItem,
                                        HighUnitPrice = item.Product.HighUnitPrice,
                                        SalePrice = item.Product.SalePrice,
                                        BarCode = item.Product.BarCode,
                                        Serial = item.Product.Serial,
                                        Guarantee = item.Product.Guarantee,
                                        ProductSizes = item.Product.ProductSizes?.Select(x => new ProductSize
                                        {
                                            ProductId = x.ProductId,
                                            SizeId = x.SizeId,
                                        }).ToList(),

                                        Code = item.Product.Code,
                                        EnglishName = item.Product.EnglishName,
                                        ArabicName = item.Product.ArabicName,
                                        IsExpire = item.Product.IsExpire,
                                        //PromotionOffer = item.Product.PromotionOffer == null
                                        //    ? null
                                        //    : new PromotionOffer
                                        //    {
                                        //        FromDate = item.Product.PromotionOffer.FromDate,
                                        //        isActive = item.Product.PromotionOffer.isActive,
                                        //        Offer = item.Product.PromotionOffer.Offer,
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
                                        //        isActive = item.Product.BundleCategory.isActive
                                        //    },
                                        TaxRateId = item.Product.TaxRateId.Value
                                    },
                                    SaleSizeAssortment = item.SaleSizeAssortments?.Select(x => new SaleSizeAssortmentModel
                                    {
                                        SizeId = x.SizeId,
                                        PurchasePostItemId = x.PurchasePostItemId,
                                        ItemName = x.PurchasePostItem.Product?.EnglishName,
                                        Quantity = x.Quantity,
                                    }).ToList()
                                });
                        }

                    }


                    string purchseOrerValue = "";
                    if (po.GoodReceiveNoteId != null)
                    {
                        purchseOrerValue = po.GoodReceiveNote.RegistrationNo + " " +
                                           po.GoodReceiveNote.Date.ToString("D") + " " +
                                           po.GoodReceiveNote.TotalAmount;
                    }
                    else if (po.PurchaseOrderId != null)
                    {
                        purchseOrerValue = po.PurchaseOrder.RegistrationNo + " " +
                                           po.PurchaseOrder.Date.ToString("D") + " " +
                                           po.PurchaseOrder.TotalAmount;

                    }

                    return new PurchasePostLookupModel
                    {
                        NationalId = po.NationalId,
                        BillingId = po.BillingId,
                        ShippingId = po.ShippingId,
                        DeliveryId = po.DeliveryId,
                        PurchseOrderValue = purchseOrerValue,
                        Id = po.Id,
                        PurchaseOrderId = po.PurchaseOrderId,
                        GoodReceiveNoteId = po.GoodReceiveNoteId,
                        RegistrationNo = po.RegistrationNo,
                        PurchaseOrderNo = po.PurchaseOrder?.RegistrationNo,
                        GoodReceiveNo = po.GoodReceiveNote?.RegistrationNo,
                        TaxRatesName = taxRates?.Name,
                        WareHouseName = wareHouseName,
                        Date = po.Date,
                        SupplierId = po.SupplierId,
                        SupplierName = (po.Supplier.Code + "--" + po.Supplier.CustomerDisplayName),
                        SupplierVAT = po.Supplier.VatNo,
                        InvoiceNo = po.InvoiceNo,
                        InvoiceDate = po.InvoiceDate,
                        IsPurchaseReturn = po.IsPurchaseReturn,
                        WareHouseId = po.WareHouseId,
                        TaxMethod = po.TaxMethod,
                        TaxRateId = po.TaxRateId,
                        IsRaw = po.Raw,
                        IsClose = po.IsClose,
                        IsCost = po.IsCost,
                        PurchasePostItems = items,
                        IsPurchasePost = po.IsPurchasePost,
                        NetAmount = netAmount.CalculateTotalAmount(po),
                        PurchaseInvoiceAttachments = purchaseInvoiceAttachments,
                        PurchaseInvoiceActions = purchaseInvoiceActions,
                        PurchasePostExpenses = purchasePostExpenses,
                        BankCashAccountId = po.BankCashAccountId,
                        PaymentType = po.PaymentType,
                        IsCredit = po.IsCredit,
                        AttachmentList = attachmentList,

                        Discount = po.Discount,
                        TransactionLevelDiscount = po.TransactionLevelDiscount,
                        IsDiscountOnTransaction = po.IsDiscountOnTransaction,
                        IsFixed = po.IsFixed,
                        IsBeforeTax = po.IsBeforeTax,

                        DiscountAmount = po.DiscountAmount,
                        VatAmount = po.VatAmount,
                        GrossAmount = po.TotalWithOutAmount,
                        TotalAmount = po.TotalAmount,
                        TotalAfterDiscount = po.TotalAfterDiscount,
                        SupplierQuotationId = po.SupplierQuotationId,

                        PoNumberAndDate =  po.poNumberAndDate,
                        GoodsRecieveNumberAndDate = po.GoodsRecieveNumberAndDate,

                        Note = po.Note,
                        Reference = po.Reference,

                        SupplierAddress = po.Supplier.Address,
                        SupplierCNIC = po.Supplier.CommercialRegistrationNo,
                        SupplierCode= po.Supplier.CustomerCode,
                        SupplierContact = po.Supplier.ContactNo1,
                        SupplierCRN = po.Supplier.CommercialRegistrationNo, 
                        SupplierRegNo= po.Supplier.VatNo,
                        SupplierPrefix = po.Supplier.Prefix,
                        SupplierArabicName = po.Supplier.ArabicName,
                        SupplierEnglishName = po.Supplier.EnglishName,



                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}