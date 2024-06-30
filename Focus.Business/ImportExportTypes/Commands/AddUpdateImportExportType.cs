using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ImportExportTypes.Commands
{
    public class AddUpdateImportExportType : IRequest<Guid>
    {
        //Get all variable in entity
        public ImportExportTypeLookUpModel ImportExportType { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateImportExportType, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateImportExportType> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateImportExportType request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.ImportExportType.Id != Guid.Empty)
                    {

                        var importExportType = await Context.ImportExportTypes.FindAsync(request.ImportExportType.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        importExportType.Name = request.ImportExportType.Name;
                        importExportType.NameArabic = request.ImportExportType.NameArabic;
                        importExportType.Code = request.ImportExportType.Code;
                        importExportType.Description = request.ImportExportType.Description;
                        importExportType.ImportExportTypes = request.ImportExportType.ImportExportTypes;
                        importExportType.isActive = request.ImportExportType.isActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return importExportType.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var importExportType = new ImportExportType
                        {
                          
                            Code = request.ImportExportType.Code,
                            Name = request.ImportExportType.Name,
                            NameArabic = request.ImportExportType.NameArabic,
                            Description = request.ImportExportType.Description,
                            isActive = request.ImportExportType.isActive,
                            ImportExportTypes = request.ImportExportType.ImportExportTypes,
                        };
                        //Add Department to database
                        await Context.ImportExportTypes.AddAsync(importExportType, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return importExportType.Id;



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
