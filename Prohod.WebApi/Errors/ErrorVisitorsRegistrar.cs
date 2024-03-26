using Microsoft.AspNetCore.Mvc;
using Prohod.Domain.ErrorsBase;
using Prohod.Infrastructure.Accounts.Errors;

namespace Prohod.WebApi.Errors;

public static class ErrorVisitorsRegistrar
{
    public static IServiceCollection AddOperationErrorVisitor(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IOperationErrorVisitor<ActionResult>, OperationErrorVisitor>()
            .AddSingleton<IAccountsServiceErrorVisitor<ActionResult>, AccountsServiceErrorVisitor>();
    }
}