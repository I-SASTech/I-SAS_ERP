using Focus.Business.Interface;
using Focus.Business.StockRequests.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.Exceptions;
using System.Linq;
using Focus.Business.Common;
using Focus.Domain.Enum;

namespace Focus.Business.StockRequests.Commands
{
    public class StockRequestAddUpdateCommand : IRequest<Message>
    {
        public StockRequestLookupModel StockRequest { get; set; }

        public class Handler : IRequestHandler<StockRequestAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<StockRequestAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(StockRequestAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if ( request.StockRequest.Id != Guid.Empty)
                    {
                        var stockRequest = await _context.StockRequests.FindAsync(request.StockRequest.Id);
                        if (stockRequest == null)
                            throw new NotFoundException("No Stock Request Found", "");

                        stockRequest.Code = request.StockRequest.Code;
                        stockRequest.Date = request.StockRequest.Date;
                        stockRequest.ApprovalStatus = request.StockRequest.ApprovalStatus;
                        stockRequest.BranchId = request.StockRequest.BranchId;
                        stockRequest.ToBranchId = request.StockRequest.ToBranchId;
                        stockRequest.FromBranchId = request.StockRequest.BranchId;
                        stockRequest.FromWareHouse = request.StockRequest.WareHouseId;
                        stockRequest.StockRequestStatus = StockRequestStatus.Default;

                        _context.StockRequestItems.RemoveRange(stockRequest.StockRequestItems);

                        stockRequest.StockRequestItems = request.StockRequest.WareHouseTransferItems.Select(x => new StockRequestItems
                        {
                            BranchId= x.BranchId,
                            ProductId= x.ProductId,
                            Quantity= x.Quantity,
                            RemainingQuantity = x.Quantity,
                            StockRequestId = stockRequest.Id,
                        }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = stockRequest.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsAddUpdate = "Data has been updated successfully",
                            IsSuccess = true,
                        };
                    }
                    else
                    {
                        var stockRequest = new StockRequest
                        {
                            Code = request.StockRequest.Code,
                            Date = request.StockRequest.Date,
                            ApprovalStatus = request.StockRequest.ApprovalStatus,
                            BranchId = request.StockRequest.BranchId,
                            ToBranchId = request.StockRequest.ToBranchId,
                            FromBranchId = request.StockRequest.BranchId,
                            FromWareHouse = request.StockRequest.WareHouseId,
                            StockRequestStatus = StockRequestStatus.Default,
                        };

                        await _context.StockRequests.AddAsync(stockRequest, cancellationToken);

                        var stockRequestItems = request.StockRequest.WareHouseTransferItems.Select(x => new StockRequestItems
                        {
                            BranchId = stockRequest.BranchId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            RemainingQuantity = x.Quantity,
                            StockRequestId = stockRequest.Id
                        }).ToList();

                        await _context.StockRequestItems.AddRangeAsync(stockRequestItems, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = stockRequest.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsAddUpdate = "Data has been updated successfully",
                            IsSuccess = true,
                        };
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
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
