using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Focus.Business.Prefix.Quries;
using DocumentFormat.OpenXml.InkML;
using NPOI.SS.Formula.Functions;
using System.Text;

namespace Focus.Business.SaleOrders.Queries.GetSaleOrderDetails
{
    public class GetSaleOrderDetailQuery : IRequest<SaleOrderDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public bool IsEmail { get; set; }
        public bool DeliveryChallan { get; set; }
        public bool IsSimpleQuery { get; set; }

        public class Handler : IRequestHandler<GetSaleOrderDetailQuery, SaleOrderDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IHostingEnvironment _hostingEnvironment;



            public Handler(IApplicationDbContext context,
                ILogger<GetSaleOrderDetailQuery> logger, IMediator mediator, IHostingEnvironment hostingEnvironment)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _hostingEnvironment = hostingEnvironment;

            }
            public async Task<SaleOrderDetailLookUpModel> Handle(GetSaleOrderDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    if (request.IsSimpleQuery)
                    {
                        var saleOrderRecord = await _context.SaleOrders
                       .AsNoTracking()
                       .Include(x => x.TaxRate)
                       .Include(x => x.SaleOrderItems)
                       .ThenInclude(x => x.Product)
                       .ThenInclude(x => x.Unit)
                       .Include(x => x.Customer)

                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                        var poItems = new List<SaleOrderItemLookupModel>();
                        {
                            foreach (var item in saleOrderRecord.SaleOrderItems)
                            {


                                {
                                    if (item.ProductId != null)
                                    {
                                        poItems.Add(new SaleOrderItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            DisplayNameForPrint = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product.DisplayName : item.Product?.DisplayNameForPrint,
                                            ProductName = item.Product.EnglishName,
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            BatchNo = !string.IsNullOrEmpty(item.Product?.Width) ? item.Product?.Width : "",
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.StyleNumber,
                                            UnitPerPack = item.Product?.UnitPerPack,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            GrossAmount = item.TotalWithOutAmount,
                                            DisplayName = item.Product?.DisplayName,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

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
                                        poItems.Add(new SaleOrderItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            BatchNo = item.BatchNo,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.StyleNumber,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            GrossAmount = item.TotalWithOutAmount,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                            DiscountAmount = item.DiscountAmount
                                        });
                                    }

                                }
                            }

                        }


                        if (saleOrderRecord.BillingId != null)
                        {
                            var address = _context.DeliveryAddresses.AsNoTracking()
                                .FirstOrDefault(x => x.Id == saleOrderRecord.BillingId);
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
                                saleOrderRecord.Customer.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }


                        }
                        else
                        {
                            {
                                StringBuilder addressBuilder = new StringBuilder();

                                string address1 = saleOrderRecord.Customer?.Address;
                                string address2 = saleOrderRecord.Customer?.DeliveryAddress;
                                string country1 = saleOrderRecord.Customer?.Country;
                                string zipCode1 = saleOrderRecord.Customer?.BillingZipCode;
                                string city1 = saleOrderRecord.Customer?.City;

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

                                saleOrderRecord.Customer.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }
                        }


                        if (saleOrderRecord.Customer != null)
                            return new SaleOrderDetailLookUpModel
                            {
                                DiscountAmount = saleOrderRecord.DiscountAmount,
                                TaxRateName = saleOrderRecord.TaxRate?.Name,
                                DeliveryChallanId = saleOrderRecord.DeliveryChallanId,
                                InquiryId = saleOrderRecord.InquiryId,
                                ProformaId = saleOrderRecord.ProformaId,
                                SaleOrderId = saleOrderRecord.SaleOrderId,
                                Id = saleOrderRecord.Id,
                                InvoiceNote = saleOrderRecord.InvoiceNote,
                                For = saleOrderRecord.For,
                                ValidityDate = saleOrderRecord.ValidityDate,
                                Attiendie = saleOrderRecord.Attiendie,
                                Subject = saleOrderRecord.Subject,
                                Purpose = saleOrderRecord.Purpose,
                                OneTimeDescription = saleOrderRecord.OneTimeDescription,
                                LogisticId = saleOrderRecord.LogisticId,
                                WarrantyLogoPath = saleOrderRecord.WarrantyLogoPath,
                                Mobile = saleOrderRecord.Customer?.ContactNo1,
                                LogisticNameEn = saleOrderRecord.Logistic?.EnglishName,
                                LogisticNameAr = saleOrderRecord.Logistic?.ArabicName,

                                CustomerAddress = saleOrderRecord.Customer?.Address,
                                //CustomerAddress2 = saleOrderRecord.Customer?.DeliveryAddress,
                                //CustomerCountry = saleOrderRecord.Customer?.Country,
                                //CustomerZipCode = saleOrderRecord.Customer?.BillingZipCode,
                                //CustomerCity = saleOrderRecord.Customer?.City,

                                CustomerBilingAddress = saleOrderRecord.Customer?.Address,
                                CustomerShippingAddress = saleOrderRecord.Customer?.ShippingAddress,
                                Date = saleOrderRecord.Date,
                                Dates = saleOrderRecord.Date.ToString("d"),
                                BarCode = saleOrderRecord.BarCode,
                                WareHouseId = saleOrderRecord.WareHouseId,
                                QuotationId = saleOrderRecord.QuotationId,
                                IsQuotation = saleOrderRecord.IsQuotation,
                                QuotationNo = saleOrderRecord.QuotationId != null
                                    ? _context.SaleOrders.FirstOrDefault(x => x.Id == saleOrderRecord.QuotationId)
                                        ?.RegistrationNo
                                    : "",
                                ClientPurchaseNo = saleOrderRecord.ClientPurchaseNo,
                                IsClose = saleOrderRecord.IsClose,
                                ApprovalStatus = saleOrderRecord.ApprovalStatus,
                                RegistrationNo = saleOrderRecord.RegistrationNo,
                                CustomerId = saleOrderRecord.CustomerId,
                                CustomerNameEn = saleOrderRecord.Customer?.EnglishName,
                                CustomerPrefix = saleOrderRecord.Customer?.Prefix,
                                CustomerVat = saleOrderRecord.Customer?.VatNo,
                                CustomerNameAr = saleOrderRecord.Customer?.ArabicName,
                                CustomerName = saleOrderRecord.Customer?.CustomerDisplayName,
                                CustomerAccountId = saleOrderRecord.Customer?.AccountId,
                                Email = saleOrderRecord.Customer?.Email,
                                Refrence = saleOrderRecord.Refrence,
                                PaymentMethod = saleOrderRecord.PaymentMethod,
                                Note = saleOrderRecord.Note,
                                NoteAr = saleOrderRecord.NoteAr,
                                IsService = saleOrderRecord.IsService,
                                SaleOrderItems = poItems,
                                Description = saleOrderRecord.Description,
                                AdvanceAccountId = saleOrderRecord.Customer?.AdvanceAccountId,
                                //ImportExport Fields
                                TaxMethod = saleOrderRecord.TaxMethod,
                                TaxRateId = saleOrderRecord.TaxRateId,
                                CustomerCode = saleOrderRecord.Customer?.Code,
                                CustomerCRN = saleOrderRecord.Customer?.CommercialRegistrationNo,
                                DeliveryNoteAndDate = saleOrderRecord.DeliveryNoteAndDate,
                                QuotationValidUpto = saleOrderRecord.QuotationValidUpto,
                                PerfomaValidUpto = saleOrderRecord.PerfomaValidUpto,
                                CustomerInquiry = saleOrderRecord.CustomerInquiry,
                                PoNumber = saleOrderRecord.PoNumber,
                                PoDate = saleOrderRecord.PoDate,
                                ReferredBy = saleOrderRecord.ReferredBy,
                                RefrenceNo = saleOrderRecord.RefrenceNo,
                                DoctorName = saleOrderRecord.DoctorName,
                                HospitalName = saleOrderRecord.HospitalName,
                                QuotationNoAndDate = saleOrderRecord.QuotationNo,
                                SaleOrderNo = saleOrderRecord.SaleOrderNo,
                                PeroformaInvoiceNo = saleOrderRecord.PeroformaInvoiceNo,
                                PurchaseOrderNo = saleOrderRecord.PurchaseOrderNo,
                                inquiryNoAndDate = saleOrderRecord.InquiryNo,
                                DeliveryId = saleOrderRecord.DeliveryId,
                                ShippingId = saleOrderRecord.ShippingId,
                                BillingId = saleOrderRecord.BillingId,
                                NationalId = saleOrderRecord.NationalId,
                                EmployeeId = saleOrderRecord.EmployeeId,
                                DueDate = saleOrderRecord.DueDate,
                                SaleOrderNoAndDate = saleOrderRecord.SaleOrderNo,
                                DeliveryDate = saleOrderRecord.DeliveryDate,
                                TotalAmount = saleOrderRecord.TotalAmount,
                                VatAmount = saleOrderRecord.VatAmount,
                                AfterDiscountAmount =
                                    saleOrderRecord.TotalWithOutAmount - saleOrderRecord.DiscountAmount,
                                GrossAmount = saleOrderRecord.TotalWithOutAmount,

                                NoteDescription = saleOrderRecord.NoteDescription,


                                Discount = saleOrderRecord.Discount,
                                TransactionLevelDiscount = saleOrderRecord.TransactionLevelDiscount,
                                IsDiscountOnTransaction = saleOrderRecord.IsDiscountOnTransaction,
                                IsFixed = saleOrderRecord.IsFixed,
                                IsBeforeTax = saleOrderRecord.IsBeforeTax,
                            };
                    }





                    if (request.DeliveryChallan)
                    {
                        var saleOrderRecord = await _context.SaleOrders
                       .AsNoTracking()
                       .Include(x => x.TaxRate)
                       .Include(x => x.SaleOrderItems)
                       .ThenInclude(x => x.Product)
                       .ThenInclude(x => x.Unit)
                       .Include(x => x.Customer)

                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                        var reservedChallan = _context.DeliveryChallanReserveds.AsNoTracking().Include(x => x.DeliveryChallanReserverdItems).FirstOrDefault(x => x.SaleOrderId == request.Id);
                        var poItems = new List<SaleOrderItemLookupModel>();
                        if (reservedChallan != null)
                        {

                            var itemsOfReserveChallan = reservedChallan.DeliveryChallanReserverdItems.Where(x => x.IsActive && x.Quantity > 0).Select(x => x.ProductId);




                            foreach (var item in saleOrderRecord.SaleOrderItems)
                            {

                                if (itemsOfReserveChallan.Contains(item.ProductId))
                                {
                                    if (item.ProductId != null)
                                    {
                                        poItems.Add(new SaleOrderItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            ProductName = item.Product.EnglishName,
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity ?? 0,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            BatchNo = item.BatchNo,
                                            SoQty = reservedChallan.DeliveryChallanReserverdItems.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity ?? 0,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.StyleNumber,
                                            UnitPerPack = item.Product?.UnitPerPack,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            GrossAmount = item.TotalWithOutAmount,
                                            DisplayName = item.Product?.DisplayName,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                            DisplayNameForPrint = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product.DisplayName : item.Product?.DisplayNameForPrint,
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
                                        poItems.Add(new SaleOrderItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                           
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            BatchNo = item.BatchNo,
                                            SoQty = item.Quantity,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.StyleNumber,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            GrossAmount = item.TotalWithOutAmount,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                        });

                                    }


                                }
                            }




                        }
                        else
                        {
                            foreach (var item in saleOrderRecord.SaleOrderItems)
                            {


                                {
                                    if (item.ProductId != null)
                                    {
                                        poItems.Add(new SaleOrderItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            DisplayNameForPrint = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product.DisplayName : item.Product?.DisplayNameForPrint,
                                            ProductName = item.Product.EnglishName,
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            BatchNo = item.BatchNo,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.StyleNumber,
                                            UnitPerPack = item.Product?.UnitPerPack,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            GrossAmount = item.TotalWithOutAmount,
                                            DisplayName = item.Product?.DisplayName,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

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
                                        poItems.Add(new SaleOrderItemLookupModel
                                        {
                                            Id = item.Id,
                                            ProductId = item.ProductId,
                                            Description = item.Description,
                                            IsFree = item.IsFree,
                                            Discount = item.Discount,
                                            FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                            Quantity = item.Quantity,
                                            TaxRateId = item.TaxRateId,
                                            UnitPrice = item.UnitPrice,
                                            Total = item.Quantity * item.UnitPrice,
                                            TaxMethod = item.TaxMethod,
                                            BatchNo = item.BatchNo,
                                            Serial = item.Serial,
                                            GuaranteeDate = item.GuaranteeDate,
                                            ServiceItem = item.ServiceItem,
                                            StyleNumber = item.StyleNumber,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                            GrossAmount = item.TotalWithOutAmount,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                        });
                                    }

                                }
                            }

                        }
                        return new SaleOrderDetailLookUpModel
                        {
                            DiscountAmount = saleOrderRecord.DiscountAmount,
                            TaxRateName = saleOrderRecord.TaxRate?.Name,
                            ValidityDate = saleOrderRecord.ValidityDate,
                            Id = saleOrderRecord.Id,
                            WarrantyLogoPath = saleOrderRecord.WarrantyLogoPath,
                            CustomerBilingAddress = saleOrderRecord.Customer?.Address,
                            CustomerShippingAddress = saleOrderRecord.Customer?.ShippingAddress,
                            Date = saleOrderRecord.Date,
                            Dates = saleOrderRecord.Date.ToString("d"),
                            BarCode = saleOrderRecord.BarCode,
                            WareHouseId = saleOrderRecord.WareHouseId,
                            QuotationId = saleOrderRecord.QuotationId,
                            IsQuotation = saleOrderRecord.IsQuotation,
                            ClientPurchaseNo = saleOrderRecord.ClientPurchaseNo,
                            InvoiceNote = saleOrderRecord.InvoiceNote,
                            IsClose = saleOrderRecord.IsClose,
                            ApprovalStatus = saleOrderRecord.ApprovalStatus,
                            RegistrationNo = saleOrderRecord.RegistrationNo,
                            CustomerId = saleOrderRecord.CustomerId,
                            CustomerNameEn = saleOrderRecord.Customer?.EnglishName,
                            CustomerVat = saleOrderRecord.Customer?.VatNo,
                            CustomerNameAr = saleOrderRecord.Customer?.ArabicName,
                            CustomerName = saleOrderRecord.Customer?.CustomerDisplayName,
                            CustomerAccountId = saleOrderRecord.Customer?.AccountId,
                            Email = saleOrderRecord.Customer?.Email,
                            Refrence = saleOrderRecord.Refrence,
                            Days = saleOrderRecord.Days,
                            SheduleDelivery = saleOrderRecord.SheduleDelivery,
                            PaymentMethod = saleOrderRecord.PaymentMethod,
                            IsFreight = saleOrderRecord.IsFreight,
                            IsLabour = saleOrderRecord.IsLabour,
                            Note = saleOrderRecord.Note,
                            NoteAr = saleOrderRecord.NoteAr,
                            IsService = saleOrderRecord.IsService,
                            SaleOrderItems = poItems,
                            Description = saleOrderRecord.Description,
                            //ImportExport Fields
                            OrderTypeId = saleOrderRecord.OrderTypeId,
                            CommodityId = saleOrderRecord.CommodityId,
                            Commodities = saleOrderRecord.Commodities,
                            NatureOfCargo = saleOrderRecord.NatureOfCargo,
                            CustomerCode = saleOrderRecord.Customer.Code,
                            Mobile = saleOrderRecord.Customer?.ContactNo1 == null ? saleOrderRecord.Customer?.ContactNo1 : saleOrderRecord.Customer?.Telephone,
                            
                            Discount = saleOrderRecord.Discount,
                            TransactionLevelDiscount = saleOrderRecord.TransactionLevelDiscount,
                            IsDiscountOnTransaction = saleOrderRecord.IsDiscountOnTransaction,
                            IsFixed = saleOrderRecord.IsFixed,
                            IsBeforeTax = saleOrderRecord.IsBeforeTax,
                            TotalAmount = saleOrderRecord.TotalAmount,
                            VatAmount = saleOrderRecord.VatAmount,
                            AfterDiscountAmount = saleOrderRecord.TotalWithOutAmount - saleOrderRecord.DiscountAmount,
                            GrossAmount = saleOrderRecord.TotalWithOutAmount,


                        };


                    }

                    if (request.IsEmail)
                        _context.DisableTenantFilter = true;

                    var saleOrder = await _context.SaleOrders
                        .AsNoTracking()
                        .Include(x => x.TaxRate)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Inventories)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.WareHouse)
                        .Include(x => x.Customer)
                        .Include(x => x.SaleOrderItems)
                        .Include(x => x.SaleOrderPayments)
                        .Include(x => x.Logistic)
                        .Include(x => x.SaleOrderVersions)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);




                    IQueryable<Inventory> inventoryData = null;
                    if (request.IsFifo)
                    {
                        inventoryData = _context.Inventories.Where(x =>
                            x.IsActive && x.IsOpen && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn)).AsQueryable();

                    }

                    var stocks = await _context.Stocks.AsNoTracking().Where(x => x.BranchId==saleOrder.BranchId).ToListAsync(cancellationToken: cancellationToken);



                    if (saleOrder != null)
                    {
                        var attachments = _context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == saleOrder.Id)
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


                        var poItems = new List<SaleOrderItemLookupModel>();
                        foreach (var item in saleOrder.SaleOrderItems)
                        {
                            var highUnit = item.Product?.HighUnitPrice != null && ((!saleOrder.IsService && (bool)item.Product?.HighUnitPrice));
                            List<InventoryBatchLookUpModel> inventoryBatchDetail = null;
                            if (request.IsFifo && item.ProductId != null)
                            {
                                inventoryBatchDetail = inventoryData?.Where(x => x.ProductId == item.ProductId).Take(request.OpenBatch)
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

                            if (item.ProductId != null)
                            {
                                poItems.Add(new SaleOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Code = item.Product.Code,
                                    ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                    ProductNameEn = item.Product.EnglishName,
                                    ProductImage = string.IsNullOrEmpty(item.Product.Image) ? "" : GetBaseImage(item.Product.Image).Result,
                                    ProductNameAr = item.Product.ArabicName,
                                    Discount = item.Discount,
                                    DisplayNameForPrint = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product.DisplayName : item.Product?.DisplayNameForPrint,
                                    FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                    Quantity = item.Quantity,
                                    QuantityOut = item.QuantityOut,
                                    SaleOrderId = item.SaleOrderId,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate.ToString("0.00"),
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = item.TaxMethod,
                                    IsExpire = item.Product.IsExpire,
                                    Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                    ServiceItem = item.ServiceItem,
                                    Serial = item.Serial,
                                    GuaranteeDate = item.GuaranteeDate,
                                    StyleNumber = item.StyleNumber,
                                    IsFree = item.IsFree,
                                    Scheme = item.Scheme,
                                    SchemeQuantity = item.SchemeQuantity,
                                    SchemePhysicalQuantity = item.SchemePhysicalQuantity,
                                    Total = highUnit ? (item.Quantity / item.Product.UnitPerPack ?? 0) * item.UnitPrice : item.Quantity * item.UnitPrice,
                                    UnitPerPack = item.Product.UnitPerPack,
                                    TotalAmount = item.TotalAmount,
                                    VatAmount = item.VatAmount,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    GrossAmount = item.TotalWithOutAmount,
                                    DisplayName = item.Product?.DisplayName,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                    //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ?
                                    //    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate) /
                                    //    (item.TaxRate.Rate + 100)) : 0,

                                    //IncludingVat = (item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل") ?
                                    //    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate) /
                                    //    100) : 0,

                                    Product = new ProductDropdownLookUpModel
                                    {
                                        Id = item.Product.Id,
                                        BarCode = item.Product.BarCode,
                                        StyleNumber = item.Product.StyleNumber,
                                        Width = item.Product.Width,
                                        UnitPerPack = item.Product.UnitPerPack,
                                        ServiceItem = item.Product.ServiceItem,
                                        WholesaleQuantity = item.Product.WholesaleQuantity,
                                        WholesalePrice = item.Product.WholesalePrice,
                                        Inventory = item.Product.Inventories == null ? null
                                            : request.IsFifo ? new Inventory()
                                            {
                                                CurrentQuantity = (inventoryBatchDetail != null && inventoryBatchDetail.Count > 0) ? inventoryBatchDetail.FirstOrDefault(x => x.BatchNumber == item.BatchNo) == null ? 0 : inventoryBatchDetail.FirstOrDefault(x => x.BatchNumber == item.BatchNo).RemainingQuantity : 0,
                                            } : new Inventory()
                                            {
                                                CurrentQuantity = stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId)?.CurrentQuantity ?? 0
                                            },
                                        InventoryBatch = inventoryBatchDetail,

                                        Code = item.Product.Code,
                                        EnglishName = item.Product.EnglishName,
                                        ArabicName = item.Product.ArabicName,
                                        SalePrice = item.Product.SalePrice,
                                        LevelOneUnit = item.Product.LevelOneUnit,
                                        BasicUnit = item.Product.Unit?.Name,

                                        //PromotionOffer = item.Product.PromotionOffer == null
                                        //    ? null
                                        //    : new PromotionOffer
                                        //    {
                                        //        Id = item.Product.PromotionOffer.Id,
                                        //        FromDate = item.Product.PromotionOffer.FromDate,
                                        //        isActive = item.Product.PromotionOffer.isActive,
                                        //        Offer = item.Product.PromotionOffer.Offer,
                                        //        //DiscountPercentage = item.Product.PromotionOffer.DiscountPercentage,
                                        //        //FixedDiscount = item.Product.PromotionOffer.FixedDiscount,
                                        //        //Quantity = item.Product.PromotionOffer.Quantity,
                                        //        ToDate = item.Product.PromotionOffer.ToDate,
                                        //        StockLimit = item.Product.PromotionOffer.StockLimit,
                                        //        QuantityOut = item.Product.PromotionOffer.QuantityOut
                                        //    },
                                        //BundleCategory = item.Product.BundleCategory == null
                                        //    ? null
                                        //    : new BundleCategory
                                        //    {
                                        //        Id = item.Product.BundleCategory.Id,
                                        //        Buy = item.Product.BundleCategory.Buy,
                                        //        Get = item.Product.BundleCategory.Get,
                                        //        Offer = item.Product.BundleCategory.Offer,
                                        //        isActive = item.Product.BundleCategory.isActive,
                                        //        StockLimit = item.Product.BundleCategory.StockLimit,
                                        //        QuantityOut = item.Product.BundleCategory.QuantityOut,
                                        //    },
                                        TaxRateId = item.Product.TaxRateId.Value,
                                        TaxMethod = item.Product.TaxMethod,
                                        Serial = item.Product.Serial,
                                        HighUnitPrice = item.Product.HighUnitPrice,
                                        Guarantee = item.Product.Guarantee,
                                    }
                                });

                            }
                            else
                            {
                                poItems.Add(new SaleOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    ProductNameEn = "",
                                    ProductNameAr = "",
                                    Discount = item.Discount,
                                    Code = item.Product?.Code,
                                    FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                    Quantity = item.Quantity,
                                    QuantityOut = item.QuantityOut,
                                    SaleOrderId = item.SaleOrderId,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate.ToString("0.00"),
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = item.TaxMethod,
                                    IsExpire = false,
                                    Description = item.Description,
                                    ServiceItem = item.ServiceItem,
                                    Serial = item.Serial,
                                    GuaranteeDate = item.GuaranteeDate,
                                    StyleNumber = item.StyleNumber,
                                    IsFree = item.IsFree,
                                    Scheme = item.Scheme,
                                    SchemeQuantity = item.SchemeQuantity,
                                    SchemePhysicalQuantity = item.SchemePhysicalQuantity,
                                    Total = item.Quantity * item.UnitPrice,
                                    UnitPerPack = 0,
                                    TotalAmount = item.TotalAmount,
                                    VatAmount = item.VatAmount,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                    GrossAmount = item.TotalWithOutAmount,
                                    //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ?
                                    //    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate) /
                                    //    (item.TaxRate.Rate + 100)) : 0,

                                    //IncludingVat = (item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل") ?
                                    //    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate) /
                                    //    100) : 0,

                                });

                            }
                        }

                        var paymentVoucher = new List<PaymentVoucherLookUpModel>();
                        if (saleOrder.SaleOrderPayments != null)
                        {
                            foreach (var item in saleOrder.SaleOrderPayments)
                            {
                                paymentVoucher.Add(new PaymentVoucherLookUpModel
                                {
                                    Id = item.Id,
                                    Date = item.Date,
                                    VoucherNumber = item.VoucherNumber,
                                    Narration = item.Narration,
                                    ChequeNumber = item.ChequeNumber,
                                    Amount = item.Amount,
                                    PaymentMode = item.PaymentMode,
                                    PaymentMethod = item.PaymentMethod
                                });
                            }
                        }
                        var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);

                        return new SaleOrderDetailLookUpModel
                        {
                            DiscountAmount = saleOrder.DiscountAmount,
                            TaxRateName = saleOrder.TaxRate?.Name,
                            DeliveryChallanId = saleOrder.DeliveryChallanId,
                            InquiryId = saleOrder.InquiryId,
                            ProformaId = saleOrder.ProformaId,
                            SaleOrderId = saleOrder.SaleOrderId,
                            Id = saleOrder.Id,
                            InvoiceNote = saleOrder.InvoiceNote,
                            For = saleOrder.For,
                            ValidityDate = saleOrder.ValidityDate,
                            Attiendie = saleOrder.Attiendie,
                            Subject = saleOrder.Subject,
                            Purpose = saleOrder.Purpose,
                            OneTimeDescription = saleOrder.OneTimeDescription,
                            AttachmentList = attachmentList,
                            LogisticId = saleOrder.LogisticId,
                            WarrantyLogoPath = saleOrder.WarrantyLogoPath,
                            Mobile = saleOrder.IsQuotation ? saleOrder.Customer?.ContactNo1 : saleOrder.Customer?.ContactNo1,
                            LogisticNameEn = saleOrder.Logistic?.EnglishName,
                            LogisticNameAr = saleOrder.Logistic?.ArabicName,
                            CustomerAddress = saleOrder.IsQuotation ? saleOrder.Customer?.Address : saleOrder.Customer?.Address,
                            CustomerBilingAddress = saleOrder.Customer?.Address,
                            CustomerShippingAddress = saleOrder.Customer?.ShippingAddress,
                            Date = saleOrder.Date,
                            Dates = saleOrder.Date.ToString("d"),
                            BarCode = saleOrder.BarCode,
                            WareHouseName = saleOrder.SaleOrderItems.FirstOrDefault()?.WareHouse?.Name,
                            WareHouseNameAr = saleOrder.SaleOrderItems.FirstOrDefault()?.WareHouse?.NameArabic,
                            WareHouseId = saleOrder.WareHouseId,
                            QuotationId = saleOrder.QuotationId,
                            IsQuotation = saleOrder.IsQuotation,
                            QuotationNo = saleOrder.QuotationId != null ? _context.SaleOrders.FirstOrDefault(x => x.Id == saleOrder.QuotationId)?.RegistrationNo : "",
                            ClientPurchaseNo = saleOrder.ClientPurchaseNo,
                            IsClose = saleOrder.IsClose,
                            ApprovalStatus = saleOrder.ApprovalStatus,
                            RegistrationNo = saleOrder.RegistrationNo,
                            CustomerId = saleOrder.CustomerId,
                            CustomerNameEn = saleOrder.Customer?.EnglishName,
                            CustomerVat = saleOrder.Customer?.VatNo,
                            CustomerNameAr = saleOrder.Customer?.ArabicName,
                            CustomerName = saleOrder.Customer?.CustomerDisplayName,
                            CustomerAccountId = saleOrder.Customer?.AccountId,
                            Email = saleOrder.Customer?.Email,
                            Refrence = saleOrder.Refrence,
                            Days = saleOrder.Days,
                            SheduleDelivery = saleOrder.SheduleDelivery,
                            PaymentMethod = saleOrder.PaymentMethod,
                            IsFreight = saleOrder.IsFreight,
                            IsLabour = saleOrder.IsLabour,
                            Note = saleOrder.Note,
                            NoteAr = saleOrder.NoteAr,
                            IsService = saleOrder.IsService,
                            SaleOrderItems = poItems,
                            PaymentVoucher = paymentVoucher,
                            Description = saleOrder.Description,
                            AdvanceAccountId = saleOrder.Customer?.AdvanceAccountId,
                            //ImportExport Fields
                            OrderTypeId = saleOrder.OrderTypeId,
                            OrderTypeName = saleOrder.OrderType?.Name,
                            OrderTypeNameAr = saleOrder.OrderType?.NameArabic,
                            CommodityId = saleOrder.CommodityId,
                            Commodities = saleOrder.Commodities,
                            NatureOfCargo = saleOrder.NatureOfCargo,
                            Attn = saleOrder.Attn,
                            QuotationValidDate = saleOrder.QuotationValidDate,
                            FreeTimePOL = saleOrder.FreeTimePOL,
                            FreeTimePOD = saleOrder.FreeTimePOD,
                            TaxMethod = saleOrder.TaxMethod,
                            TaxRateId = saleOrder.TaxRateId,
                            CustomerCode = saleOrder.Customer.Code,
                            CustomerCRN = saleOrder.Customer.CommercialRegistrationNo,
                            DeliveryNoteAndDate = saleOrder.DeliveryNoteAndDate,
                            QuotationValidUpto = saleOrder.QuotationValidUpto,
                            PerfomaValidUpto = saleOrder.PerfomaValidUpto,
                            CustomerInquiry = saleOrder.CustomerInquiry,
                            PoNumber = saleOrder.PoNumber,
                            PoDate = saleOrder.PoDate,
                            ReferredBy = saleOrder.ReferredBy,
                            RefrenceNo = saleOrder.RefrenceNo,
                            DoctorName = saleOrder.DoctorName,
                            HospitalName = saleOrder.HospitalName,
                            QuotationNoAndDate = saleOrder.QuotationNo,
                            SaleOrderNo = saleOrder.SaleOrderNo,
                            PeroformaInvoiceNo = saleOrder.PeroformaInvoiceNo,
                            PurchaseOrderNo = saleOrder.PurchaseOrderNo,
                            inquiryNoAndDate = saleOrder.InquiryNo,
                            DeliveryId = saleOrder.DeliveryId,
                            ShippingId = saleOrder.ShippingId,
                            BillingId = saleOrder.BillingId,
                            NationalId = saleOrder.NationalId,
                            EmployeeId = saleOrder.EmployeeId,
                            DueDate = saleOrder.DueDate,
                            SaleOrderNoAndDate = saleOrder.SaleOrderNo,
                            DeliveryDate = saleOrder.DeliveryDate,
                            TotalAmount = saleOrder.TotalAmount,
                            VatAmount = saleOrder.VatAmount,
                            AfterDiscountAmount = saleOrder.TotalWithOutAmount - saleOrder.DiscountAmount,
                            GrossAmount = saleOrder.TotalWithOutAmount,
                            NoteDescription = saleOrder.NoteDescription,
                            Discount = saleOrder.Discount,
                            TransactionLevelDiscount = saleOrder.TransactionLevelDiscount,
                            IsDiscountOnTransaction = saleOrder.IsDiscountOnTransaction,
                            IsFixed = saleOrder.IsFixed,
                            IsBeforeTax = saleOrder.IsBeforeTax,
                            prefixiesLookupModel = prefixRecord,

                            SaleOrderVersion = saleOrder.ApprovalStatus == ApprovalStatus.Draft ? saleOrder.SaleOrderVersions.SkipLast(1).Select(x => new SaleOrderVersion()
                            {
                                Id = x.Id,
                                Version = x.Version
                            }).ToList()
                            : saleOrder.SaleOrderVersions.Select(x => new SaleOrderVersion()
                            {
                                Id = x.Id,
                                Version = x.Version
                            }).ToList(),
                        };
                    }
                    throw new NotFoundException(nameof(saleOrder), request.Id);
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
            public async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
