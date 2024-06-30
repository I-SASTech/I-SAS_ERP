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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Attachments.Commands;
using Focus.Business.Colors.Queries.GetColorDetails;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.Sales.Commands.AddSale;
using Focus.Business.Sales.Queries.CashVoucherQuery;
using Focus.Business.Users;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Focus.Business.Contacts.Queries.GetContactRunningBalance;
using Microsoft.Extensions.Hosting;
using Focus.Business.Prefix.Quries;
using DocumentFormat.OpenXml.InkML;
using System.Text;

namespace Focus.Business.Sales.Queries.GetSaleDetail
{
    public class GetSaleDetailQuery : IRequest<SaleDetailLookupModel>
    {
        public Guid Id { get; set; }
        public bool IsReturn { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public string InvoiceNo { get; set; }
        public bool ColorVariants { get; set; }
        public bool SimpleQuery { get; set; }
        public bool DeliveryChallan { get; set; }
        public decimal OpeningBalance { get; set; }

        public class Handler : IRequestHandler<GetSaleDetailQuery, SaleDetailLookupModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            public readonly IMapper Mapper;
            public readonly UserManager<ApplicationUser> UserManager;
            private readonly IConfiguration _configuration;
            private readonly IHostingEnvironment _hostingEnvironment;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetSaleDetailQuery> logger,
                IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IHostingEnvironment hostingEnvironment, IMediator mediator)
            {
                UserManager = userManager;
                Context = context;
                Logger = logger;
                Mapper = mapper;
                _configuration = configuration;
                _hostingEnvironment = hostingEnvironment;
                _mediator = mediator;

            }
            public async Task<SaleDetailLookupModel> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.SimpleQuery)
                    {
                        var salesRecord = await Context.Sales.AsNoTracking()
                            .Include(x => x.Customer)
                            .Include(x => x.SaleItems)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Unit)
                            .Include(x => x.Quotation)
                            .Include(x => x.SaleOrder)
                            .Include(x => x.TaxRate)
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                        if (salesRecord == null)
                            throw new NotFoundException("Sale Invoice Cannot be found", request.Id);

                        var saleRecordItems = new List<SaleItemLookupModel>();

                        {
                            foreach (var item in salesRecord.SaleItems)
                            {
                                saleRecordItems.Add(new SaleItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductName = item.Product?.EnglishName,
                                    Code = string.IsNullOrEmpty(item.Product?.Code) ? item.StyleNumber : item.Product?.Code,
                                    ProductId = item.ProductId,
                                    ServiceProductId = item.Id,
                                    SoItemId = item.SoItemId,
                                    Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                    IsFree = item.IsFree,
                                    ReturnQuantity = 0,
                                    Quantity = item.Quantity,
                                    TaxRateId = item.TaxRateId,
                                    UnitPrice = item.UnitPrice,
                                    Total = item.Quantity * item.UnitPrice,
                                    TaxMethod = item.TaxMethod,
                                    TaxRateName = Convert.ToInt32(salesRecord.TaxRate?.Rate) +"%",
                                    PromotionId = item.PromotionId,
                                    BundleId = item.BundleId,
                                    BatchNo = !string.IsNullOrEmpty(item.Product?.Width) ? item.Product?.Width : "",
                                    BatchExpiry = item.BatchExpiry,
                                    Serial = item.Serial,
                                    GuaranteeDate = item.GuaranteeDate,
                                    ServiceItem = item.ServiceItem,
                                    StyleNumber = !string.IsNullOrEmpty(item.Product?.StyleNumber) ? item.Product?.StyleNumber : item.StyleNumber,
                                    Width = !string.IsNullOrEmpty(item.Product?.Width) ? item.Product?.Width : "",
                                    GrossAmount = item.TotalWithOutAmount,
                                    DiscountAmount = item.DiscountAmount,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    VatAmount = item.VatAmount,
                                    TotalAmount = item.TotalAmount,
                                    LineTotal = item.TotalAmount,
                                    DisplayName = item.Product?.DisplayName,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                });
                            }

                        }

                     
                        string deliveryChallanNumber12 = "";
                        if (salesRecord.DeliveryChallanId != null)
                        {
                            var deliveryChallan = await Context.DeliveryChallans.AsNoTracking().
                                Select(x => new
                                {
                                    x.Id,
                                    x.RegistrationNo,
                                }).FirstOrDefaultAsync(x => x.Id == salesRecord.DeliveryChallanId, cancellationToken: cancellationToken);

                            if (deliveryChallan != null)
                            {
                                deliveryChallanNumber12 = deliveryChallan.RegistrationNo;
                            }
                        }

                        if (salesRecord.BillingId != null)
                        {
                            var address = Context.DeliveryAddresses.AsNoTracking()
                                .FirstOrDefault(x => x.Id == salesRecord.BillingId);
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
                                salesRecord.Customer.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }


                        }
                        else
                        {
                            {
                                StringBuilder addressBuilder = new StringBuilder();

                                string address1 = salesRecord.Customer?.Address;
                                string address2 = salesRecord.Customer?.DeliveryAddress;
                                string country1 = salesRecord.Customer?.Country;
                                string zipCode1 = salesRecord.Customer?.BillingZipCode;
                                string city1 = salesRecord.Customer?.City;

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

                                salesRecord.Customer.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }
                        }

                        string wareHouseName = "";
                        string wareHouseNameAr = "";

                        var wareHouse = Context.Warehouses.AsNoTracking()
                            .FirstOrDefault(x => x.Id == salesRecord.WareHouseId);
                        if (wareHouse != null)
                        {
                            wareHouseName = wareHouse.Name;
                            wareHouseNameAr = wareHouse.NameArabic;
                        }


                        return new SaleDetailLookupModel

                        {
                            PurchaseOrderId = salesRecord.PurchaseOrderId,
                            ProformaId = salesRecord.ProformaId,
                            InquiryId = salesRecord.InquiryId,
                            WareHouseName = wareHouseName,
                            WareHouseNameAr = wareHouseNameAr,
                            CustomerInquiry = salesRecord.CustomerInquiry,
                            DeliveryAddress = salesRecord.DeliveryAddress,
                            DeliveryDate = salesRecord.DeliveryDate,
                            BillingAddress = salesRecord.BillingAddress,
                            ReferredBy = salesRecord.ReferredBy,
                            DeliveryChallanId = salesRecord.DeliveryChallanId,
                            DeliveryNo = deliveryChallanNumber12,
                            Id = salesRecord.Id,
                            DispatchDate = salesRecord.DispatchDate,
                            PickUpDate = salesRecord.PickUpDate,
                            IsWarranty = salesRecord.IsWarranty,
                            TaxMethod = salesRecord.TaxMethod,
                            TaxRateId = salesRecord.TaxRateId,
                            LogisticId = salesRecord.LogisticId,
                            PoNumber = salesRecord.PoNumber,
                            PoDate = salesRecord.PoDate,
                            QuotationId = salesRecord.QuotationId,
                            DoctorName = salesRecord.DoctorName,
                            HospitalName = salesRecord.HospitalName,
                            IsService = salesRecord.IsService,
                            Mobile = salesRecord.Mobile,
                            Date = salesRecord.Date,
                            BarCode = salesRecord.BarCode,
                            OpeningBalance = request.OpeningBalance,
                            Description = salesRecord.Description,
                            CustomerAddressWalkIn = salesRecord.CustomerAddressWalkIn,
                            SaleOrderId = salesRecord.SaleOrderId,
                            SaleOrderNo = salesRecord.SaleOrder?.RegistrationNo,
                            SaleInvoiceId = salesRecord.SaleInvoiceId,
                            InvoiceNo = salesRecord.SaleInvoiceId != null ? Context.Sales.FirstOrDefault(x => x.Id == salesRecord.SaleInvoiceId)?.RegistrationNo : null,
                            RefrenceNo = salesRecord.RefrenceNo,
                            EmployeeId = salesRecord.EmployeeId,
                            EmployeeName = salesRecord.Employee?.EnglishName,
                            TerminalId = salesRecord.TerminalId.ToString(),
                            AreaId = salesRecord.AreaId,
                            AreaName = salesRecord.Area?.Area,
                            IsSaleReturnPost = salesRecord.IsSaleReturnPost,
                            Change = salesRecord.Change.ToString("0.00"),
                            InvoiceType = salesRecord.InvoiceType,
                            RegistrationNo = salesRecord.RegistrationNo,
                            CustomerId = salesRecord.CustomerId,
                            CreditCustomer = salesRecord.CustomerId == null ? null : salesRecord.Customer.Code + "--" + salesRecord.Customer.EnglishName,
                            CustomerCRN = salesRecord.Customer?.CommercialRegistrationNo,
                            UnRegisteredVAtAmount = salesRecord.UnRegisteredVAtAmount,
                            DueDate = salesRecord.DueDate,
                            CashCustomer = salesRecord.CashCustomer?.Name,
                            CustomerCode = salesRecord.Customer?.Code,
                            CustomerCategory = salesRecord.Customer?.Category,
                            CustomerNameEn = salesRecord.Customer?.EnglishName,
                            CustomerNameAr = salesRecord.Customer?.ArabicName,
                            CustomerTelephone = salesRecord.Customer?.ContactNo1,
                            ContactPerson1 = salesRecord.Customer?.ContactPerson1,
                            CustomerVat = salesRecord.Customer?.VatNo,

                            CustomerAddress = salesRecord.Customer?.Address,
                            //CustomerAddress2 = salesRecord.Customer?.DeliveryAddress,
                            //CustomerCountry =  salesRecord.Customer?.Country,
                            //CustomerZipCode =  salesRecord.Customer?.BillingZipCode,
                            //CustomerCity =  salesRecord.Customer?.City,

                            ShippingAddress = salesRecord.Customer?.ShippingAddress,
                            Email = salesRecord.Customer == null ? "" : salesRecord.Customer?.Email,
                            SaleItems = saleRecordItems,
                            IsCredit = salesRecord.IsCredit,
                            InvoiceNote = salesRecord.InvoiceNote == "" ? (salesRecord.QuotationId != null ? "This Invoice Is Generated by Quotation" : salesRecord.SaleOrderId != null ? "This Invoice Is Generated by Sale Order" : "") : salesRecord.InvoiceNote,
                            TermAndCondition = salesRecord.TermAndCondition,
                            TermAndConditionAr = salesRecord.TermAndConditionAr,
                            WarrantyLogoPath = salesRecord.WarrantyLogoPath,
                            CustomerName = salesRecord.CustomerId != null && salesRecord.CustomerId != Guid.Empty ? salesRecord.Customer.CustomerDisplayName : null,
                            Discount = salesRecord.Discount,
                            TransactionLevelDiscount = salesRecord.TransactionLevelDiscount,
                            IsDiscountOnTransaction = salesRecord.IsDiscountOnTransaction,
                            IsFixed = salesRecord.IsFixed,
                            IsBeforeTax = salesRecord.IsBeforeTax,
                            DeliveryId = salesRecord.DeliveryId,
                            ShippingId = salesRecord.ShippingId,
                            BillingId = salesRecord.BillingId,
                            NationalId = salesRecord.NationalId,
                            SaleOrderNoAndDate = salesRecord.SaleOrderNo,
                            PeroformaInvoiceNo = salesRecord.PeroformaInvoiceNo,
                            PurchaseOrderNo = salesRecord.PurchaseOrderNo,
                            InquiryNoAndDate = salesRecord.InquiryNo,
                            QuotationNoAndDate = salesRecord.QuotationNo,
                            DeliveryNoteAndDate = salesRecord.DeliveryNoteAndDate,
                            QuotationValidUpto = salesRecord.QuotationValidUpto,
                            PerfomaValidUpto = salesRecord.PerfomaValidUpto,
                            Note = salesRecord.Note,
                            TotalAmount = salesRecord.TotalAmount,
                            DiscountAmount = salesRecord.DiscountAmount,
                            VatAmount = salesRecord.VatAmount,
                            GrossAmount = salesRecord.TotalWithOutAmount,
                            AfterDiscountAmount = salesRecord.TotalWithOutAmount,
                            CustomerDisplayName = salesRecord.Customer.CustomerDisplayName,
                         
                        };



                    }

                    if (request.DeliveryChallan)
                    {
                        var salesRecord = await Context.Sales
                       .AsNoTracking()
                       .Include(x => x.SaleItems)
                       .ThenInclude(x => x.Product)
                       .Include(x => x.Customer)
                       .FirstOrDefaultAsync(x => x.Id == request.Id || x.RegistrationNo == request.InvoiceNo, cancellationToken);

                        if (salesRecord == null)
                            throw new NotFoundException("Sale Invoice Cannot be found", request.Id);

                        var saleRecordItems = new List<SaleItemLookupModel>();

                        var reservedChallan = Context.DeliveryChallanReserveds.AsNoTracking().Include(x => x.DeliveryChallanReserverdItems).FirstOrDefault(x => x.SaleInvoiceId == request.Id);
                        if (reservedChallan != null)
                        {
                            var itemsOfReserveChallan = reservedChallan.DeliveryChallanReserverdItems.Where(x => x.IsActive && x.Quantity > 0).Select(x => x.ProductId);
                            var itemsOfReserveChallanService = reservedChallan.DeliveryChallanReserverdItems.Where(x => x.IsActive && x.Quantity > 0).Select(x => x.ServiceProductId);

                            foreach (var item in salesRecord.SaleItems)
                            {
                                if (item.ProductId != null)
                                {
                                    if (itemsOfReserveChallan.Contains(item.ProductId))
                                    {
                                        if (item.ProductId != null)
                                        {
                                            saleRecordItems.Add(new SaleItemLookupModel
                                            {
                                                Id = item.Id,
                                                ProductId = item.ProductId,
                                                SoItemId = item.SoItemId,
                                                Code = item.Product.Code,
                                                ProductName = item.Product.EnglishName,
                                                Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                                IsFree = item.IsFree,
                                                ReturnQuantity = 0,
                                                ArabicName = item.Product.ArabicName,
                                                SaleReturnDays = item.Product.SaleReturnDays,
                                                Discount = item.Discount,
                                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                                Quantity = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity ?? 0,
                                                OfferQuantity = item.OfferQuantity,
                                                TaxRateId = item.TaxRateId,
                                                UnitPrice = item.UnitPrice,
                                                Total = item.Quantity * item.UnitPrice,
                                                TaxMethod = item.TaxMethod,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                BatchNo = item.BatchNo,
                                                SoQty = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity ?? 0,
                                                BatchExpiry = item.BatchExpiry,
                                                Serial = item.Serial,
                                                GuaranteeDate = item.GuaranteeDate,
                                                ServiceItem = item.ServiceItem,
                                                StyleNumber = item.Product?.StyleNumber,
                                                UnitPerPack = item.Product?.UnitPerPack,
                                                GrossAmount = item.TotalWithOutAmount,
                                                DiscountAmount = item.DiscountAmount,
                                                AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                                VatAmount = item.VatAmount,
                                                TotalAmount = item.TotalAmount,
                                                LineTotal = item.TotalAmount,
                                                DisplayName = item.Product?.DisplayName,
                                                UnitName = item.Product?.Unit?.Name,
                                                Product = new ProductDropdownLookUpModel
                                                {
                                                    Id = item.Product.Id,
                                                    BarCode = item.Product.BarCode,
                                                    Serial = item.Product.Serial,
                                                    Guarantee = item.Product.Guarantee,
                                                    StockLevel = item.Product.StockLevel,
                                                    Code = item.Product.Code,
                                                    SaleReturnDays = item.Product.SaleReturnDays,
                                                    EnglishName = item.Product.EnglishName,
                                                    ArabicName = item.Product.ArabicName,
                                                    DisplayName = item.Product.DisplayName,
                                                    LevelOneUnit = item.Product.LevelOneUnit,
                                                    BasicUnit = item.Product.Unit?.Name,
                                                    IsExpire = item.Product.IsExpire,
                                                    ServiceItem = item.Product.ServiceItem,
                                                    HighUnitPrice = item.Product.HighUnitPrice,
                                                }


                                            });

                                        }

                                        else
                                        {
                                            saleRecordItems.Add(new SaleItemLookupModel
                                            {

                                                Id = item.Id,
                                                ProductId = item.ProductId,
                                                SoItemId = item.SoItemId,
                                                Description = item.Description,
                                                IsFree = item.IsFree,
                                                ReturnQuantity = 0,
                                                Discount = item.Discount,
                                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                                Quantity = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity ?? 0,
                                                OfferQuantity = item.OfferQuantity,
                                                TaxRateId = item.TaxRateId,
                                                UnitPrice = item.UnitPrice,
                                                Total = item.Quantity * item.UnitPrice,
                                                TaxMethod = item.TaxMethod,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                BatchNo = item.BatchNo,
                                                SoQty = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity ?? 0,
                                                BatchExpiry = item.BatchExpiry,
                                                Serial = item.Serial,
                                                GuaranteeDate = item.GuaranteeDate,
                                                ServiceItem = item.ServiceItem,
                                                StyleNumber = item.Product?.StyleNumber,
                                                GrossAmount = item.TotalWithOutAmount,
                                                DiscountAmount = item.DiscountAmount,
                                                AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                                VatAmount = item.VatAmount,
                                                TotalAmount = item.TotalAmount,
                                                LineTotal = item.TotalAmount


                                            });

                                        }
                                    }


                                }
                                else if (item.Id != null)
                                {
                                    if (itemsOfReserveChallanService.Contains(item.Id))
                                    {
                                        if (item.ProductId != null)
                                        {
                                            saleRecordItems.Add(new SaleItemLookupModel
                                            {
                                                Id = item.Id,
                                                ProductId = item.ProductId,
                                                SoItemId = item.SoItemId,
                                                Code = item.Product.Code,
                                                ProductName = item.Product.EnglishName,
                                                Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                                IsFree = item.IsFree,
                                                ReturnQuantity = 0,
                                                ArabicName = item.Product.ArabicName,
                                                SaleReturnDays = item.Product.SaleReturnDays,
                                                Discount = item.Discount,
                                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                                Quantity = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ServiceProductId == item.Id)?.Quantity ?? 0,
                                                OfferQuantity = item.OfferQuantity,
                                                TaxRateId = item.TaxRateId,
                                                UnitPrice = item.UnitPrice,
                                                Total = item.Quantity * item.UnitPrice,
                                                TaxMethod = item.TaxMethod,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                BatchNo = item.BatchNo,
                                                SoQty = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ServiceProductId == item.Id)?.Quantity ?? 0,
                                                BatchExpiry = item.BatchExpiry,
                                                Serial = item.Serial,
                                                GuaranteeDate = item.GuaranteeDate,
                                                ServiceItem = item.ServiceItem,
                                                StyleNumber = item.Product?.StyleNumber,
                                                UnitPerPack = item.Product?.UnitPerPack,
                                                GrossAmount = item.TotalWithOutAmount,
                                                DiscountAmount = item.DiscountAmount,
                                                AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                                VatAmount = item.VatAmount,
                                                TotalAmount = item.TotalAmount,
                                                DisplayName = item.Product?.DisplayName,
                                                LineTotal = item.TotalAmount,
                                                UnitName = item.Product?.Unit?.Name,
                                                Product = new ProductDropdownLookUpModel
                                                {
                                                    Id = item.Product.Id,
                                                    BarCode = item.Product.BarCode,
                                                    Serial = item.Product.Serial,
                                                    Guarantee = item.Product.Guarantee,
                                                    StockLevel = item.Product.StockLevel,
                                                    Code = item.Product.Code,
                                                    SaleReturnDays = item.Product.SaleReturnDays,
                                                    EnglishName = item.Product.EnglishName,
                                                    ArabicName = item.Product.ArabicName,
                                                    DisplayName = item.Product.DisplayName,
                                                    LevelOneUnit = item.Product.LevelOneUnit,
                                                    BasicUnit = item.Product.Unit?.Name,
                                                    IsExpire = item.Product.IsExpire,
                                                    ServiceItem = item.Product.ServiceItem,
                                                    HighUnitPrice = item.Product.HighUnitPrice,
                                                }


                                            });

                                        }

                                        else
                                        {
                                            saleRecordItems.Add(new SaleItemLookupModel
                                            {

                                                Id = item.Id,
                                                ProductId = item.ProductId,
                                                SoItemId = item.SoItemId,
                                                Description = item.Description,
                                                IsFree = item.IsFree,
                                                ReturnQuantity = 0,
                                                Discount = item.Discount,
                                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                                Quantity = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ServiceProductId == item.Id)?.Quantity ?? 0,
                                                OfferQuantity = item.OfferQuantity,
                                                TaxRateId = item.TaxRateId,
                                                UnitPrice = item.UnitPrice,
                                                Total = item.Quantity * item.UnitPrice,
                                                TaxMethod = item.TaxMethod,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                BatchNo = item.BatchNo,
                                                SoQty = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ServiceProductId == item.Id)?.Quantity ?? 0,
                                                BatchExpiry = item.BatchExpiry,
                                                Serial = item.Serial,
                                                GuaranteeDate = item.GuaranteeDate,
                                                ServiceItem = item.ServiceItem,
                                                StyleNumber = item.Product?.StyleNumber,
                                                GrossAmount = item.TotalWithOutAmount,
                                                DiscountAmount = item.DiscountAmount,
                                                AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                                VatAmount = item.VatAmount,
                                                TotalAmount = item.TotalAmount,
                                                LineTotal = item.TotalAmount


                                            });

                                        }
                                    }

                                }
                            }

                        }
                        else
                        {
                            foreach (var item in salesRecord.SaleItems)
                            {
                                {
                                    if (item.ProductId != null)
                                    {
                                        saleRecordItems.Add(new SaleItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            SoItemId = item.SoItemId,
                                            Code = item.Product.Code,
                                            ProductName = item.Product.EnglishName,
                                            Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                            IsFree = item.IsFree,
                                            ReturnQuantity = 0,
                                            ArabicName = item.Product.ArabicName,
                                            SaleReturnDays = item.Product.SaleReturnDays,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            OfferQuantity = item.OfferQuantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            PromotionId = item.PromotionId,
                                            BundleId = item.BundleId,
                                            BatchNo = item.BatchNo,
                                            BatchExpiry = item.BatchExpiry,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.Product?.StyleNumber,
                                            UnitPerPack = item.Product?.UnitPerPack,
                                            DisplayName = item.Product?.DisplayName,
                                            UnitName = item.Product?.Unit?.Name,
                                            Product = new ProductDropdownLookUpModel
                                            {
                                                Id = item.Product.Id,
                                                BarCode = item.Product.BarCode,
                                                Serial = item.Product.Serial,
                                                Guarantee = item.Product.Guarantee,
                                                StockLevel = item.Product.StockLevel,
                                                Code = item.Product.Code,
                                                SaleReturnDays = item.Product.SaleReturnDays,
                                                EnglishName = item.Product.EnglishName,
                                                ArabicName = item.Product.ArabicName,
                                                DisplayName = item.Product.DisplayName,
                                                LevelOneUnit = item.Product.LevelOneUnit,
                                                BasicUnit = item.Product.Unit?.Name,
                                                IsExpire = item.Product.IsExpire,
                                                ServiceItem = item.Product.ServiceItem,
                                                HighUnitPrice = item.Product.HighUnitPrice,
                                            },
                                            GrossAmount = item.TotalWithOutAmount,
                                            DiscountAmount = item.DiscountAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            VatAmount = item.VatAmount,
                                            TotalAmount = item.TotalAmount,
                                            LineTotal = item.TotalAmount


                                        });
                                    }
                                    else
                                    {
                                        saleRecordItems.Add(new SaleItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            ServiceProductId = item.Id,
                                            SoItemId = item.SoItemId,
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            ReturnQuantity = 0,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            OfferQuantity = item.OfferQuantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            PromotionId = item.PromotionId,
                                            BundleId = item.BundleId,
                                            BatchNo = item.BatchNo,
                                            BatchExpiry = item.BatchExpiry,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.Product?.StyleNumber,
                                            GrossAmount = item.TotalWithOutAmount,
                                            DiscountAmount = item.DiscountAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            VatAmount = item.VatAmount,
                                            TotalAmount = item.TotalAmount,
                                            LineTotal = item.TotalAmount

                                        });

                                    }
                                }









                            }

                        }




                        return new SaleDetailLookupModel
                        {

                            Id = salesRecord.Id,
                            DispatchDate = salesRecord.DispatchDate,
                            PickUpDate = salesRecord.PickUpDate,
                            IsWarranty = salesRecord.IsWarranty,
                            TaxMethod = salesRecord.TaxMethod,
                            TaxRateId = salesRecord.TaxRateId,
                            LogisticId = salesRecord.LogisticId,
                            PoNumber = salesRecord.PoNumber,
                            PoDate = salesRecord.PoDate,
                            QuotationId = salesRecord.QuotationId,
                            DoctorName = salesRecord.DoctorName,
                            HospitalName = salesRecord.HospitalName,
                            IsService = salesRecord.IsService,
                            Mobile = salesRecord.Mobile,
                            Date = salesRecord.Date,
                            BarCode = salesRecord.BarCode,
                            OpeningBalance = request.OpeningBalance,
                            Description = salesRecord.Description,
                            CustomerAddressWalkIn = salesRecord.CustomerAddressWalkIn,
                            SaleOrderId = salesRecord.SaleOrderId,
                            SaleOrderNo = salesRecord.SaleOrder?.RegistrationNo,
                            SaleInvoiceId = salesRecord.SaleInvoiceId,
                            RefrenceNo = salesRecord.RefrenceNo,
                            EmployeeId = salesRecord.EmployeeId,
                            AreaId = salesRecord.AreaId,
                            IsSaleReturnPost = salesRecord.IsSaleReturnPost,
                            InvoiceNote = salesRecord.InvoiceNote == "" ? (salesRecord.QuotationId != null ? "This Invoice Is Generated by Quotation" : salesRecord.SaleOrderId != null ? "This Invoice Is Generated by Sale Order" : "") : salesRecord.InvoiceNote,
                            Change = salesRecord.Change.ToString("0.00"),
                            InvoiceType = salesRecord.InvoiceType,
                            RegistrationNo = salesRecord.RegistrationNo,
                            CustomerId = salesRecord.CustomerId,
                            CustomerCRN = salesRecord.Customer?.CommercialRegistrationNo,
                            UnRegisteredVAtAmount = salesRecord.UnRegisteredVAtAmount,
                            DueDate = salesRecord.DueDate,
                            CustomerCode = salesRecord.Customer?.Code,
                            CustomerCategory = salesRecord.Customer?.Category,
                            CustomerNameEn = salesRecord.Customer?.EnglishName,
                            CustomerNameAr = salesRecord.Customer?.ArabicName,
                            CustomerTelephone = salesRecord.Customer?.ContactNo1,
                            CustomerName = salesRecord.Customer?.CustomerDisplayName,
                            ContactPerson1 = salesRecord.Customer?.ContactPerson1,
                            CustomerVat = salesRecord.Customer?.VatNo,
                            CustomerAddress = salesRecord.Customer?.Address,
                            ShippingAddress = salesRecord.Customer?.ShippingAddress,
                            Code = salesRecord.CashCustomer?.Code,
                            CashCustomerId = salesRecord.CashCustomer?.CustomerId,
                            WareHouseId = salesRecord.WareHouseId,
                            SaleItems = saleRecordItems,
                            IsCredit = salesRecord.IsCredit,
                            TermAndCondition = salesRecord.TermAndCondition,
                            TermAndConditionAr = salesRecord.TermAndConditionAr,
                            WarrantyLogoPath = salesRecord.WarrantyLogoPath,
                            PartiallyInvoice = salesRecord.PartiallyInvoices.ToString(),
                            Discount = salesRecord.Discount,
                            UnRegisteredVatId = salesRecord.UnRegisteredVatId,
                            TransactionLevelDiscount = salesRecord.TransactionLevelDiscount,
                            IsDiscountOnTransaction = salesRecord.IsDiscountOnTransaction,
                            IsFixed = salesRecord.IsFixed,
                            IsBeforeTax = salesRecord.IsBeforeTax,
                            TotalAmount = salesRecord.TotalAmount,
                            DiscountAmount = salesRecord.DiscountAmount,
                            VatAmount = salesRecord.VatAmount,
                            GrossAmount = salesRecord.TotalWithOutAmount,
                            AfterDiscountAmount = salesRecord.TotalWithOutAmount,
                            DeliveryChallanId = salesRecord.DeliveryChallanId,
                            ProformaId = salesRecord.ProformaId,

                            CustomerDisplayName = salesRecord.Customer.CustomerDisplayName,
                        };


                    }

                    string counterCode = null;
                    string userName = null;
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
                        .AsNoTracking()
                        .Include(x => x.Employee)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        //.ThenInclude(x => x.BundleCategory)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Inventories)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.WareHouse)
                        .Include(x => x.Customer)
                        .Include(x => x.SalePayments)
                        .ThenInclude(x => x.PaymentOption)
                        .Include(x => x.SaleOrder)
                        .ThenInclude(x => x.SaleOrderItems)
                        .Include(x => x.Logistic)
                        .FirstOrDefaultAsync(x => x.Id == request.Id || x.RegistrationNo == request.InvoiceNo, cancellationToken);

                    if (sale == null)
                        throw new NotFoundException("Sale Invoice Cannot be found", request.Id);

                    List<Stock> stocks = null;
                    List<VariationInventory> variationInventories = null;
                    if (request.ColorVariants)
                    {
                        variationInventories = await Context.VariationInventories.ToListAsync(cancellationToken: cancellationToken);
                    }
                    else
                    {
                        stocks = await Context.Stocks.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    }


                    IQueryable<Inventory> inventoryData = null;
                    if (request.IsFifo)
                    {

                        inventoryData = Context.Inventories.Where(x =>
                                x.IsActive && x.IsOpen && (x.TransactionType == TransactionType.PurchasePost ||
                                                           x.TransactionType == TransactionType.StockIn)).AsQueryable();

                    }


                    if (!string.IsNullOrEmpty(request.InvoiceNo) && sale.InvoiceType == InvoiceType.Hold)
                    {
                        throw new NotFoundException(request.InvoiceNo + " Sale Invoice Cannot be found", request.Id);
                    }


                    var attachmentList = await Context.Attachments.AsNoTracking().Where(x => x.DocumentId == request.Id).Select(x => new AttachmentLookUpModel
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
                            .Where(x => x.SaleInvoice == sale.Id && x.ApprovalStatus == ApprovalStatus.Approved).ToListAsync(cancellationToken: cancellationToken);
                    }
                    IQueryable<SaleItem> itemList = null;
                    //var itemList = new IQueryable<SaleItem>();
                    if (request.IsReturn)
                    {
                        itemList = Context.SaleItems.AsNoTracking()
                            .Where(x => x.SaleId == sale.SaleInvoiceId).AsQueryable();
                    }

                    var cashVoucher = new CashVoucherLookUpModel();
                    var saleItems = new List<SaleItemLookupModel>();
                    var salePayments = new SalePaymentLookupModel();
                    var voucher = await Context.CashVouchers.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.SaleReturnId == sale.Id, cancellationToken: cancellationToken);
                    if (voucher != null)
                    {
                        cashVoucher.Id = voucher.Id;
                        cashVoucher.VoucherNo = voucher.VoucherNo;
                        cashVoucher.Date = voucher.Date;
                        cashVoucher.Amount = voucher.Amount;
                    }

                    var products = Context.Products.AsNoTracking()
                        .Include(x => x.Category)
                        .AsQueryable();



                    decimal amountWithDiscount = 0;
                    foreach (var item in sale.SaleItems)
                    {
                        decimal discount = 0;
                        var productList = await products.FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken: cancellationToken);
                        List<InventoryBatchLookUpModel> inventoryBatchDetail = null;
                        if (request.IsFifo)
                        {
                            inventoryBatchDetail = inventoryData.Where(x => x.ProductId == item.ProductId).Take(request.OpenBatch)
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

                        //Variants
                        ColorDetailLookUpModel saleSizeAssortmentInventory = null;
                        if (request.ColorVariants)
                        {
                            var variationList = variationInventories.Where(x => x.ColorId == item.ColorId && x.ProductId == item.ProductId).Select(x => new VariationInventory()
                            {
                                ColorId = x.ColorId,
                                SizeId = x.SizeId,
                                Quantity = x.Quantity,
                            }).ToList();

                            saleSizeAssortmentInventory = new ColorDetailLookUpModel()
                            {
                                Id = item.ColorId.Value,
                                Quantity = variationList.Sum(x => x.Quantity),
                                VariationInventories = variationList
                            };
                        }


                        discount += item.DiscountAmount;
                        amountWithDiscount += discount;

                        if (item.ProductId != null)
                        {
                            var highUnit = item.Product?.HighUnitPrice != null && ((!item.ServiceItem && (bool)item.Product?.HighUnitPrice));
                            saleItems.Add(new SaleItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId.Value,
                                Code = productList.Code,
                                ProductName = string.IsNullOrEmpty(productList.DisplayNameForPrint) ? productList.EnglishName : productList.DisplayNameForPrint,
                                Description = string.IsNullOrEmpty(productList.DisplayNameForPrint) ? productList.Description : productList.DisplayNameForPrint,
                                IsFree = item.IsFree,
                                SerialNo = item.SerialNo,
                                ReturnQuantity = 0,
                                SoQty = sale.SaleOrder?.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.QuantityOut ?? 0,
                                ArabicName = productList.ArabicName,
                                SaleReturnDays = productList.SaleReturnDays,
                                CategoryName = productList.Category.Code + "--" + productList.Category.Name,
                                Discount = item.Discount,
                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                Quantity = item.Quantity,
                                SchemePhysicalQuantity = item.SchemePhysicalQuantity,
                                Scheme = item.Scheme,
                                SchemeQuantity = item.SchemeQuantity,
                                TaxRateId = item.TaxRateId,
                                TaxRate = item.TaxRate.Rate,
                                TaxMethod = item.TaxMethod,
                                DisplayName = item.Product?.DisplayName,

                                PromotionId = item.PromotionItemId,
                                BundleId = item.BundleId,
                                OfferQuantity = item.OfferQuantity,
                                PromotionOfferQuantity = item.PromotionOfferQuantity,
                                Buy = item.Buy,
                                Get = item.Get,

                                BatchNo = item.BatchNo,
                                BatchExpiry = item.BatchExpiry,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                ServiceItem = item.ServiceItem,
                                ColorId = item.ColorId,
                                ColorName = item.Color?.Name,
                                StyleNumber = item.Product?.StyleNumber,
                                UnitPerPack = item.Product.UnitPerPack,
                                SaleSizeAssortmentInventory = saleSizeAssortmentInventory,
                                RemainingQuantity = request.IsReturn == false ? item.RemainingQuantity : itemList?.FirstOrDefault(x => x.ProductId == item.ProductId)?.RemainingQuantity ?? 0,
                                BundleAmount = item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0,

                                UnitPrice = item.UnitPrice,
                                Total = CalculateTotal(item, highUnit),

                                DiscountAmount = item.DiscountAmount,
                                VatAmount = item.VatAmount,
                                LineTotal = item.TotalAmount,
                                TotalAmount = item.TotalAmount,
                                GrossAmount = item.TotalWithOutAmount,
                                AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                UnitName = string.IsNullOrEmpty(item.UnitName)? item.Product?.Unit?.Name: item.UnitName,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    DisplayName = item.Product.DisplayName,
                                    StyleNumber = item.Product.StyleNumber,
                                    Length = item.Product.Length,
                                    Width = item.Product.Width,
                                    UnitPerPack = item.Product.UnitPerPack,
                                    WholesaleQuantity = item.Product.WholesaleQuantity,
                                    WholesalePrice = item.Product.WholesalePrice,
                                    InventoryBatch = inventoryBatchDetail,
                                    ProductSizes = item.Product.ProductSizes?.Select(x => new ProductSize
                                    {
                                        ProductId = x.ProductId,
                                        SizeId = x.SizeId,
                                    }).ToList(),

                                    Code = item.Product.Code,
                                    SaleReturnDays = item.Product.SaleReturnDays,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    LevelOneUnit = item.Product.LevelOneUnit,
                                    BasicUnit = item.Product.Unit?.Name,
                                    IsExpire = item.Product.IsExpire,
                                    ServiceItem = item.Product.ServiceItem,
                                    Serial = item.Product.Serial,
                                    HighUnitPrice = item.Product.HighUnitPrice,
                                    Guarantee = item.Product.Guarantee,
                                    PromotionOffer = item.PromotionItemId == null ? null : Context.PromotionOfferItems.Select(offer => new PromotionOffer
                                    {
                                        Id = offer.Id,
                                        ProductId = offer.ProductId,
                                        Offer = offer.PromotionOffer.Offer,
                                        Discount = offer.Discount,
                                        DiscountType = offer.DiscountType,
                                        BaseQuantity = offer.BaseQuantity,
                                        UpToQuantity = offer.UpToQuantity,
                                        StockLimit = offer.StockLimit,
                                        QuantityOut = offer.QuantityOut,
                                        IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                        Buy = offer.Buy,
                                        Get = offer.Get,
                                        PromotionType = offer.PromotionType,
                                        ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                        GetProductId = offer.GetProduct.Id,
                                    }).FirstOrDefault(x => x.Id == item.PromotionItemId),

                                    BundleCategory = item.BundleId == null ? null : Context.BundleCategories.Select(bundle => new BundleCategory
                                    {
                                        Id = bundle.Id,
                                        ProductId = bundle.ProductId,
                                        Offer = bundle.Offer,
                                        Buy = bundle.Buy,
                                        Get = bundle.Get,
                                        QuantityLimit = bundle.QuantityLimit,
                                        StockLimit = bundle.StockLimit,
                                        QuantityOut = bundle.QuantityOut,
                                        ToDate = bundle.ToDate,
                                        FromDate = bundle.FromDate,
                                        IsActive = bundle.IsActive,
                                    }).FirstOrDefault(x => x.Id == item.BundleId),

                                    TaxRateId = item.Product.TaxRateId.Value,
                                    Inventory = item.Product.Inventories == null ? null : request.IsFifo ? new Inventory()
                                    {
                                        CurrentQuantity = inventoryBatchDetail.Count > 0 ? inventoryBatchDetail.FirstOrDefault(x => x.BatchNumber == item.BatchNo) == null ? 0 : inventoryBatchDetail.FirstOrDefault(x => x.BatchNumber == item.BatchNo).RemainingQuantity : 0,
                                    } : new Inventory()
                                    {
                                        CurrentQuantity = request.ColorVariants ? variationInventories.Where(x => x.ColorId == item.ColorId).Sum(x => x.Quantity) : stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId)?.CurrentQuantity ?? 0
                                    }

                                },
                                SaleSizeAssortment = item.SaleSizeAssortments?.Select(x => new SaleSizeAssortmentModel
                                {
                                    SizeId = x.SizeId,
                                    SaleItemId = x.SaleItemId,
                                    ItemName = x.SaleItem.Product?.EnglishName,
                                    Quantity = x.Quantity,
                                }).ToList()
                            });
                        }
                        else
                        {
                            saleItems.Add(new SaleItemLookupModel
                            {

                                Id = item.Id,
                                ProductId = item.ProductId,
                                SoItemId = item.SoItemId,
                                Code = string.IsNullOrEmpty(productList?.Code) ? item.StyleNumber : productList?.Code,
                                ProductName = productList?.EnglishName,
                                DisplayName = item.Product?.DisplayName,
                                Description = item.Description,
                                IsFree = item.IsFree,
                                ReturnQuantity = 0,
                                SoQty = sale.SaleOrder?.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.QuantityOut ?? 0,
                                ArabicName = productList?.ArabicName,
                                SaleReturnDays = productList?.SaleReturnDays,
                                CategoryName = productList?.Category.Code + "--" + productList?.Category.Name,
                                Discount = item.Discount,
                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                Quantity = item.Quantity,
                                SchemePhysicalQuantity = item.SchemePhysicalQuantity,
                                Scheme = item.Scheme,
                                SchemeQuantity = item.SchemeQuantity,
                                OfferQuantity = item.OfferQuantity,
                                TaxRateId = item.TaxRateId,
                                TaxRate = item.TaxRate.Rate,
                                UnitPrice = item.UnitPrice,
                                Total = item.Quantity * item.UnitPrice,
                                TaxMethod = item.TaxMethod,
                                PromotionId = item.PromotionId,
                                BundleId = item.BundleId,
                                BatchNo = item.BatchNo,
                                BatchExpiry = item.BatchExpiry,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                ServiceItem = item.ServiceItem,
                                StyleNumber = item.StyleNumber,
                                UnitPerPack = item.Product?.UnitPerPack,
                                RemainingQuantity = request.IsReturn == false ? item.RemainingQuantity : itemList?.FirstOrDefault(x => x.ProductId == item.ProductId)?.RemainingQuantity ?? 0,
                                BundleAmount = item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0,
                                GrossAmount = item.TotalWithOutAmount,
                                DiscountAmount = item.DiscountAmount,
                                AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                VatAmount = item.VatAmount,
                                TotalAmount = item.TotalAmount,
                                LineTotal = item.TotalAmount,
                                UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                
                                //       DiscountAmount = ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),
                                SerialNo = item.SerialNo,
                                //InclusiveVat =
                                //    (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                //        ? ((item.Quantity * item.UnitPrice) -
                                //           ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                //            (item.OfferQuantity == 0
                                //                ?
                                //                (item.Discount == 0
                                //                    ? item.Quantity * item.FixDiscount
                                //                    : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                //                : item.Discount == 0
                                //                    ? item.OfferQuantity * item.FixDiscount
                                //                    : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                //        item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                //        : 0,

                                //IncludingVat = item.TaxRate.Rate == 0 ? 0
                                //    : ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                //        ? ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                //            ?
                                //            (item.Discount == 0
                                //                ? item.Quantity * item.FixDiscount
                                //                : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                //            : item.Discount == 0
                                //                ? item.OfferQuantity * item.FixDiscount
                                //                : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                //        item.TaxRate.Rate / 100
                                //        : 0),
                                SaleSizeAssortment = item.SaleSizeAssortments?.Select(x => new SaleSizeAssortmentModel
                                {
                                    SizeId = x.SizeId,
                                    SaleItemId = x.SaleItemId,
                                    Quantity = x.Quantity,
                                }).ToList()
                            });

                        }
                    }

                    var paymentTypes = new List<PaymentTypeLookupModel>();

                    if (sale.InvoiceType == InvoiceType.Paid || sale.InvoiceType == InvoiceType.SaleReturn || sale.IsCredit)
                    {
                        foreach (var payment in sale.SalePayments)
                        {
                            paymentTypes.Add(new PaymentTypeLookupModel
                            {
                                Id = payment.Id,
                                Amount = payment.Received,
                                PaymentType = payment.PaymentType,
                                BankCashAccountId = payment.BankCashAccountId,
                                Name = payment.Name,
                                Rate = payment.Rate,
                                AmountCurrency = payment.Amount,
                                Description = payment.Description,
                                BankAccountName = Context.Accounts.AsNoTracking().Where(x => x.Id == payment.BankCashAccountId).Select(x => x.Name).SingleOrDefault()
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
                    if (sale.CustomerId != null)
                    {
                        var runningBalance = await _mediator.Send(new GetContactRunningBalanceDetail
                        {
                            AccountId = (Guid)(sale.Customer?.AccountId.Value)
                        });
                        request.OpeningBalance = runningBalance;

                    }

                    
                    var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);

                    string deliveryChallanNumber = "";
                    if (sale.DeliveryChallanId != null)
                    {
                        var deliveryChallan =await Context.DeliveryChallans.AsNoTracking().
                            Select(x=>new
                            {
                                x.Id,
                                x.RegistrationNo,
                            }).FirstOrDefaultAsync(x => x.Id == sale.DeliveryChallanId, cancellationToken: cancellationToken);
                        
                        if (deliveryChallan != null)
                        {
                            deliveryChallanNumber = deliveryChallan.RegistrationNo;
                        }
                    }

                    return new SaleDetailLookupModel

                    {
                        prefixiesLookupModel = prefixRecord,
                        PurchaseOrderId = sale.PurchaseOrderId,
                        ProformaId = sale.ProformaId,
                        InquiryId = sale.InquiryId,
                        CustomerInquiry = sale.CustomerInquiry,
                        DeliveryAddress = sale.DeliveryAddress,
                        DeliveryDate = sale.DeliveryDate,
                        BillingAddress = sale.BillingAddress,
                        ReferredBy = sale.ReferredBy,
                        DeliveryChallanId = sale.DeliveryChallanId,
                        DeliveryNo = deliveryChallanNumber,
                        Id = sale.Id,
                        DispatchDate = sale.DispatchDate,
                        PickUpDate = sale.PickUpDate,
                        AttachmentList = attachmentList,
                        IsWarranty = sale.IsWarranty,
                        TaxMethod = sale.TaxMethod,
                        TaxRateId = sale.TaxRateId,
                        LogisticId = sale.LogisticId,
                        PoNumber = sale.PoNumber,
                        PoDate = sale.PoDate,
                        QuotationId = sale.QuotationId,
                        DoctorName = sale.DoctorName,
                        HospitalName = sale.HospitalName,
                        IsService = sale.IsService,
                        Mobile = sale.Mobile,
                        LogisticNameEn = sale.Logistic?.EnglishName,
                        LogisticNameAr = sale.Logistic?.ArabicName,
                        Date = sale.Date,
                        BarCode = sale.BarCode,
                        OpeningBalance = request.OpeningBalance,
                        CounterCode = counterCode,
                        UserName = userName,
                        Description = sale.Description,
                        CustomerAddressWalkIn = sale.CustomerAddressWalkIn,
                        SaleOrderId = sale.SaleOrderId,
                        SaleOrderNo = sale.SaleOrder?.RegistrationNo,
                        SaleInvoiceId = sale.SaleInvoiceId,
                        InvoiceNo = sale.SaleInvoiceId != null ? Context.Sales.FirstOrDefault(x => x.Id == sale.SaleInvoiceId)?.RegistrationNo : null,
                        RefrenceNo = sale.RefrenceNo,
                        EmployeeId = sale.EmployeeId,
                        EmployeeName = sale.Employee?.EnglishName,
                        TerminalId = sale.TerminalId.ToString(),
                        AreaId = sale.AreaId,
                        AreaName = sale.Area?.Area,
                        IsSaleReturnPost = sale.IsSaleReturnPost,
                        Change = sale.Change.ToString("0.00"),
                        InvoiceType = sale.InvoiceType,
                        PaymentAmount = paymentTypes.Sum(x => x.Amount).ToString("0.00"),
                        PaymentTypes = paymentTypes,
                        RegistrationNo = sale.RegistrationNo,
                        CustomerId = sale.CustomerId,
                        CreditCustomer = sale.CustomerId == null ? null : sale.Customer.Code + "--" + sale.Customer.EnglishName,
                        CustomerCRN = sale.Customer?.CommercialRegistrationNo,
                        UnRegisteredVAtAmount = sale.UnRegisteredVAtAmount,
                        DueDate = sale.DueDate,
                        CashCustomer = sale.CashCustomer?.Name,
                        CustomerCode = sale.Customer?.Code,
                        CustomerCategory = sale.Customer?.Category,
                        CustomerNameEn = sale.Customer?.EnglishName,
                        CustomerNameAr = sale.Customer?.ArabicName,
                        CustomerTelephone = sale.Customer?.ContactNo1,
                        ContactPerson1 = sale.Customer?.ContactPerson1,
                        CustomerVat = sale.Customer?.VatNo,
                        CustomerAddress = sale.Customer?.Address,
                        CustomerCity = sale.Customer?.Country == null ? "" : sale.Customer?.Country + " " + sale.Customer?.City,
                        ShippingAddress = sale.Customer?.ShippingAddress,
                        Code = sale.CashCustomer?.Code,
                        Email = sale.Customer == null ? sale.CashCustomer?.Email : sale.Customer?.Email,
                        CashCustomerIdForeign = sale.CashCustomerId.ToString(),
                        CashCustomerId = sale.CashCustomer?.CustomerId,
                        WareHouseId = sale.WareHouseId,
                        WareHouseName = sale.SaleItems.FirstOrDefault()?.WareHouse.Name,
                        WareHouseNameAr = sale.SaleItems.FirstOrDefault()?.WareHouse.NameArabic,
                        SaleItems = saleItems,
                        SalePayment = salePayments,
                        IsCredit = sale.IsCredit,
                        InvoiceNote = sale.InvoiceNote == "" ? (sale.QuotationId != null ? "This Invoice Is Generated by Quotation" : sale.SaleOrderId != null ? "This Invoice Is Generated by Sale Order" : "") : sale.InvoiceNote,
                        TermAndCondition = sale.TermAndCondition,
                        TermAndConditionAr = sale.TermAndConditionAr,
                        WarrantyLogoPath = sale.WarrantyLogoPath,
                        CashVoucher = cashVoucher,
                        PaymentVoucher = paymentVoucher,
                        PartiallyInvoice = sale.PartiallyInvoices.ToString(),
                        CustomerName = sale.CustomerId != null && sale.CustomerId != Guid.Empty ? sale.Customer.CustomerDisplayName : null,
                        Discount = sale.Discount,
                        UnRegisteredRate = sale.UnRegisteredVAT == null ? 0 : sale.UnRegisteredVAT.Rate,
                        UnRegisteredVatId = sale.UnRegisteredVatId,
                        TransactionLevelDiscount = sale.TransactionLevelDiscount,
                        IsDiscountOnTransaction = sale.IsDiscountOnTransaction,
                        IsFixed = sale.IsFixed,
                        IsBeforeTax = sale.IsBeforeTax,
                        TaxRateName = sale.SaleItems.FirstOrDefault()?.TaxRate.Name,
                        DeliveryId = sale.DeliveryId,
                        ShippingId = sale.ShippingId,
                        BillingId = sale.BillingId,
                        NationalId = sale.NationalId,
                        SaleOrderNoAndDate = sale.SaleOrderNo,
                        PeroformaInvoiceNo = sale.PeroformaInvoiceNo,
                        PurchaseOrderNo = sale.PurchaseOrderNo,
                        InquiryNoAndDate = sale.InquiryNo,
                        QuotationNoAndDate = sale.QuotationNo,
                        DeliveryNoteAndDate = sale.DeliveryNoteAndDate,
                        QuotationValidUpto = sale.QuotationValidUpto,
                        PerfomaValidUpto = sale.PerfomaValidUpto,
                        Note = sale.Note,
                        TotalAmount = sale.TotalAmount,
                        DiscountAmount = sale.DiscountAmount,
                        VatAmount = sale.VatAmount,
                        GrossAmount = sale.TotalWithOutAmount,
                        AfterDiscountAmount = sale.TotalWithOutAmount,
                        CustomerDisplayName = sale.Customer.CustomerDisplayName,
                        //TotalAmount = CalculateGrandTotal(sale.SaleItems) ,

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
            }

            //public decimal CalculateGrandTotal(List<SaleItem> saleItem)
            //{
            //    var total = highUnit ? (saleItem.Quantity / saleItem.Product.UnitPerPack ?? 0) * saleItem.UnitPrice : saleItem.Quantity * saleItem.UnitPrice;

            //    return total;
            //}
            public decimal CalculateTotal(SaleItem saleItem, bool highUnit)
            {
                var total = highUnit ? (saleItem.Quantity / saleItem.Product.UnitPerPack ?? 0) * saleItem.UnitPrice : saleItem.Quantity * saleItem.UnitPrice;

                return total;
            }
            public decimal CalculateVat(SaleItem saleItem)
            {
                decimal vatAmount;
                decimal discountAmount = LineDiscount(saleItem);
                if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
                {
                    vatAmount = saleItem.FixDiscount > 0 ? ((saleItem.UnitPrice * saleItem.Quantity * 100 / (100 + saleItem.TaxRate.Rate)) - discountAmount) * saleItem.TaxRate.Rate / 100
                        : ((saleItem.UnitPrice * saleItem.Quantity) - discountAmount) * saleItem.TaxRate.Rate / 100;
                }
                else
                {
                    vatAmount = ((saleItem.UnitPrice * saleItem.Quantity) - discountAmount) * saleItem.TaxRate.Rate / 100;
                }

                return vatAmount;
            }
            public decimal LineDiscount(SaleItem saleItem)
            {
                decimal discount;
                if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
                {
                    discount = saleItem.Discount == 0 ? saleItem.FixDiscount
                        : saleItem.PromotionOfferQuantity > 0 ? (saleItem.PromotionOfferQuantity ?? 0) * saleItem.UnitPrice * saleItem.Discount / 100 : saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100;
                }
                else
                {
                    discount = saleItem.Discount == 0 ? saleItem.FixDiscount : saleItem.PromotionOfferQuantity > 0 ? (saleItem.PromotionOfferQuantity ?? 0) * saleItem.UnitPrice * saleItem.Discount / 100 : saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100;
                }

                return discount;
            }
        }
    }
}
