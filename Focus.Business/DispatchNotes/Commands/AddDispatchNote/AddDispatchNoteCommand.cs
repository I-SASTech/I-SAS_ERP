using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.DispatchNotes.Commands.AddDispatchNote
{
    public class AddDispatchNoteCommand : IRequest<Guid>
    {

        public DispatchNoteLookupModel DispatchNote { get; set; }
        public class Handler : IRequestHandler<AddDispatchNoteCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddDispatchNoteCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddDispatchNoteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.DispatchNote.Id == Guid.Empty)
                    {
                        var dispatchNote = new DispatchNote()
                        {
                            Date = request.DispatchNote.Date,
                            RegistrationNo = request.DispatchNote.RegistrationNo,
                            CustomerId = request.DispatchNote.CustomerId,
                            SaleOrderId = request.DispatchNote.SaleOrderId,
                            Refrence = request.DispatchNote.Refrence,
                            Note = request.DispatchNote.Note,
                            ApprovalStatus = request.DispatchNote.ApprovalStatus,
                            IsClose = request.DispatchNote.IsClose,
                            BranchId = request.DispatchNote.BranchId,
                            DispatchNoteItems = request.DispatchNote.DispatchNoteItems.Select(x =>
                                new DispatchNoteItem()
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    UnitPrice = x.UnitPrice
                                }).ToList()
                        };
                       
                        await _context.DispatchNotes.AddAsync(dispatchNote, cancellationToken);

                        if (request.DispatchNote.SaleOrderId!=null && request.DispatchNote.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var soItem = _context.SaleOrders.AsNoTracking()
                                .Include(x => x.SaleOrderItems)
                                .FirstOrDefaultAsync(x => x.Id == request.DispatchNote.SaleOrderId, cancellationToken: cancellationToken);
                            if (soItem == null)
                                throw new NotFoundException("Dispatch Note", "Was not found");

                            foreach (var item in request.DispatchNote.DispatchNoteItems)
                            {
                                var soItemDetail = soItem.Result.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                if (soItemDetail != null)
                                {
                                    soItemDetail.QuantityOut -= item.Quantity;
                                    _context.SaleOrderItems.Update(soItemDetail);
                                }
                            }
                            var close = soItem.Result.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.DispatchNote.SaleOrderId && x.QuantityOut > 0);
                            if (close == null)
                            {
                                soItem.Result.IsClose = true;
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = dispatchNote.BranchId,
                            DocumentNo=dispatchNote.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return dispatchNote.Id;
                    }
                    else
                    {
                        var purchase = await _context.DispatchNotes.FindAsync(request.DispatchNote.Id);
                        if (purchase==null)
                        {
                            throw new NotFoundException("Dispatch Notes not found", "");
                        }
                        
                        purchase.Date = request.DispatchNote.Date;
                        purchase.SaleOrderId = request.DispatchNote.SaleOrderId;
                        purchase.Refrence = request.DispatchNote.Refrence;
                        purchase.RegistrationNo = request.DispatchNote.RegistrationNo;
                        purchase.CustomerId = request.DispatchNote.CustomerId;
                        purchase.Note = request.DispatchNote.Note;
                        purchase.ApprovalStatus = request.DispatchNote.ApprovalStatus;
                        purchase.IsClose = request.DispatchNote.IsClose;

                        _context.DispatchNoteItems.RemoveRange(purchase.DispatchNoteItems);
                        purchase.DispatchNoteItems = request.DispatchNote.DispatchNoteItems.Select(x =>
                                                       new DispatchNoteItem()
                                                       {
                                                           ProductId = x.ProductId,
                                                           Quantity = x.Quantity,
                                                           UnitPrice = x.UnitPrice

                                                       }).ToList();

                        if (request.DispatchNote.SaleOrderId != null && request.DispatchNote.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var soItem = _context.SaleOrders.AsNoTracking()
                                .Include(x => x.SaleOrderItems)
                                .FirstOrDefaultAsync(x => x.Id == request.DispatchNote.SaleOrderId, cancellationToken: cancellationToken);
                            if (soItem == null)
                                throw new NotFoundException("Dispatch Note", "Was not found");

                            foreach (var item in request.DispatchNote.DispatchNoteItems)
                            {
                                var soItemDetail = soItem.Result.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                if (soItemDetail != null)
                                {
                                    soItemDetail.QuantityOut = soItemDetail.QuantityOut - item.Quantity;
                                    _context.SaleOrderItems.Update(soItemDetail);
                                }
                            }

                            var close = soItem.Result.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.DispatchNote.SaleOrderId && x.QuantityOut > 0);
                            if (close == null)
                            {
                                soItem.Result.IsClose = true;
                            }

                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = purchase.BranchId,
                            DocumentNo= purchase.RegistrationNo
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
