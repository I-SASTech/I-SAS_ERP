using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PrintOptions.Commands.AddPrintOption
{
    public class AddUpdatePrintOptionCommand : IRequest<bool>
    {
        //Get all variable in entity
        public PrintOptionLookUp PrintOption { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdatePrintOptionCommand, bool>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdatePrintOptionCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<bool> Handle(AddUpdatePrintOptionCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.PrintOption.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        //Data already exist ; Update data
                        //get data from database using id

                        var printOptions = await Context.PrintOptions.FindAsync(request.PrintOption.Id);


                        if (printOptions!=null)
                        {
                            printOptions.PrintSettingId = request.PrintOption.PrintSettingId;
                            printOptions.Label = request.PrintOption.Label;
                            printOptions.LabelNameArabic = request.PrintOption.LabelNameArabic;
                            printOptions.Value = request.PrintOption.Value;
                            printOptions.Type = request.PrintOption.Type;

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            //Save changes to database
                            await Context.SaveChangesAsync(cancellationToken);
                        }


                        // Return Department id after successfully updating data
                        return true;
                        //Check Department exist

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var printOption = new PrintOption
                        {
                            PrintSettingId = request.PrintOption.PrintSettingId,
                            Label = request.PrintOption.Label,
                            LabelNameArabic = request.PrintOption.LabelNameArabic,
                            Value = request.PrintOption.Value,
                            Type = request.PrintOption.Type,
                            BranchId = request.PrintOption.BranchId,
                        };
                        //Add Department to database
                        await Context.PrintOptions.AddAsync(printOption, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return true;



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
