using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PostSys.ReadModels;

namespace PostSys.Dal.ReadModel;

/// <summary>Представляет конфигурацию для типа <see cref="Postman"/>.</summary>
public class PostmanConfiguration : IEntityTypeConfiguration<Postman>
{
	#region Methods

	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Postman> builder)
	{
		builder.ToTable("postmen").HasKey(p => p.Id);
		builder.Property(c => c.Id).HasColumnName("postman_id");
		builder.OwnsOne(p => p.Fullname, nameBuilder =>
		{
			nameBuilder.Property(n => n.Surname).HasColumnName("postman_surname");
			nameBuilder.Property(n => n.Name).HasColumnName("postman_name");
			nameBuilder.Property(n => n.Patronymic).HasColumnName("postman_patronymic");
		});
		builder.Property(p => p.PostmanPackageId).HasColumnName("postman_package_id");
		builder.Property(c => c.Email).HasColumnName("postman_email");
		builder.Property(c => c.Password).HasColumnName("postman_password");
	}

	#endregion
}