using AutoMapper;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Sales.DocumentHistory
{
    public class DocumentHistory : IRequest<List<DocumentHistoryModel>>
    {
        public string DocumentName { get; set; }
        public string CurrentDocument { get; set; }
        public Guid Id { get; set; }
        public Guid CurrentDocumentId { get; set; }

        public class Handler : IRequestHandler<DocumentHistory, List<DocumentHistoryModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<DocumentHistory> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<DocumentHistoryModel>> Handle(DocumentHistory request, CancellationToken cancellationToken)
            {
                try
                {
                    request.CurrentDocumentId = request.Id;

                    var historyList = new List<DocumentHistoryModel>();

                   

                    {
                        
                        repeat:
                        switch (request.CurrentDocument)
                        {
                            case "Sales":
                                if (request.CurrentDocumentId != Guid.Empty)
                                {

                                    {
                                        var sales = _context.Sales
                                            .Where(x => x.Id == request.CurrentDocumentId)
                                            .Select(x => new
                                            {
                                                Id = x.Id,
                                                RegistrationNo = x.RegistrationNo,
                                                Date = x.Date,
                                                QuotationId = x.QuotationId,
                                                SaleOrderId = x.SaleOrderId,
                                                DeliveryChallanId = x.DeliveryChallanId,
                                                ProformaId = x.ProformaId,
                                                PurchaseOrderId = x.PurchaseOrderId,
                                            })
                                            .FirstOrDefault();
                                        if (sales != null)
                                        {
                                            AddToHistory(sales.RegistrationNo, sales.Date, "Sale");
                                            if (sales.SaleOrderId != null)
                                            {
                                                request.CurrentDocumentId = sales.SaleOrderId.Value;
                                                request.CurrentDocument = "SaleOrder";
                                                goto repeat;

                                            }
                                            else if (sales.QuotationId != null)
                                            {
                                                request.CurrentDocumentId = sales.QuotationId.Value;
                                                request.CurrentDocument = "Quotation";
                                                goto repeat;

                                            }
                                            else if (sales.ProformaId != null)
                                            {
                                                request.CurrentDocumentId = sales.ProformaId.Value;
                                                request.CurrentDocument = "ProformaInvoice";
                                                goto repeat;

                                            }
                                            else if (sales.DeliveryChallanId != null)
                                            {
                                                request.CurrentDocumentId = sales.DeliveryChallanId.Value;
                                                request.CurrentDocument = "DeliveryChallan";
                                                goto repeat;

                                            }
                                            else
                                            {
                                                request.CurrentDocumentId = Guid.Empty;
                                                request.CurrentDocument = "";
                                                goto repeat;

                                            }


                                        }
                                    }



                                }
                                break;

                            case "DeliveryChallan":
                                if ( request.CurrentDocumentId!=Guid.Empty)
                                {
                                  
                                    {
                                        var deliveryChallan = _context.DeliveryChallans
                                            .Where(x => x.Id == request.CurrentDocumentId)
                                            .Select(x => new
                                            {
                                                Id = x.Id,
                                                RegistrationNo = x.RegistrationNo,
                                                Date = x.Date,
                                                SaleOrderId = x.SaleOrderId,
                                            })
                                            .FirstOrDefault();

                                        if (deliveryChallan != null)
                                        {
                                            AddToHistory(deliveryChallan.RegistrationNo, deliveryChallan.Date, "Delivery Challan");
                                        }
                                    }



                                }
                                break;
                            case "Quotation":
                                if ( request.CurrentDocumentId != Guid.Empty)
                                {
                                    var quotation = _context.SaleOrders
                                        .Where(x => x.Id == request.CurrentDocumentId)
                                        .Select(x => new
                                        {
                                            Id = x.Id,
                                            RegistrationNo = x.RegistrationNo,
                                            Date = x.Date,
                                            QuotationId = x.QuotationId,
                                            SaleOrderId = x.SaleOrderId,
                                            DeliveryChallanId = x.DeliveryChallanId,
                                            ProformaId = x.ProformaId,
                                            PurchaseOrderId = x.PurchaseOrderId,
                                        })
                                        .FirstOrDefault();

                                    if (quotation != null)
                                    {
                                        AddToHistory(quotation.RegistrationNo, quotation.Date, "Quotation");
                                        if (quotation.SaleOrderId != null)
                                        {
                                            request.CurrentDocumentId = quotation.SaleOrderId.Value;
                                            request.CurrentDocument = "SaleOrder";
                                            goto repeat;

                                        }
                                        else if (quotation.QuotationId != null)
                                        {
                                            request.CurrentDocumentId = quotation.QuotationId.Value;
                                            request.CurrentDocument = "Quotation";
                                            goto repeat;

                                        }
                                        else if (quotation.ProformaId != null)
                                        {
                                            request.CurrentDocumentId = quotation.ProformaId.Value;
                                            request.CurrentDocument = "ProformaInvoice";
                                            goto repeat;

                                        }
                                        else if (quotation.DeliveryChallanId != null)
                                        {
                                            request.CurrentDocumentId = quotation.DeliveryChallanId.Value;
                                            request.CurrentDocument = "DeliveryChallan";
                                            goto repeat;

                                        }
                                        else
                                        {
                                            request.CurrentDocumentId = Guid.Empty;
                                            request.CurrentDocument = "";
                                            goto repeat;

                                        }


                                    }

                                }
                                break;
                            case "ProformaInvoice":
                                var proforma = _context.Sales
                                    .Where(x => x.Id == request.CurrentDocumentId)
                                    .Select(x => new
                                    {
                                        Id = x.Id,
                                        RegistrationNo = x.RegistrationNo,
                                        Date = x.Date,
                                        QuotationId = x.QuotationId,
                                        SaleOrderId = x.SaleOrderId,
                                        DeliveryChallanId = x.DeliveryChallanId,
                                        ProformaId = x.ProformaId,
                                        PurchaseOrderId = x.PurchaseOrderId,
                                    })
                                .FirstOrDefault();

                                if (proforma != null)
                                {
                                    AddToHistory(proforma.RegistrationNo, proforma.Date, "Proforma Invoice");
                                    if (proforma.SaleOrderId != null)
                                    {
                                        request.CurrentDocumentId = proforma.SaleOrderId.Value;
                                        request.CurrentDocument = "SaleOrder";
                                        goto repeat;

                                    }
                                    else if (proforma.QuotationId != null)
                                    {
                                        request.CurrentDocumentId = proforma.QuotationId.Value;
                                        request.CurrentDocument = "Quotation";
                                        goto repeat;

                                    }
                                    else if (proforma.ProformaId != null)
                                    {
                                        request.CurrentDocumentId = proforma.ProformaId.Value;
                                        request.CurrentDocument = "ProformaInvoice";
                                        goto repeat;

                                    }
                                    else if (proforma.DeliveryChallanId != null)
                                    {
                                        request.CurrentDocumentId = proforma.DeliveryChallanId.Value;
                                        request.CurrentDocument = "DeliveryChallan";
                                        goto repeat;

                                    }
                                    else
                                    {
                                        request.CurrentDocumentId = Guid.Empty;
                                        request.CurrentDocument = "";
                                        goto repeat;

                                    }


                                }
                                break;

                            case "PurchaseOrder":
                                var purchaseOrder = _context.Sales
                                    .Where(x => x.Id == request.CurrentDocumentId)
                                    .Select(x => new
                                    {
                                        Id = x.Id,
                                        RegistrationNo = x.RegistrationNo,
                                        Date = x.Date,
                                        QuotationId = x.QuotationId,
                                        SaleOrderId = x.SaleOrderId,
                                        DeliveryChallanId = x.DeliveryChallanId,
                                        ProformaId = x.ProformaId,
                                        PurchaseOrderId = x.PurchaseOrderId,
                                    })
                                .FirstOrDefault();

                                if (purchaseOrder != null)
                                {
                                    AddToHistory(purchaseOrder.RegistrationNo, purchaseOrder.Date, "Customer PurchaseOrder");
                                    if (purchaseOrder.SaleOrderId != null)
                                    {
                                        request.CurrentDocumentId = purchaseOrder.SaleOrderId.Value;
                                        request.CurrentDocument = "SaleOrder";
                                        goto repeat;

                                    }
                                    else if (purchaseOrder.QuotationId != null)
                                    {
                                        request.CurrentDocumentId = purchaseOrder.QuotationId.Value;
                                        request.CurrentDocument = "Quotation";
                                        goto repeat;

                                    }
                                    else if (purchaseOrder.ProformaId != null)
                                    {
                                        request.CurrentDocumentId = purchaseOrder.ProformaId.Value;
                                        request.CurrentDocument = "ProformaInvoice";
                                        goto repeat;

                                    }
                                    else if (purchaseOrder.DeliveryChallanId != null)
                                    {
                                        request.CurrentDocumentId = purchaseOrder.DeliveryChallanId.Value;
                                        request.CurrentDocument = "DeliveryChallan";
                                        goto repeat;

                                    }
                                    else
                                    {
                                        request.CurrentDocumentId = Guid.Empty;
                                        request.CurrentDocument = "";
                                        goto repeat;

                                    }


                                }
                                break;


                            case "SaleOrder":
                                if (request.CurrentDocumentId != Guid.Empty)
                                {
                                    var saleOrder = _context.SaleOrders
                                        .Where(x => x.Id == request.CurrentDocumentId)
                                        .Select(x => new
                                        {
                                            Id = x.Id,
                                            RegistrationNo = x.RegistrationNo,
                                            Date = x.Date,
                                            QuotationId = x.QuotationId,
                                            SaleOrderId = x.SaleOrderId,
                                            DeliveryChallanId = x.DeliveryChallanId,
                                            ProformaId = x.ProformaId,
                                            PurchaseOrderId = x.PurchaseOrderId,
                                        })
                                        .FirstOrDefault();

                                    if (saleOrder != null)
                                    {
                                        AddToHistory(saleOrder.RegistrationNo, saleOrder.Date, "Sale Order");
                                        if (saleOrder.SaleOrderId != null)
                                        {
                                            request.CurrentDocumentId = saleOrder.SaleOrderId.Value;
                                            request.CurrentDocument = "SaleOrder";
                                            goto repeat;

                                        }
                                        else if (saleOrder.QuotationId != null)
                                        {
                                            request.CurrentDocumentId = saleOrder.QuotationId.Value;
                                            request.CurrentDocument = "Quotation";
                                            goto repeat;

                                        }
                                        else if (saleOrder.ProformaId != null)
                                        {
                                            request.CurrentDocumentId = saleOrder.ProformaId.Value;
                                            request.CurrentDocument = "ProformaInvoice";
                                            goto repeat;

                                        }
                                        else if (saleOrder.DeliveryChallanId != null)
                                        {
                                            request.CurrentDocumentId = saleOrder.DeliveryChallanId.Value;
                                            request.CurrentDocument = "DeliveryChallan";
                                            goto repeat;

                                        }
                                        else
                                        {
                                            request.CurrentDocumentId = Guid.Empty;
                                            request.CurrentDocument = "";
                                            goto repeat;

                                        }


                                    }

                                }
                                break;


                        }
                        void AddToHistory(string registrationNo, DateTime date, string documentName)
                        {
                            historyList.Add(new DocumentHistoryModel
                            {
                                RegistrationNo = registrationNo,
                                Date = date.ToString("D"),
                                DocumentName = documentName,
                            });
                        }
                    }

                    return historyList;




                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
