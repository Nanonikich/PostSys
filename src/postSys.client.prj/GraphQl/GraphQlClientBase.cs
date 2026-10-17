using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;

namespace PostSys.Client.GraphQl;

/// <summary>Базовый клиент GraphQL поверх общего <see cref="GraphQlConnection"/>.</summary>
public abstract class GraphQlClientBase(GraphQlConnection connection)
{
    /// <summary>Общее подключение к сервису.</summary>
    protected GraphQlConnection Connection { get; } = connection ?? throw new ArgumentNullException(nameof(connection));

    /// <summary>Выполняет query и проверяет ошибки GraphQL.</summary>
    protected async Task<TResponse> SendQueryAsync<TResponse>(
        GraphQLRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await Connection.Client.SendQueryAsync<TResponse>(request, cancellationToken);
            EnsureNoErrors(response);
            return response.Data ?? throw new GraphQlClientException("Empty GraphQL response.");
        }
        catch(GraphQlClientException)
        {
            throw;
        }
        catch(Exception ex)
        {
            throw new GraphQlClientException("Error during GraphQL request.", ex);
        }
    }

    /// <summary>Выполняет mutation и проверяет ошибки GraphQL.</summary>
    protected async Task<TResponse> SendMutationAsync<TResponse>(
        GraphQLRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await Connection.Client.SendMutationAsync<TResponse>(request, cancellationToken);
            EnsureNoErrors(response);
            return response.Data ?? throw new GraphQlClientException("Empty GraphQL response.");
        }
        catch(GraphQlClientException)
        {
            throw;
        }
        catch(Exception ex)
        {
            throw new GraphQlClientException("Error during GraphQL request.", ex);
        }
    }

    private static void EnsureNoErrors<TResponse>(GraphQLResponse<TResponse> response)
    {
        if(response.Errors is { Length: > 0 })
        {
            var errorMessages = response.Errors.Select(error => error.Message);
            throw new GraphQlClientException("GraphQL errors occurred: " + string.Join(", ", errorMessages));
        }
    }
}