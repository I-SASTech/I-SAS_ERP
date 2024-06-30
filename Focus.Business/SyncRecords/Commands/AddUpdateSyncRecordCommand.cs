using Focus.Business.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnitM = MediatR.Unit;

namespace Focus.Business.SyncRecords.Commands
{
    public class AddUpdateSyncRecordCommand : IRequest<UnitM>
    {
        public List<SyncRecordModel> SyncRecordModels { get; set; }
        public Guid? BranchId { get; set; }
        public string Code { get; set; }
        public bool IsServer { get; set; }
        public string DocumentNo { get; set; }

        public class Handler : IRequestHandler<AddUpdateSyncRecordCommand, UnitM>
        {
            private readonly ILogger _logger;
            //Constructor

            public Handler(ILogger<AddUpdateSyncRecordCommand> logger)
            {
                _logger = logger;
            }

            public async Task<UnitM> Handle(AddUpdateSyncRecordCommand request, CancellationToken cancellationToken)
            {
                try
                {


                    return UnitM.Value;
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
