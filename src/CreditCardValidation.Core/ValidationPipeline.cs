using FluentValidation.Results;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using CreditCardValidation.Core.Contracts;

namespace CreditCardValidation.Core;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : new()
{
    private readonly INotifier _notifier;
    private readonly ILogger<ValidationPipeline<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationPipeline(
    INotifier notifier,
        ILogger<ValidationPipeline<TRequest, TResponse>> logger,
        IEnumerable<IValidator<TRequest>>? validators)
    {
        _notifier = notifier;
        _logger = logger;
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators is null) return next();

        var errors = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
        .ToList();

        _logger.LogDebug("{RequestType} - Validated with {ValidationErrorQuantity} error(s)",
            typeof(TRequest).Name,
            errors.Count);

        return errors.Any() ? ReturnError(errors) : next();
    }

    private Task<TResponse> ReturnError(IEnumerable<ValidationFailure> failures)
    {
        var result = new TResponse();

        foreach (var fail in failures)
        {
            _logger.LogInformation("{RequestType} - Validation error: {ValidationError}",
                typeof(TRequest).Name,
                fail.ErrorMessage);

            _notifier.Notify(fail.ErrorMessage);
        }

        return Task.FromResult(result);
    }
}
