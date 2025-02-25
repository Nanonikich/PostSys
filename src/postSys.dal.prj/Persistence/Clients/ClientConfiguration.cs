using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PostSys.Domain.Clients;

namespace PostSys.Dal.Persistence.Clients;

/// <summary>Представляет конфигурацию для типа <see cref="Client" />.</summary>
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
			nameBuilder.Property(n => n.Surname).HasColumnName("client_surname").IsRequired();
			nameBuilder.Property(n => n.Name).HasColumnName("client_name").IsRequired();
			nameBuilder.Property(n => n.Patronymic).HasColumnName("client_patronymic");
		});
		builder.OwnsOne(c => c.PhoneNumber,
						phoneBuilder =>
						{
							phoneBuilder.Property(p => p.Value).HasColumnName("client_phone_number").IsRequired();
						});

		builder
			.HasMany(c => c.Packages)
			.WithOne(p => p.Client)
			.HasForeignKey(p => p.PackageClientId)
			.OnDelete(DeleteBehavior.NoAction);
	}

	#endregion
}