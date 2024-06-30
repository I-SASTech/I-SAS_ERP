using System.Threading;
using System.Threading.Tasks;
using Focus.Business.AutoPurchasing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;

namespace ScheduledTasks.ScheduledTasks
{
    public class AutoPurchaseOrder : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public string Schedule => "*/2 * * * *";

        public AutoPurchaseOrder(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Invoke(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new AddAutoPurchaseCommand(), cancellationToken);
            }
        }
    }
}
