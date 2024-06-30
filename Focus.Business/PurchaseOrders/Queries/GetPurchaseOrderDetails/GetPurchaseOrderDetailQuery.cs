using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using Focus.Business.PurchaseOrders.Commands;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderDetails
{
    public class GetPurchaseOrderDetailQuery : IRequest<PurchaseOrderDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsPoView { get; set; }
        public string DocumentType { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool SimpleQuery { get; set; }

        public class Handler : IRequestHandler<GetPurchaseOrderDetailQuery, PurchaseOrderDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPurchaseOrderDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PurchaseOrderDetailLookUpModel> Handle(GetPurchaseOrderDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //Simple Query For Reporting
                    if (request.SimpleQuery)
                    
                    
                    {
                        string supplierQuotationText1 = "";
                        var purchaseOrderSimple =  _context.PurchaseOrders
                            .AsNoTracking()
                            .Include(x=>x.Supplier)
                            .Include(x=>x.PurchaseOrderItems)
                            .ThenInclude(x=>x.Product)
                            .ThenInclude(x=>x.Unit)
                            .FirstOrDefault(x=>x.Id==request.Id);
                        if (purchaseOrderSimple != null && purchaseOrderSimple.SupplierQuotationId != null)
                        {
                            var supplierQuotation = await _context.PurchaseOrders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == purchaseOrderSimple.SupplierQuotationId, cancellationToken: cancellationToken);
                            if (supplierQuotation != null)
                            {
                                supplierQuotationText1 = supplierQuotation.RegistrationNo + " " +
                                                         supplierQuotation.Date.ToString("D") + " " +
                                                         supplierQuotation.TotalAmount;

                            }
                        }


                        var poItems = new List<PurchaseOrderItemLookupModel>();

                        decimal discount = 0;
                        decimal amountWithDiscount = 0;
                        foreach (var item in purchaseOrderSimple.PurchaseOrderItems)
                        {


                            if (item.ProductId == null)
                            {
                                    poItems.Add(new PurchaseOrderItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        ProductName = item.Description,
                                        ProductNameArabic = item.Description,
                                        Description = item.Description,
                                        IsService = item.IsService,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.RemainingQuantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = !string.IsNullOrEmpty(item.Product?.Width) ? item.Product?.Width : "",
                                        TaxMethod = item.TaxMethod,
                                        IsExpire = false,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        Total = item.TotalAmount,
                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        DiscountAmount = item.DiscountAmount,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                     

                                    });
                            }
                            else
                            {
                                    poItems.Add(new PurchaseOrderItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        Code = item.Product.Code,
                                        ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product?.EnglishName : item.Product?.DisplayNameForPrint,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        ProductNameArabic = item.Product?.ArabicName,
                                        StyleNumber = item.Product?.StyleNumber,
                                        IsService = item.IsService,
                                        QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.RemainingQuantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = item.TaxMethod,
                                        IsExpire = item.Product.IsExpire,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        Total = item.TotalAmount,
                                        DisplayName = item.Product?.DisplayName,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        DiscountAmount = item.DiscountAmount,



                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            Serial = item.Product.Serial,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            IsExpire = item.Product.IsExpire,
                                            ServiceItem = item.Product.ServiceItem,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            StyleNumber = item.Product.StyleNumber,
                                            DisplayName = item.Product.DisplayName,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                        }
                                    });
                            }

                        }

                        if (purchaseOrderSimple.BillingId != null)
                        {
                            var address = _context.DeliveryAddresses.AsNoTracking()
                                .FirstOrDefault(x => x.Id == purchaseOrderSimple.BillingId);
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
                                purchaseOrderSimple.Supplier.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }


                        }
                        else
                        {
                            {
                                StringBuilder addressBuilder = new StringBuilder();

                                string address1 = purchaseOrderSimple.Supplier?.Address;
                                string address2 = purchaseOrderSimple.Supplier?.DeliveryAddress;
                                string country1 = purchaseOrderSimple.Supplier?.Country;
                                string zipCode1 = purchaseOrderSimple.Supplier?.BillingZipCode;
                                string city1 = purchaseOrderSimple.Supplier?.City;

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

                                purchaseOrderSimple.Supplier.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }
                        }






                        return new PurchaseOrderDetailLookUpModel
                        {
                            Id = purchaseOrderSimple.Id,
                            SupplierName = purchaseOrderSimple.Supplier.CustomerDisplayName,
                            EnglishName = purchaseOrderSimple.Supplier.EnglishName,
                            Prefix = purchaseOrderSimple.Supplier.Prefix,
                            ArabicName = purchaseOrderSimple.Supplier.ArabicName,
                            SupplierCode = purchaseOrderSimple.Supplier.Code,
                            Date = purchaseOrderSimple.Date,
                            VatAmount = purchaseOrderSimple.VatAmount,
                            TotalAmount = purchaseOrderSimple.TotalAmount,
                            SupplierVAT = purchaseOrderSimple.Supplier.VatNo,
                            IsClose = purchaseOrderSimple.IsClose,
                            DocumentType = purchaseOrderSimple.DocumentType,
                            DiscountAmount = amountWithDiscount,
                            ApprovalStatus = purchaseOrderSimple.ApprovalStatus,
                            RegistrationNo = purchaseOrderSimple.RegistrationNo,
                            SupplierId = purchaseOrderSimple.SupplierId,
                            InvoiceNo = purchaseOrderSimple.InvoiceNo,
                            InvoiceDate = purchaseOrderSimple.InvoiceDate,
                            Note = purchaseOrderSimple.Note,
                            TaxMethod = purchaseOrderSimple.TaxMethod,
                            TaxRateId = purchaseOrderSimple.TaxRateId,
                            IsRaw = purchaseOrderSimple.Raw,
                            PurchaseOrderItems = poItems,
                            Discount = purchaseOrderSimple.Discount,
                            TotalAfterDiscount = purchaseOrderSimple.TotalAfterDiscount,
                            TransactionLevelDiscount = purchaseOrderSimple.TransactionLevelDiscount,
                            GrossAmount = purchaseOrderSimple.TotalWithOutAmount,
                            IsDiscountOnTransaction = purchaseOrderSimple.IsDiscountOnTransaction,
                            IsFixed = purchaseOrderSimple.IsFixed,
                            IsBeforeTax = purchaseOrderSimple.IsBeforeTax,
                            NetAmount = purchaseOrderSimple.TotalAmount,
                            SupplierQuotationId = purchaseOrderSimple.SupplierQuotationId,
                            SupplierQuotationNo = supplierQuotationText1,
                            Reference = purchaseOrderSimple.Reference,
                            SupplierCRN = purchaseOrderSimple.Supplier.CommercialRegistrationNo,
                            CustomerAddress = purchaseOrderSimple.Supplier.Address,
                            DeliveryAddress = purchaseOrderSimple.Supplier.DeliveryAddress,
                            DisplatName = purchaseOrderSimple.DocumentType,
                        };
                    }

                    //End Simple Query



                    string supplierQuotationText = "";
                    var purchaseOrder = await _context.PurchaseOrders.FindAsync(request.Id);
                    if (purchaseOrder != null && purchaseOrder.SupplierQuotationId != null )
                    {
                        var supplierQuotation = await _context.PurchaseOrders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == purchaseOrder.SupplierQuotationId, cancellationToken: cancellationToken);
                        if (supplierQuotation != null)
                        {
                            supplierQuotationText = supplierQuotation.RegistrationNo + " " +
                                                    supplierQuotation.Date.ToString("D") + " " +
                                                    supplierQuotation.TotalAmount;

                        }
                    }

                    if (purchaseOrder != null)
                    {
                        var attachments = _context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == purchaseOrder.Id)
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


                        List<PurchaseOrderVersion> purchaseOrderVersion = null;
                        if (purchaseOrder.PurchaseOrderVersions != null)
                        {
                            purchaseOrderVersion = purchaseOrder.PurchaseOrderVersions.Select(x =>
                                new PurchaseOrderVersion()
                                {
                                    Id = x.Id,
                                    PurchaseOrderId = x.PurchaseOrderId,
                                    Version = x.Version,
                                }).ToList();
                        }

                        var taxRates = _context.TaxRates.FirstOrDefault(x => x.Id == purchaseOrder.TaxRateId);
                        var purchaseAttachment = new List<PurchaseAttachment>();
                        foreach (var item in purchaseOrder.PurchaseAttachments)
                        {
                            purchaseAttachment.Add(new PurchaseAttachment
                            {
                                Id = item.Id,
                                PurchaseOrderId = item.PurchaseOrderId,
                                Description = item.Description,
                                Date = item.Date,
                                Path = item.Path
                            });
                        }

                        var actionProcess = new List<ActionLookUpModel>();
                        foreach (var action in purchaseOrder.PurchaseOrderActions)
                        {
                            actionProcess.Add(new ActionLookUpModel()
                            {
                                Id = action.Id,
                                ProcessId = action.ProcessId,
                                ProcessName = action.CompanyProcess.ProcessName,
                                ProcessNameArabic = action.CompanyProcess.ProcessNameArabic,
                                PurchaseOrderId = action.PurchaseOrderId,
                                Date = action.Date,
                                Description = action.Description
                            });
                        }

                        var paymentVoucher = new List<PaymentVoucherLookUpModel>();
                        foreach (var item in purchaseOrder.PurchaseOrderPayments)
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
                        var purchaseOrderExpenses = new List<Domain.Entities.PurchaseOrderExpense>();
                        foreach (var item in purchaseOrder.PurchaseOrderExpenses)
                        {
                            purchaseOrderExpenses.Add(new Domain.Entities.PurchaseOrderExpense()
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
                                UsedAmount = item.UsedAmount,
                                PaymentMode = item.PaymentMode,
                                PaymentMethod = item.PaymentMethod
                            });
                        }

                        var versionId = purchaseOrder.PurchaseOrderVersions?.LastOrDefault()?.Id;
                        purchaseOrder.PurchaseOrderItems = purchaseOrder.PurchaseOrderItems.Where(x => x.PurchaseOrderVersionId == versionId).ToList();

                        var poItems = new List<PurchaseOrderItemLookupModel>();

                        var netAmount = new TotalAmount();
                        decimal discount = 0;
                        decimal amountWithDiscount = 0;
                        foreach (var item in purchaseOrder.PurchaseOrderItems)
                        {
                            discount += item.Discount == 0 ? item.FixDiscount * item.Quantity : (item.UnitPrice * item.Quantity * item.Discount) / 100;
                            amountWithDiscount += discount;

                            var total = item.IsService
                                ? item.Quantity * item.UnitPrice
                                : (item.Product.HighUnitPrice
                                    ? ((item.Quantity / item.Product.UnitPerPack ?? 0) * item.UnitPrice)
                                    : item.Quantity * item.UnitPrice);

                           
                            if (item.ProductId == null)
                            {
                                if (!request.IsPoView)
                                    poItems.Add(new PurchaseOrderItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        ProductName = item.Description,
                                        ProductNameArabic = item.Description,
                                        Description = item.Description,
                                        IsService = item.IsService,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.RemainingQuantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        TaxRate = item.TaxRate?.Rate ?? 0,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = item.TaxMethod,
                                        IsExpire = false,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        Total = total,
                                        AfterDiscountAmount=item.TotalWithOutAmount-item.DiscountAmount,
                                        DiscountAmount = ((purchaseOrder.TaxMethod == "Inclusive" || purchaseOrder.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),
                                        
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                        //    ? ((item.Quantity * item.UnitPrice) -

                                        //       (discountCal)) * item.TaxRate.Rate / (100 + item.TaxRate.Rate) : 0
                                        //,


                                        //IncludingVat =
                                        //    ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                        //        ? ((item.Quantity * item.UnitPrice) -
                                        //           (item.Discount == 0
                                        //               ? item.Quantity * item.FixDiscount
                                        //               : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / 100 : 0),

                                    });
                                else
                                {
                                    poItems.Add(new PurchaseOrderItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Code = item.Product.Code,
                                        Discount = item.Discount,
                                        ProductName = item.Product?.EnglishName,
                                        ProductNameArabic = item.Description,
                                        Description = item.Description,
                                        IsService = item.IsService,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        TaxRate = item.TaxRate?.Rate ?? 0,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = item.TaxMethod,
                                        IsExpire = item.ProductId != null && item.Product.IsExpire,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        Total = total,

                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,



                                        DiscountAmount = ((purchaseOrder.TaxMethod == "Inclusive" || purchaseOrder.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),



                                        //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                        //    ? ((item.Quantity * item.UnitPrice) -

                                        //       (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / (100 + item.TaxRate.Rate) : 0
                                        //,


                                        //IncludingVat =
                                        //    ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                        //        ? ((item.Quantity * item.UnitPrice) -
                                        //           (item.Discount == 0
                                        //               ? item.Quantity * item.FixDiscount
                                        //               : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / 100 : 0),

                                    });
                                }
                            }
                            else
                            {
                                if (item.Product.TaxRateId != null && !request.IsPoView)
                                    poItems.Add(new PurchaseOrderItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        Code = item.Product.Code,
                                        ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product?.EnglishName : item.Product?.DisplayNameForPrint,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        ProductNameArabic = item.Product?.ArabicName,
                                        StyleNumber = item.Product?.StyleNumber,
                                        IsService = item.IsService,
                                        QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.RemainingQuantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        TaxRate = item.TaxRate?.Rate ?? 0,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = item.TaxMethod,
                                        IsExpire = item.Product.IsExpire,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        Total = total,
                                        DisplayName = item.Product?.DisplayName,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        DiscountAmount = ((purchaseOrder.TaxMethod == "Inclusive" || purchaseOrder.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),


                                        //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                        //    ? ((item.Quantity * item.UnitPrice) -

                                        //       (discountCal)) * item.TaxRate.Rate / (100 + item.TaxRate.Rate) : 0
                                        //,


                                        //IncludingVat =
                                        //    ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                        //        ? ((item.Quantity * item.UnitPrice) -
                                        //           (item.Discount == 0
                                        //               ? item.Quantity * item.FixDiscount
                                        //               : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / 100 : 0),

                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            Serial = item.Product.Serial,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            IsExpire = item.Product.IsExpire,
                                            ServiceItem = item.Product.ServiceItem,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            StyleNumber = item.Product.StyleNumber,
                                            DisplayName = item.Product.DisplayName,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                            TaxRateId = item.Product.TaxRateId.Value
                                        }
                                    });
                                else
                                {
                                    poItems.Add(new PurchaseOrderItemLookupModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product?.EnglishName : item.Product?.DisplayNameForPrint,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        ProductNameArabic = item.Product?.ArabicName,
                                        StyleNumber = item.Product?.StyleNumber,
                                        IsService = item.IsService,
                                        Code = item.Product.Code,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                        Quantity = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        TaxRate = item.TaxRate?.Rate ?? 0,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = item.TaxMethod,
                                        IsExpire = item.Product.IsExpire,
                                        HighQty = (request.IsMultiUnit && !item.IsService) ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        Total = total,
                                        DisplayName = item.Product?.DisplayName,
                                        AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                        GrossAmount = item.TotalWithOutAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        DiscountAmount = ((purchaseOrder.TaxMethod == "Inclusive" || purchaseOrder.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,



                                        //InclusiveVat = (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                        //    ? ((item.Quantity * item.UnitPrice) -

                                        //       (discountCal)) * item.TaxRate.Rate / (100 + item.TaxRate.Rate) : 0
                                        //,


                                        //IncludingVat =
                                        //    ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                        //        ? ((item.Quantity * item.UnitPrice) -
                                        //           (item.Discount == 0
                                        //               ? item.Quantity * item.FixDiscount
                                        //               : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * item.TaxRate.Rate / 100 : 0),

                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            Serial = item.Product.Serial,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            StyleNumber = item.Product.StyleNumber,
                                            ServiceItem = item.Product.ServiceItem,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            DisplayName = item.Product.DisplayName,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
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
                                        }
                                    });
                                }
                            }

                        }
                        if (request.DocumentType == "SupplierQuotation")
                        {
                            return new PurchaseOrderDetailLookUpModel
                            {
                                NationalId = purchaseOrder.NationalId,
                                BillingId = purchaseOrder.BillingId,
                                ShippingId = purchaseOrder.ShippingId,
                                DeliveryId = purchaseOrder.DeliveryId,
                                Id = purchaseOrder.Id,
                                SupplierName = (purchaseOrder.Supplier.Code + "--" + purchaseOrder.Supplier.CustomerDisplayName),
                                Date = purchaseOrder.Date,
                                VatAmount = purchaseOrder.VatAmount,
                                TotalAmount = purchaseOrder.TotalAmount,
                                SupplierVAT = purchaseOrder.Supplier.VatNo,
                                IsClose = purchaseOrder.IsClose,
                                DocumentType = purchaseOrder.DocumentType,
                                DiscountAmount = purchaseOrder.DiscountAmount,
                                ApprovalStatus = purchaseOrder.ApprovalStatus,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                SupplierId = purchaseOrder.SupplierId,
                                InvoiceNo = purchaseOrder.InvoiceNo,
                                InvoiceDate = purchaseOrder.InvoiceDate,
                                Note = purchaseOrder.Note,
                                TaxMethod = purchaseOrder.TaxMethod,
                                TaxRateId = purchaseOrder.TaxRateId,
                                IsRaw = purchaseOrder.Raw,
                                PurchaseOrderItems = poItems,
                                AdvanceAccountId = purchaseOrder.Supplier?.AdvanceAccountId,
                                AttachmentList = attachmentList,
                                Prefix = purchaseOrder?.Supplier?.Prefix,
                                Discount = purchaseOrder.Discount,
                                TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                                IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                                IsFixed = purchaseOrder.IsFixed,
                                IsBeforeTax = purchaseOrder.IsBeforeTax,
                                NetAmount = purchaseOrder.TotalAmount,
                                Reference = purchaseOrder.Reference,
                                TaxRateName = taxRates?.Name,
                                SupplierCRN = purchaseOrder.Supplier.CommercialRegistrationNo,
                                CustomerAddress = purchaseOrder.Supplier.Address,
                                DeliveryAddress = purchaseOrder.Supplier.DeliveryAddress,
                                DisplatName = purchaseOrder.DocumentType,
                            };
                        }

                        return new PurchaseOrderDetailLookUpModel
                        {
                            NationalId = purchaseOrder.NationalId,
                            BillingId = purchaseOrder.BillingId,
                            ShippingId = purchaseOrder.ShippingId,
                            DeliveryId = purchaseOrder.DeliveryId,
                            Id = purchaseOrder.Id,
                            TaxRatesName = taxRates?.Name + ' ' + taxRates?.Rate + '%',
                            SupplierName = purchaseOrder.Supplier.CustomerDisplayName,
                            EnglishName = purchaseOrder.Supplier.EnglishName,
                            ArabicName = purchaseOrder.Supplier.ArabicName,
                            SupplierCode = purchaseOrder.Supplier.Code ,
                            Date = purchaseOrder.Date,
                            VatAmount = purchaseOrder.VatAmount,
                            TotalAmount = purchaseOrder.TotalAmount,
                            SupplierVAT = purchaseOrder.Supplier.VatNo,
                            IsClose = purchaseOrder.IsClose,
                            DocumentType = purchaseOrder.DocumentType,
                            DiscountAmount = amountWithDiscount,
                            ApprovalStatus = purchaseOrder.ApprovalStatus,
                            RegistrationNo = purchaseOrder.RegistrationNo,
                            SupplierId = purchaseOrder.SupplierId,
                            InvoiceNo = purchaseOrder.InvoiceNo,
                            InvoiceDate = purchaseOrder.InvoiceDate,
                            Note = purchaseOrder.Note,
                            PurchaseAttachments = purchaseAttachment,
                            TaxMethod = purchaseOrder.TaxMethod,
                            TaxRateId = purchaseOrder.TaxRateId,
                            IsRaw = purchaseOrder.Raw,
                            PurchaseOrderItems = poItems,
                            ActionProcess = actionProcess,
                            PaymentVoucher = paymentVoucher,
                            ExpenseUse = purchaseOrder.PurchasePosts.Sum(x => x.ExpenseUse),
                            PurchaseOrderExpenses = purchaseOrderExpenses,
                            AdvanceAccountId = purchaseOrder.Supplier?.AdvanceAccountId,
                            Version = purchaseOrder.PurchaseOrderVersions?.FirstOrDefault()?.Version,
                            PurchaseOrderVersion = purchaseOrderVersion,
                            AttachmentList = attachmentList,
                            Prefix = purchaseOrder.Supplier?.Prefix,
                            Discount = purchaseOrder.Discount,
                            TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,
                            TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                            GrossAmount = purchaseOrder.TotalWithOutAmount,
                            IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                            IsFixed = purchaseOrder.IsFixed,
                            IsBeforeTax = purchaseOrder.IsBeforeTax,
                            NetAmount = purchaseOrder.TotalAmount,
                            SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                            SupplierQuotationNo = supplierQuotationText,
                            Reference = purchaseOrder.Reference,
                            TaxRateName = taxRates?.Name,
                            SupplierCRN = purchaseOrder.Supplier.CommercialRegistrationNo,
                            CustomerAddress = purchaseOrder.Supplier.Address,
                            DeliveryAddress = purchaseOrder.Supplier.DeliveryAddress,
                            DisplatName = purchaseOrder.DocumentType,
                        };
                    }
                    throw new NotFoundException(nameof(purchaseOrder), request.Id);
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
