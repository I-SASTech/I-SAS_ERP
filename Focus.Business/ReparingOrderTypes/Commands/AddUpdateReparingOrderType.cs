using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrderTypes.Commands
{
    public class AddUpdateReparingOrderType : IRequest<Guid>
    {
        //Get all variable in entity
        public ReparingOrderTypeLookUpModel ReparingOrdertType { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateReparingOrderType, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateReparingOrderType> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateReparingOrderType request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.ReparingOrdertType.Id != Guid.Empty)
                    {
                        var repairingOrderType = await Context.ReparingOrderTypes.FindAsync(request.ReparingOrdertType.Id);

                        if (request.ReparingOrdertType.Name != "")
                        {
                            var isContact = await Context.ReparingOrderTypes.AnyAsync(x => x.Name == request.ReparingOrdertType.Name && x.ReparingOrderTypeEnums == request.ReparingOrdertType.ReparingOrderTypes, cancellationToken);
                            if (isContact && repairingOrderType.Name != request.ReparingOrdertType.Name)
                                throw new ObjectAlreadyExistsException(" Name " + request.ReparingOrdertType.Name + " Already Exist");
                        }


                        repairingOrderType.Name = request.ReparingOrdertType.Name;
                        repairingOrderType.NameArabic = request.ReparingOrdertType.NameArabic;
                        repairingOrderType.ReparingOrderTypeEnums = request.ReparingOrdertType.ReparingOrderTypes;
                        repairingOrderType.isActive = request.ReparingOrdertType.isActive;
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                          
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return repairingOrderType.Id;

                    }
                    else
                    {
                        if (request.ReparingOrdertType.Name != "")
                        {
                            var isContact = await Context.ReparingOrderTypes.FirstOrDefaultAsync(x => x.Name == request.ReparingOrdertType.Name && x.ReparingOrderTypeEnums == request.ReparingOrdertType.ReparingOrderTypes, cancellationToken);
                            if (isContact != null)
                            {
                                return isContact.Id;
                            }

                        }

                        var reparingOrdertType = new ReparingOrderType
                        {

                            Name = request.ReparingOrdertType.Name,
                            NameArabic = request.ReparingOrdertType.NameArabic,
                            ReparingOrderTypeEnums = request.ReparingOrdertType.ReparingOrderTypes,
                            isActive = request.ReparingOrdertType.isActive,
                            BranchId = request.ReparingOrdertType.BranchId,
                        };
                        //Add Department to database
                        await Context.ReparingOrderTypes.AddAsync(reparingOrdertType, cancellationToken);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return reparingOrdertType.Id;



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
