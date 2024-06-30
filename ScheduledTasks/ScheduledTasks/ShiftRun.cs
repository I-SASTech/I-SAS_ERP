using Focus.Business.HR.Payroll.ShiftRuns.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledTasks.ScheduledTasks
{
    public class ShiftRun : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public IMediator _mediator { get; }

        public ShiftRun(IServiceScopeFactory scopeFactory, IConfiguration configuration, IMediator mediator)
        {
            _scopeFactory = scopeFactory;
            Configuration = configuration;
            _mediator = mediator;

        }
        public string Schedule => "0 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new AddUpdateShiftRunCommand(), cancellationToken);
        }
    }
}
