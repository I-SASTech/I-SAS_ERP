using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly string _userName;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, IUserHttpContextProvider contextProvider)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _userName = contextProvider.GetUserName();
        }

        //public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        //{
            
        //}

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                _logger.LogWarning("Noble Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                    name, _timer.ElapsedMilliseconds, _userName, request);
            }

            return response;
        }
    }
}
