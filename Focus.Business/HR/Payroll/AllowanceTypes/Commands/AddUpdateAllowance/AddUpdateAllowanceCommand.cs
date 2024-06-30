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

namespace Focus.Business.HR.Payroll.AllowanceTypes.Commands.AddUpdateAllowance
{
    public class AddUpdateAllowanceTypeCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateAllowanceTypeCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateAllowanceTypeCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateAllowanceTypeCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Id != Guid.Empty)
                    {
                        var allowanceTypes = await Context.AllowanceTypes.FindAsync(request.Id);
                        if (allowanceTypes == null)
                            throw new NotFoundException("Allowance Type ", request.Name);


                        if (request.Name != "" && request.NameArabic != "")
                        {
                           
                        }
                        else if (request.Name != "")
                        {
                            //Check AllowanceType name is already exists
                            var isAllowanceTypeNameExist = await Context.AllowanceTypes.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (allowanceTypes.Name != request.Name && isAllowanceTypeNameExist)
                                throw new ObjectAlreadyExistsException(" Allowance Type " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check AllowanceType name is already exists
                            var isAllowanceTypeNameExist = await Context.AllowanceTypes.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (allowanceTypes.NameArabic != request.NameArabic && isAllowanceTypeNameExist)
                                throw new ObjectAlreadyExistsException(" Allowance Type " + request.NameArabic + " Already Exist");
                        }

                        allowanceTypes.Name = request.Name;
                        allowanceTypes.NameArabic = request.NameArabic;
                        allowanceTypes.Description = request.Description;
                        allowanceTypes.IsActive = request.IsActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return allowanceTypes.Id;
                    }

                    if (request.Name!="" && request.NameArabic!="")
                    {
                        //Check AllowanceType name is already exists
                        var isAllowanceTypeNameExist = await Context.AllowanceTypes.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                        if (isAllowanceTypeNameExist!=null)
                            throw new ObjectAlreadyExistsException(" Allowance Type " + request.Name +" "+ request.NameArabic + " Already Exist");
                    }
                    else if(request.Name != "")
                    {
                        //Check AllowanceType name is already exists
                        var isAllowanceTypeNameExist = await Context.AllowanceTypes.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                        if (isAllowanceTypeNameExist != null)
                            throw new ObjectAlreadyExistsException(" Allowance Type " + request.Name + " Already Exist");
                    }
                    else
                    {
                        //Check AllowanceType name is already exists
                        var isAllowanceTypeNameExist = await Context.AllowanceTypes.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                        if (isAllowanceTypeNameExist != null)
                            throw new ObjectAlreadyExistsException("Product AllowanceType " + request.NameArabic + " Already Exist");
                    }


                    //New allowanceType Added
                    var allowanceType = new AllowanceType
                    {
                        Name = request.Name,
                        NameArabic = request.NameArabic,
                        Description = request.Description,
                        IsActive = request.IsActive
                    };
                    //Add Department to database
                    await Context.AllowanceTypes.AddAsync(allowanceType, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return allowanceType.Id;

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
