using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using PostSys.Application.Commands.Packages;
using PostSys.Application.Contracts;
using PostSys.Service.Common.Converters;
using PostSys.Service.Common.GraphQL.Attributes;
using DomainPackageStatus = PostSys.Domain.Contracts.PackageStatus;
using ReadPackageStatus = PostSys.ReadModels.Contracts.PackageStatus;
using ReadAddress = PostSys.ReadModels.Helpers.Address;
using ReadDimensions = PostSys.ReadModels.Helpers.Dimensions;

namespace PostSys.Service.Api.Packages;

/// <summary>Определяет мутации для посылок.</summary>
[Authorize]
[MutationType]
public static class Mutations
{
    #region Methods

    /// <summary>Создает посылку.</summary>
    [UseCreateMutationConvention]
    public static Task<Guid> CreatePackageAsync(
        ReadDimensions dimensions,
        ReadAddress address,
        ReadPackageStatus status,
        Guid clientId,
        Guid postmanId,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(
            new CreatePackageCommand(
                ToDto(dimensions),
                ToDto(address),
                EnumConverter<ReadPackageStatus, DomainPackageStatus>.Convert(status),
                clientId,
                postmanId),
            cancellationToken);
    }

    /// <summary>Обновляет данные о посылке.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePackageAsync(
        Guid id,
        Guid clientId,
        Guid postmanId,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePackageCommand(id, clientId, postmanId), cancellationToken);
    }

    /// <summary>Обновляет параметры посылки.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePackageDimensionsAsync(
        Guid id,
        ReadDimensions dimensions,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePackageDimensionsCommand(id, ToDto(dimensions)), cancellationToken);
    }

    /// <summary>Обновляет местоположение посылки.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePackageAddressAsync(
        Guid id,
        ReadAddress address,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new ChangePackageAddressCommand(id, ToDto(address)), cancellationToken);
    }

    /// <summary>Обновляет статус посылки.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> ChangePackageStatusAsync(
        Guid id,
        ReadPackageStatus status,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(
            new ChangePackageStatusCommand(
                id,
                EnumConverter<ReadPackageStatus, DomainPackageStatus>.Convert(status)),
            cancellationToken);
    }

    /// <summary>Удаляет посылку.</summary>
    [UseSuccessMutationConvention]
    public static Task<bool> DeletePackageAsync(
        Guid id,
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        return sender.Send(new DeletePackageCommand(id), cancellationToken);
    }

    private static DimensionsDto ToDto(ReadDimensions dimensions) =>
        new(dimensions.Length, dimensions.Height, dimensions.Weight);

    private static AddressDto ToDto(ReadAddress address) =>
        new(address.Longitude, address.Latitude);

    #endregion
}