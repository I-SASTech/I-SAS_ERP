using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.InquiryRequestModule.Commands.DeleteInquiryModule
{
    public class DeleteInquiryModuleCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteInquiryModuleCommand, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DeleteInquiryModuleCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(DeleteInquiryModuleCommand request, CancellationToken cancellationToken)
            {
                //Get Data from database in specific id which we want to delete
                var module = await _context.InquiryModules.FindAsync(request.Id);

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (module != null)
                    {
                        _context.InquiryModules.Remove(module);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                       
                    }
                    return request.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to module your request please contact support");
                }
            }
        }
    }
}
