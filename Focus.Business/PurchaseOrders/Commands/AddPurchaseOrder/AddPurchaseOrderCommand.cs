using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Focus.Domain.Enum;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Models;
using System.Text;
using Focus.Business.PurchasePosts.Commands.AddPurchasePost;
using Focus.Domain.Interface;

namespace Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder
{
    public class AddPurchaseOrderCommand : IRequest<Message>
    {

        public PurchaseOrderLookupModel PurchaseOrder { get; set; }
        public class Handler : IRequestHandler<AddPurchaseOrderCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly ISendEmail sendEmail;
            public Handler(IApplicationDbContext context, ILogger<AddPurchaseOrderCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider, ISendEmail _sendEmail)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                sendEmail = _sendEmail;
            }

            public async Task<Message> Handle(AddPurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.PurchaseOrder.Date.Year && x.PeriodStart.Month == request.PurchaseOrder.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.PurchaseOrder.Id == Guid.Empty)
                    {
                        if (request.PurchaseOrder.DocumentType == "SupplierQuotation")
                        {
                            var purchaseOrder = new PurchaseOrder()
                            {
                                NationalId = request.PurchaseOrder.NationalId,
                                BillingId = request.PurchaseOrder.BillingId,
                                ShippingId = request.PurchaseOrder.ShippingId,
                                DeliveryId = request.PurchaseOrder.DeliveryId,
                                Date = request.PurchaseOrder.Date,
                                RegistrationNo = request.PurchaseOrder.RegistrationNo,
                                SupplierId = request.PurchaseOrder.SupplierId,
                                InvoiceNo = request.PurchaseOrder.InvoiceNo,
                                InvoiceDate = request.PurchaseOrder.InvoiceDate,
                                Note = request.PurchaseOrder.Note,
                                ApprovalStatus = request.PurchaseOrder.ApprovalStatus,
                                TaxMethod = request.PurchaseOrder.TaxMethod,
                                TaxRateId = request.PurchaseOrder.TaxRateId,
                                IsClose = request.PurchaseOrder.IsClose,
                                Raw = request.PurchaseOrder.IsRaw,
                                TransactionLevelDiscount = request.PurchaseOrder.TransactionLevelDiscount,
                                IsDiscountOnTransaction = request.PurchaseOrder.IsDiscountOnTransaction,
                                IsFixed = request.PurchaseOrder.IsFixed,
                                IsBeforeTax = request.PurchaseOrder.IsBeforeTax,
                                Discount = request.PurchaseOrder.Discount,
                                DiscountAmount = request.PurchaseOrder.DiscountAmount,
                                TotalAfterDiscount = request.PurchaseOrder.TotalAfterDiscount,
                                TotalAmount = request.PurchaseOrder.TotalAmount,
                                VatAmount = request.PurchaseOrder.VatAmount,
                                TotalWithOutAmount = request.PurchaseOrder.GrossAmount,
                                DocumentType = request.PurchaseOrder.DocumentType,
                                SupplierQuotationId = request.PurchaseOrder.SupplierQuotationId,
                                BranchId = request.PurchaseOrder.BranchId,
                                Reference = request.PurchaseOrder.Reference,
                                PurchaseOrderItems = request.PurchaseOrder.PurchaseOrderItems.Select(x =>
                                    new PurchaseOrderItem()
                                    {
                                        UnitName = x.UnitName,
                                        ProductId = x.ProductId,
                                        TaxRateId = x.TaxRateId,
                                        TaxMethod = x.TaxMethod,
                                        Discount = x.Discount,
                                        Description = x.Description,
                                        IsService = x.IsService,
                                        FixDiscount = Math.Round(x.FixDiscount, 2),
                                        Quantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                        RemainingQuantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                        SerialNo = x.SerialNo,
                                        GuaranteeDate = x.GuaranteeDate,
                                        ExpiryDate = x.ExpiryDate,
                                        BatchNo = x.BatchNo,
                                        HighQty = x.HighQty,
                                        UnitPerPack = x.UnitPerPack,
                                        UnitPrice = Math.Round(x.UnitPrice, 2),
                                        WarrantyTypeId = x.WarrantyTypeId,
                                        DiscountAmount = x.DiscountAmount,
                                        TotalAmount = x.TotalAmount,
                                        VatAmount = x.VatAmount,
                                        TotalWithOutAmount = x.GrossAmount,
                                    }).ToList(),
                            };
                            await _context.PurchaseOrders.AddAsync(purchaseOrder, cancellationToken);

                            if (request.PurchaseOrder.AttachmentList != null && request.PurchaseOrder.AttachmentList.Count > 0)
                            {
                                foreach (var item in request.PurchaseOrder.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = purchaseOrder.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);
                                }
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchaseOrder.BranchId,
                                DocumentNo=purchaseOrder.RegistrationNo
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);
                            // Return Product id after successfully updating data


                            

                            var message = new Message
                            {
                                Id = purchaseOrder.Id,
                                IsAddUpdate = "Data has been added successfully"
                            };
                            return message;


                        }
                        else
                        {
                            var purchaseOrder = new PurchaseOrder()
                            {
                                NationalId = request.PurchaseOrder.NationalId,
                                BillingId = request.PurchaseOrder.BillingId,
                                ShippingId = request.PurchaseOrder.ShippingId,
                                DeliveryId = request.PurchaseOrder.DeliveryId,
                                Date = request.PurchaseOrder.Date,
                                RegistrationNo = request.PurchaseOrder.RegistrationNo,
                                SupplierId = request.PurchaseOrder.SupplierId,
                                InvoiceNo = request.PurchaseOrder.InvoiceNo,
                                InvoiceDate = request.PurchaseOrder.InvoiceDate,
                                Note = request.PurchaseOrder.Note,
                                ApprovalStatus = request.PurchaseOrder.ApprovalStatus,
                                TaxMethod = request.PurchaseOrder.TaxMethod,
                                TaxRateId = request.PurchaseOrder.TaxRateId,
                                IsClose = request.PurchaseOrder.IsClose,
                                Raw = request.PurchaseOrder.IsRaw,
                                TransactionLevelDiscount = request.PurchaseOrder.TransactionLevelDiscount,
                                IsDiscountOnTransaction = request.PurchaseOrder.IsDiscountOnTransaction,
                                IsFixed = request.PurchaseOrder.IsFixed,
                                IsBeforeTax = request.PurchaseOrder.IsBeforeTax,
                                Discount = request.PurchaseOrder.Discount,
                                DiscountAmount = request.PurchaseOrder.DiscountAmount,
                                TotalAfterDiscount = request.PurchaseOrder.TotalAfterDiscount,
                                TotalAmount = request.PurchaseOrder.TotalAmount,
                                VatAmount = request.PurchaseOrder.VatAmount,
                                TotalWithOutAmount = request.PurchaseOrder.GrossAmount,
                                DocumentType = request.PurchaseOrder.DocumentType,
                                SupplierQuotationId=request.PurchaseOrder.SupplierQuotationId,
                                BranchId= request.PurchaseOrder.BranchId,
                                Reference = request.PurchaseOrder.Reference,

                            };
                            await _context.PurchaseOrders.AddAsync(purchaseOrder, cancellationToken);

                            if (request.PurchaseOrder.AttachmentList != null && request.PurchaseOrder.AttachmentList.Count > 0)
                            {
                                foreach (var item in request.PurchaseOrder.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = purchaseOrder.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);
                                }
                            }

                            var purchaseOrderVersion = new PurchaseOrderVersion()
                            {
                                Version = "V.00",
                                PurchaseOrderId = purchaseOrder.Id,
                                Code = request.PurchaseOrder.RegistrationNo,
                                Date = DateTime.UtcNow
                            };
                            await _context.PurchaseOrderVersions.AddAsync(purchaseOrderVersion, cancellationToken);

                            var itemList = new List<PurchaseOrderItem>();
                            foreach (var item in request.PurchaseOrder.PurchaseOrderItems)
                            {
                                if (item.Serial && Convert.ToInt32(string.IsNullOrEmpty(item.SerialNo) ? "0" : item.SerialNo) > 0)
                                {
                                    for (int i = 0; i < item.Quantity; i++)
                                    {
                                        string serialNo = item.SerialNo;
                                        string lastFragment = serialNo.Split('-').Last();
                                        string firstFragment = serialNo.Substring(0, serialNo.Length - lastFragment.Length);
                                        Int32 autoNo = Convert.ToInt32(lastFragment) + i;
                                        var format = "";

                                        for (int j = 0; j < lastFragment.Length; j++)
                                        {
                                            format += "0";
                                        }

                                        var prefix = firstFragment;
                                        var newCode = prefix + autoNo.ToString(format);

                                        itemList.Add(new PurchaseOrderItem
                                        {
                                            ProductId = item.ProductId,
                                            TaxRateId = item.TaxRateId,
                                            TaxMethod = item.TaxMethod,
                                            Discount = item.Discount,
                                            Description = item.Description,
                                            IsService = item.IsService,
                                            FixDiscount = Math.Round(item.FixDiscount, 2),
                                            Quantity = 1,
                                            RemainingQuantity = 1,
                                            SerialNo = Convert.ToString(newCode),
                                            ExpiryDate = item.ExpiryDate,
                                            BatchNo = item.BatchNo,
                                            HighQty = item.HighQty,
                                            GuaranteeDate = item.GuaranteeDate,
                                            UnitPerPack = item.UnitPerPack,
                                            UnitPrice = Math.Round(item.UnitPrice, 2),
                                            WarrantyTypeId = item.WarrantyTypeId,
                                            PurchaseOrderId = purchaseOrder.Id,
                                            PurchaseOrderVersionId = purchaseOrderVersion.Id,

                                            DiscountAmount = item.DiscountAmount,
                                            TotalAmount = item.TotalAmount,
                                            VatAmount = item.VatAmount,
                                            TotalWithOutAmount = item.GrossAmount,
                                        });
                                    }
                                }
                                else
                                {
                                    itemList.Add(new PurchaseOrderItem
                                    {
                                        ProductId = item.ProductId,
                                        TaxRateId = item.TaxRateId,
                                        TaxMethod = item.TaxMethod,
                                        Discount = item.Discount,
                                        Description = item.Description,
                                        IsService = item.IsService,
                                        FixDiscount = Math.Round(item.FixDiscount, 2),
                                        Quantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                        RemainingQuantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                        SerialNo = item.SerialNo,
                                        GuaranteeDate = item.GuaranteeDate,
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        HighQty = item.HighQty,
                                        UnitPerPack = item.UnitPerPack,
                                        UnitPrice = Math.Round(item.UnitPrice, 2),
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        PurchaseOrderId = purchaseOrder.Id,
                                        PurchaseOrderVersionId = purchaseOrderVersion.Id,
                                        DiscountAmount = item.DiscountAmount,
                                        TotalAmount = item.TotalAmount,
                                        VatAmount = item.VatAmount,
                                        TotalWithOutAmount = item.GrossAmount,
                                    });
                                }

                            }
                            await _context.PurchaseOrderItems.AddRangeAsync(itemList, cancellationToken);

                            var actionData = await _context.CompanyProcess.AsNoTracking().FirstOrDefaultAsync(x =>
                                (x.ProcessName == "Add" || x.ProcessName == "يضيف") && x.Type == "Purchase", cancellationToken: cancellationToken);
                            if (actionData != null)
                            {
                                var action = new ActionLookUpModel()
                                {
                                    PurchaseOrderId = purchaseOrder.Id,
                                    ProcessId = actionData.Id,
                                    Date = DateTime.UtcNow,
                                    Description = "Add Purchase Order"
                                };
                                await _mediator.Send(new AddUpdateActionCommand
                                {
                                    Action = action,
                                    Code = _code,
                                    DocumentNo = purchaseOrder.RegistrationNo


                                }, cancellationToken);
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchaseOrder.BranchId,
                                DocumentNo=purchaseOrder.RegistrationNo
                            }, cancellationToken);
                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            if (request.PurchaseOrder.InternationalPurchase)
                            {
                                var costCenters = _context.CostCenters
                                    .AsNoTracking()
                                    .Include(x => x.Accounts)
                                    .AsQueryable();

                                var costCenterLegalExpense = await costCenters.Include(x => x.Accounts).FirstOrDefaultAsync(x =>
                                  x.Code == "605001", cancellationToken: cancellationToken);

                                var costCenter = await costCenters.FirstOrDefaultAsync(x =>
                                    x.Code == "160000", cancellationToken: cancellationToken);

                                var costCenterExpense = await costCenters.FirstOrDefaultAsync(x =>
                                    x.Code == "609000", cancellationToken: cancellationToken);

                                //VAT Paid On Expense Account
                                var vatPaidOnExpense = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x =>
                                    x.Code == "1300002", cancellationToken: cancellationToken);

                                if (vatPaidOnExpense == null)
                                {
                                    var vat = await costCenters.FirstOrDefaultAsync(x =>
                                        x.Code == "130000", cancellationToken: cancellationToken);
                                    var newAccount = new Account
                                    {
                                        Code = "1300002",
                                        Name = "VAT Paid On Expense",
                                        NameArabic = "دفع ضريبة القيمة المضافة على المصاريف",
                                        Description = "VAT Paid On Expense",
                                        IsActive = true,
                                        CostCenterId = vat.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                }

                                if (costCenterLegalExpense != null)
                                {
                                    var custom = costCenterLegalExpense.Accounts.FirstOrDefault(x => x.Name == "Custom Duty");
                                    if (custom == null)
                                    {
                                        var code = costCenterLegalExpense.Accounts.OrderBy(x => x.Code).LastOrDefault()?.Code;
                                        var newAccount = new Account
                                        {
                                            Code = (Convert.ToInt64(code) + 1).ToString(),
                                            Name = "Custom Duty",
                                            NameArabic = "الرسوم الجمركية",
                                            Description = "Custom Duty",
                                            IsActive = true,
                                            CostCenterId = costCenterLegalExpense.Id
                                        };
                                        await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                    }
                                }

                                if (costCenterExpense == null)
                                {
                                    var accountType = await _context.AccountTypes.AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Name == "Expenses" || x.NameArabic == "المصاريف", cancellationToken: cancellationToken);
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "609000",
                                        Name = "Advance Expense",
                                        NameArabic = "مصروفات مسبقة",
                                        Description = "Advance Expense",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                                    var newAccount = new Account
                                    {
                                        Code = "60900001",
                                        Name = "Advance Expense",
                                        NameArabic = "مصروفات مسبقة",
                                        Description = "Advance Expense",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                }
                                else
                                {
                                    var accounts = await _context.Accounts.AsNoTracking()
                                        .Where(x => x.CostCenterId == costCenterExpense.Id)
                                        .OrderBy(x => x.Code)
                                        .ToListAsync(cancellationToken: cancellationToken);

                                    var advanceExpense = accounts.FirstOrDefault(x => x.Name == "Advance Expense");

                                    if (advanceExpense == null)
                                    {
                                        var newAdvanceExpense = accounts.LastOrDefault()?.Code;
                                        var code = Convert.ToInt64(newAdvanceExpense);
                                        var newCode = code + 1;
                                        var format = Convert.ToString(newCode);
                                        var newAccount = new Account
                                        {
                                            Code = format,
                                            Name = "Advance Expense",
                                            NameArabic = "مصروفات مسبقة",
                                            Description = "Advance Expense",
                                            IsActive = true,
                                            CostCenterId = costCenterExpense.Id
                                        };
                                        await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                    }
                                }
                                if (costCenter == null)
                                {
                                    var accountType = await _context.AccountTypes.AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Name == "Assets" || x.NameArabic == "الاساس", cancellationToken: cancellationToken);
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "160000",
                                        Name = "Supplier Advances",
                                        NameArabic = "سلف الموردين",
                                        Description = "Supplier Advances",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);
                                    var supplier = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.PurchaseOrder.SupplierId, cancellationToken: cancellationToken);
                                    if (supplier == null)
                                        throw new NotFoundException("Supplier", "Cannot be found.");

                                    var newAccount = new Account
                                    {
                                        Code = "16000001",
                                        Name = supplier.EnglishName + " Advance",
                                        NameArabic = supplier.ArabicName + "يتقدم ",
                                        Description = "Supplier Advances",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                    supplier.AdvanceAccountId = newAccount.Id;
                                    _context.Contacts.Update(supplier);
                                }
                                else
                                {
                                    var supplier = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.PurchaseOrder.SupplierId, cancellationToken: cancellationToken);
                                    if (supplier == null)
                                        throw new NotFoundException("Supplier", "Cannot be found.");
                                    if (supplier.AdvanceAccountId == null)
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
                                            Name = supplier.EnglishName == "" ? supplier.ArabicName : supplier.EnglishName + " Advance",
                                            NameArabic = supplier.ArabicName + " يتقدم ",
                                            Description = "Supplier Advances",
                                            IsActive = true,
                                            CostCenterId = costCenter.Id
                                        };
                                        await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                        supplier.AdvanceAccountId = newAccount.Id;
                                        _context.Contacts.Update(supplier);
                                    }
                                }
                            }

                            {
                                if (request.PurchaseOrder.SupplierQuotationId != Guid.Empty && request.PurchaseOrder.SupplierQuotationId != null)
                                {
                                   


                                    var purchaseOrder12 = await _context.PurchaseOrders.FindAsync(request.PurchaseOrder.SupplierQuotationId);

                                    if (purchaseOrder12 != null)
                                    {
                                        foreach (var item in request.PurchaseOrder.PurchaseOrderItems)
                                        {
                                            if (item.ProductId == null)
                                            {
                                                var poDetail = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                                if (poDetail != null)
                                                {
                                                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchaseOrder.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                                   
                                                    _context.PurchaseOrderItems.Update(poDetail);

                                                }
                                            }
                                            else
                                            {
                                                var poDetail = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                                if (poDetail != null)
                                                {
                                                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchaseOrder.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                                    _context.PurchaseOrderItems.Update(poDetail);

                                                }
                                            }
                                        }

                                        var close = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x=>x.RemainingQuantity > 0);
                                        if (close == null)
                                        {
                                            purchaseOrder12.IsProcessed = true;

                                            if (String.IsNullOrEmpty(purchaseOrder12.Reason))
                                            {
                                                purchaseOrder12.Reason = purchaseOrder.RegistrationNo + " " + purchaseOrder.Date.ToString("dd/MM/yyyy");

                                            }
                                            else

                                            {
                                                purchaseOrder12.Reason = purchaseOrder12.Reason  + "-------" + purchaseOrder.RegistrationNo + " " + purchaseOrder.Date.ToString("dd/MM/yyyy");

                                            }



                                            purchaseOrder12.IsClose = true;
                                            purchaseOrder12.PartiallyReceived = PartiallyInvoices.Fully;
                                        }
                                        else

                                        {



                                            purchaseOrder12.IsProcessed = true;

                                            if (String.IsNullOrEmpty(purchaseOrder12.Reason))
                                            {
                                                purchaseOrder12.Reason = purchaseOrder.RegistrationNo + " " + purchaseOrder12.Date.ToString("dd/MM/yyyy");

                                            }
                                            else

                                            {
                                                purchaseOrder12.Reason = purchaseOrder12.Reason + " ------- " + purchaseOrder.RegistrationNo + " " + purchaseOrder12.Date.ToString("dd/MM/yyyy");

                                            }

                                            purchaseOrder12.PartiallyReceived = PartiallyInvoices.Partially;

                                        }

                                    }






                                }
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchaseOrder.BranchId,
                                DocumentNo=purchaseOrder.RegistrationNo
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);
                            // Return Product id after successfully updating data
                            var message = new Message
                            {
                                Id = purchaseOrder.Id,
                                IsAddUpdate = "Data has been added successfully"
                            };
                            return message;

                        }                    
                    }
                    else
                    {

                        var purchase = await _context.PurchaseOrders.FindAsync(request.PurchaseOrder.Id);
                        if (purchase == null)
                            throw new NotFoundException("purchase Order Not Found", "");

                        if (request.PurchaseOrder.DocumentType == "SupplierQuotation")
                        {
                          



                            purchase.NationalId = request.PurchaseOrder.NationalId;
                            purchase.BillingId = request.PurchaseOrder.BillingId;
                            purchase.ShippingId = request.PurchaseOrder.ShippingId;
                            purchase.DeliveryId = request.PurchaseOrder.DeliveryId;
                            purchase.Date = request.PurchaseOrder.Date;
                            purchase.InvoiceDate = request.PurchaseOrder.InvoiceDate;
                            purchase.InvoiceNo = request.PurchaseOrder.InvoiceNo;
                            purchase.RegistrationNo = request.PurchaseOrder.RegistrationNo;
                            purchase.SupplierId = request.PurchaseOrder.SupplierId;
                            purchase.Note = request.PurchaseOrder.Note;
                            purchase.ApprovalStatus = request.PurchaseOrder.ApprovalStatus;
                            purchase.IsClose = request.PurchaseOrder.IsClose;
                            purchase.TaxMethod = request.PurchaseOrder.TaxMethod;
                            purchase.TaxRateId = request.PurchaseOrder.TaxRateId;
                            purchase.Raw = request.PurchaseOrder.IsRaw;
                            purchase.TransactionLevelDiscount = request.PurchaseOrder.TransactionLevelDiscount;
                            purchase.IsDiscountOnTransaction = request.PurchaseOrder.IsDiscountOnTransaction;
                            purchase.IsFixed = request.PurchaseOrder.IsFixed;
                            purchase.IsBeforeTax = request.PurchaseOrder.IsBeforeTax;
                            purchase.Discount = request.PurchaseOrder.Discount;
                            purchase.DiscountAmount = request.PurchaseOrder.DiscountAmount;
                            purchase.TotalAfterDiscount = request.PurchaseOrder.TotalAfterDiscount;
                            purchase.TotalAmount = request.PurchaseOrder.TotalAmount;
                            purchase.VatAmount = request.PurchaseOrder.VatAmount;
                            purchase.TotalWithOutAmount = request.PurchaseOrder.GrossAmount;
                            purchase.DocumentType = request.PurchaseOrder.DocumentType;
                            purchase.SupplierQuotationId = request.PurchaseOrder?.SupplierQuotationId;
                            purchase.Reference = request.PurchaseOrder.Reference;

                            _context.PurchaseOrderItems.RemoveRange(purchase.PurchaseOrderItems);

                            purchase.PurchaseOrderItems = request.PurchaseOrder.PurchaseOrderItems.Select(x =>
                                new PurchaseOrderItem()
                                {
                                    UnitName = x.UnitName,
                                    ProductId = x.ProductId,
                                    TaxRateId = x.TaxRateId,
                                    TaxMethod = x.TaxMethod,
                                    Discount = x.Discount,
                                    FixDiscount = x.FixDiscount,
                                    Description = x.Description,
                                    IsService = x.IsService,
                                    Quantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                    RemainingQuantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                    ReceiveQuantity = x.ReceiveQuantity,
                                    UnitPrice = x.UnitPrice,
                                    ExpiryDate = x.ExpiryDate,
                                    BatchNo = x.BatchNo,
                                    UnitPerPack = x.UnitPerPack,
                                    HighQty = x.HighQty,
                                    SerialNo = x.SerialNo,
                                    GuaranteeDate = x.GuaranteeDate,
                                    WarrantyTypeId = x.WarrantyTypeId,
                                    PurchaseOrderVersionId = purchase.PurchaseOrderVersions?.LastOrDefault()?.Id,
                                    DiscountAmount = x.DiscountAmount,
                                    TotalAmount = x.TotalAmount,
                                    VatAmount = x.VatAmount,
                                    TotalWithOutAmount = x.GrossAmount,
                                }).ToList();

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchase.BranchId,
                                DocumentNo=purchase.RegistrationNo
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            var message = new Message
                            {
                                Id = purchase.Id,
                                IsAddUpdate = "Data has been Updated successfully"
                            };
                            return message;
                        }

                       


                        if (request.PurchaseOrder.AttachmentList != null)
                        {
                            var attachments = _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == purchase.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }
                            foreach (var item in request.PurchaseOrder.AttachmentList)
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

                        if (request.PurchaseOrder.IsClose)
                        {
                            purchase.IsClose = true;

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchase.BranchId,
                                DocumentNo=purchase.RegistrationNo
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            var message = new Message
                            {
                                Id = purchase.Id,
                                IsAddUpdate = "Purchase Order Closed successfully"
                            };
                            return message;
                        }

                        if (purchase.ApprovalStatus == ApprovalStatus.InProcess && request.PurchaseOrder.ApprovalStatus == ApprovalStatus.InProcess)
                        {
                            purchase.Date = request.PurchaseOrder.Date;
                            purchase.InvoiceDate = request.PurchaseOrder.InvoiceDate;

                            // Generate version No
                            string name;
                            var version = purchase.PurchaseOrderVersions.MaxBy(x => x.Version);
                            if (version == null)
                            {
                                name = "V.01";
                            }
                            else
                            {
                                string fetchNo = version.Version.Substring(2);
                                Int32 autoNo = Convert.ToInt32((fetchNo));
                                var format = "00";
                                autoNo++;
                                var newCode = "V." + autoNo.ToString(format);
                                name = newCode;
                            }

                            //Create Version
                            var purchaseOrderVersion = new PurchaseOrderVersion()
                            {
                                Version = name,
                                PurchaseOrderId = purchase.Id,
                                Code = purchase.RegistrationNo,
                                Date = DateTime.UtcNow
                            };
                            await _context.PurchaseOrderVersions.AddAsync(purchaseOrderVersion, cancellationToken);

                            //Add Line Item
                            var itemList = new List<PurchaseOrderItem>();
                            foreach (var item in request.PurchaseOrder.PurchaseOrderItems)
                            {
                                itemList.Add(new PurchaseOrderItem
                                {
                                    ProductId = item.ProductId,
                                    TaxRateId = item.TaxRateId,
                                    TaxMethod = item.TaxMethod,
                                    Discount = item.Discount,
                                    Description = item.Description,
                                    IsService = item.IsService,
                                    FixDiscount = Math.Round(item.FixDiscount, 2),
                                    Quantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                    RemainingQuantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                    SerialNo = item.SerialNo,
                                    GuaranteeDate = item.GuaranteeDate,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    UnitPrice = Math.Round(item.UnitPrice, 2),
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    PurchaseOrderId = purchase.Id,
                                    PurchaseOrderVersionId = purchaseOrderVersion.Id,
                                    DiscountAmount = item.DiscountAmount,
                                    TotalAmount = item.TotalAmount,
                                    VatAmount = item.VatAmount,
                                    TotalWithOutAmount = item.GrossAmount,
                                });
                            }
                            await _context.PurchaseOrderItems.AddRangeAsync(itemList, cancellationToken);

                            //Add Action
                            var actionData = await _context.CompanyProcess.AsNoTracking().FirstOrDefaultAsync(x =>
                                (x.ProcessName == "Update" || x.ProcessName == "تحديث") && x.Type == "Purchase", cancellationToken: cancellationToken);
                            if (actionData != null)
                            {
                                var action = new ActionLookUpModel()
                                {
                                    PurchaseOrderId = purchase.Id,
                                    ProcessId = actionData.Id,
                                    Date = DateTime.UtcNow,
                                    Description = "Update Purchase Order Version"
                                };
                                await _mediator.Send(new AddUpdateActionCommand
                                {
                                    Action = action,
                                    Code = _code,
                                    DocumentNo = request.PurchaseOrder.RegistrationNo
                                }, cancellationToken);
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchase.BranchId,
                                DocumentNo=purchase.RegistrationNo
                            }, cancellationToken);

                            {
                                //if (request.PurchaseOrder.SupplierQuotationId != Guid.Empty && request.PurchaseOrder.SupplierQuotationId != null)
                                //{



                                //    var purchaseOrder12 = await _context.PurchaseOrders.FindAsync(request.PurchaseOrder.SupplierQuotationId);

                                //    if (purchaseOrder12 != null)
                                //    {
                                //        foreach (var item in request.PurchaseOrder.PurchaseOrderItems)
                                //        {
                                //            if (item.ProductId == null)
                                //            {
                                //                var poDetail = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                //                if (poDetail != null)
                                //                {
                                //                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchaseOrder.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);

                                //                    _context.PurchaseOrderItems.Update(poDetail);

                                //                }
                                //            }
                                //            else
                                //            {
                                //                var poDetail = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                //                if (poDetail != null)
                                //                {
                                //                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchaseOrder.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                //                    _context.PurchaseOrderItems.Update(poDetail);

                                //                }
                                //            }
                                //        }

                                //        var close = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.RemainingQuantity > 0);
                                //        if (close == null)
                                //        {
                                //            purchaseOrder12.IsProcessed = true;

                                //            if (String.IsNullOrEmpty(purchaseOrder12.Reason))
                                //            {
                                //                purchaseOrder12.Reason = request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }
                                //            else

                                //            {
                                //                purchaseOrder12.Reason = purchaseOrder12.Reason + "   \n    " + request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }



                                //            purchaseOrder12.IsClose = true;
                                //            purchaseOrder12.PartiallyReceived = PartiallyInvoices.Fully;
                                //        }
                                //        else

                                //        {



                                //            purchaseOrder12.IsProcessed = true;

                                //            if (String.IsNullOrEmpty(purchaseOrder12.Reason))
                                //            {
                                //                purchaseOrder12.Reason = request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }
                                //            else

                                //            {
                                //                purchaseOrder12.Reason = purchaseOrder12.Reason + "   \n    " + request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }

                                //            purchaseOrder12.PartiallyReceived = PartiallyInvoices.Partially;

                                //        }

                                //    }






                                //}

                            }

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            // Return Product id after successfully Adding data
                            var message = new Message
                            {
                                Id = purchase.Id,
                                IsAddUpdate = "Purchase Order new version has been added successfully"
                            };
                            return message;
                        }
                        else
                        {
                            purchase.NationalId = request.PurchaseOrder.NationalId;
                            purchase.BillingId = request.PurchaseOrder.BillingId;
                            purchase.ShippingId = request.PurchaseOrder.ShippingId;
                            purchase.DeliveryId = request.PurchaseOrder.DeliveryId;
                            purchase.Date = request.PurchaseOrder.Date;
                            purchase.InvoiceDate = request.PurchaseOrder.InvoiceDate;
                            purchase.InvoiceNo = request.PurchaseOrder.InvoiceNo;
                            purchase.RegistrationNo = request.PurchaseOrder.RegistrationNo;
                            purchase.SupplierId = request.PurchaseOrder.SupplierId;
                            purchase.Note = request.PurchaseOrder.Note;
                            purchase.ApprovalStatus = request.PurchaseOrder.ApprovalStatus;
                            purchase.IsClose = request.PurchaseOrder.IsClose;
                            purchase.TaxMethod = request.PurchaseOrder.TaxMethod;
                            purchase.TaxRateId = request.PurchaseOrder.TaxRateId;
                            purchase.Raw = request.PurchaseOrder.IsRaw;
                            purchase.TransactionLevelDiscount = request.PurchaseOrder.TransactionLevelDiscount;
                            purchase.IsDiscountOnTransaction = request.PurchaseOrder.IsDiscountOnTransaction;
                            purchase.IsFixed = request.PurchaseOrder.IsFixed;
                            purchase.IsBeforeTax = request.PurchaseOrder.IsBeforeTax;
                            purchase.Discount = request.PurchaseOrder.Discount;
                            purchase.DiscountAmount = request.PurchaseOrder.DiscountAmount;
                            purchase.TotalAmount = request.PurchaseOrder.TotalAmount;
                            purchase.VatAmount = request.PurchaseOrder.VatAmount;
                            purchase.TotalWithOutAmount = request.PurchaseOrder.GrossAmount;
                            purchase.DocumentType = request.PurchaseOrder.DocumentType;
                            purchase.SupplierQuotationId = request.PurchaseOrder.SupplierQuotationId;
                            purchase.Reference = request.PurchaseOrder.Reference;

                            _context.PurchaseOrderItems.RemoveRange(purchase.PurchaseOrderItems);

                            purchase.PurchaseOrderItems = request.PurchaseOrder.PurchaseOrderItems.Select(x =>
                                new PurchaseOrderItem()
                                {
                                    UnitName = x.UnitName,
                                    ProductId = x.ProductId,
                                    TaxRateId = x.TaxRateId,
                                    TaxMethod = x.TaxMethod,
                                    Discount = x.Discount,
                                    FixDiscount = x.FixDiscount,
                                    Description = x.Description,
                                    IsService = x.IsService,
                                    Quantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                    RemainingQuantity = request.PurchaseOrder.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                    ReceiveQuantity = x.ReceiveQuantity,
                                    UnitPrice = x.UnitPrice,
                                    ExpiryDate = x.ExpiryDate,
                                    BatchNo = x.BatchNo,
                                    UnitPerPack = x.UnitPerPack,
                                    HighQty = x.HighQty,
                                    SerialNo = x.SerialNo,
                                    GuaranteeDate = x.GuaranteeDate,
                                    WarrantyTypeId = x.WarrantyTypeId,
                                    PurchaseOrderVersionId = purchase.PurchaseOrderVersions?.LastOrDefault()?.Id,
                                    DiscountAmount = x.DiscountAmount,
                                    TotalAmount = x.TotalAmount,
                                    VatAmount = x.VatAmount,
                                    TotalWithOutAmount = x.GrossAmount,
                                }).ToList();

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = purchase.BranchId,
                                DocumentNo=purchase.RegistrationNo
                            }, cancellationToken);

                            {
                                //if (request.PurchaseOrder.SupplierQuotationId != Guid.Empty && request.PurchaseOrder.SupplierQuotationId != null)
                                //{



                                //    var purchaseOrder12 = await _context.PurchaseOrders.FindAsync(request.PurchaseOrder.SupplierQuotationId);

                                //    if (purchaseOrder12 != null)
                                //    {
                                //        foreach (var item in request.PurchaseOrder.PurchaseOrderItems)
                                //        {
                                //            if (item.ProductId == null)
                                //            {
                                //                var poDetail = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                //                if (poDetail != null)
                                //                {
                                //                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchaseOrder.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);

                                //                    _context.PurchaseOrderItems.Update(poDetail);

                                //                }
                                //            }
                                //            else
                                //            {
                                //                var poDetail = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                //                if (poDetail != null)
                                //                {
                                //                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchaseOrder.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                //                    _context.PurchaseOrderItems.Update(poDetail);

                                //                }
                                //            }
                                //        }

                                //        var close = purchaseOrder12.PurchaseOrderItems.FirstOrDefault(x => x.RemainingQuantity > 0);
                                //        if (close == null)
                                //        {
                                //            purchaseOrder12.IsProcessed = true;

                                //            if (String.IsNullOrEmpty(purchaseOrder12.Reason))
                                //            {
                                //                purchaseOrder12.Reason = request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }
                                //            else

                                //            {
                                //                purchaseOrder12.Reason = purchaseOrder12.Reason + "   \n    " + request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }



                                //            purchaseOrder12.IsClose = true;
                                //            purchaseOrder12.PartiallyReceived = PartiallyInvoices.Fully;
                                //        }
                                //        else

                                //        {



                                //            purchaseOrder12.IsProcessed = true;

                                //            if (String.IsNullOrEmpty(purchaseOrder12.Reason))
                                //            {
                                //                purchaseOrder12.Reason = request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }
                                //            else

                                //            {
                                //                purchaseOrder12.Reason = purchaseOrder12.Reason + "   \n    " + request.PurchaseOrder.RegistrationNo + " " + request.PurchaseOrder.Date.ToString("dd/MM/yyyy");

                                //            }

                                //            purchaseOrder12.PartiallyReceived = PartiallyInvoices.Partially;

                                //        }

                                //    }






                                //}

                            }

                            await _context.SaveChangesAsync(cancellationToken);

                            var actionData = await _context.CompanyProcess.AsNoTracking().FirstOrDefaultAsync(x => (x.ProcessName == "Update" || x.ProcessName == "تحديث") && x.Type == "Purchase", cancellationToken: cancellationToken);
                            if (actionData != null)
                            {
                                var action = new ActionLookUpModel()
                                {
                                    PurchaseOrderId = request.PurchaseOrder.Id,
                                    ProcessId = actionData.Id,
                                    Date = DateTime.UtcNow,
                                    Description = "Update Purchase Order"
                                };
                                await _mediator.Send(new AddUpdateActionCommand
                                {
                                    Action = action,
                                    Code = _code,
                                    DocumentNo = request.PurchaseOrder.RegistrationNo
                                }, cancellationToken);
                            }

                            var message = new Message
                            {
                                Id = purchase.Id,
                                IsAddUpdate = "Data has been Updated successfully"
                            };
                            return message;
                        }
                    }
                  

                }
                catch (NotFoundException notFoundException)
                {
                    var message = new Message
                    {
                        IsAddUpdate = notFoundException.Message
                    };
                    _logger.LogError(notFoundException.Message);
                    return message;
                }
                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsAddUpdate = e.Message
                    };
                    _logger.LogError(e.Message);

                    return message;
                }
            }
        }
    }
}
