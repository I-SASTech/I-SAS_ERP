using Focus.Business.StocksTransfer.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledTasks.ScheduledTasks 
{
    public class StockTransferVouchers : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public string Schedule => "0 1 * * *";

        public StockTransferVouchers(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

        }

        public async Task Invoke(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                //var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                //await mediator.Send(new AddStockTransferVoucherCommand(), cancellationToken);
            }
        }
    }
}
