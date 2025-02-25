using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PostSys.Domain.Postmen;

namespace PostSys.Dal.Persistence.Postmen;

/// <summary>Представляет конфигурацию для типа <see cref="Postman"/>.</summary>
public class PostmanConfiguration : IEntityTypeConfiguration<Postman>
{
	#region Methods

	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Postman> builder)
	{
		builder.ToTable("postmen").HasKey(p => p.Id);
		builder.Property(p => p.Id).HasColumnName("postman_id");
		builder.OwnsOne(p => p.Fullname, nameBuilder =>
		{
			nameBuilder.Property(n => n.Surname).HasColumnName("postman_surname").IsRequired();
			nameBuilder.Property(n => n.Name).HasColumnName("postman_name").IsRequired();
			nameBuilder.Property(n => n.Patronymic).HasColumnName("postman_patronymic");
		});
		builder.Property(p => p.PostmanPackageId).HasColumnName("postman_package_id");
		builder.OwnsOne(p => p.Email,
						emailBuilder =>
						{
							emailBuilder.Property(a => a.Value).HasColumnName("postman_email").IsRequired();
						});
		builder.OwnsOne(p => p.Password,
						passwordBuilder =>
						{
							passwordBuilder.Property(a => a.Value).HasColumnName("postman_password").IsRequired();
						});
	}

	#endregion
}