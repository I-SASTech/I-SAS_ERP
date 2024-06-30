using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;
using System.Threading.Tasks;
using System.Threading;
using Focus.Business.PushPullQuery;
namespace ScheduledTasks.ScheduledTasks
{
    public class PushPullTransactionLog : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public string Schedule => "0 0 * * 0";

        public PushPullTransactionLog(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

        }

        public async Task Invoke(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new PushPullQuery(), cancellationToken);
            }
        }
    }
}
