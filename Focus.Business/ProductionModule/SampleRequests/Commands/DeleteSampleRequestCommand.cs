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

namespace Focus.Business.ProductionModule.SampleRequests.Commands
{
    public class DeleteSampleRequestCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<DeleteSampleRequestCommand, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DeleteSampleRequestCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(DeleteSampleRequestCommand request, CancellationToken cancellationToken)
            {
                //Get Data from database in specific id which we want to delete
                var additionalCompanyInformation = await _context.companyInformations.FindAsync(request.Id);

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (additionalCompanyInformation != null)
                    {
                        _context.companyInformations.Remove(additionalCompanyInformation);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                    }
                    return request.Id;
                }
                //throw exception which data not exist
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
