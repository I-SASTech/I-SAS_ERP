using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Models;
using System.Text;

namespace Focus.Business.SaleOrders.Commands.AddSaleOrder
{
    public class AddSaleOrderCommand : IRequest<Message>
    {
        public SaleOrderLookupModel SaleOrder { get; set; }

        public class Handler : IRequestHandler<AddSaleOrderCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            private readonly IHostingEnvironment _hostingEnvironment;
            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly ISendEmail sendEmail;

            public Handler(IApplicationDbContext context, ILogger<AddSaleOrderCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider, IHostingEnvironment hostingEnvironment, ISendEmail _sendEmail)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                _hostingEnvironment = hostingEnvironment;
                sendEmail = _sendEmail;

            }

            public async Task<Message> Handle(AddSaleOrderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.SaleOrder.Date.Year && x.PeriodStart.Month == request.SaleOrder.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");


                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.SaleOrder.Id == Guid.Empty)
                    {
                        if (!string.IsNullOrEmpty(request.SaleOrder.WarrantyLogoPath))
                        {
                            if (request.SaleOrder.WarrantyLogoPath.Contains("Attachment"))
                            {
                                request.SaleOrder.WarrantyLogoPath = GetBaseImage(request.SaleOrder.WarrantyLogoPath).Result;
                            }

                        }



                        var saleOrder = new SaleOrder()
                        {
                            DispatchDate = request.SaleOrder.DispatchDate,
                            PickUpDate = request.SaleOrder.PickUpDate,
                            SaleOrderId = request.SaleOrder.SaleOrderId,
                            ProformaId = request.SaleOrder.ProformaId,
                            PurchaseOrderId = request.SaleOrder.PurchaseOrderId,
                            InquiryId = request.SaleOrder.InquiryId,
                            DeliveryNoteAndDate = request.SaleOrder.DeliveryNoteAndDate,
                            QuotationValidUpto = request.SaleOrder.QuotationValidUpto,
                            PerfomaValidUpto = request.SaleOrder.PerfomaValidUpto,
                            CustomerInquiry = request.SaleOrder.CustomerInquiry,
                            PoNumber = request.SaleOrder.PoNumber,
                            PoDate = request.SaleOrder.PoDate,
                            ReferredBy = request.SaleOrder.ReferredBy,
                            RefrenceNo = request.SaleOrder.RefrenceNo,
                            DoctorName = request.SaleOrder.DoctorName,
                            HospitalName = request.SaleOrder.HospitalName,
                            QuotationNo = request.SaleOrder.QuotationNo,
                            SaleOrderNo = request.SaleOrder.SaleOrderNo,
                            PeroformaInvoiceNo = request.SaleOrder.PeroformaInvoiceNo,
                            PurchaseOrderNo = request.SaleOrder.PurchaseOrderNo,
                            InquiryNo = request.SaleOrder.InquiryNo,
                            DeliveryId = request.SaleOrder.DeliveryId,
                            ShippingId = request.SaleOrder.ShippingId,
                            BillingId = request.SaleOrder.BillingId,
                            NationalId = request.SaleOrder.NationalId,
                            EmployeeId = request.SaleOrder.EmployeeId,
                            DueDate = request.SaleOrder.DueDate,
                            NoteDescription = request.SaleOrder.NoteDescription,
                            Date = request.SaleOrder.Date,
                            ValidityDate = request.SaleOrder.ValidityDate,
                            Subject = request.SaleOrder.Subject,
                            Attiendie = request.SaleOrder.Attiendie,
                            Purpose = request.SaleOrder.Purpose,
                            For = request.SaleOrder.For,
                            OneTimeDescription = request.SaleOrder.OneTimeDescription,
                            CustomerAddress = request.SaleOrder.CustomerAddress,
                            Mobile = request.SaleOrder.Mobile,
                            LogisticId = request.SaleOrder.LogisticId,
                            BarCode = request.SaleOrder.BarCode,
                            RegistrationNo = request.SaleOrder.RegistrationNo,
                            QuotationId = request.SaleOrder.QuotationId,
                            WareHouseId = request.SaleOrder.WareHouseId,
                            ClientPurchaseNo = request.SaleOrder.ClientPurchaseNo,
                            CustomerId = request.SaleOrder.CustomerId,
                            Refrence = request.SaleOrder.Refrence,
                            PaymentMethod = request.SaleOrder.PaymentMethod,
                            SheduleDelivery = request.SaleOrder.SheduleDelivery,
                            Days = request.SaleOrder.Days,
                            IsFreight = request.SaleOrder.IsFreight,
                            IsLabour = request.SaleOrder.IsLabour,
                            Note = request.SaleOrder.Note,
                            NoteAr = request.SaleOrder.NoteAr,
                            ApprovalStatus = request.SaleOrder.ApprovalStatus,
                            IsClose = request.SaleOrder.IsClose,
                            IsQuotation = request.SaleOrder.IsQuotation,
                            IsService = request.SaleOrder.IsService,
                            Description = request.SaleOrder.Description,
                            TaxMethod = request.SaleOrder.TaxMethod,
                            TaxRateId = request.SaleOrder.TaxRateId,
                            WarrantyLogoPath = request.SaleOrder.WarrantyLogoPath,
                            TerminalId = request.SaleOrder.TerminalId,
                            IsSaleOrderTracking = request.SaleOrder.IsSaleOrderTracking,
                            DeliveryChallanId = request.SaleOrder.DeliveryChallanId,
                            DeliveryDate = request.SaleOrder.DeliveryDate,

                            //For ImportExport Field
                            OrderTypeId = request.SaleOrder.OrderTypeId,
                            IncotermsId = request.SaleOrder.IncotermsId,
                            CommodityId = request.SaleOrder.CommodityId,
                            Commodities = request.SaleOrder.Commodities,
                            NatureOfCargo = request.SaleOrder.NatureOfCargo,
                            Attn = request.SaleOrder.Attn,
                            QuotationValidDate = request.SaleOrder.QuotationValidDate ?? DateTime.UtcNow,
                            FreeTimePOL = request.SaleOrder.FreeTimePOL,
                            FreeTimePOD = request.SaleOrder.FreeTimePOD,

                            Discount = request.SaleOrder.Discount,
                            TransactionLevelDiscount = request.SaleOrder.TransactionLevelDiscount,
                            IsDiscountOnTransaction = request.SaleOrder.IsDiscountOnTransaction,
                            IsFixed = request.SaleOrder.IsFixed,
                            IsBeforeTax = request.SaleOrder.IsBeforeTax,
                            TotalAmount = request.SaleOrder.TotalAmount,
                            VatAmount = request.SaleOrder.VatAmount,
                            DiscountAmount = request.SaleOrder.DiscountAmount,
                            TotalAfterDiscount = request.SaleOrder.TotalAfterDiscount,
                            TotalWithOutAmount = request.SaleOrder.GrossAmounts,
                            BranchId = request.SaleOrder.BranchId,
                            CreatedBy = _contextProvider.GetUserId(),
                        };

                        if (request.SaleOrder.ImportExportItems != null && request.SaleOrder.ImportExportItems.Any())
                        {
                            saleOrder.ImportExportItems = request.SaleOrder.ImportExportItems.Select(x =>
                                new ImportExportItem()
                                {
                                    ServiceId = x.ServiceId,
                                    StuffingLocationId = x.StuffingLocationId,
                                    PortOfLoadingId = x.PortOfLoadingId,
                                    PortOfDestinationId = x.PortOfDestinationId,
                                    CarrierId = x.CarrierId,
                                    Description = x.Description,
                                    Ft = x.Ft,
                                    Hc = x.Hc,
                                    Tt = x.Tt,
                                    Etd = x.Etd
                                }).ToList();
                        }

                        if (request.SaleOrder.QuotationId != null)
                        {
                            var salesOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.SaleOrder.QuotationId, cancellationToken: cancellationToken);
                            if (salesOrder != null)
                            {
                                saleOrder.InvoiceNote = salesOrder.RegistrationNo + " - " + salesOrder.Date.ToString("dd/MM/yyyy");
                            }
                        }
                        else if (request.SaleOrder.SaleOrderId != null)
                        {
                            var salesOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.SaleOrder.SaleOrderId, cancellationToken: cancellationToken);
                            if (salesOrder != null)
                            {
                                saleOrder.InvoiceNote = salesOrder.RegistrationNo + " - " + salesOrder.Date.ToString("dd/MM/yyyyy");
                            }
                        }
                        else if (request.SaleOrder.ProformaId != null)
                        {
                            var proformaInvoice = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.SaleOrder.ProformaId, cancellationToken: cancellationToken);
                            if (proformaInvoice != null)
                            {
                                saleOrder.InvoiceNote = proformaInvoice.RegistrationNo + " - " + proformaInvoice.Date.ToString("dd/yy/yyyy");
                            }

                        }
                        else if (request.SaleOrder.DeliveryChallanId != null)
                        {
                            var deliveryChallanId = await _context.DeliveryChallans.FirstOrDefaultAsync(x => x.Id == request.SaleOrder.DeliveryChallanId, cancellationToken: cancellationToken);

                            if (deliveryChallanId != null)
                            {
                                saleOrder.InvoiceNote = deliveryChallanId.RegistrationNo + " - " + deliveryChallanId.Date.ToString("dd/yy/yyyy");
                            }
                        }

                        await _context.SaleOrders.AddAsync(saleOrder, cancellationToken);

                        //Create SaleOrder Version

                        {

                            var saleOrderVersion = new SaleOrderVersion()
                            {
                                Version = "V.01",
                                SaleOrderId = saleOrder.Id,
                                DocumentNo = saleOrder.RegistrationNo,
                                Date = DateTime.UtcNow,
                                IsQuotation = saleOrder.IsQuotation
                            };
                            await _context.SaleOrderVersions.AddAsync(saleOrderVersion, cancellationToken);

                            var saleOrderItems = request.SaleOrder.SaleOrderItems.Select(x =>
                                new SaleOrderItem()
                                {
                                    UnitName = x.UnitName,
                                    SaleOrderId = saleOrder.Id,
                                    ProductId = x.ProductId,
                                    TaxRateId = x.TaxRateId,
                                    TaxMethod = x.TaxMethod,
                                    WareHouseId = request.SaleOrder.WareHouseId,
                                    Discount = x.Discount,
                                    FixDiscount = x.FixDiscount,
                                    Quantity = x.Quantity,
                                    QuantityOut = x.Quantity,
                                    UnitPrice = x.UnitPrice,
                                    ExpiryDate = x.ExpiryDate,
                                    BatchNo = x.BatchNo,
                                    Description = x.Description,
                                    ServiceItem = x.ServiceItem,
                                    Serial = x.Serial,
                                    GuaranteeDate = x.GuaranteeDate,
                                    StyleNumber = x.StyleNumber,
                                    IsFree = x.IsFree,
                                    Scheme = x.Scheme,
                                    SchemeQuantity = x.SchemeQuantity,
                                    SchemePhysicalQuantity = x.SchemePhysicalQuantity,
                                    SaleOrderVersionId = saleOrderVersion.Id,

                                    TotalAmount = x.TotalAmount,
                                    VatAmount = x.VatAmount,
                                    DiscountAmount = x.DiscountAmount,
                                    TotalWithOutAmount = x.GrossAmounts,
                                }).ToList();

                            await _context.SaleOrderItems.AddRangeAsync(saleOrderItems, cancellationToken);

                            var userLog = new UserLog()
                            {
                                Date = DateTime.UtcNow,
                                DocumentId = saleOrder.Id,
                                DocumentNo = saleOrder.RegistrationNo,
                                DocumentType = saleOrder.IsQuotation ? "Quotation" : "Sale Order",
                                ApprovalStatus = saleOrder.ApprovalStatus.ToString(),
                                Description = $"{saleOrder.ApprovalStatus} by {_contextProvider.GetUserFullName()}",
                                UserName = _contextProvider.GetUserFullName(),
                                UserId = _contextProvider.GetUserId() ?? Guid.Empty
                            };
                            await _context.UserLogs.AddAsync(userLog, cancellationToken);
                        }



                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        if (request.SaleOrder.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.SaleOrder.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = saleOrder.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }

                        if (request.SaleOrder.ApprovalStatus == ApprovalStatus.Approved && request.SaleOrder.SoInventoryReserve == "true" && !request.SaleOrder.IsQuotation)
                        {
                            var stocks = _context.Stocks.AsQueryable();
                            foreach (var item in saleOrder.SaleOrderItems)
                            {
                                if (item.ProductId != null && !item.ServiceItem)
                                {
                                    var stock = saleOrder.BranchId == null ? await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId, cancellationToken: cancellationToken) : await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.BranchId == saleOrder.BranchId && x.WareHouseId == item.WareHouseId, cancellationToken: cancellationToken);

                                    if (stock == null)
                                    {
                                        var newStock = new Stock()
                                        {
                                            ProductId = item.ProductId.Value,
                                            WareHouseId = item.WareHouseId ?? Guid.Empty,
                                            CurrentQuantity = -(item.Quantity + (item.SchemePhysicalQuantity ?? 0)),
                                            BranchId = saleOrder.BranchId
                                        };
                                        await _context.Stocks.AddAsync(newStock, cancellationToken);
                                        stock = newStock;
                                    }
                                    else
                                    {
                                        stock.CurrentQuantity -= (item.Quantity + (item.SchemePhysicalQuantity ?? 0));
                                        stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                        if (!request.SaleOrder.IsFifo)
                                        {
                                            _context.Stocks.Update(stock);
                                        }
                                    }

                                    //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    //{
                                    //    ProductId = item.ProductId.Value,
                                    //    WareHouseId = item.WareHouseId ?? Guid.Empty
                                    //}, cancellationToken);

                                    var itemDetail = request.SaleOrder.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);

                                    if (request.SaleOrder.IsSerial && itemDetail.IsSerial)
                                    {
                                        var serialArray = item.Serial.Split(',');
                                        for (int i = 0; i < item.Quantity; i++)
                                        {
                                            var inv = new Inventory()
                                            {
                                                Date = DateTime.UtcNow,
                                                DocumentNumber = saleOrder.RegistrationNo,
                                                DocumentId = saleOrder.Id,
                                                Quantity = 1,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = saleOrder.WareHouseId ?? Guid.Empty,
                                                TransactionType = TransactionType.SaleInvoice,
                                                //Price = currentInventory.Price,
                                                SalePrice = item.UnitPrice,
                                                Serial = serialArray[i],
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                //CurrentQuantity = currentInventory.CurrentQuantity - (i + 1),
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - (i + 1)) * currentInventory.AveragePrice)
                                                BranchId = saleOrder.BranchId
                                            };
                                            await _context.Inventories.AddAsync(inv, cancellationToken);
                                        }

                                    }
                                    else
                                    {
                                        Inventory batchInventory = null;
                                        if (request.SaleOrder.IsFifo)
                                        {
                                            batchInventory = _context.Inventories.FirstOrDefault(x => x.BatchNumber == item.BatchNo && x.IsActive && x.IsOpen && x.ProductId == item.ProductId);
                                            if (batchInventory != null)
                                            {
                                                if (batchInventory.RemainingQuantity > item.Quantity)
                                                {
                                                    batchInventory.RemainingQuantity -= item.Quantity;
                                                }
                                                else if (batchInventory.RemainingQuantity == item.Quantity)
                                                {
                                                    batchInventory.RemainingQuantity = 0;
                                                    batchInventory.IsActive = false;
                                                    batchInventory.IsOpen = false;
                                                }
                                                else
                                                {
                                                    var batchInventoryForRemaining = _context.Inventories.FirstOrDefault(x => x.IsActive && x.IsOpen && x.ProductId == item.ProductId && x.AutoNumbering > batchInventory.AutoNumbering);
                                                    batchInventoryForRemaining.RemainingQuantity -= (item.Quantity - batchInventory.RemainingQuantity);

                                                    var inventory = new Inventory()
                                                    {
                                                        Date = DateTime.UtcNow,
                                                        DocumentId = saleOrder.Id,
                                                        DocumentNumber = saleOrder.RegistrationNo,
                                                        Quantity = item.Quantity - batchInventory.RemainingQuantity,
                                                        ProductId = item.ProductId.Value,
                                                        StockId = stock.Id,
                                                        WareHouseId = saleOrder.WareHouseId ?? Guid.Empty,
                                                        TransactionType = TransactionType.SaleInvoice,
                                                        //Price = currentInventory.Price,
                                                        SalePrice = item.UnitPrice,
                                                        //AveragePrice = currentInventory.AveragePrice,
                                                        //ExpiryDate = currentInventory.ExpiryDate,
                                                        BatchNumber = batchInventoryForRemaining.BatchNumber,
                                                        AutoNumbering = batchInventoryForRemaining.AutoNumbering,
                                                        //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                        //CurrentStockValue = ((currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity)) * currentInventory.AveragePrice)
                                                        BranchId = saleOrder.BranchId
                                                    };
                                                    //currentInventory.CurrentQuantity = currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity);
                                                    item.Quantity = batchInventory.RemainingQuantity;
                                                    if (inventory.CurrentQuantity == 0)
                                                    {
                                                        batchInventoryForRemaining.RemainingQuantity = 0;
                                                        batchInventoryForRemaining.IsActive = false;
                                                        batchInventoryForRemaining.IsOpen = false;
                                                    }
                                                    batchInventory.RemainingQuantity = 0;
                                                    batchInventory.IsActive = false;
                                                    batchInventory.IsOpen = false;
                                                    await _context.Inventories.AddAsync(inventory, cancellationToken);
                                                }
                                            }
                                        }
                                        var inv = new Inventory()
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentNumber = saleOrder.RegistrationNo,
                                            DocumentId = saleOrder.Id,
                                            Quantity = item.Quantity,
                                            ProductId = item.ProductId.Value,
                                            StockId = stock.Id,
                                            WareHouseId = saleOrder.WareHouseId ?? Guid.Empty,
                                            TransactionType = TransactionType.SaleInvoice,
                                            //Price = currentInventory.Price,
                                            SalePrice = item.UnitPrice,
                                            Serial = item.Serial,
                                            //AveragePrice = currentInventory.AveragePrice,
                                            //ExpiryDate = currentInventory.ExpiryDate,
                                            BatchNumber = batchInventory?.BatchNumber,
                                            AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                            //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice)
                                            BranchId = saleOrder.BranchId
                                        };
                                        await _context.Inventories.AddAsync(inv, cancellationToken);

                                        if (item.SchemePhysicalQuantity is > 0)
                                        {
                                            var invScheme = new Inventory()
                                            {
                                                Date = DateTime.UtcNow,
                                                DocumentNumber = saleOrder.RegistrationNo,
                                                DocumentId = saleOrder.Id,
                                                Quantity = (decimal)item.SchemePhysicalQuantity,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = saleOrder.WareHouseId ?? Guid.Empty,
                                                TransactionType = TransactionType.SaleInvoice,
                                                //Price = currentInventory.Price,
                                                SalePrice = item.UnitPrice,
                                                Serial = item.Serial,
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                BatchNumber = batchInventory?.BatchNumber,
                                                AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                                //CurrentQuantity = currentInventory.CurrentQuantity - (decimal)item.SchemePhysicalQuantity,
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - (decimal)item.SchemePhysicalQuantity) * currentInventory.AveragePrice)
                                                BranchId = saleOrder.BranchId
                                            };
                                            await _context.Inventories.AddAsync(invScheme, cancellationToken);
                                        }
                                    }


                                    if (request.SaleOrder.IsFifo)
                                    {
                                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                                        {
                                            SyncRecordModels = _context.SyncLog(),
                                            Code = _code,
                                            BranchId = saleOrder.BranchId,
                                            DocumentNo = saleOrder.RegistrationNo
                                        }, cancellationToken);

                                        await _context.SaveChangesAsync(CancellationToken.None);
                                    }
                                }
                            }
                        }

                        if (request.SaleOrder.QuotationId != null && request.SaleOrder.QuotationId != Guid.Empty && request.SaleOrder.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var quotation = await _context.SaleOrders.FindAsync(request.SaleOrder.QuotationId);
                            if (quotation == null)
                                throw new NotFoundException("Quotation Not Found", "");

                            if (quotation.IsClose)
                                throw new NotFoundException("Quotation is already close", "");

                            if (request.SaleOrder.IsService)
                            {
                                foreach (var item in request.SaleOrder.SaleOrderItems)
                                {
                                    SaleOrderItem soItemDetail;
                                    if (item.ProductId == null)
                                    {
                                        soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                    }
                                    else
                                    {
                                        soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                    }
                                    if (soItemDetail != null)
                                    {
                                        soItemDetail.QuantityOut -= item.Quantity;
                                        _context.SaleOrderItems.Update(soItemDetail);
                                    }
                                }
                            }
                            var close = quotation.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.SaleOrder.QuotationId && x.QuantityOut > 0);
                            if (close == null || !request.SaleOrder.IsService)
                            {
                                quotation.IsClose = true;
                            }
                        }


                        //Create Customer Advance Account
                        var costCenter = await _context.CostCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "220000", cancellationToken: cancellationToken);
                        if (costCenter == null)
                        {
                            var accountType = await _context.AccountTypes.AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Name == "Liabilities" || x.NameArabic == "التزامات", cancellationToken: cancellationToken);
                            var newCostCenter = new CostCenter()
                            {
                                Code = "220000",
                                Name = "Cash Advances",
                                NameArabic = "السلف النقدية",
                                Description = "Cash Advances",
                                IsActive = true,
                                AccountTypeId = accountType.Id
                            };
                            await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);
                            var customer = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.SaleOrder.CustomerId, cancellationToken: cancellationToken);
                            if (customer == null)
                                throw new NotFoundException("Customer", "Cannot be found.");

                            var newAccount = new Account
                            {
                                Code = "22000001",
                                Name = customer.EnglishName == "" ? customer.ArabicName : customer.EnglishName + " Advance",
                                NameArabic = customer.ArabicName + "يتقدم  ",
                                Description = "Cash Advances",
                                IsActive = true,
                                CostCenterId = newCostCenter.Id
                            };
                            await _context.Accounts.AddAsync(newAccount, cancellationToken);
                            customer.AdvanceAccountId = newAccount.Id;
                            _context.Contacts.Update(customer);
                        }
                        else
                        {
                            var customer = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.SaleOrder.CustomerId, cancellationToken: cancellationToken);
                            if (customer == null)
                                throw new NotFoundException("Customer", "Cannot be found.");
                            if (customer.AdvanceAccountId == null)
                            {
                                var accounts = await _context.Accounts.AsNoTracking()
                                    .Where(x => x.CostCenterId == costCenter.Id)
                                    .OrderBy(x => x.Code)
                                    .LastOrDefaultAsync(cancellationToken: cancellationToken);

                                var code = Convert.ToInt64(accounts.Code);
                                var newCode = code + 1;
                                var format = Convert.ToString(newCode);
                                var newAccount = new Account
                                {
                                    Code = format,
                                    Name = customer.EnglishName == "" ? customer.ArabicName : customer.EnglishName + " Advance",
                                    NameArabic = customer.ArabicName + " يتقدم",
                                    Description = "Cash Advances",
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                customer.AdvanceAccountId = newAccount.Id;
                                _context.Contacts.Update(customer);
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = saleOrder.BranchId,
                            DocumentNo = saleOrder.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                       
                        scope.Complete();

                      

                        return new Message()
                        {
                            Id = saleOrder.Id
                        };
                    }
                    else
                    {
                        if (request.SaleOrder.IsClose && request.SaleOrder.Id != Guid.Empty)
                        {
                            var so = await _context.SaleOrders.FindAsync(request.SaleOrder.Id);

                            if (so == null)
                                throw new NotFoundException("Sale Order", request.SaleOrder.Id);

                            if (request.SaleOrder.SoInventoryReserve == "true")
                            {
                                var stocks = _context.Stocks.AsQueryable();
                                var inventory = _context.Inventories;

                                foreach (var item in so.SaleOrderItems)
                                {
                                    if (!item.ServiceItem && item.ProductId != null)
                                    {
                                        if (item.QuantityOut > 0)
                                        {
                                            var stock = await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId, cancellationToken: cancellationToken);
                                            if (stock != null)
                                            {
                                                stock.CurrentQuantity += (item.Quantity + item.SchemePhysicalQuantity ?? 0);
                                                stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                                if (!request.SaleOrder.IsFifo)
                                                {
                                                    _context.Stocks.Update(stock);
                                                }
                                            }

                                            //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                            //{
                                            //    ProductId = item.ProductId.Value,
                                            //    WareHouseId = item.WareHouseId ?? Guid.Empty
                                            //}, cancellationToken);

                                            if (request.SaleOrder.IsFifo)
                                            {
                                                var batchInventory = inventory.Where(x => x.DocumentId == so.Id && x.ProductId == item.ProductId && x.BatchNumber == item.BatchNo).ToList();

                                                if (batchInventory.Count > 0)
                                                {
                                                    //decimal remainingQuantity = ;
                                                    for (var i = 0; i < batchInventory.Count(); i++)
                                                    {
                                                        var batchData = inventory.FirstOrDefault(x => x.BatchNumber == batchInventory[i].BatchNumber && x.AutoNumbering == batchInventory[i].AutoNumbering && (x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.PurchasePost));
                                                        batchData.IsOpen = true;
                                                        batchData.IsActive = true;
                                                        batchData.RemainingQuantity += Math.Abs(item.QuantityOut);
                                                        //remainingQuantity = remainingQuantity - batchInventory[i].Quantity;
                                                    }
                                                }
                                            }
                                            var inv = new Inventory()
                                            {
                                                Date = DateTime.UtcNow,
                                                DocumentId = so.Id,
                                                DocumentNumber = so.RegistrationNo,
                                                Quantity = Math.Abs(item.QuantityOut),
                                                //Price = currentInventory.Price,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = stock.WareHouseId,
                                                Serial = item.Serial,
                                                TransactionType = TransactionType.SaleReturn,
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                //CurrentQuantity = currentInventory.CurrentQuantity + Math.Abs(item.QuantityOut),
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity + Math.Abs(item.QuantityOut)) * currentInventory.AveragePrice)
                                                BranchId = so.BranchId
                                            };
                                            await _context.Inventories.AddAsync(inv, cancellationToken);
                                        }

                                    }
                                }
                            }


                            so.IsClose = request.SaleOrder.IsClose;
                            so.Reason = request.SaleOrder.Reason;
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                DocumentNo = so.RegistrationNo,
                                BranchId = so.BranchId
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return new Message()
                            {
                                Id = so.Id
                            };
                        }
                        else
                        {
                            var purchase = await _context.SaleOrders.FindAsync(request.SaleOrder.Id);

                            if (request.SaleOrder.IsEditPaidInvoice)
                            {
                                purchase.SaleOrderId = request.SaleOrder.SaleOrderId;
                                purchase.ProformaId = request.SaleOrder.ProformaId;
                                purchase.PurchaseOrderId = request.SaleOrder.PurchaseOrderId;
                                purchase.InquiryId = request.SaleOrder.InquiryId;
                                purchase.DeliveryDate = request.SaleOrder.DeliveryDate;
                                purchase.DeliveryNoteAndDate = request.SaleOrder.DeliveryNoteAndDate;
                                purchase.QuotationValidUpto = request.SaleOrder.QuotationValidUpto;
                                purchase.PerfomaValidUpto = request.SaleOrder.PerfomaValidUpto;
                                purchase.CustomerInquiry = request.SaleOrder.CustomerInquiry;
                                purchase.PoNumber = request.SaleOrder.PoNumber;
                                purchase.PoDate = request.SaleOrder.PoDate;
                                purchase.ReferredBy = request.SaleOrder.ReferredBy;
                                purchase.RefrenceNo = request.SaleOrder.RefrenceNo;
                                purchase.DoctorName = request.SaleOrder.DoctorName;
                                purchase.HospitalName = request.SaleOrder.HospitalName;
                                purchase.QuotationNo = request.SaleOrder.QuotationNo;
                                purchase.SaleOrderNo = request.SaleOrder.SaleOrderNo;
                                purchase.PeroformaInvoiceNo = request.SaleOrder.PeroformaInvoiceNo;
                                purchase.PurchaseOrderNo = request.SaleOrder.PurchaseOrderNo;
                                purchase.InquiryNo = request.SaleOrder.InquiryNo;
                                purchase.DeliveryId = request.SaleOrder.DeliveryId;
                                purchase.ShippingId = request.SaleOrder.ShippingId;
                                purchase.BillingId = request.SaleOrder.BillingId;
                                purchase.NationalId = request.SaleOrder.NationalId;
                                purchase.EmployeeId = request.SaleOrder.EmployeeId;
                                purchase.DueDate = request.SaleOrder.DueDate;
                                purchase.NoteDescription = request.SaleOrder.NoteDescription;

                                purchase.Date = request.SaleOrder.Date;
                                purchase.Attiendie = request.SaleOrder.Attiendie;
                                purchase.Subject = request.SaleOrder.Subject;
                                purchase.ValidityDate = request.SaleOrder.ValidityDate;
                                purchase.Purpose = request.SaleOrder.Purpose;
                                purchase.For = request.SaleOrder.For;
                                purchase.OneTimeDescription = request.SaleOrder.OneTimeDescription;
                                purchase.Mobile = request.SaleOrder.Mobile;
                                purchase.LogisticId = request.SaleOrder.LogisticId;
                                purchase.CustomerAddress = request.SaleOrder.CustomerAddress;
                                purchase.BarCode = request.SaleOrder.BarCode;
                                purchase.ClientPurchaseNo = request.SaleOrder.ClientPurchaseNo;
                                purchase.WareHouseId = request.SaleOrder.WareHouseId;
                                purchase.QuotationId = request.SaleOrder.QuotationId;
                                purchase.IsQuotation = request.SaleOrder.IsQuotation;
                                purchase.Refrence = request.SaleOrder.Refrence;
                                purchase.PaymentMethod = request.SaleOrder.PaymentMethod;
                                purchase.SheduleDelivery = request.SaleOrder.SheduleDelivery;
                                purchase.Days = request.SaleOrder.Days;
                                purchase.IsFreight = request.SaleOrder.IsFreight;
                                purchase.IsLabour = request.SaleOrder.IsLabour;
                                purchase.RegistrationNo = request.SaleOrder.RegistrationNo;
                                //purchase.CustomerId = request.SaleOrder.CustomerId;
                                purchase.Note = request.SaleOrder.Note;
                                purchase.NoteAr = request.SaleOrder.NoteAr;
                                purchase.ApprovalStatus = request.SaleOrder.ApprovalStatus;
                                purchase.Description = request.SaleOrder.Description;
                                purchase.IsService = request.SaleOrder.IsService;
                                purchase.TaxMethod = request.SaleOrder.TaxMethod;
                                purchase.TaxRateId = request.SaleOrder.TaxRateId;



                                if (!string.IsNullOrEmpty(request.SaleOrder.WarrantyLogoPath))
                                {
                                    if (request.SaleOrder.WarrantyLogoPath.Contains("Attachment"))
                                    {
                                        purchase.WarrantyLogoPath = GetBaseImage(request.SaleOrder.WarrantyLogoPath).Result;
                                    }


                                }


                                //For ImportExport Field
                                purchase.OrderTypeId = request.SaleOrder.OrderTypeId;
                                purchase.IncotermsId = request.SaleOrder.IncotermsId;
                                purchase.CommodityId = request.SaleOrder.CommodityId;
                                purchase.Commodities = request.SaleOrder.Commodities;
                                purchase.NatureOfCargo = request.SaleOrder.NatureOfCargo;
                                purchase.Attn = request.SaleOrder.Attn;
                                purchase.QuotationValidDate = request.SaleOrder.QuotationValidDate ?? DateTime.UtcNow;
                                purchase.FreeTimePOL = request.SaleOrder.FreeTimePOL;
                                purchase.FreeTimePOD = request.SaleOrder.FreeTimePOD;
                                purchase.Discount = request.SaleOrder.Discount;
                                purchase.TransactionLevelDiscount = request.SaleOrder.TransactionLevelDiscount;
                                purchase.IsDiscountOnTransaction = request.SaleOrder.IsDiscountOnTransaction;
                                purchase.IsFixed = request.SaleOrder.IsFixed;
                                purchase.IsBeforeTax = request.SaleOrder.IsBeforeTax;
                                purchase.IsSaleOrderTracking = request.SaleOrder.IsSaleOrderTracking;
                                purchase.TotalAmount = request.SaleOrder.TotalAmount;
                                purchase.VatAmount = request.SaleOrder.VatAmount;
                                purchase.DiscountAmount = request.SaleOrder.DiscountAmount;
                                purchase.TotalAfterDiscount = request.SaleOrder.TotalAfterDiscount  ;
                                purchase.TotalWithOutAmount = request.SaleOrder.GrossAmounts;

                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                    DocumentNo = purchase.RegistrationNo,
                                    BranchId = purchase.BranchId
                                }, cancellationToken);

                                await _context.SaveChangesAsync(cancellationToken);

                                return new Message()
                                {
                                    Id = purchase.Id,
                                };
                            }
                            // Generate version No
                            Guid? saleOrderVersionId = null;
                            if (request.SaleOrder.ApprovalStatus == ApprovalStatus.Draft)
                            {
                                if (!request.SaleOrder.IsQuotation)
                                {
                                    string newVersion;
                                    var version = purchase.SaleOrderVersions.OrderBy(x => x.Version).LastOrDefault(x => x.IsQuotation == purchase.IsQuotation);
                                    if (version == null)
                                    {
                                        newVersion = "V.01";
                                    }
                                    else
                                    {
                                        string fetchNo = version.Version.Substring(2);
                                        Int32 autoNo = Convert.ToInt32(fetchNo);
                                        var format = "00";
                                        autoNo++;
                                        var newCode = "V." + autoNo.ToString(format);
                                        newVersion = newCode;
                                    }
                                    //Create Version
                                    var saleOrderVersion = new SaleOrderVersion()
                                    {
                                        Version = newVersion,
                                        SaleOrderId = purchase.Id,
                                        DocumentNo = purchase.RegistrationNo,
                                        Date = DateTime.UtcNow,
                                        IsQuotation = purchase.IsQuotation
                                    };
                                    await _context.SaleOrderVersions.AddAsync(saleOrderVersion, cancellationToken);
                                    saleOrderVersionId = saleOrderVersion.Id;
                                }

                            }

                            purchase.SaleOrderId = request.SaleOrder.SaleOrderId;
                            purchase.ProformaId = request.SaleOrder.ProformaId;
                            purchase.PurchaseOrderId = request.SaleOrder.PurchaseOrderId;
                            purchase.InquiryId = request.SaleOrder.InquiryId;
                            purchase.DeliveryDate = request.SaleOrder.DeliveryDate;
                            purchase.DeliveryNoteAndDate = request.SaleOrder.DeliveryNoteAndDate;
                            purchase.QuotationValidUpto = request.SaleOrder.QuotationValidUpto;
                            purchase.PerfomaValidUpto = request.SaleOrder.PerfomaValidUpto;
                            purchase.CustomerInquiry = request.SaleOrder.CustomerInquiry;
                            purchase.PoNumber = request.SaleOrder.PoNumber;
                            purchase.PoDate = request.SaleOrder.PoDate;
                            purchase.ReferredBy = request.SaleOrder.ReferredBy;
                            purchase.RefrenceNo = request.SaleOrder.RefrenceNo;
                            purchase.DoctorName = request.SaleOrder.DoctorName;
                            purchase.HospitalName = request.SaleOrder.HospitalName;
                            purchase.QuotationNo = request.SaleOrder.QuotationNo;
                            purchase.SaleOrderNo = request.SaleOrder.SaleOrderNo;
                            purchase.PeroformaInvoiceNo = request.SaleOrder.PeroformaInvoiceNo;
                            purchase.PurchaseOrderNo = request.SaleOrder.PurchaseOrderNo;
                            purchase.InquiryNo = request.SaleOrder.InquiryNo;
                            purchase.DeliveryId = request.SaleOrder.DeliveryId;
                            purchase.ShippingId = request.SaleOrder.ShippingId;
                            purchase.BillingId = request.SaleOrder.BillingId;
                            purchase.NationalId = request.SaleOrder.NationalId;
                            purchase.EmployeeId = request.SaleOrder.EmployeeId;
                            purchase.DueDate = request.SaleOrder.DueDate;
                            purchase.NoteDescription = request.SaleOrder.NoteDescription;

                            purchase.Date = request.SaleOrder.Date;
                            purchase.Attiendie = request.SaleOrder.Attiendie;
                            purchase.Subject = request.SaleOrder.Subject;
                            purchase.ValidityDate = request.SaleOrder.ValidityDate;
                            purchase.Purpose = request.SaleOrder.Purpose;
                            purchase.For = request.SaleOrder.For;
                            purchase.OneTimeDescription = request.SaleOrder.OneTimeDescription;
                            purchase.Mobile = request.SaleOrder.Mobile;
                            purchase.LogisticId = request.SaleOrder.LogisticId;
                            purchase.CustomerAddress = request.SaleOrder.CustomerAddress;
                            purchase.BarCode = request.SaleOrder.BarCode;
                            purchase.ClientPurchaseNo = request.SaleOrder.ClientPurchaseNo;
                            purchase.WareHouseId = request.SaleOrder.WareHouseId;
                            purchase.QuotationId = request.SaleOrder.QuotationId;
                            purchase.IsQuotation = request.SaleOrder.IsQuotation;
                            purchase.Refrence = request.SaleOrder.Refrence;
                            purchase.PaymentMethod = request.SaleOrder.PaymentMethod;
                            purchase.SheduleDelivery = request.SaleOrder.SheduleDelivery;
                            purchase.Days = request.SaleOrder.Days;
                            purchase.IsFreight = request.SaleOrder.IsFreight;
                            purchase.IsLabour = request.SaleOrder.IsLabour;
                            purchase.RegistrationNo = request.SaleOrder.RegistrationNo;
                            purchase.CustomerId = request.SaleOrder.CustomerId;
                            purchase.Note = request.SaleOrder.Note;
                            purchase.NoteAr = request.SaleOrder.NoteAr;
                            purchase.ApprovalStatus = request.SaleOrder.ApprovalStatus;
                            purchase.Description = request.SaleOrder.Description;
                            purchase.IsService = request.SaleOrder.IsService;
                            purchase.TaxMethod = request.SaleOrder.TaxMethod;
                            purchase.TaxRateId = request.SaleOrder.TaxRateId;

                            //For ImportExport Field
                            purchase.OrderTypeId = request.SaleOrder.OrderTypeId;
                            purchase.IncotermsId = request.SaleOrder.IncotermsId;
                            purchase.CommodityId = request.SaleOrder.CommodityId;
                            purchase.Commodities = request.SaleOrder.Commodities;
                            purchase.NatureOfCargo = request.SaleOrder.NatureOfCargo;
                            purchase.Attn = request.SaleOrder.Attn;
                            purchase.QuotationValidDate = request.SaleOrder.QuotationValidDate ?? DateTime.UtcNow;
                            purchase.FreeTimePOL = request.SaleOrder.FreeTimePOL;
                            purchase.FreeTimePOD = request.SaleOrder.FreeTimePOD;
                            purchase.Discount = request.SaleOrder.Discount;
                            purchase.TransactionLevelDiscount = request.SaleOrder.TransactionLevelDiscount;
                            purchase.IsDiscountOnTransaction = request.SaleOrder.IsDiscountOnTransaction;
                            purchase.IsFixed = request.SaleOrder.IsFixed;
                            purchase.IsBeforeTax = request.SaleOrder.IsBeforeTax;
                            purchase.IsSaleOrderTracking = request.SaleOrder.IsSaleOrderTracking;
                            purchase.TotalAmount = request.SaleOrder.TotalAmount;
                            purchase.VatAmount = request.SaleOrder.VatAmount;
                            purchase.DiscountAmount = request.SaleOrder.DiscountAmount;
                            purchase.TotalAfterDiscount = request.SaleOrder.TotalAfterDiscount;
                            purchase.TotalWithOutAmount = request.SaleOrder.GrossAmounts;

                            if (!string.IsNullOrEmpty(request.SaleOrder.WarrantyLogoPath))
                            {
                                if (request.SaleOrder.WarrantyLogoPath.Contains("Attachment"))
                                {
                                    purchase.WarrantyLogoPath = GetBaseImage(request.SaleOrder.WarrantyLogoPath).Result;
                                }


                            }

                            _context.SaleOrderItems.RemoveRange(purchase.SaleOrderItems);
                            purchase.SaleOrderItems = request.SaleOrder.SaleOrderItems.Select(x =>
                                                           new SaleOrderItem()
                                                           {
                                                               UnitName = x.UnitName,
                                                               ProductId = x.ProductId,
                                                               TaxRateId = x.TaxRateId,
                                                               TaxMethod = x.TaxMethod,
                                                               WareHouseId = request.SaleOrder.WareHouseId,
                                                               Discount = x.Discount,
                                                               FixDiscount = x.FixDiscount,
                                                               Quantity = x.Quantity,
                                                               QuantityOut = x.Quantity,
                                                               UnitPrice = x.UnitPrice,
                                                               ExpiryDate = x.ExpiryDate,
                                                               BatchNo = x.BatchNo,
                                                               Description = x.Description,
                                                               ServiceItem = x.ServiceItem,
                                                               Serial = x.Serial,
                                                               GuaranteeDate = x.GuaranteeDate,
                                                               StyleNumber = x.StyleNumber,
                                                               IsFree = x.IsFree,
                                                               Scheme = x.Scheme,
                                                               SchemeQuantity = x.SchemeQuantity,
                                                               SchemePhysicalQuantity = x.SchemePhysicalQuantity,
                                                               TotalAmount = x.TotalAmount,
                                                               VatAmount = x.VatAmount,
                                                               DiscountAmount = x.DiscountAmount,
                                                               TotalWithOutAmount = x.GrossAmounts,
                                                               SaleOrderVersionId = request.SaleOrder.ApprovalStatus == ApprovalStatus.Approved ? purchase.SaleOrderVersions?.LastOrDefault()?.Id : saleOrderVersionId
                                                           }).ToList();


                            _context.ImportExportItems.RemoveRange(purchase.ImportExportItems);
                            if (request.SaleOrder.ImportExportItems != null && request.SaleOrder.ImportExportItems.Any())
                            {
                                purchase.ImportExportItems = request.SaleOrder.ImportExportItems.Select(x =>
                                    new ImportExportItem()
                                    {
                                        ServiceId = x.ServiceId,
                                        StuffingLocationId = x.StuffingLocationId,
                                        PortOfLoadingId = x.PortOfLoadingId,
                                        PortOfDestinationId = x.PortOfDestinationId,
                                        CarrierId = x.CarrierId,
                                        Description = x.Description,
                                        Ft = x.Ft,
                                        Hc = x.Hc,
                                        Tt = x.Tt,
                                        Etd = x.Etd
                                    }).ToList();
                            }


                            var userLog = new UserLog()
                            {
                                Date = DateTime.UtcNow,
                                DocumentId = purchase.Id,
                                DocumentNo = purchase.RegistrationNo,
                                DocumentType = purchase.IsQuotation ? "Quotation" : "Sale Order",
                                ApprovalStatus = purchase.ApprovalStatus.ToString(),
                                Description = $"{purchase.ApprovalStatus} by {_contextProvider.GetUserFullName()}",
                                UserName = _contextProvider.GetUserFullName(),
                                UserId = _contextProvider.GetUserId() ?? Guid.Empty
                            };
                            await _context.UserLogs.AddAsync(userLog, cancellationToken);


                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            if (request.SaleOrder.AttachmentList != null)
                            {
                                var attachments = _context.Attachments
                                    .AsNoTracking()
                                    .Where(x => x.DocumentId == purchase.Id)
                                    .AsQueryable();

                                if (attachments.Any())
                                {
                                    _context.Attachments.RemoveRange(attachments);
                                }
                                foreach (var item in request.SaleOrder.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = purchase.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);

                                }
                            }

                            if (request.SaleOrder.ApprovalStatus == ApprovalStatus.Approved && !request.SaleOrder.IsQuotation && request.SaleOrder.SoInventoryReserve == "true")
                            {
                                var stocks = _context.Stocks.AsQueryable();

                                foreach (var item in request.SaleOrder.SaleOrderItems)
                                {
                                    if (item.ProductId != null && !item.ServiceItem)
                                    {
                                        var stock = await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == request.SaleOrder.WareHouseId, cancellationToken: cancellationToken);

                                        if (stock == null)
                                        {
                                            var newStock = new Stock()
                                            {
                                                ProductId = item.ProductId.Value,
                                                WareHouseId = request.SaleOrder.WareHouseId ?? Guid.Empty,
                                                CurrentQuantity = -(item.Quantity + (item.SchemePhysicalQuantity ?? 0)),
                                                BranchId = purchase.BranchId
                                            };
                                            await _context.Stocks.AddAsync(newStock, cancellationToken);
                                            stock = newStock;
                                        }
                                        else
                                        {
                                            stock.CurrentQuantity -= (item.Quantity + (item.SchemePhysicalQuantity ?? 0));
                                            stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                            if (!request.SaleOrder.IsFifo)
                                            {
                                                _context.Stocks.Update(stock);
                                            }
                                        }

                                        //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                        //{
                                        //    ProductId = item.ProductId.Value,
                                        //    WareHouseId = request.SaleOrder.WareHouseId ?? Guid.Empty
                                        //}, cancellationToken);


                                        var itemDetail = request.SaleOrder.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);

                                        if (request.SaleOrder.IsSerial && itemDetail.IsSerial)
                                        {
                                            var serialArray = item.Serial.Split(',');
                                            for (int i = 0; i < item.Quantity; i++)
                                            {
                                                var inv = new Inventory()
                                                {
                                                    Date = DateTime.UtcNow,
                                                    DocumentNumber = purchase.RegistrationNo,
                                                    DocumentId = purchase.Id,
                                                    Quantity = 1,
                                                    ProductId = item.ProductId.Value,
                                                    StockId = stock.Id,
                                                    WareHouseId = purchase.WareHouseId ?? Guid.Empty,
                                                    TransactionType = TransactionType.SaleInvoice,
                                                    //Price = currentInventory.Price,
                                                    SalePrice = item.UnitPrice,
                                                    Serial = serialArray[i],
                                                    //AveragePrice = currentInventory.AveragePrice,
                                                    //ExpiryDate = currentInventory.ExpiryDate,
                                                    //CurrentQuantity = currentInventory.CurrentQuantity - (i + 1),
                                                    //CurrentStockValue = ((currentInventory.CurrentQuantity - (i + 1)) * currentInventory.AveragePrice)
                                                    BranchId = purchase.BranchId
                                                };
                                                await _context.Inventories.AddAsync(inv, cancellationToken);
                                            }

                                        }
                                        else
                                        {
                                            Inventory batchInventory = null;
                                            if (request.SaleOrder.IsFifo)
                                            {
                                                batchInventory = _context.Inventories.FirstOrDefault(x => x.BatchNumber == item.BatchNo && x.IsActive && x.IsOpen && x.ProductId == item.ProductId);
                                                if (batchInventory != null)
                                                {
                                                    if (batchInventory.RemainingQuantity > item.Quantity)
                                                    {
                                                        batchInventory.RemainingQuantity -= item.Quantity;
                                                    }
                                                    else if (batchInventory.RemainingQuantity == item.Quantity)
                                                    {
                                                        batchInventory.RemainingQuantity = 0;
                                                        batchInventory.IsActive = false;
                                                        batchInventory.IsOpen = false;
                                                    }
                                                    else
                                                    {
                                                        var batchInventoryForRemaining = _context.Inventories.FirstOrDefault(x => x.IsActive && x.IsOpen && x.ProductId == item.ProductId && x.AutoNumbering > batchInventory.AutoNumbering);
                                                        batchInventoryForRemaining.RemainingQuantity -= (item.Quantity - batchInventory.RemainingQuantity);

                                                        var inventory = new Inventory()
                                                        {
                                                            Date = DateTime.UtcNow,
                                                            DocumentId = request.SaleOrder.Id,
                                                            DocumentNumber = purchase.RegistrationNo,
                                                            Quantity = item.Quantity - batchInventory.RemainingQuantity,
                                                            ProductId = item.ProductId.Value,
                                                            StockId = stock.Id,
                                                            WareHouseId = request.SaleOrder.WareHouseId ?? Guid.Empty,
                                                            TransactionType = TransactionType.SaleInvoice,
                                                            //Price = currentInventory.Price,
                                                            SalePrice = item.UnitPrice,
                                                            //AveragePrice = currentInventory.AveragePrice,
                                                            //ExpiryDate = currentInventory.ExpiryDate,
                                                            BatchNumber = batchInventoryForRemaining.BatchNumber,
                                                            AutoNumbering = batchInventoryForRemaining.AutoNumbering,
                                                            //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity)) * currentInventory.AveragePrice)
                                                            BranchId = purchase.BranchId
                                                        };
                                                        //currentInventory.CurrentQuantity = currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity);
                                                        item.Quantity = batchInventory.RemainingQuantity;
                                                        if (inventory.CurrentQuantity == 0)
                                                        {
                                                            batchInventoryForRemaining.RemainingQuantity = 0;
                                                            batchInventoryForRemaining.IsActive = false;
                                                            batchInventoryForRemaining.IsOpen = false;
                                                        }
                                                        batchInventory.RemainingQuantity = 0;
                                                        batchInventory.IsActive = false;
                                                        batchInventory.IsOpen = false;
                                                        await _context.Inventories.AddAsync(inventory, cancellationToken);
                                                    }
                                                }
                                            }
                                            var inv = new Inventory()
                                            {
                                                Date = DateTime.UtcNow,
                                                DocumentId = request.SaleOrder.Id,
                                                DocumentNumber = purchase.RegistrationNo,
                                                Quantity = item.Quantity,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = request.SaleOrder.WareHouseId ?? Guid.Empty,
                                                TransactionType = TransactionType.SaleInvoice,
                                                //Price = currentInventory.Price,
                                                SalePrice = item.UnitPrice,
                                                Serial = item.Serial,
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                BatchNumber = batchInventory?.BatchNumber,
                                                AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                                //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice)
                                                BranchId = purchase.BranchId
                                            };
                                            await _context.Inventories.AddAsync(inv, cancellationToken);

                                            if (item.SchemePhysicalQuantity is > 0)
                                            {
                                                var invScheme = new Inventory()
                                                {
                                                    Date = DateTime.UtcNow,
                                                    DocumentId = request.SaleOrder.Id,
                                                    DocumentNumber = purchase.RegistrationNo,
                                                    Quantity = (decimal)item.SchemePhysicalQuantity,
                                                    ProductId = item.ProductId.Value,
                                                    StockId = stock.Id,
                                                    WareHouseId = request.SaleOrder.WareHouseId ?? Guid.Empty,
                                                    TransactionType = TransactionType.SaleInvoice,
                                                    //Price = currentInventory.Price,
                                                    SalePrice = item.UnitPrice,
                                                    Serial = item.Serial,
                                                    //AveragePrice = currentInventory.AveragePrice,
                                                    //ExpiryDate = currentInventory.ExpiryDate,
                                                    BatchNumber = batchInventory?.BatchNumber,
                                                    AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                                    //CurrentQuantity = currentInventory.CurrentQuantity - (decimal)item.SchemePhysicalQuantity,
                                                    //CurrentStockValue = ((currentInventory.CurrentQuantity - (decimal)item.SchemePhysicalQuantity) * currentInventory.AveragePrice)
                                                    BranchId = purchase.BranchId
                                                };
                                                await _context.Inventories.AddAsync(invScheme, cancellationToken);
                                            }
                                        }


                                        if (request.SaleOrder.IsFifo)
                                        {
                                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                                            {
                                                SyncRecordModels = _context.SyncLog(),
                                                Code = _code,
                                                DocumentNo = request.SaleOrder.RegistrationNo,
                                                BranchId = request.SaleOrder.BranchId,
                                            }, cancellationToken);

                                            await _context.SaveChangesAsync(CancellationToken.None);
                                        }
                                    }
                                }
                            }


                            if (request.SaleOrder.QuotationId != null && request.SaleOrder.QuotationId != Guid.Empty && request.SaleOrder.ApprovalStatus == ApprovalStatus.Approved)
                            {
                                var quotation = await _context.SaleOrders.FindAsync(request.SaleOrder.QuotationId);
                                if (quotation == null)
                                    throw new NotFoundException("Quotation Not Found", "");

                                if (quotation.IsClose)
                                    throw new NotFoundException("Quotation is already close", "");

                                if (request.SaleOrder.IsService)
                                {
                                    foreach (var item in request.SaleOrder.SaleOrderItems)
                                    {
                                        SaleOrderItem soItemDetail;
                                        if (item.ProductId == null)
                                        {
                                            soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.Description == item.Description);

                                        }
                                        else
                                        {
                                            soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);

                                        }
                                        if (soItemDetail != null)
                                        {
                                            soItemDetail.QuantityOut -= item.Quantity;
                                            _context.SaleOrderItems.Update(soItemDetail);
                                        }
                                    }
                                }
                                var close = quotation.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.SaleOrder.QuotationId && x.QuantityOut > 0);
                                if (close == null || !request.SaleOrder.IsService)
                                {
                                    quotation.IsClose = true;
                                }
                            }
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                DocumentNo = request.SaleOrder.RegistrationNo,
                                BranchId = request.SaleOrder.BranchId,
                            }, cancellationToken);

                         
                            await _context.SaveChangesAsync(cancellationToken);
                            scope.Complete();

                           
                            return new Message()
                            {
                                Id = purchase.Id,

                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
                finally
                {
                    Requests.IsDuplicateSale = false;
                }


            }

            private async Task<string> GetBaseImage(string filePath)
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
