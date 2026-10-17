using System;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostSys.Service.Common.GraphQL;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.Extensions;

/// <summary>Определяет расширения DI для использования GraphQL.</summary>
public static class GraphQlDependencyInjectionExtensions
{
    /// <summary>Настраивает и регистрирует зависимости для использования GraphQL.</summary>
    /// <param name="collection">Объект <see cref="IServiceCollection"/>.</param>
    /// <returns>Объект <see cref="IRequestExecutorBuilder"/>.</returns>
    public static IRequestExecutorBuilder AddGraphQl(this IServiceCollection collection)
    {
        return collection
            .AddGraphQLServer()
            .AddErrorFilter<GraphQlErrorFilter>()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .AddMutationConventions()
            .AddSubscriptionType<Subscription>()
            .AddInMemorySubscriptions()
            .AddserviceTypes()
            .ModifyPagingOptions(pagingOptions =>
            {
                pagingOptions.DefaultPageSize = 50;
                pagingOptions.MaxPageSize = 500;
                pagingOptions.IncludeTotalCount = true;
            })
            .ConfigureSchema(sb => sb.ModifyOptions(opts => opts.StrictValidation = false))
            .ModifyRequestOptions(options =>
            {
                options.IncludeExceptionDetails = true;
                options.ExecutionTimeout = TimeSpan.FromSeconds(30);
            });
    }
}