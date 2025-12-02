using FluentValidation;
using MediatR;

namespace Flashcards.Application.Behaviors.ValidationBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Om det inte finns några validatorer, fortsätt utan validering
            if (!_validators.Any())
                return await next();

            // Skapar en valideringskontext som skickas till validatorerna
            var context = new ValidationContext<TRequest>(request);

            // Kör alla validatorer parallellt och samlar resultaten
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Samlar alla felmeddelanden från valideringsresultaten
            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f is not null)
                .ToList();

            // Om det finns några valideringsfel, kasta ett undantag
            if (failures.Any())
            {
                // Skapa en lista med felmeddelanden
                var failureMessages = failures.Select(f => f.ErrorMessage).ToList();
                // Om TResponse har en statisk metod Failure som tar en lista med felmeddelanden, anropa den
                var failureMethod = typeof(TResponse).GetMethod("Failure", new[] { typeof(List<string>) });
                // Om metoden finns, anropa den och returnera resultatet
                return (TResponse)failureMethod!.Invoke(null, new object[] { failureMessages })!;
            }

            // Om inga fel, fortsätt med nästa steg i pipeline
            return await next();
        }
    }
}
