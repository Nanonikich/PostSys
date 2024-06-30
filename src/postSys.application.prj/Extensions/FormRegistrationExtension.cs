using Autofac;

using PostSys.Application.Views.Forms.EditingForms;
using PostSys.Application.Views.Forms;

namespace PostSys.Application.Extensions;

/// <summary>Расширение для регистрации форм.</summary>
public static class FormRegistrationExtension
{
	/// <summary>Регистрирует все формы.</summary>
	/// <param name="builder">Контейнер сборки для регистрации.</param>
	public static void FormsRegistration(ContainerBuilder builder)
	{
		builder
			.RegisterType<AuthentificationForm>()
			.AsSelf();

		builder
			.RegisterType<MainForm>()
			.AsSelf();

		builder
			.RegisterType<PostmansForm>()
			.AsSelf();

		builder
			.RegisterType<SendersForm>()
			.AsSelf();

		builder
			.RegisterType<RecipientsForm>()
			.AsSelf();

		builder
			.RegisterType<StreetCityForm>()
			.AsSelf();

		builder
			.RegisterType<PlotsForm>()
			.AsSelf();

		builder
			.RegisterType<UsersForm>()
			.AsSelf();

		builder
			.RegisterType<EditAddressForm>()
			.InstancePerDependency()
			.AsSelf();

		builder
			.RegisterType<EditPostmanForm>()
			.AsSelf();

		builder
			.RegisterType<EditRecipientForm>()
			.AsSelf();

		builder
			.RegisterType<EditSenderForm>()
			.AsSelf();

		builder
			.RegisterType<EditUserForm>()
			.AsSelf();
	}
}
