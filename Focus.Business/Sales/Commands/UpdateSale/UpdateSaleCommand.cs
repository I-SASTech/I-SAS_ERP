using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.SaleOrders;
using Microsoft.EntityFrameworkCore;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommand : IRequest<Message>
    {
        public UpdateSaleLookUpModel Sale { get; set; }

        public class Handler : IRequestHandler<UpdateSaleCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<UpdateSaleCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.Sale.IsDeliveryChallan)
                    {
                        var sale = _context.DeliveryChallans.AsNoTracking().FirstOrDefault(x => x.Id == request.Sale.Id);
                        if (sale != null)
                        {
                            if (request.Sale.IsClose)
                            {
                                sale.IsClose = true;
                                sale.Reason = request.Sale.Reason;
                            }

                            _context.DeliveryChallans.Update(sale);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId=sale.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = sale.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;

                    }
                    else if(request.Sale.IsPurchaseOrder)
                    {
                        var purchaseOrder = _context.PurchaseOrders.AsNoTracking().FirstOrDefault(x => x.Id == request.Sale.Id);

                        if(purchaseOrder != null)
                        {
                            if(request.Sale.IsClose)
                            {
                                purchaseOrder.IsClose = true;
                                purchaseOrder.Reason = request.Sale.Reason;
                            }

                            _context.PurchaseOrders.Update(purchaseOrder);
                        }
                        _context.SaveChanges();

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = purchaseOrder.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;
                    }
                    else
                    {
                        var sale = _context.Sales.AsNoTracking().FirstOrDefault(x => x.Id == request.Sale.Id);
                        if (sale != null)
                        {
                            if (request.Sale.IsClose)
                            {
                                sale.IsClose = true;
                                sale.Reason = request.Sale.Reason;
                            }

                            _context.Sales.Update(sale);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId=sale.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = sale.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;
                    }
                   

                  
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
