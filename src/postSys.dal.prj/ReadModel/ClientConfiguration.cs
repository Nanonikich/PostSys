using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PostSys.ReadModels;

namespace PostSys.Dal.ReadModel;

/// <summary>Представляет конфигурацию для типа <see cref="Client"/>.</summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
	#region Methods

	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Client> builder)
	{
		builder.ToTable("clients").HasKey(c => c.Id);
		builder.Property(c => c.Id).HasColumnName("client_id");
		builder.OwnsOne(c => c.Fullname, nameBuilder =>
		{
			nameBuilder.Property(n => n.Surname).HasColumnName("client_surname");
			nameBuilder.Property(n => n.Name).HasColumnName("client_name");
			nameBuilder.Property(n => n.Patronymic).HasColumnName("client_patronymic");
		});
		builder.Property(c => c.PhoneNumber).HasColumnName("client_phone_number");
	}

	#endregion
}