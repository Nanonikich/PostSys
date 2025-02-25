using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PostSys.Domain.Contracts;
using PostSys.Domain.Packages;

namespace PostSys.Dal.Persistence.Packages;

/// <summary>Представляет конфигурацию для типа <see cref="Package"/>.</summary>
public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
	#region Methods

	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Package> builder)
	{
		builder.ToTable("packages").HasKey(p => p.Id);
		builder.Property(p => p.Id).HasColumnName("package_id");
		builder.OwnsOne(p => p.Dimensions, dimensionBuilder =>
		{
			dimensionBuilder.Property(a => a.Length).HasColumnName("package_length");
			dimensionBuilder.Property(d => d.Height).HasColumnName("package_height");
			dimensionBuilder.Property(a => a.Weight).HasColumnName("package_weight").IsRequired();
		});
		builder.OwnsOne(p => p.Address, addressBuilder =>
		{
			addressBuilder.Property(a => a.Longitude).HasColumnName("package_longitude").IsRequired();
			addressBuilder.Property(a => a.Latitude).HasColumnName("package_latitude").IsRequired();
		});
		builder.OwnsOne(p => p.Status, statusBuilder =>
		{
			statusBuilder
				.Property(a => a.Value)
				.HasColumnName("package_status")
				.HasConversion(
					v => v.ToString(),
					v => (PackageStatus)Enum.Parse(typeof(PackageStatus), v, true))
				.IsRequired();
		});
		builder.Property(p => p.PackageClientId).HasColumnName("package_client_id").IsRequired();
		builder.Property(p => p.PackagePostmanId).HasColumnName("package_postman_id").IsRequired();

		builder
			.HasOne(p => p.Postman)
			.WithMany(pm => pm.Packages)
			.HasForeignKey(p => p.PackagePostmanId)
			.OnDelete(DeleteBehavior.NoAction);
	}

	#endregion
}