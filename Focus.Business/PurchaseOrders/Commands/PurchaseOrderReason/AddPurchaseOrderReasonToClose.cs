using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Focus.Business.PurchaseOrders.Commands.PurchaseOrderReason
{
    public class AddPurchaseOrderReasonToClose : IRequest<Message>
    {
        public string FormName { get; set; }
        public PurchaseOrderLookupModel PurchaseOrder { get; set; }

        public class Handler : IRequestHandler<AddPurchaseOrderReasonToClose, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddPurchaseOrderReasonToClose> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddPurchaseOrderReasonToClose request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.PurchaseOrder.DocumentTypeForClose == "GoodReceive")
                    {

                        var purchaseOrder = await _context.GoodReceives.FindAsync(request.PurchaseOrder.Id);

                        if (purchaseOrder == null && request.FormName == "SupplierQuotation")
                        {
                            return new Message
                            {
                                IsSuccess = false,
                                IsAddUpdate = "Supplier Quotation Not Found"
                            };
                        }
                        else if (purchaseOrder == null)
                        {
                            var result = new Message
                            {
                                IsSuccess = false,
                                IsAddUpdate = "Purchase order Not Found"
                            };

                            return result;
                        }

                        purchaseOrder.Reason = request.PurchaseOrder.Reason;
                        purchaseOrder.IsClose = request.PurchaseOrder.IsClose;
                        purchaseOrder.PartiallyReceived = PartiallyInvoices.Fully;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = purchaseOrder.BranchId,
                            DocumentNo = purchaseOrder.RegistrationNo
                        }, cancellationToken);

                    }
                    else
                    {
                        var purchaseOrder = await _context.PurchaseOrders.FindAsync(request.PurchaseOrder.Id);

                        if (purchaseOrder == null && request.FormName == "SupplierQuotation")
                        {
                            return new Message
                            {
                                IsSuccess = false,
                                IsAddUpdate = "Supplier Quotation Not Found"
                            };
                        }
                        else if (purchaseOrder == null)
                        {
                            var result = new Message
                            {
                                IsSuccess = false,
                                IsAddUpdate = "Purchase order Not Found"
                            };

                            return result;
                        }

                        purchaseOrder.Reason = request.PurchaseOrder.Reason;
                        purchaseOrder.IsClose = request.PurchaseOrder.IsClose;
                        purchaseOrder.PartiallyReceived = PartiallyInvoices.Fully;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = purchaseOrder.BranchId,
                            DocumentNo = purchaseOrder.RegistrationNo
                        }, cancellationToken);
                    }


                    

                

                    //Save changes to database
                    await _context.SaveChangesAsync(cancellationToken);

                    var message = new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Data has been added successfully"
                    };
                    return message;
                }
                catch (Exception)
                {
                    return new Message
                    {
                        IsAddUpdate = "Something went wrong! Contact to Support"
                    };
                }
            }
        }
    }
}
