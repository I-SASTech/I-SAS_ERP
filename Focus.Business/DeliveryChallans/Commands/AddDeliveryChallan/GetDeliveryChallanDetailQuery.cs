using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;
using System.Text;

namespace Focus.Business.DeliveryChallans.Commands.AddDeliveryChallan
{
    public class GetDeliveryChallanDetailQuery : IRequest<DeliveryChallanLookUp>
    {
        public Guid Id { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsSale { get; set; }
        public bool IsReserved { get; set; }
        public bool ManualClose { get; set; }
        public bool IsDeliveryChallan { get; set; }
        public bool IsFifo { get; set; }
        public bool IsSmpleQuery { get; set; }

        public class Handler : IRequestHandler<GetDeliveryChallanDetailQuery, DeliveryChallanLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetDeliveryChallanDetailQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<DeliveryChallanLookUp> Handle(GetDeliveryChallanDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    if (request.IsSmpleQuery)
                    {

                        var purchaseOrder12 = _context.DeliveryChallans
                            .AsNoTracking()
                            .Include(x => x.Contact)
                            .Include(x => x.SaleInvoice)
                            .Include(x => x.SaleOrder)
                            .Include(x => x.DeliveryChallanItems)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Unit)
                            .FirstOrDefault(x => x.Id == request.Id);


                        if (purchaseOrder12 != null)
                        {
                            var poItems = new List<DeliveryChallanItemLookUpModel>();
                            foreach (var item in purchaseOrder12.DeliveryChallanItems)
                            {
                                if (item.ProductId != null)
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint)
                                            ? item.Description
                                            : item.Product?.DisplayNameForPrint,
                                        Quantity = item.Quantity,
                                        SoQty = item.Quantity,
                                        ProductName = string.IsNullOrEmpty(item.Product.EnglishName)
                                            ? item.Product.ArabicName
                                            : item.Product.EnglishName,
                                        ProductNameEn = item.Product.EnglishName,
                                        ProductNameAr = item.Product.ArabicName,
                                        IsActive = item.IsActive,
                                        StyleNumber = item.Product?.StyleNumber,
                                        DisplayNameForPrint = item.Product?.DisplayNameForPrint,
                                        UnitName = string.IsNullOrEmpty(item.UnitName)
                                            ? item.Product?.Unit?.Name
                                            : item.UnitName,
                                        UnitPerPack = Convert.ToDecimal(item.Product.UnitPerPack),
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            StyleNumber = item.Product?.StyleNumber,
                                            Width = item.Product.Width,
                                            UnitPerPack = item.Product.UnitPerPack,
                                            ServiceItem = item.Product.ServiceItem,
                                            WholesaleQuantity = item.Product.WholesaleQuantity,
                                            WholesalePrice = item.Product.WholesalePrice,
                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                            TaxRateId = item.Product.TaxRateId.Value,
                                            TaxMethod = item.Product.TaxMethod,
                                            Serial = item.Product.Serial,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            Guarantee = item.Product.Guarantee,
                                            SalePrice = item.Product.SalePrice,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            DisplayName = item.Product.DisplayName,



                                        }
                                    });
                                }
                                else
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        UnitName = string.IsNullOrEmpty(item.UnitName)
                                            ? item.Product?.Unit?.Name
                                            : item.UnitName,

                                        ServiceProductId = item.ServiceProductId,
                                        Description = item.Description,
                                        Quantity = item.Quantity,
                                        IsActive = item.IsActive,

                                    });
                                }

                            }

                            string deliveryAddress = "";

                            if (purchaseOrder12.DeliveryId != null)
                            {
                                var address12 = _context.DeliveryAddresses.AsNoTracking()
                                    .FirstOrDefault(x => x.Id == purchaseOrder12.DeliveryId);
                                if (address12 != null)
                                {
                                    StringBuilder addressBuilder = new StringBuilder();

                                    // Add customerAddress with a line break
                                    if (!string.IsNullOrEmpty(address12.Address))
                                    {
                                        addressBuilder.AppendLine(address12.Address);
                                    }

                                    // Add customerAddress2 with a line break if it has a value
                                    if (!string.IsNullOrEmpty(address12.Address2))
                                    {
                                        addressBuilder.AppendLine(address12.Address2);
                                    }

                                    // Combine zipCode, city, and country into a single line, if any of them have a value
                                    string zipCode = string.IsNullOrEmpty(address12.BillingZipCode) ? "" : address12.BillingZipCode;
                                    string city = string.IsNullOrEmpty(address12.City) ? "" : address12.City;
                                    string country = string.IsNullOrEmpty(address12.Country) ? "" : address12.Country;

                                    string lastLine = $"{zipCode}{(string.IsNullOrEmpty(zipCode) ? "" : ", ")}{city}{(string.IsNullOrEmpty(city) ? "" : ", ")}{country}".Trim();
                                    if (!string.IsNullOrEmpty(lastLine))
                                    {
                                        addressBuilder.AppendLine(lastLine);
                                    }

                                    // Set the Text property of Address2
                                    deliveryAddress = addressBuilder.ToString().TrimEnd('\r', '\n');



                                }


                            }
                            //else
                            //{
                            //    {
                            //        StringBuilder addressBuilder = new StringBuilder();

                            //        string address1 = purchaseOrder12.Contact?.Address;
                            //        string address2 = purchaseOrder12.Contact?.DeliveryAddress;
                            //        string country1 = purchaseOrder12.Contact?.Country;
                            //        string zipCode1 = purchaseOrder12.Contact?.BillingZipCode;
                            //        string city1 = purchaseOrder12.Contact?.City;

                            //        if (!string.IsNullOrEmpty(address1))
                            //        {
                            //            addressBuilder.AppendLine(address1);
                            //        }

                            //        if (!string.IsNullOrEmpty(address2))
                            //        {
                            //            addressBuilder.AppendLine(address2);
                            //        }

                            //        string zipCode = string.IsNullOrEmpty(zipCode1) ? "" : zipCode1;
                            //        string city = string.IsNullOrEmpty(city1) ? "" : city1;
                            //        string country = string.IsNullOrEmpty(country1) ? "" : country1;

                            //        string lastLine = $"{zipCode}{(string.IsNullOrEmpty(zipCode) ? "" : ", ")}{city}{(string.IsNullOrEmpty(city) ? "" : ", ")}{country}".Trim();
                            //        if (!string.IsNullOrEmpty(lastLine))
                            //        {
                            //            addressBuilder.AppendLine(lastLine);
                            //        }

                            //        if (purchaseOrder12.Contact != null)
                            //            purchaseOrder12.Contact.DeliveryAddress = addressBuilder.ToString().TrimEnd('\r', '\n');
                            //    }
                            //}

                            if (purchaseOrder12.BillingId != null)
                            {
                                var address12 = _context.DeliveryAddresses.AsNoTracking()
                                    .FirstOrDefault(x => x.Id == purchaseOrder12.BillingId);
                                if (address12 != null)
                                {
                                    StringBuilder addressBuilder = new StringBuilder();

                                    // Add customerAddress with a line break
                                    if (!string.IsNullOrEmpty(address12.Address))
                                    {
                                        addressBuilder.AppendLine(address12.Address);
                                    }

                                    // Add customerAddress2 with a line break if it has a value
                                    if (!string.IsNullOrEmpty(address12.Address2))
                                    {
                                        addressBuilder.AppendLine(address12.Address2);
                                    }

                                    // Combine zipCode, city, and country into a single line, if any of them have a value
                                    string zipCode = string.IsNullOrEmpty(address12.BillingZipCode) ? "" : address12.BillingZipCode;
                                    string city = string.IsNullOrEmpty(address12.City) ? "" : address12.City;
                                    string country = string.IsNullOrEmpty(address12.Country) ? "" : address12.Country;

                                    string lastLine = $"{zipCode}{(string.IsNullOrEmpty(zipCode) ? "" : ", ")}{city}{(string.IsNullOrEmpty(city) ? "" : ", ")}{country}".Trim();
                                    if (!string.IsNullOrEmpty(lastLine))
                                    {
                                        addressBuilder.AppendLine(lastLine);
                                    }

                                    // Set the Text property of Address2
                                    purchaseOrder12.Contact.Address = addressBuilder.ToString().TrimEnd('\r', '\n');



                                }


                            }
                            else
                            {
                                {
                                    StringBuilder addressBuilder = new StringBuilder();

                                    string address1 = purchaseOrder12.Contact?.Address;
                                    string address2 = purchaseOrder12.Contact?.DeliveryAddress;
                                    string country1 = purchaseOrder12.Contact?.Country;
                                    string zipCode1 = purchaseOrder12.Contact?.BillingZipCode;
                                    string city1 = purchaseOrder12.Contact?.City;

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

                                    if (purchaseOrder12.Contact != null)
                                        purchaseOrder12.Contact.Address = addressBuilder.ToString().TrimEnd('\r', '\n');
                                }
                            }




                            return new DeliveryChallanLookUp
                            {
                                Id = purchaseOrder12.Id,
                                DeliveryId = purchaseOrder12.DeliveryId,
                                ShippingId = purchaseOrder12.ShippingId,
                                NationalId = purchaseOrder12.NationalId,
                                BillingId = purchaseOrder12.BillingId,
                                Date = purchaseOrder12.Date,
                                IsService = purchaseOrder12.IsService,
                                CustomerNameEn = purchaseOrder12.Contact?.CustomerDisplayName,
                                CustomerNameAr = purchaseOrder12.Contact?.CustomerDisplayName,
                                CustomerCode = purchaseOrder12.Contact?.Code,
                                SaleInvoiceRegistrationNumber = purchaseOrder12.SaleInvoice?.RegistrationNo,
                                CustomerId = purchaseOrder12.CustomerId,
                                SaleInvoiceId = purchaseOrder12.SaleInvoiceId,
                                CustomerAddress = purchaseOrder12.Contact?.Address,
                                DeliveryAddress = deliveryAddress,
                                BilingAddress = purchaseOrder12.BilingAddress,
                                ApprovalStatus = purchaseOrder12.ApprovalStatus,
                                RegistrationNo = purchaseOrder12.RegistrationNo,
                                Description = purchaseOrder12.Description,
                                TemplateName = purchaseOrder12.TemplateName,
                                DeliveryChallanItems = poItems,
                                SaleOrderRegistrationNumber = purchaseOrder12.SaleOrder?.RegistrationNo,
                                IsQuotation = purchaseOrder12.SaleOrder?.IsQuotation ?? false,
                                CustomerPoNumber = purchaseOrder12.SaleInvoice?.RegistrationNo,
                                CustomerName = purchaseOrder12.Contact?.CustomerDisplayName,
                                
                            };


                        }
                    }




                    var address = _context.DeliveryAddresses.AsNoTracking().ToList();

                    if (request.IsSale)
                    {
                        if (request.IsReserved)
                        {
                            var reservedChallan = _context.DeliveryChallanReserveds
                          .AsNoTracking()
                          .Include(x => x.DeliveryChallanReserverdItems)
                          .ThenInclude(x => x.Product)
                          .ThenInclude(x => x.Unit)
                           .FirstOrDefault(x => x.Id == request.Id);

                            if (request.ManualClose && reservedChallan != null)
                            {
                                reservedChallan.IsClose = true;
                                _context.DeliveryChallanReserveds.Update(reservedChallan);
                                Random rnd = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd.Next(0, 9).ToString();
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);
                                _context.SaveChanges();
                            }

                            var poItems = new List<DeliveryChallanItemLookUpModel>();
                            foreach (var item in reservedChallan.DeliveryChallanReserverdItems)
                            {
                                if (item.ProductId != null)
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        IsActive = item.IsActive,
                                        Quantity = item.Quantity,
                                        ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                        ProductNameEn = item.Product.EnglishName,
                                        ProductNameAr = item.Product.ArabicName,
                                        StyleNumber = item.Product?.StyleNumber,
                                        DisplayNameForPrint = item.Product?.DisplayNameForPrint,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        UnitPerPack = Convert.ToDecimal(item.Product.UnitPerPack),
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            StyleNumber = item.Product?.StyleNumber,
                                            Width = item.Product.Width,
                                            UnitPerPack = item.Product.UnitPerPack,
                                            ServiceItem = item.Product.ServiceItem,
                                            WholesaleQuantity = item.Product.WholesaleQuantity,
                                            WholesalePrice = item.Product.WholesalePrice,
                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                            TaxRateId = item.Product.TaxRateId.Value,
                                            TaxMethod = item.Product.TaxMethod,
                                            Serial = item.Product.Serial,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            Guarantee = item.Product.Guarantee,
                                            SalePrice = item.Product.SalePrice,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            DisplayName = item.Product.DisplayName,



                                        }
                                    });
                                }
                                else
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = item.Description,
                                        Quantity = item.Quantity,
                                        IsActive = item.IsActive,


                                    });
                                }
                            }



                            return new DeliveryChallanLookUp
                            {
                                Id = reservedChallan.Id,
                                DeliveryId = reservedChallan.DeliveryId,
                                ShippingId = reservedChallan.ShippingId,
                                NationalId = reservedChallan.NationalId,
                                BillingId = reservedChallan.BillingId,
                                Date = reservedChallan.Date,
                                IsService = reservedChallan.IsService,
                                IsClose = reservedChallan.IsClose,

                                SaleInvoiceId = reservedChallan.SaleInvoiceId,
                                SaleOrderId = reservedChallan.SaleOrderId,
                                CustomerAddress = reservedChallan.CustomerAddress,
                                BilingAddress = reservedChallan.BilingAddress,
                                ApprovalStatus = reservedChallan.ApprovalStatus,
                                RegistrationNo = reservedChallan.RegistrationNo,
                                Description = reservedChallan.Description,
                                TemplateName = reservedChallan.TemplateName,
                                DeliveryChallanItems = poItems,
                                
                            };

                        }

                        var purchaseOrder = _context.DeliveryChallans
                          .AsNoTracking()
                          .Include(x => x.Contact)
                          .Include(x => x.SaleInvoice)
                          .Include(x => x.DeliveryChallanItems)
                          .ThenInclude(x => x.Product)
                          .ThenInclude(x => x.Unit)
                           .FirstOrDefault(x => x.Id == request.Id);


                        if (purchaseOrder != null)
                        {
                            var poItems = new List<DeliveryChallanItemLookUpModel>();
                            foreach (var item in purchaseOrder.DeliveryChallanItems)
                            {
                                if (item.ProductId != null)
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        Quantity = item.Quantity,
                                        SoQty = item.Quantity,
                                        ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                        ProductNameEn = item.Product.EnglishName,
                                        ProductNameAr = item.Product.ArabicName,
                                        IsActive = item.IsActive,
                                        StyleNumber = item.Product?.StyleNumber,
                                        DisplayNameForPrint = item.Product?.DisplayNameForPrint,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        UnitPerPack = Convert.ToDecimal(item.Product.UnitPerPack),
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            StyleNumber = item.Product?.StyleNumber,
                                            Width = item.Product.Width,
                                            UnitPerPack = item.Product.UnitPerPack,
                                            ServiceItem = item.Product.ServiceItem,
                                            WholesaleQuantity = item.Product.WholesaleQuantity,
                                            WholesalePrice = item.Product.WholesalePrice,
                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                            TaxRateId = item.Product.TaxRateId.Value,
                                            TaxMethod = item.Product.TaxMethod,
                                            Serial = item.Product.Serial,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            Guarantee = item.Product.Guarantee,
                                            SalePrice = item.Product.SalePrice,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            DisplayName = item.Product.DisplayName,



                                        }
                                    });
                                }
                                else
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                        ServiceProductId = item.ServiceProductId,
                                        Description = item.Description,
                                        Quantity = item.Quantity,
                                        IsActive = item.IsActive,

                                    });
                                }

                            }



                            return new DeliveryChallanLookUp
                            {
                                Id = purchaseOrder.Id,
                                DeliveryAddress = address.FirstOrDefault(x => x.Id == purchaseOrder.DeliveryId)?.Address,
                                ShippingAddress = address.FirstOrDefault(x => x.Id == purchaseOrder.ShippingId)?.Address,
                                NationalAddress = address.FirstOrDefault(x => x.Id == purchaseOrder.NationalId)?.Address,
                                BillingAddress1 = address.FirstOrDefault(x => x.Id == purchaseOrder.BillingId)?.Address,
                                DeliveryId = purchaseOrder.DeliveryId,
                                ShippingId = purchaseOrder.ShippingId,
                                NationalId = purchaseOrder.NationalId,
                                BillingId = purchaseOrder.BillingId,
                                Date = purchaseOrder.Date,
                                IsService = purchaseOrder.IsService,
                                CustomerNameEn = purchaseOrder.Contact?.CustomerDisplayName,
                                CustomerNameAr = purchaseOrder.Contact?.CustomerDisplayName,
                                CustomerCode = purchaseOrder.Contact?.Code,
                                SaleInvoiceRegistrationNumber = purchaseOrder.SaleInvoice?.RegistrationNo,

                                CustomerId = purchaseOrder.CustomerId,
                                SaleInvoiceId = purchaseOrder.SaleInvoiceId,
                                CustomerAddress = purchaseOrder.Contact?.Address,
                                BilingAddress = purchaseOrder.BilingAddress,
                                ApprovalStatus = purchaseOrder.ApprovalStatus,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                Description = purchaseOrder.Description,
                                TemplateName = purchaseOrder.TemplateName,
                                DeliveryChallanItems = poItems,
                                CustomerName = purchaseOrder.Contact?.CustomerDisplayName,
                                //DeliveryAddress = purchaseOrder.Contact?.DeliveryAddress,
                                
                            };
                        }
                        throw new NotFoundException(nameof(purchaseOrder), request.Id);
                    }
                    else
                    {

                        if (request.IsReserved)
                        {
                            var reservedChallan = _context.DeliveryChallanReserveds
                          .AsNoTracking()
                          .Include(x => x.DeliveryChallanReserverdItems)
                          .ThenInclude(x => x.Product)
                          .ThenInclude(x => x.Unit)
                           .FirstOrDefault(x => x.Id == request.Id);
                            if (request.ManualClose && reservedChallan != null)
                            {
                                reservedChallan.IsClose = true;
                                _context.DeliveryChallanReserveds.Update(reservedChallan);
                                Random rnd = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd.Next(0, 9).ToString();
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);
                                await _context.SaveChangesAsync(cancellationToken);

                            }

                            var poItems = new List<DeliveryChallanItemLookUpModel>();
                            foreach (var item in reservedChallan.DeliveryChallanReserverdItems)
                            {
                                if (item.ProductId != null)
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        DisplayName = item.Product?.DisplayName,
                                        Quantity = item.Quantity,
                                        ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                        ProductNameEn = item.Product.EnglishName,
                                        ProductNameAr = item.Product.ArabicName,
                                        IsActive = item.IsActive,
                                        StyleNumber = item.Product?.StyleNumber,
                                        DisplayNameForPrint = item.Product?.DisplayNameForPrint,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        UnitPerPack = Convert.ToDecimal(item.Product.UnitPerPack),
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            StyleNumber = item.Product?.StyleNumber,
                                            Width = item.Product.Width,
                                            UnitPerPack = item.Product.UnitPerPack,
                                            ServiceItem = item.Product.ServiceItem,
                                            WholesaleQuantity = item.Product.WholesaleQuantity,
                                            WholesalePrice = item.Product.WholesalePrice,
                                            Code = item.Product.Code,
                                            EnglishName = item.Product.EnglishName,
                                            ArabicName = item.Product.ArabicName,
                                            TaxRateId = item.Product.TaxRateId.Value,
                                            TaxMethod = item.Product.TaxMethod,
                                            Serial = item.Product.Serial,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            Guarantee = item.Product.Guarantee,
                                            SalePrice = item.Product.SalePrice,
                                            LevelOneUnit = item.Product.LevelOneUnit,
                                            DisplayName = item.Product.DisplayName,



                                        }
                                    });
                                }
                                else
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = item.Description,
                                        IsActive = item.IsActive,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,


                                        Quantity = item.Quantity,

                                    });
                                }
                            }



                            return new DeliveryChallanLookUp
                            {
                                Id = reservedChallan.Id,
                                Date = reservedChallan.Date,
                                IsService = reservedChallan.IsService,
                                IsClose = reservedChallan.IsClose,

                                SaleInvoiceId = reservedChallan.SaleInvoiceId,
                                SaleOrderId = reservedChallan.SaleOrderId,
                                CustomerAddress = reservedChallan.CustomerAddress,
                                BilingAddress = reservedChallan.BilingAddress,
                                ApprovalStatus = reservedChallan.ApprovalStatus,
                                RegistrationNo = reservedChallan.RegistrationNo,
                                Description = reservedChallan.Description,
                                TemplateName = reservedChallan.TemplateName,
                                DeliveryChallanItems = poItems

                            };
                        }


                        if (request.IsDeliveryChallan)
                        {
                            var deliveryChallan = await _context.DeliveryChallans
                                .AsNoTracking()
                                .Include(x => x.Contact)
                                .Include(x => x.DeliveryChallanItems)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Unit)
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);



                            var reservedChallan = await _context.DeliveryChallanReserveds
                                .AsNoTracking()
                                .Include(x => x.DeliveryChallanReserverdItems)
                                .FirstOrDefaultAsync(x => x.DeliveryChallanId == request.Id, cancellationToken: cancellationToken);

                            if (reservedChallan != null)
                            {
                                foreach (var reservd in reservedChallan.DeliveryChallanReserverdItems)
                                {
                                    foreach (var x in deliveryChallan.DeliveryChallanItems)
                                    {
                                        if (x.ProductId != null)
                                        {
                                            if (x.ProductId == reservd.ProductId)
                                            {
                                                if (reservd.Quantity > 0)
                                                {
                                                    x.Quantity = reservd.Quantity;

                                                }
                                            }
                                        }

                                        if (x.ServiceProductId == reservd.ServiceProductId && x.ServiceProductId != null)
                                        {
                                            if (reservd.Quantity > 0)
                                            {
                                                x.Quantity = reservd.Quantity;

                                            }
                                        }
                                    }
                                }
                            }



                            if (deliveryChallan != null)
                            {
                                var stocks = deliveryChallan.BranchId == null ? await _context.Stocks.AsNoTracking().ToListAsync(cancellationToken: cancellationToken) : await _context.Stocks.AsNoTracking().Where(x => x.BranchId == deliveryChallan.BranchId).ToListAsync(cancellationToken: cancellationToken);

                                var poItems = new List<DeliveryChallanItemLookUpModel>();
                                foreach (var item in deliveryChallan.DeliveryChallanItems)
                                {
                                    if (item.ProductId != null)
                                    {
                                        poItems.Add(new DeliveryChallanItemLookUpModel
                                        {
                                            Id = item.Id,
                                            UnitPrice = item.UnitPrice,
                                            ProductId = item.ProductId,
                                            ServiceProductId = item.ServiceProductId,
                                            DisplayName = item.Product?.DisplayName,
                                            Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                            SoQty = item.Quantity,
                                            Quantity = item.Quantity,
                                            ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                            ProductNameEn = item.Product.EnglishName,
                                            ProductNameAr = item.Product.ArabicName,
                                            IsActive = item.IsActive,
                                            StyleNumber = item.Product?.StyleNumber,
                                            DisplayNameForPrint = item.Product?.DisplayNameForPrint,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                            UnitPerPack = Convert.ToDecimal(item.Product.UnitPerPack),
                                            Product = new ProductDropdownLookUpModel
                                            {
                                                Id = item.Product.Id,
                                                BarCode = item.Product.BarCode,
                                                StyleNumber = item.Product?.StyleNumber,
                                                Width = item.Product.Width,
                                                UnitPerPack = item.Product.UnitPerPack,
                                                ServiceItem = item.Product.ServiceItem,
                                                WholesaleQuantity = item.Product.WholesaleQuantity,
                                                WholesalePrice = item.Product.WholesalePrice,
                                                Code = item.Product.Code,
                                                EnglishName = item.Product.EnglishName,
                                                ArabicName = item.Product.ArabicName,
                                                TaxRateId = item.Product.TaxRateId.Value,
                                                TaxMethod = item.Product.TaxMethod,
                                                Serial = item.Product.Serial,
                                                HighUnitPrice = item.Product.HighUnitPrice,
                                                Guarantee = item.Product.Guarantee,
                                                SalePrice = item.Product.SalePrice,
                                                LevelOneUnit = item.Product.LevelOneUnit,
                                                DisplayName = item.Product.DisplayName,
                                                Inventory = new Inventory()
                                                {
                                                    CurrentQuantity = stocks.FirstOrDefault(x => x.ProductId == item.ProductId)?.CurrentQuantity ?? 0
                                                },
                                            }
                                        });
                                    }
                                    else
                                    {
                                        poItems.Add(new DeliveryChallanItemLookUpModel
                                        {
                                            Id = item.Id,
                                            UnitPrice = item.UnitPrice,
                                            ProductId = item.ProductId,
                                            UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,

                                            ServiceProductId = item.ServiceProductId,
                                            Description = item.Description,
                                            Quantity = item.Quantity,
                                            IsActive = item.IsActive,
                                        });
                                    }
                                }

                                return new DeliveryChallanLookUp
                                {
                                    Id = deliveryChallan.Id,
                                    DeliveryAddress = address.FirstOrDefault(x => x.Id == deliveryChallan.DeliveryId)?.Address,
                                    ShippingAddress = address.FirstOrDefault(x => x.Id == deliveryChallan.ShippingId)?.Address,
                                    NationalAddress = address.FirstOrDefault(x => x.Id == deliveryChallan.NationalId)?.Address,
                                    BillingAddress1 = address.FirstOrDefault(x => x.Id == deliveryChallan.BillingId)?.Address,
                                    DeliveryId = deliveryChallan.DeliveryId,
                                    ShippingId = deliveryChallan.ShippingId,
                                    NationalId = deliveryChallan.NationalId,
                                    BillingId = deliveryChallan.BillingId,
                                    Date = deliveryChallan.Date,
                                    IsService = deliveryChallan.IsService,
                                    SaleOrderId = deliveryChallan.SaleOrderId,
                                    CustomerNameEn = deliveryChallan.Contact?.EnglishName,
                                    CustomerNameAr = deliveryChallan.Contact?.ArabicName,
                                    CustomerId = deliveryChallan.CustomerId,
                                    CustomerAddress = deliveryChallan.CustomerAddress,
                                    BilingAddress = deliveryChallan.BilingAddress,
                                    ApprovalStatus = deliveryChallan.ApprovalStatus,
                                    RegistrationNo = deliveryChallan.RegistrationNo,
                                    Description = deliveryChallan.Description,
                                    TemplateName = deliveryChallan.TemplateName,
                                    SaleOrderRegistrationNumber = deliveryChallan.SaleOrder?.RegistrationNo,
                                    DeliveryChallanItems = poItems
                                };
                            }
                        }


                        var purchaseOrder = await _context.DeliveryChallans
                         .AsNoTracking()
                         .Include(x => x.Contact)
                         .Include(x => x.SaleOrder)
                         .Include(x => x.SaleInvoice)
                         .Include(x => x.DeliveryChallanItems)
                         .ThenInclude(x => x.Product)
                         .ThenInclude(x => x.Unit)
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);










                        if (purchaseOrder != null)
                        {
                            var poItems = new List<DeliveryChallanItemLookUpModel>();
                            foreach (var item in purchaseOrder.DeliveryChallanItems)
                            {
                                if (item.ProductId != null)
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                        SoQty = item.Quantity,
                                        Quantity = item.Quantity,
                                        ProductName = item.Product.DisplayName,
                                        ProductNameEn = item.Product.DisplayName,
                                        ProductNameAr = item.Product.DisplayName,
                                        StyleNumber = item.Product?.StyleNumber,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,
                                        IsActive = item.IsActive,
                                        DisplayName = item.Product?.DisplayName,
                                        DisplayNameForPrint = item.Product?.DisplayNameForPrint,
                                        UnitPerPack = Convert.ToDecimal(item.Product.UnitPerPack),
                                        Product = new ProductDropdownLookUpModel
                                        {
                                            Id = item.Product.Id,
                                            BarCode = item.Product.BarCode,
                                            StyleNumber = item.Product?.StyleNumber,
                                            Width = item.Product.Width,
                                            UnitPerPack = item.Product.UnitPerPack,
                                            ServiceItem = item.Product.ServiceItem,
                                            WholesaleQuantity = item.Product.WholesaleQuantity,
                                            WholesalePrice = item.Product.WholesalePrice,
                                            Code = item.Product.Code,
                                            DisplayName = item.Product.DisplayName,
                                            TaxRateId = item.Product.TaxRateId.Value,
                                            TaxMethod = item.Product.TaxMethod,
                                            Serial = item.Product.Serial,
                                            HighUnitPrice = item.Product.HighUnitPrice,
                                            Guarantee = item.Product.Guarantee,
                                            SalePrice = item.Product.SalePrice,
                                            LevelOneUnit = item.Product.LevelOneUnit,

                                        }
                                    });
                                }
                                else
                                {
                                    poItems.Add(new DeliveryChallanItemLookUpModel
                                    {
                                        Id = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductId = item.ProductId,
                                        ServiceProductId = item.ServiceProductId,
                                        Description = item.Description,
                                        Quantity = item.Quantity,
                                        IsActive = item.IsActive,
                                        UnitName = string.IsNullOrEmpty(item.UnitName) ? item.Product?.Unit?.Name : item.UnitName,



                                    });
                                }
                            }



                            return new DeliveryChallanLookUp
                            {
                                Id = purchaseOrder.Id,
                                Date = purchaseOrder.Date,
                                DeliveryId = purchaseOrder.DeliveryId,
                                DeliveryAddress = address.FirstOrDefault(x => x.Id == purchaseOrder.DeliveryId)?.Address,
                                ShippingAddress = address.FirstOrDefault(x => x.Id == purchaseOrder.ShippingId)?.Address,
                                NationalAddress = address.FirstOrDefault(x => x.Id == purchaseOrder.NationalId)?.Address,
                                BillingAddress1 = address.FirstOrDefault(x => x.Id == purchaseOrder.BillingId)?.Address,
                                ShippingId = purchaseOrder.ShippingId,
                                NationalId = purchaseOrder.NationalId,
                                BillingId = purchaseOrder.BillingId,
                                IsService = purchaseOrder.IsService,
                                SaleOrderId = purchaseOrder.SaleOrderId,
                                CustomerNameEn = purchaseOrder.Contact?.CustomerDisplayName,
                                CustomerNameAr = purchaseOrder.Contact?.CustomerDisplayName,
                                CustomerCode = purchaseOrder.Contact?.Code,
                                CustomerId = purchaseOrder.CustomerId,
                                CustomerAddress = purchaseOrder.Contact?.Address,
                                BilingAddress = purchaseOrder.BilingAddress,
                                ApprovalStatus = purchaseOrder.ApprovalStatus,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                Description = purchaseOrder.Description,
                                TemplateName = purchaseOrder.TemplateName,
                                SaleOrderRegistrationNumber = purchaseOrder.SaleOrder?.RegistrationNo,
                                IsQuotation = purchaseOrder.SaleOrder?.IsQuotation??false,
                                CustomerPoNumber = purchaseOrder.SaleInvoice?.RegistrationNo,
                                DeliveryChallanItems = poItems


                            };
                        }

                        throw new NotFoundException(nameof(purchaseOrder), request.Id);


                    }
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
