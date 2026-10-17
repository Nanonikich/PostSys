using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Application.Abstractions;
using PostSys.Domain.Events;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.EventHandlers;

/// <summary>Публикует создание клиента в GraphQL-подписки.</summary>
public sealed class ClientCreatedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<ClientCreated>
{
	/// <inheritdoc/>
	public Task HandleAsync(ClientCreated domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityCreated),
			new EntityCreationMessageModel("client", domainEvent.ClientId),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение ФИО клиента в GraphQL-подписки.</summary>
public sealed class ClientFullnameChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<ClientFullnameChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(ClientFullnameChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"client",
				"changeFullname",
				domainEvent.ClientId,
				JsonSerializer.Serialize(domainEvent.Fullname)),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение телефона клиента в GraphQL-подписки.</summary>
public sealed class ClientPhoneNumberChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<ClientPhoneNumberChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(ClientPhoneNumberChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"client",
				"changePhoneNumber",
				domainEvent.ClientId,
				domainEvent.PhoneNumber),
			cancellationToken).AsTask();
}

/// <summary>Публикует удаление клиента в GraphQL-подписки.</summary>
public sealed class ClientDeletedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<ClientDeleted>
{
	/// <inheritdoc/>
	public Task HandleAsync(ClientDeleted domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityDeleted),
			new EntityDeletionMessageModel("client", domainEvent.ClientId),
			cancellationToken).AsTask();
}

/// <summary>Публикует создание посылки в GraphQL-подписки.</summary>
public sealed class PackageCreatedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PackageCreated>
{
	/// <inheritdoc/>
	public Task HandleAsync(PackageCreated domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityCreated),
			new EntityCreationMessageModel("package", domainEvent.PackageId),
			cancellationToken).AsTask();
}

/// <summary>Публикует смену клиента/почтальона посылки в GraphQL-подписки.</summary>
public sealed class PackageAssignmentsChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PackageAssignmentsChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PackageAssignmentsChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityChanged),
			new EntityChangeMessageModel("package", domainEvent.PackageId),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение габаритов посылки в GraphQL-подписки.</summary>
public sealed class PackageDimensionsChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PackageDimensionsChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PackageDimensionsChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"package",
				"changeDimensions",
				domainEvent.PackageId,
				JsonSerializer.Serialize(domainEvent.Dimensions)),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение адреса посылки в GraphQL-подписки.</summary>
public sealed class PackageAddressChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PackageAddressChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PackageAddressChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"package",
				"changeAddress",
				domainEvent.PackageId,
				JsonSerializer.Serialize(domainEvent.Address)),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение статуса посылки в GraphQL-подписки.</summary>
public sealed class PackageStatusChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PackageStatusChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PackageStatusChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"package",
				"changeStatus",
				domainEvent.PackageId,
				JsonSerializer.Serialize(domainEvent.Status.Value)),
			cancellationToken).AsTask();
}

/// <summary>Публикует удаление посылки в GraphQL-подписки.</summary>
public sealed class PackageDeletedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PackageDeleted>
{
	/// <inheritdoc/>
	public Task HandleAsync(PackageDeleted domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityDeleted),
			new EntityDeletionMessageModel("package", domainEvent.PackageId),
			cancellationToken).AsTask();
}

/// <summary>Публикует создание почтальона в GraphQL-подписки.</summary>
public sealed class PostmanCreatedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PostmanCreated>
{
	/// <inheritdoc/>
	public Task HandleAsync(PostmanCreated domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityCreated),
			new EntityCreationMessageModel("postman", domainEvent.PostmanId),
			cancellationToken).AsTask();
}

/// <summary>Публикует смену посылки почтальона в GraphQL-подписки.</summary>
public sealed class PostmanPackageChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PostmanPackageChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PostmanPackageChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityChanged),
			new EntityChangeMessageModel("postman", domainEvent.PostmanId),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение ФИО почтальона в GraphQL-подписки.</summary>
public sealed class PostmanFullnameChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PostmanFullnameChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PostmanFullnameChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"postman",
				"changeFullname",
				domainEvent.PostmanId,
				JsonSerializer.Serialize(domainEvent.Fullname)),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение email почтальона в GraphQL-подписки.</summary>
public sealed class PostmanEmailChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PostmanEmailChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PostmanEmailChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"postman",
				"changeEmail",
				domainEvent.PostmanId,
				domainEvent.Email),
			cancellationToken).AsTask();
}

/// <summary>Публикует изменение пароля почтальона в GraphQL-подписки (без значения пароля).</summary>
public sealed class PostmanPasswordChangedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PostmanPasswordChanged>
{
	/// <inheritdoc/>
	public Task HandleAsync(PostmanPasswordChanged domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityParameterChanged),
			new EntityParameterChangeMessageModel(
				"postman",
				"changePassword",
				domainEvent.PostmanId,
				string.Empty),
			cancellationToken).AsTask();
}

/// <summary>Публикует удаление почтальона в GraphQL-подписки.</summary>
public sealed class PostmanDeletedSubscriptionHandler(ITopicEventSender topicEventSender)
	: IDomainEventHandler<PostmanDeleted>
{
	/// <inheritdoc/>
	public Task HandleAsync(PostmanDeleted domainEvent, CancellationToken cancellationToken = default) =>
		topicEventSender.SendAsync(
			nameof(Subscription.OnEntityDeleted),
			new EntityDeletionMessageModel("postman", domainEvent.PostmanId),
			cancellationToken).AsTask();
}
