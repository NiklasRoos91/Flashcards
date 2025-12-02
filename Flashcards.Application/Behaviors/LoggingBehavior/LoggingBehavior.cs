using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Flashcards.Application.Behaviors.LoggingBehavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            // Logga att begäran hanteras
            _logger.LogInformation("Handling {RequestName} with {@Request}", typeof(TRequest).Name, request);

            try
            {
                var response = await next(cancellationToken);

                stopwatch.Stop();
                // Logga att begäran har hanterats framgångsrikt
                _logger.LogInformation("Handled {RequestName} with {@Response} in {ElapsedMilliseconds} ms",
                    typeof(TRequest).Name, response, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                // Logga ett fel om något gick fel under hanteringen
                _logger.LogError(ex, "Error handling {RequestName} with {@Request} after {ElapsedMilliseconds} ms",
                    typeof(TRequest).Name, request, stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
