using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolate.Execution;

namespace PostSys.IntegrationTests.Api.Extensions;

/// <summary>Выполнение GraphQL с тестовым пользователем (для [Authorize] на mutations).</summary>
public static class GraphQlTestExecute
{
    private static readonly ClaimsPrincipal TestUser = new(
        new ClaimsIdentity(
            [
                new(ClaimTypes.NameIdentifier, "00000000-0000-0000-0000-000000000001"),
                new("postman_id", "00000000-0000-0000-0000-000000000001")
            ],
            authenticationType: "Test"));

    /// <summary>Выполняет документ с аутентифицированным контекстом.</summary>
    public static Task<IExecutionResult> ExecuteAsync(
        IRequestExecutor executor,
        string document,
        IReadOnlyDictionary<string, object?>? variables = null)
    {
        var builder = OperationRequestBuilder.New()
            .SetDocument(document)
            .SetUser(TestUser);

        if(variables is not null)
            builder.SetVariableValues(variables);

        return executor.ExecuteAsync(builder.Build());
    }
}