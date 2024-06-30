using System.Security.Principal;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Interface;

namespace Focus.Business.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly string _userName;

        public RequestLogger(ILogger<TRequest> logger, IUserHttpContextProvider contextProvider)
        {
            _logger = logger;
            _userName = contextProvider.GetUserName();
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Novus Request: {Name} {@UserId} {@Request}",
                name, _userName, request);

            return Task.CompletedTask;
        }
    }
}
