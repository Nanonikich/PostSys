using Autofac;

using PostSys.Application.Views.Controls.Plots;
using PostSys.Application.Views.Controls.StreetCity;
using PostSys.Application.Views.Controls;

namespace PostSys.Application.Extensions;

/// <summary>Расширение для регистрации элементов управления.</summary>
public static class ControlRegistrationExtension
{
	/// <summary>Регистрирует все элементы управления.</summary>
	/// <param name="builder">Контейнер сборки для регистрации.</param>
	public static void ControlsRegistration(ContainerBuilder builder)
	{
		builder
			.RegisterType<DgvAddressesControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<DgvPostmansControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<DgvSendersControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<DgvRecipientsControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<DgvUsersControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<CitiesControl>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<DgvCitiesControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<StreetsControl>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<DgvStreetsControl>()
			.AsSelf()
			.InstancePerLifetimeScope();

		builder
			.RegisterType<PlotOptionsControl>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<DgvPlotsControl>()
			.AsSelf()
			.InstancePerLifetimeScope();
	}
}
