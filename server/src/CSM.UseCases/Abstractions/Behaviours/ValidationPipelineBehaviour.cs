using CSM.Core.Common;
using FluentValidation;
using MediatR;

namespace CSM.UseCases.Abstractions.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    /// <summary>
    /// 1. Validate request
    /// 2. If there is any errors, return validation result
    /// 3. Otherwise, return next();
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(cancellationToken);
        }

        var validationResults = await Task.WhenAll(_validators
            .Select(validator => validator.ValidateAsync(request, cancellationToken)));

        var failures  = validationResults
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationResult => validationResult is not null)
            .ToArray();
        
        if (failures.Length > 0)
        {
            var error = new ValidationError(failures
                .Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage))
                .ToArray());

            return (TResponse)Result.Failure(error);
        }

        return await next(cancellationToken);
    }

}