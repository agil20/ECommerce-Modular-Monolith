using Common.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceApi.Filters
{
    public class FuentValidatorFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public FuentValidatorFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                // Hər argument tipi üçün DI-da IValidator<T> varmı deyə baxırıq
                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                if (_serviceProvider.GetService(validatorType) is not IValidator validator)
                    continue;

                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument));

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);

                    context.Result = new BadRequestObjectResult(
                        new ApiResponseModel(false, 400, string.Join(" | ", errors)));

                    return;
                }
            }

            await next();
        }
    }
}
