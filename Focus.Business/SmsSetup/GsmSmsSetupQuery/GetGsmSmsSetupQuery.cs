using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Focus.Business.SmsSetup.GsmSmsSetupCommand;
using System.IO.Ports;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.SmsSetup.GsmSmsSetupQuery
{
    public class GetGsmSmsSetupQuery : IRequest<GsmSmsSetupModel>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetGsmSmsSetupQuery, GsmSmsSetupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetGsmSmsSetupQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<GsmSmsSetupModel> Handle(GetGsmSmsSetupQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.BranchId != null)
                    {
                        var gsmSmsSetup = await _context.GsmSmsSetups.FirstOrDefaultAsync(x => x.BranchId == request.BranchId, cancellationToken: cancellationToken);
                        var data = new GsmSmsSetupModel();
                        if (gsmSmsSetup == null)
                        {
                            data.Ports = SerialPort.GetPortNames();
                            return data;
                        }

                        data.ComPort = gsmSmsSetup.ComPort;
                        data.DefaultMessage = gsmSmsSetup.DefaultMessage;
                        data.Ports = SerialPort.GetPortNames();
                        return data;
                    }
                    else
                    {
                        var gsmSmsSetup = await _context.GsmSmsSetups.FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        var data = new GsmSmsSetupModel();
                        if (gsmSmsSetup == null)
                        {
                            data.Ports = SerialPort.GetPortNames();
                            return data;
                        }

                        data.ComPort = gsmSmsSetup.ComPort;
                        data.DefaultMessage = gsmSmsSetup.DefaultMessage;
                        data.Ports = SerialPort.GetPortNames();
                        return data;
                    }

                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
