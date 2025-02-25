using HotChocolate.Execution.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PostSys.Service.Common.GraphQL;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.Extensions;

/// <summary>Определяет расширения DI для использования GraphQL.</summary>
public static class GraphQlDependencyInjectionExtensions
{
	#region Methods

	/// <summary>Настраивает и регистрирует зависимости для использования GraphQL.</summary>
	/// <param name="collection">Объект <see cref="IServiceCollection"/>.</param>
	/// <typeparam name="TDbContext">Тип контекста БД.</typeparam>
	/// <returns>Объект <see cref="IRequestExecutorBuilder"/>.</returns>
	/// <remarks>При подключении к проекту, добавить .AddServiceTypes().</remarks>
	public static IRequestExecutorBuilder AddGraphQl<TDbContext>(this IServiceCollection collection)
		where TDbContext : DbContext
	{
		return collection
			.AddGraphQLServer()
			.RegisterDbContextFactory<TDbContext>()
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
				pagingOptions.DefaultPageSize = 1073741823;
				pagingOptions.MaxPageSize = 1073741823;
				pagingOptions.IncludeTotalCount = true;
			})
			.ConfigureSchema(sb => sb.ModifyOptions(opts => opts.StrictValidation = false))
			.ModifyRequestOptions(options => options.IncludeExceptionDetails = true);
	}

	#endregion
}