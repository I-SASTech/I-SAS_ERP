using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryRequestType.Queries.GetInquiryTypeDetails;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryRequestType.Commands.AddUpdateInquiryType
{
    public class AddUpdateInquiryTypeCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public InquiryTypeDetailsLookUpModel TypeDetails { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryTypeCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryTypeCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateInquiryTypeCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.TypeDetails.Id != Guid.Empty)
                    {
                        var type = await Context.InquiryTypes.FindAsync(request.TypeDetails.Id);
                        if (type == null)
                            throw new NotFoundException("Inquiry Type Name", request.TypeDetails.Name);

                        var isTypeNameExist = await Context.InquiryTypes.AnyAsync(x => x.Name == request.TypeDetails.Name, cancellationToken: cancellationToken);
                        if (type.Name != request.TypeDetails.Name && isTypeNameExist)
                            throw new ObjectAlreadyExistsException("Inquiry Type " + request.TypeDetails.Name + " Already Exist");


                        type.Name = request.TypeDetails.Name;
                        type.Label = request.TypeDetails.Label;
                        type.Description = request.TypeDetails.Description;
                        type.IsActive = request.TypeDetails.IsActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);


                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return type.Id;
                    }


                    var isTypeExist =  Context.InquiryTypes.FirstOrDefault(x => x.Name.ToLower() == request.TypeDetails.Name.ToLower());
                    if (isTypeExist != null)
                        throw new ObjectAlreadyExistsException("Inquiry Type " + request.TypeDetails.Name + " Already Exist");
                    //New brand Added
                    var inq = new InquiryType()
                    {
                        Name = request.TypeDetails.Name,
                        Label = request.TypeDetails.Label,
                        Description = request.TypeDetails.Description,
                        Code = request.TypeDetails.Code,
                        IsActive = request.TypeDetails.IsActive
                    };
                    //Add Department to database
                    await Context.InquiryTypes.AddAsync(inq, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);


                    await Context.SaveChangesAsync(cancellationToken);
                    return inq.Id;

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
                    throw new ApplicationException("Unable to type your request please contact support");
                }
            }
        }
    }
}
