using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Business.MobileOrders.Queries.GetMobileOrderRegistrationNo;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.MobileOrders.Commands.AddMobileOrder
{
    public class AddMobileOrderCommand : IRequest<Guid>
    {

        public MobileOrderLookupModel mobileOrder { get; set; }
        public class Handler : IRequestHandler<AddMobileOrderCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddMobileOrderCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddMobileOrderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.mobileOrder.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var registrationNo = await _mediator.Send(new GetMobileOrderRegistrationNoQuery(), cancellationToken);
                        var mobileOrder = new Domain.Entities.MobileOrder()
                        {
                            OrderDate = request.mobileOrder.OrderDate,
                            CustomerId = request.mobileOrder.CustomerId,
                            Name = request.mobileOrder.Name,
                            OrderNo = registrationNo,

                            MobileOrderItems = request.mobileOrder.MobileOrderItemLookupModels.Select(x =>
                                new MobileOrderItem()
                                {
                                    ProductId = x.ProductId,
                                    Price = x.Price,
                                    Quantity = x.Quantity,

                                }).ToList()
                        };

                        await _context.MobileOrders.AddAsync(mobileOrder, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            
                            
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return mobileOrder.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var purchase = await _context.MobileOrders
                          .FindAsync(request.mobileOrder.Id);

                        purchase.OrderDate = request.mobileOrder.OrderDate;
                        purchase.OrderNo = request.mobileOrder.OrderNo;
                        purchase.CustomerId = request.mobileOrder.CustomerId;
                        purchase.Name = request.mobileOrder.Name;



                        _context.MobileOrderItems.RemoveRange(purchase.MobileOrderItems);
                        purchase.MobileOrderItems = request.mobileOrder.MobileOrderItemLookupModels.Select(x =>
                                                       new MobileOrderItem()
                                                       {
                                                           ProductId = x.ProductId,
                                                           Price = x.Price,
                                                           Quantity = x.Quantity,

                                                       }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return purchase.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
