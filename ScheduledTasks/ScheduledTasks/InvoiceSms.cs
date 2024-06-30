using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduledTasks.Interface;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using MediatR;
using Focus.Business.Sales.Queries.SendSmsQuery;

namespace ScheduledTasks.ScheduledTasks
{
    internal class InvoiceSms : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public ILogger<PushDataService> _logger { get; }
        public IMediator _mediator { get; }

        public InvoiceSms(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<PushDataService> logger, IMediator mediator)
        {
            _scopeFactory = scopeFactory;
            Configuration = configuration;
            _logger = logger;
            _mediator = mediator;

        }
        public string Schedule => "*/1 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new SendInvoiceSmsQuery(), cancellationToken);
            }
        }
    }
}
