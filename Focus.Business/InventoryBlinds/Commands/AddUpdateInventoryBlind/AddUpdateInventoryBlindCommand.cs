using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Categories.Command.AddUpdateCategory;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail;
using Focus.Business.InventoryBlinds.ViewModel;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InventoryBlinds.Commands.AddUpdateInventoryBlind
{
    public class AddUpdateInventoryBlindCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public InventoryBlindVm InventoryBlindVm { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInventoryBlindCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInventoryBlindCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateInventoryBlindCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.InventoryBlindVm.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var inventoryBlind =  await Context.InventoryBlinds.FirstOrDefaultAsync(x => x.Id == request.InventoryBlindVm.Id,cancellationToken);
                        if (inventoryBlind == null)
                            throw new NotFoundException("Inventory Blind ",request.InventoryBlindVm.DocumentId);

                        if (inventoryBlind.InventoryBlindDetails.Count > 0)
                        {
                            Context.InventoryBlindDetails.RemoveRange(inventoryBlind.InventoryBlindDetails);
                        }

                        //Date not update because of already exist
                        //Document Id Not update 

                        inventoryBlind.WarehouseId = request.InventoryBlindVm.WarehouseId;
                        inventoryBlind.Note = request.InventoryBlindVm.Note;
                        inventoryBlind.IsCounted = request.InventoryBlindVm.IsCounted;

                        var inventoryBlindDetailList = new List<InventoryBlindDetail>();
                        foreach (var blindDetail in request.InventoryBlindVm.InventoryBlindDetailVms)
                        {
                            var inventoryBlindDetail = new InventoryBlindDetail()
                            {
                                ProductId = blindDetail.ProductId,
                                CurrentQuantity = blindDetail.CurrentQuantity,
                                PhysicalQuantity = blindDetail.PhysicalQuantity,
                                Remarks = blindDetail.Remarks,
                                InventoryBlindId = inventoryBlind.Id
                            };
                            inventoryBlindDetailList.Add(inventoryBlindDetail);
                        }

                        await Context.InventoryBlindDetails.AddRangeAsync(inventoryBlindDetailList, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        var success = await Context.SaveChangesAsync(cancellationToken);
                        if (success > 0)
                        {
                            return inventoryBlind.Id;
                        }
                        else
                        {
                            return Guid.Empty;
                        }

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var isDocumentIdExist =  await Context.InventoryBlinds.AnyAsync(x => x.DocumentId == request.InventoryBlindVm.DocumentId,cancellationToken);
                        if (isDocumentIdExist)
                            throw new ObjectAlreadyExistsException("Document Id" + request.InventoryBlindVm.DocumentId + " Already Exist");


                        var inventoryBlind = new InventoryBlind()
                        {
                            DateTime = request.InventoryBlindVm.DateTime,
                            WarehouseId = request.InventoryBlindVm.WarehouseId,
                            DocumentId = request.InventoryBlindVm.DocumentId,
                            Note = request.InventoryBlindVm.Note,
                            IsCounted = request.InventoryBlindVm.IsCounted
                        };

                        await Context.InventoryBlinds.AddAsync(inventoryBlind, cancellationToken);
                        var inventoryBlindDetailList = new List<InventoryBlindDetail>();
                        foreach (var blindDetail in request.InventoryBlindVm.InventoryBlindDetailVms)
                        {
                            var inventoryBlindDetail = new InventoryBlindDetail()
                            {
                                ProductId = blindDetail.ProductId,
                                CurrentQuantity = blindDetail.CurrentQuantity,
                                PhysicalQuantity = blindDetail.PhysicalQuantity,
                                Remarks = blindDetail.Remarks,
                                InventoryBlindId = inventoryBlind.Id
                            };
                            inventoryBlindDetailList.Add(inventoryBlindDetail);
                        }

                        
                        await Context.InventoryBlindDetails.AddRangeAsync(inventoryBlindDetailList, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);


                        var success = await Context.SaveChangesAsync(cancellationToken);
                        if (success > 0)
                        {
                            return inventoryBlind.Id;
                        }
                        else
                        {
                            return Guid.Empty;
                        }
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
