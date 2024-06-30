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
using Focus.Business.InquiryRequestProcess.Queries.GetInquiryProcessDetails;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryRequestProcess.Commands.AddUpdateInquiryProcess
{
    public class AddUpdateInquiryProcessCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public InquiryProcessDetailsLookUpModel ProcessDetails { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryProcessCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryProcessCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateInquiryProcessCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    if (request.ProcessDetails.Id != Guid.Empty)
                    {

                        var process = await Context.InquiryProcesses.FindAsync(request.ProcessDetails.Id);
                        if (process == null)
                            throw new NotFoundException("Inquiry Process Name", request.ProcessDetails.Name);

                        var isProcessNameExist = await Context.InquiryProcesses.AnyAsync(x => x.Name == request.ProcessDetails.Name, cancellationToken: cancellationToken);
                        if (process.Name != request.ProcessDetails.Name && isProcessNameExist)
                            throw new ObjectAlreadyExistsException("Inquiry Process " + request.ProcessDetails.Name + " Already Exist");


                        process.Name = request.ProcessDetails.Name;
                        process.Label = request.ProcessDetails.Label;
                        process.Description = request.ProcessDetails.Description;
                        process.IsActive = request.ProcessDetails.IsActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return process.Id;
                    }


                    var isProcessExist =  Context.InquiryProcesses.FirstOrDefault(x => x.Name.ToLower() == request.ProcessDetails.Name.ToLower());
                    if (isProcessExist != null)
                        throw new ObjectAlreadyExistsException("Inquiry Process " + request.ProcessDetails.Name + " Already Exist");
                    //New brand Added
                    var inq = new InquiryProcess()
                    {
                        Name = request.ProcessDetails.Name,
                        Label = request.ProcessDetails.Label,
                        Description = request.ProcessDetails.Description,
                        Code = request.ProcessDetails.Code,
                        IsActive = request.ProcessDetails.IsActive
                    };
                    //Add Department to database
                    await Context.InquiryProcesses.AddAsync(inq, cancellationToken);

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
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
