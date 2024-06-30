using System;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CompanyActionProcess.Commands
{
    public class AddUpdateCommand : IRequest<Guid>
    {
        public ProcessLookUpModel Process { get; set; }
        public class Handler : IRequestHandler<AddUpdateCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Update
                    if (request.Process.Id != Guid.Empty)
                    {
                        var process = await Context.CompanyProcess.FindAsync(request.Process.Id);
                        if (process == null)
                            throw new NotFoundException("Color Name", request.Process.ProcessName);

                        //if (request.Process.ProcessName != "" && request.Process.ProcessNameArabic != "")
                        //{
                        //    var isColorNameExist = await Context.CompanyProcess.FirstOrDefaultAsync(x => x.ProcessName == request.Process.ProcessName || x.ProcessNameArabic == request.Process.ProcessNameArabic, cancellationToken: cancellationToken);

                        //    if (process.ProcessName != request.Process.ProcessName && isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessName + " Already Exist");
                        //    if (process.ProcessNameArabic != request.Process.ProcessNameArabic && isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessNameArabic + " Already Exist");
                        //}
                        //else if (request.Process.ProcessName != "")
                        //{
                        //    var isColorNameExist = await Context.CompanyProcess.FirstOrDefaultAsync(x => x.ProcessName == request.Process.ProcessName, cancellationToken: cancellationToken);

                        //    if (process.ProcessName != request.Process.ProcessName && isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessName + " Already Exist");
                        //}
                        //else
                        //{
                        //    var isColorNameExist = await Context.CompanyProcess.FirstOrDefaultAsync(x => x.ProcessNameArabic == request.Process.ProcessNameArabic, cancellationToken: cancellationToken);

                        //    if (process.ProcessNameArabic != request.Process.ProcessNameArabic && isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessNameArabic + " Already Exist");
                        //}

                        process.ProcessName = request.Process.ProcessName;
                        process.ProcessNameArabic = request.Process.ProcessNameArabic;
                        process.Type = request.Process.Type;
                        process.Status = request.Process.Status;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return process.Id;
                        //Check Department exist

                    }

                    //Add New
                    //if (request.Process.ProcessName != "" && request.Process.ProcessNameArabic != "")
                    //{
                    //    var isColorNameExist = await Context.CompanyProcess.FirstOrDefaultAsync(x => x.ProcessName == request.Process.ProcessName || x.ProcessNameArabic == request.Process.ProcessNameArabic, cancellationToken: cancellationToken);

                    //    if (isColorNameExist != null)
                    //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessName + " " + request.Process.ProcessNameArabic + " Already Exist");
                    //}
                    //else if (request.Process.ProcessName != "")
                    //{
                    //    var isColorNameExist = await Context.CompanyProcess.FirstOrDefaultAsync(x => x.ProcessName == request.Process.ProcessName, cancellationToken: cancellationToken);

                    //    if (isColorNameExist != null)
                    //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessName + " Already Exist");
                    //}
                    //else
                    //{
                    //    var isColorNameExist = await Context.CompanyProcess.FirstOrDefaultAsync(x => x.ProcessNameArabic == request.Process.ProcessNameArabic, cancellationToken: cancellationToken);

                    //    if (isColorNameExist != null)
                    //        throw new ObjectAlreadyExistsException("Process " + request.Process.ProcessNameArabic + " Already Exist");
                    //}

                    var proces = new CompanyProcess
                    {
                        ProcessName = request.Process.ProcessName,
                        ProcessNameArabic = request.Process.ProcessNameArabic,
                        Type = request.Process.Type,
                        Status = request.Process.Status
                    };

                    //Add Department to database
                    await Context.CompanyProcess.AddAsync(proces, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return proces.Id;

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
