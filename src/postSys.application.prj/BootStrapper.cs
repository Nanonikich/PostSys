using Autofac;

using PostSys.Application.Context;
using PostSys.Application.Extensions;
using PostSys.Application.Helpers;

namespace PostSys.Application;

internal class BootStrapper
{
	public IContainer BootStrap
	{
		get
		{
			var builder = new ContainerBuilder();

			builder
				.RegisterType<PostSysContext>()
				.WithParameter("options", DbContextOptionsFactory.Get())
				.SingleInstance();

			builder.RegisterType<UserInfo>()
				.AsSelf()
				.SingleInstance();

			FormRegistrationExtension.FormsRegistration(builder);
			ControlRegistrationExtension.ControlsRegistration(builder);

			return builder.Build();
		}
	}
}
