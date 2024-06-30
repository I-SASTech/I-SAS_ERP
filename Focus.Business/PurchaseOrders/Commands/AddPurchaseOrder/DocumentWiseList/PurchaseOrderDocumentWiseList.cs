using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PurchaseOrders.Commands.PurchaseOrderReason;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderDetails;
using System.Collections.Generic;
using Focus.Business.GoodReceives.Commands.AddGoodReceive;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;

namespace Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder.DocumentWiseList
{
    public class PurchaseOrderDocumentWiseList : IRequest<DocumentWiseLookUpModel>
    {
        public string FormName { get; set; }
        public Guid? DocumentId { get; set; }

        public class Handler : IRequestHandler<PurchaseOrderDocumentWiseList, DocumentWiseLookUpModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<PurchaseOrderDocumentWiseList> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<DocumentWiseLookUpModel> Handle(PurchaseOrderDocumentWiseList request, CancellationToken cancellationToken)
            {
                try
                {
                   

                    var lookUpModel = new DocumentWiseLookUpModel();
                    var purchaseOrderModel = new List<PurchaseOrderDetailLookUpModel>();





                    if (request.FormName== "SupplierQuotation")
                    {
                        var purchaseOrder12 =  _context.PurchaseOrders.AsNoTracking().Where(x=>x.SupplierQuotationId==request.DocumentId).ToList();


                        foreach (var x in purchaseOrder12)
                        {

                            var purchaseOrder = await _mediator.Send(new
                                GetPurchaseOrderDetailQuery(){ Id = x.Id}, cancellationToken);
                            purchaseOrderModel.Add(new PurchaseOrderDetailLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                TaxRatesName = purchaseOrder.TaxRatesName,
                                SupplierName = purchaseOrder.SupplierName,
                                Date = purchaseOrder.Date,
                                VatAmount = purchaseOrder.VatAmount,
                                TotalAmount = purchaseOrder.TotalAmount,
                                SupplierVAT = purchaseOrder.SupplierVAT,
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
                                PurchaseOrderItems = purchaseOrder.PurchaseOrderItems,

                                Discount = purchaseOrder.Discount,
                                TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                                GrossAmount = purchaseOrder.GrossAmount,
                                IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                                IsFixed = purchaseOrder.IsFixed,
                                IsBeforeTax = purchaseOrder.IsBeforeTax,
                                NetAmount = purchaseOrder.TotalAmount,
                                TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,
                                SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                                SupplierQuotationNo = purchaseOrder.SupplierQuotationNo,
                                Reference = "Purchase Order",

                            } );


                        }

                        var goodReceive = _context.GoodReceives.AsNoTracking().Where(x => x.SupplierQuotationId == request.DocumentId).ToList();



                        foreach (var x in goodReceive)
                        {

                            var purchaseOrder = await _mediator.Send(new
                                GetGoodReceiveDetailQuery()
                            { Id = x.Id }, cancellationToken);
                            purchaseOrderModel.Add(new PurchaseOrderDetailLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                TaxRatesName = purchaseOrder.TaxRatesName,
                                SupplierName = purchaseOrder.SupplierName,
                                Date = purchaseOrder.Date,
                                VatAmount = purchaseOrder.VatAmount,
                                TotalAmount = purchaseOrder.TotalAmount,
                                IsClose = purchaseOrder.IsClose,
                                DiscountAmount = purchaseOrder.DiscountAmount,
                                ApprovalStatus = purchaseOrder.ApprovalStatus,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                SupplierId = purchaseOrder.SupplierId,
                                InvoiceNo = purchaseOrder.InvoiceNo,
                                InvoiceDate = purchaseOrder.InvoiceDate,
                                TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,

                                Note = purchaseOrder.Note,
                                TaxMethod = purchaseOrder.TaxMethod,
                                TaxRateId = purchaseOrder.TaxRateId,
                                PurchaseOrderItems = purchaseOrder.GoodReceiveNoteItems.Select(item => new PurchaseOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Product = item.Product,
                                    Description = item.Description,
                                    ProductName = item.Product?.EnglishName,
                                    DisplayName = item.Product?.DisplayName,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    Quantity = item.Quantity,       
                                    RemainingQuantity = item.RemainingQuantity,
                                    ReceiveQuantity = item.ReceiveQuantity,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = purchaseOrder.TaxMethod,
                                    IsExpire = item.IsExpire,
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    SerialNo = item.SerialNo,
                                    Total = item.Total,

                                }).ToList(),

                                Discount = purchaseOrder.Discount,
                                TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                                GrossAmount = purchaseOrder.GrossAmount,
                                IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                                IsFixed = purchaseOrder.IsFixed,
                                IsBeforeTax = purchaseOrder.IsBeforeTax,
                                NetAmount = purchaseOrder.TotalAmount,
                                SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                                Reference = "Good Receive",

                            });


                        }



                    }    
                    else if (request.FormName== "PurchaseOrder")
                    {
                      

                        var goodReceive = _context.GoodReceives.AsNoTracking().Where(x => x.PurchaseOrderId == request.DocumentId).ToList();



                        foreach (var x in goodReceive)
                        {

                            var purchaseOrder = await _mediator.Send(new
                                GetGoodReceiveDetailQuery()
                            { Id = x.Id }, cancellationToken);
                            purchaseOrderModel.Add(new PurchaseOrderDetailLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                TaxRatesName = purchaseOrder.TaxRatesName,
                                SupplierName = purchaseOrder.SupplierName,
                                Date = purchaseOrder.Date,
                                VatAmount = purchaseOrder.VatAmount,
                                TotalAmount = purchaseOrder.TotalAmount,
                                IsClose = purchaseOrder.IsClose,
                                DiscountAmount = purchaseOrder.DiscountAmount,
                                ApprovalStatus = purchaseOrder.ApprovalStatus,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                SupplierId = purchaseOrder.SupplierId,
                                InvoiceNo = purchaseOrder.InvoiceNo,
                                InvoiceDate = purchaseOrder.InvoiceDate,
                                TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,
                                Note = purchaseOrder.Note,
                                TaxMethod = purchaseOrder.TaxMethod,
                                TaxRateId = purchaseOrder.TaxRateId,
                                PurchaseOrderItems = purchaseOrder.GoodReceiveNoteItems.Select(item => new PurchaseOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Product = item.Product,
                                    Description = item.Description,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    Quantity = item.Quantity,       
                                    RemainingQuantity = item.RemainingQuantity,
                                    ReceiveQuantity = item.ReceiveQuantity,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = purchaseOrder.TaxMethod,
                                    IsExpire = item.IsExpire,
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    SerialNo = item.SerialNo,
                                    Total = item.Total,
                                    ProductName = item.Product?.EnglishName,
                                    DisplayName = item.Product?.DisplayName,
                                }).ToList(),

                                Discount = purchaseOrder.Discount,
                                TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                                GrossAmount = purchaseOrder.GrossAmount,
                                IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                                IsFixed = purchaseOrder.IsFixed,
                                IsBeforeTax = purchaseOrder.IsBeforeTax,
                                NetAmount = purchaseOrder.TotalAmount,
                                SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                                Reference = "Good Receive",

                            });


                        }


                        var purchaseInvoice = _context.PurchasePosts.AsNoTracking().Where(x => x.PurchaseOrderId == request.DocumentId).ToList();



                        foreach (var x in purchaseInvoice)
                        {

                            var purchaseOrder = await _mediator.Send(new
                                PurchasePostDetailQuery()
                            { Id = x.Id }, cancellationToken);
                            purchaseOrderModel.Add(new PurchaseOrderDetailLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                TaxRatesName = purchaseOrder.TaxRatesName,
                                SupplierName = purchaseOrder.SupplierName,
                                Date = purchaseOrder.Date,
                                VatAmount = purchaseOrder.VatAmount,
                                TotalAmount = purchaseOrder.TotalAmount,
                                IsClose = purchaseOrder.IsClose,
                                DiscountAmount = purchaseOrder.DiscountAmount,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                SupplierId = purchaseOrder.SupplierId,
                                InvoiceNo = purchaseOrder.InvoiceNo,
                                InvoiceDate = purchaseOrder.InvoiceDate,
                                TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,
                                Note = purchaseOrder.Note,
                                TaxMethod = purchaseOrder.TaxMethod,
                                TaxRateId = purchaseOrder.TaxRateId,

                                PurchaseOrderItems = purchaseOrder.PurchasePostItems.Select(item => new PurchaseOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Product = item.Product,
                                    Description = item.Description,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    Quantity = item.Quantity,
                                    RemainingQuantity = item.RemainingQuantity,
                                    ReceiveQuantity = item.ReceiveQuantity,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = purchaseOrder.TaxMethod,
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    SerialNo = item.SerialNo,
                                    Total = item.Total,
                                    ProductName = item.Product?.EnglishName,
                                    DisplayName = item.Product?.DisplayName,
                                }).ToList(),

                                Discount = purchaseOrder.Discount,
                                TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                                GrossAmount = purchaseOrder.GrossAmount,
                                IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                                IsFixed = purchaseOrder.IsFixed,
                                IsBeforeTax = purchaseOrder.IsBeforeTax,
                                NetAmount = purchaseOrder.TotalAmount,
                                SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                                Reference = "Purchase Invoice",

                            });


                        }



                    }
                    else if (request.FormName == "GoodReceive")
                    {


                        var goodReceive = _context.PurchasePosts.AsNoTracking().Where(x => x.GoodReceiveNoteId == request.DocumentId).ToList();



                        foreach (var x in goodReceive)
                        {

                            var purchaseOrder = await _mediator.Send(new
                                PurchasePostDetailQuery()
                            { Id = x.Id }, cancellationToken);
                            purchaseOrderModel.Add(new PurchaseOrderDetailLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                TaxRatesName = purchaseOrder.TaxRatesName,
                                SupplierName = purchaseOrder.SupplierName,
                                Date = purchaseOrder.Date,
                                VatAmount = purchaseOrder.VatAmount,
                                TotalAmount = purchaseOrder.TotalAmount,
                                IsClose = purchaseOrder.IsClose,
                                DiscountAmount = purchaseOrder.DiscountAmount,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                SupplierId = purchaseOrder.SupplierId,
                                TotalAfterDiscount = purchaseOrder.TotalAfterDiscount,
                                InvoiceNo = purchaseOrder.InvoiceNo,
                                InvoiceDate = purchaseOrder.InvoiceDate,
                                Note = purchaseOrder.Note,
                                TaxMethod = purchaseOrder.TaxMethod,
                                TaxRateId = purchaseOrder.TaxRateId,
                                PurchaseOrderItems = purchaseOrder.PurchasePostItems.Select(item => new PurchaseOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Product = item.Product,
                                    Description = item.Description,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    Quantity = item.Quantity,
                                    RemainingQuantity = item.RemainingQuantity,
                                    ReceiveQuantity = item.ReceiveQuantity,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = purchaseOrder.TaxMethod,
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    SerialNo = item.SerialNo,
                                    Total = item.Total,
                                    ProductName = item.Product?.EnglishName,
                                    DisplayName = item.Product?.DisplayName,
                                }).ToList(),

                                Discount = purchaseOrder.Discount,
                                TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                                GrossAmount = purchaseOrder.GrossAmount,
                                IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                                IsFixed = purchaseOrder.IsFixed,
                                IsBeforeTax = purchaseOrder.IsBeforeTax,
                                NetAmount = purchaseOrder.TotalAmount,
                                SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                                Reference = "Purchase Invoice",

                            });


                        }



                    }


                    lookUpModel.Lookups = purchaseOrderModel;


                    //Save changes to database
                    await _context.SaveChangesAsync(cancellationToken);

                  
                    return lookUpModel;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);


                }
            }
        }
    }
}
