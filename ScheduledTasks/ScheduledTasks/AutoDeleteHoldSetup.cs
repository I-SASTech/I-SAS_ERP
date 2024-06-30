using Focus.Business.AutoPurchasing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using NPOI.OpenXml4Net.OPC;
using System.Data;
using Focus.Business.HoldTypeSetup.Commands;

namespace ScheduledTasks.ScheduledTasks
{
    public class AutoDeleteHoldSetup : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public string Schedule => "0 0 * * *";

        public AutoDeleteHoldSetup(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

        }

        public async Task Invoke(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new PermanentDeleteHoldSetupDeleteCommand(), cancellationToken);
            }
        }
    }
}
