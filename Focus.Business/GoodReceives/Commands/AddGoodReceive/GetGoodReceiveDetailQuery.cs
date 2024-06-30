using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.GoodReceives.Commands.AddGoodReceive
{
    public class GetGoodReceiveDetailQuery : IRequest<GoodReceiveLookUp>
    {
        public Guid Id { get; set; }
        public bool IsMultiUnit { get; set; }

        public bool SimpleQuery { get; set; }

        public class Handler : IRequestHandler<GetGoodReceiveDetailQuery, GoodReceiveLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetGoodReceiveDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<GoodReceiveLookUp> Handle(GetGoodReceiveDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {


                    if (request.SimpleQuery)
                    {

                        string purchaseOrderValue1 = "";
                        var purchaseOrder1 = await _context.GoodReceives
                            .AsNoTracking()
                            .Include(x=>x.Supplier)
                            .Include(x=>x.PurchaseOrder)
                            .Include(x=>x.GoodReceiveNoteItems)
                            .ThenInclude(x=>x.Product)
                            .ThenInclude(x=>x.Unit)
                            .FirstOrDefaultAsync(x=>x.Id==request.Id, cancellationToken: cancellationToken);

                        if (purchaseOrder1 != null && purchaseOrder1.PurchaseOrderId != null)
                        {
                            purchaseOrderValue1 = purchaseOrder1.PurchaseOrder.RegistrationNo + " " +
                                                  purchaseOrder1.PurchaseOrder.Date.ToString("D") + " " +
                                                  purchaseOrder1.PurchaseOrder.TotalAmount;
                        }
                        else if (purchaseOrder1 != null && purchaseOrder1.SupplierQuotationId != null)
                        {
                            var supplierQuotation = await _context.PurchaseOrders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == purchaseOrder1.SupplierQuotationId, cancellationToken: cancellationToken);
                            if (supplierQuotation != null)
                            {
                                purchaseOrderValue1 = supplierQuotation.RegistrationNo + " " +
                                                      supplierQuotation.Date.ToString("D") + " " +
                                                      supplierQuotation.TotalAmount;

                            }
                        }



                        var poItems = new List<GoodReceiveItemLookUpModel>();

                        var netAmount = new TotalAmount();
                        decimal discount = 0;
                        decimal amountWithDiscount = 0;
                        foreach (var item in purchaseOrder1.GoodReceiveNoteItems)
                        {

                            if (item.Product != null)
                            {
                                {
                                    poItems.Add(new GoodReceiveItemLookUpModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        QuantityforCanvas = item.Quantity,
                                        Quantity = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        PoQuantity = item.PoQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = purchaseOrder1.TaxMethod,
                                        IsExpire = item.Product.IsExpire,
                                        HighQty = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        Total = item.Quantity * item.UnitPrice,
                                        DisplayName = item.Product?.DisplayName,
                                        ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            Serial = item.Product.Serial,
                                            DisplayName = item.Product.DisplayName,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            IsExpire = item.Product.IsExpire,
                                            StyleNumber = item.Product.StyleNumber,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,

                                            TaxRateId = item.Product.TaxRateId.Value
                                        },
                                        GrossAmount = item.TotalWithOutAmount,
                                        VatAmount = item.VatAmount,
                                        TotalAmount = item.TotalAmount,
                                        DiscountAmount = item.DiscountAmount,
                                    });
                                }

                            }
                            else
                            {
                                poItems.Add(new GoodReceiveItemLookUpModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Description = item.Description,
                                    IsService = item.IsService,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    QuantityforCanvas = item.Quantity,
                                    Quantity = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                    RemainingQuantity = item.RemainingQuantity,
                                    PoQuantity = item.PoQuantity,
                                    ReceiveQuantity = item.ReceiveQuantity,
                                    TaxRateId = item.TaxRateId,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = purchaseOrder1.TaxMethod,
                                    HighQty = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                    UnitPerPack = item.UnitPerPack ?? 0,
                                    SerialNo = item.SerialNo,
                                    Total = item.Quantity * item.UnitPrice,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                    GrossAmount = item.TotalWithOutAmount,
                                    VatAmount = item.VatAmount,
                                    TotalAmount = item.TotalAmount,
                                    DiscountAmount = item.DiscountAmount,
                                });
                            }
                        }


                        if (purchaseOrder1.BillingId != null)
                        {
                            var address = _context.DeliveryAddresses.AsNoTracking()
                                .FirstOrDefault(x => x.Id == purchaseOrder1.BillingId);
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
                                purchaseOrder1.Supplier.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }


                        }
                        else
                        {
                            {
                                StringBuilder addressBuilder = new StringBuilder();

                                string address1 = purchaseOrder1.Supplier?.Address;
                                string address2 = purchaseOrder1.Supplier?.DeliveryAddress;
                                string country1 = purchaseOrder1.Supplier?.Country;
                                string zipCode1 = purchaseOrder1.Supplier?.BillingZipCode;
                                string city1 = purchaseOrder1.Supplier?.City;

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

                                purchaseOrder1.Supplier.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                            }
                        }




                        return new GoodReceiveLookUp
                        {
                            Id = purchaseOrder1.Id,
                            VatAmount = purchaseOrder1.VatAmount,
                            PurchaseOrderNo = purchaseOrder1.PurchaseOrder?.RegistrationNo,
                            SupplierName = (purchaseOrder1.Supplier.Code + "--" + purchaseOrder1.Supplier.EnglishName),
                            SupplierVAT = purchaseOrder1.Supplier.VatNo,
                            SupplierCommercialRegistrationNo = purchaseOrder1.Supplier.CommercialRegistrationNo,
                            ContactNo1 = purchaseOrder1.Supplier.ContactNo1,
                            PurchaseOrderId = purchaseOrder1.PurchaseOrderId,
                            ApprovedBy = purchaseOrder1.ApprovedBy,
                            Date = purchaseOrder1.Date,
                            IsClose = purchaseOrder1.IsClose,
                            ApprovalStatus = purchaseOrder1.ApprovalStatus,
                            RegistrationNo = purchaseOrder1.RegistrationNo,
                            SupplierId = purchaseOrder1.SupplierId,
                            InvoiceNo = purchaseOrder1.InvoiceNo,
                            InvoiceDate = purchaseOrder1.InvoiceDate,
                            Note = purchaseOrder1.Note,
                            TaxMethod = purchaseOrder1.TaxMethod,
                            TaxRateId = purchaseOrder1.TaxRateId,
                            IsRaw = purchaseOrder1.Raw,
                            GoodReceiveNoteItems = poItems,
                            TotalAmount = purchaseOrder1.TotalAmount,
                            NetAmount = purchaseOrder1.TotalAmount,
                            GrossAmount = purchaseOrder1.TotalWithOutAmount,
                            BankCashAccountId = purchaseOrder1.BankCashAccountId,
                            PaymentType = purchaseOrder1.PaymentType,
                            PoDate = purchaseOrder1.PoDate,
                            PoNumber = purchaseOrder1.PoNumber,
                            TotalAfterDiscount = purchaseOrder1.TotalAfterDiscount,
                            SupplierQuotationId = purchaseOrder1.SupplierQuotationId,
                            Discount = purchaseOrder1.Discount,
                            IsBeforeTax = purchaseOrder1.IsBeforeTax,
                            IsFixed = purchaseOrder1.IsFixed,
                            IsDiscountOnTransaction = purchaseOrder1.IsDiscountOnTransaction,
                            TransactionLevelDiscount = purchaseOrder1.TransactionLevelDiscount,
                            IsCredit = purchaseOrder1.IsCredit,
                            ReceivedAmount = purchaseOrder1.ReceivedAmount,
                            GoodsRecieveNumberAndDate = purchaseOrder1.SoNumber,
                            Reference = purchaseOrder1.Reference,
                            PurchaseOrderValue = purchaseOrderValue1,
                            SupplierAddress = purchaseOrder1.Supplier.Address,
                            SupplierCode = purchaseOrder1.Supplier.CustomerCode,
                        };



                    }



                    string purchaseOrderValue = "";
                    var purchaseOrder = await _context.GoodReceives.FindAsync(request.Id);

                    if (purchaseOrder != null && purchaseOrder.PurchaseOrderId != null)
                    {
                        purchaseOrderValue= purchaseOrder.PurchaseOrder.RegistrationNo + " " +
                                            purchaseOrder.PurchaseOrder.Date.ToString("D") + " " +
                                            purchaseOrder.PurchaseOrder.TotalAmount;
                    }
                    else if (purchaseOrder != null && purchaseOrder.SupplierQuotationId != null)
                    {
                        var supplierQuotation = await _context.PurchaseOrders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == purchaseOrder.SupplierQuotationId, cancellationToken: cancellationToken);
                        if (supplierQuotation != null)
                        {
                            purchaseOrderValue = supplierQuotation.RegistrationNo + " " +
                                                 supplierQuotation.Date.ToString("D") + " " +
                                                 supplierQuotation.TotalAmount;

                        }
                    }


                    if (purchaseOrder != null)
                    {
                      

                     
                        var taxRates = _context.TaxRates.FirstOrDefault(x => x.Id == purchaseOrder.TaxRateId);
                       

                        

                     
                      


                        var poItems = new List<GoodReceiveItemLookUpModel>();

                        var netAmount = new TotalAmount();
                        decimal discount = 0;
                        decimal amountWithDiscount = 0;
                        foreach (var item in purchaseOrder.GoodReceiveNoteItems)
                        {

                            if (item.Product != null)
                            {
                                if (item.Product.TaxRateId != null)
                                {
                                    poItems.Add(new GoodReceiveItemLookUpModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        QuantityforCanvas = item.Quantity,
                                        Quantity = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        PoQuantity = item.PoQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        TaxRate = item.TaxRate?.Rate ?? 0,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = purchaseOrder.TaxMethod,
                                        IsExpire = item.Product.IsExpire,
                                        HighQty = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.RemainingQuantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        Total = item.Quantity * item.UnitPrice,
                                        DisplayName = item.Product?.DisplayName,
                                        ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description: item.Product?.DisplayNameForPrint,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            Serial = item.Product.Serial,
                                            DisplayName = item.Product.DisplayName,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            IsExpire = item.Product.IsExpire,
                                            StyleNumber = item.Product.StyleNumber,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,

                                            TaxRateId = item.Product.TaxRateId.Value
                                        },
                                        GrossAmount = item.TotalWithOutAmount,
                                        VatAmount = item.VatAmount,
                                        TotalAmount = item.TotalAmount,
                                        DiscountAmount = item.DiscountAmount,
                                    });
                                }
                                else
                                {
                                    poItems.Add(new GoodReceiveItemLookUpModel
                                    {
                                        Id = item.Id,
                                        ProductId = item.ProductId,
                                        Discount = item.Discount,
                                        QuantityforCanvas = item.Quantity,
                                        FixDiscount = Convert.ToInt32(item.FixDiscount),
                                        Quantity = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                        RemainingQuantity = item.RemainingQuantity,
                                        PoQuantity = item.PoQuantity,
                                        ReceiveQuantity = item.ReceiveQuantity,
                                        TaxRateId = item.TaxRateId,
                                        TaxRate = item.TaxRate?.Rate ?? 0,
                                        UnitPrice = item.UnitPrice,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        TaxMethod = purchaseOrder.TaxMethod,
                                        IsExpire = item.Product.IsExpire,
                                        HighQty = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                        UnitPerPack = item.UnitPerPack ?? 0,
                                        SerialNo = item.SerialNo,
                                        Total = item.Quantity * item.UnitPrice,
                                        ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        DisplayName = item.Product?.DisplayName,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            Serial = item.Product.Serial,
                                            Guarantee = item.Product.Guarantee,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            BasicUnit = item.Product.Unit?.Name,
                                            StyleNumber = item.Product.StyleNumber,

                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,

                                            TaxRateId = item.Product.TaxRateId.Value
                                        },
                                        GrossAmount = item.TotalWithOutAmount,
                                        VatAmount = item.VatAmount,
                                        TotalAmount = item.TotalAmount,
                                        DiscountAmount = item.DiscountAmount,
                                    });

                                }

                            }
                            else
                            {
                                poItems.Add(new GoodReceiveItemLookUpModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Description = item.Description,
                                    IsService = item.IsService,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    QuantityforCanvas = item.Quantity,
                                    Quantity = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,
                                    RemainingQuantity = item.RemainingQuantity,
                                    PoQuantity = item.PoQuantity,
                                    ReceiveQuantity = item.ReceiveQuantity,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate?.Rate ?? 0,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = purchaseOrder.TaxMethod,
                                    HighQty = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                    UnitPerPack = item.UnitPerPack ?? 0,
                                    SerialNo = item.SerialNo,
                                    Total = item.Quantity * item.UnitPrice,
                                    UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                    GrossAmount = item.TotalWithOutAmount,
                                    VatAmount = item.VatAmount,
                                    TotalAmount = item.TotalAmount,
                                    DiscountAmount= item.DiscountAmount,
                                });
                            }
                        }

                        return new GoodReceiveLookUp
                        {
                            NationalId = purchaseOrder.NationalId,
                            BillingId = purchaseOrder.BillingId,
                            ShippingId = purchaseOrder.ShippingId,
                            DeliveryId = purchaseOrder.DeliveryId,
                            Id = purchaseOrder.Id,
                            VatAmount = purchaseOrder.VatAmount,
                            PurchaseOrderNo = purchaseOrder.PurchaseOrder?.RegistrationNo,
                            TaxRatesName = taxRates?.Name + ' ' + taxRates?.Rate + '%',
                            SupplierName = (purchaseOrder.Supplier.Code + "--" + purchaseOrder.Supplier.EnglishName),
                            SupplierVAT = purchaseOrder.Supplier.VatNo,
                            SupplierCommercialRegistrationNo= purchaseOrder.Supplier.CommercialRegistrationNo,
                            ContactNo1 = purchaseOrder.Supplier.ContactNo1,
                            PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                            ApprovedBy = purchaseOrder.ApprovedBy,
                            Date = purchaseOrder.Date,
                            IsClose = purchaseOrder.IsClose,
                            ApprovalStatus = purchaseOrder.ApprovalStatus,
                            RegistrationNo = purchaseOrder.RegistrationNo,
                            SupplierId = purchaseOrder.SupplierId,
                            InvoiceNo = purchaseOrder.InvoiceNo,
                            InvoiceDate = purchaseOrder.InvoiceDate,
                            Note = purchaseOrder.Note,
                            TaxMethod = purchaseOrder.TaxMethod,
                            TaxRateId = purchaseOrder.TaxRateId,
                            IsRaw = purchaseOrder.Raw,
                            GoodReceiveNoteItems = poItems,
                            TotalAmount = purchaseOrder.TotalAmount,
                            NetAmount =purchaseOrder.TotalAmount,
                            GrossAmount = purchaseOrder.TotalWithOutAmount,
                            BankCashAccountId = purchaseOrder.BankCashAccountId,
                            PaymentType = purchaseOrder.PaymentType,
                            PoDate = purchaseOrder.PoDate,
                            PoNumber = purchaseOrder.PoNumber,
                            TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,
                            SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                            Discount = purchaseOrder.Discount,
                            IsBeforeTax = purchaseOrder.IsBeforeTax,
                            IsFixed = purchaseOrder.IsFixed,
                            IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                            TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                            IsCredit = purchaseOrder.IsCredit,
                            ReceivedAmount = purchaseOrder.ReceivedAmount,
                            GoodsRecieveNumberAndDate = purchaseOrder.SoNumber,
                            Reference = purchaseOrder.Reference,
                            PurchaseOrderValue = purchaseOrderValue,
                            SupplierAddress =purchaseOrder.Supplier.Address,
                            SupplierCode =purchaseOrder.Supplier.CustomerCode,
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
