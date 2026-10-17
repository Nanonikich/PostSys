using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using PostSys.Application.Commands.Postmen;
using PostSys.Application.Contracts;
using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.GraphQL.Attributes;

namespace PostSys.Service.Api.Postmen;

/// <summary>Определяет мутации для почтальонов.</summary>
[Authorize]
[MutationType]
public static class Mutations
{
    #region Methods

    /// <summary>Создает почтальона.</summary>
    [UseCreateMutationConvention]
    public static Task<Guid> CreatePostmanAsync(
        Fullname fullname,
        Guid packageId,
        string email,
        string password,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(
            new CreatePostmanCommand(ToDto(fullname), packageId, email, password),
            cancellationToken);
    }

    /// <summary>Обновляет данные почтальона.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePostmanAsync(
        Guid id,
        Guid postmanPackageId,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePostmanCommand(id, postmanPackageId), cancellationToken);
    }

    /// <summary>Обновляет данные об имени почтальона.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePostmanFullnameAsync(
        Guid id,
        Fullname fullname,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePostmanFullnameCommand(id, ToDto(fullname)), cancellationToken);
    }

    /// <summary>Обновляет Email почтальона.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePostmanEmailAsync(
        Guid id,
        string email,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePostmanEmailCommand(id, email), cancellationToken);
    }

    /// <summary>Обновляет пароль почтальона.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePostmanPasswordAsync(
        Guid id,
        string password,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePostmanPasswordCommand(id, password), cancellationToken);
    }

    /// <summary>Удаляет почтальона.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> DeletePostmanAsync(
        Guid id,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new DeletePostmanCommand(id), cancellationToken);
    }

    private static FullnameDto ToDto(Fullname fullname) =>
        new(fullname.Surname, fullname.Name, fullname.Patronymic);

    #endregion
}