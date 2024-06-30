using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;
using DocumentFormat.OpenXml.Bibliography;

namespace Focus.Business.SmsSetup.GsmSmsSetupCommand
{
    public class AddUpdateGsmSmsSetupCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public GsmSmsSetupModel GsmSmsSetupModel { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateGsmSmsSetupCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateGsmSmsSetupCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateGsmSmsSetupCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.GsmSmsSetupModel.BranchId != null)
                    {
                        var data =await _context.GsmSmsSetups.FirstOrDefaultAsync(x=>x.BranchId == request.GsmSmsSetupModel.BranchId, cancellationToken: cancellationToken);
                        if (data == null)
                        {
                            var gsmSetup = new GsmSmsSetup()
                            {
                                ComPort = request.GsmSmsSetupModel.ComPort,
                                DefaultMessage = request.GsmSmsSetupModel.DefaultMessage,
                                BranchId = request.GsmSmsSetupModel.BranchId,

                            };
                            await _context.GsmSmsSetups.AddAsync(gsmSetup, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                              
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return gsmSetup.Id;
                        }
                        else
                        {
                            data.ComPort = request.GsmSmsSetupModel.ComPort;
                            data.DefaultMessage = request.GsmSmsSetupModel.DefaultMessage;

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return data.Id;
                        }

                    }
                    else
                    {
                        var data =await _context.GsmSmsSetups.FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        if (data == null)
                        {
                            var gsmSetup = new GsmSmsSetup()
                            {
                                ComPort = request.GsmSmsSetupModel.ComPort,
                                DefaultMessage = request.GsmSmsSetupModel.DefaultMessage,

                            };
                            await _context.GsmSmsSetups.AddAsync(gsmSetup, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                               
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return gsmSetup.Id;
                        }
                        else
                        {
                            data.ComPort = request.GsmSmsSetupModel.ComPort;
                            data.DefaultMessage = request.GsmSmsSetupModel.DefaultMessage;

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                            return data.Id;
                        }

                    }


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
