using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Application;
using CSM.UseCases.Abstractions.Authentication;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CSM.UseCases.Abstractions.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators, 
    IUserProvider userProvider, 
    IErrorMessageRepository errorMessageRepository,
    ITransformer transformer)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    /// <summary>
    /// 1. Validate request
    /// 2. If there is any errors, return validation result
    /// 3. Otherwise, return next();
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var validationResults = await Task.WhenAll(validators
            .Select(validator => validator.ValidateAsync(request, cancellationToken)));

        var failures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();
        
        if (failures.Length > 0)
        {
            var errorMessages = await GetErrorMessages(cancellationToken, failures);

            var error = new ValidationError(errorMessages
                .Select(f => Error.Validation(((int)f.ErrorCode).ToString(), f.Details))
                .ToArray());

            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];

                var failureMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result<object>.ValidationFailure));

                if (failureMethod is not null)
                {
                    return (TResponse)failureMethod.Invoke(null, [error])!;
                }
            }
            else if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)Result.Failure(error);
            }
            else
            {
                throw new ValidationException(failures);
            }
        }

        return await next(cancellationToken);
    }

    /// <summary>
    /// To get list error messages by language string, list error codes
    /// </summary>
    /// <returns></returns>
    private async Task<IReadOnlyList<ErrorMessage>> GetErrorMessages(CancellationToken cancellationToken, ValidationFailure[] failures)
    {
        var errorCodes = failures
            .Select(failure => transformer.FromErrorCodeStringToEnum(failure.ErrorCode))
            .ToList();

        var language = userProvider.GetLanguageFromHeader();

        var languageEnum = transformer.FromLanguageStringToEnum(language);
            
        var errorMessages = await errorMessageRepository
            .GetErrorMessageByErrorCodesAndLanguageTypeAsync(errorCodes, languageEnum, cancellationToken);
        return errorMessages;
    }
}