using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;

namespace PostSys.Dal.ReadModel;

/// <summary>Представляет конфигурацию для типа <see cref="Package"/>.</summary>
public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
	#region Methods

	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Package> builder)
	{
		builder.ToTable("packages").HasKey(p => p.Id);
		builder.Property(c => c.Id).HasColumnName("package_id");
		builder.OwnsOne(p => p.Dimensions, dimensionBuilder =>
		{
			dimensionBuilder.Property(a => a.Length).HasColumnName("package_length");
			dimensionBuilder.Property(d => d.Height).HasColumnName("package_height");
			dimensionBuilder.Property(a => a.Weight).HasColumnName("package_weight");
		});
		builder.OwnsOne(p => p.Address, addressBuilder =>
		{
			addressBuilder.Property(a => a.Longitude).HasColumnName("package_longitude");
			addressBuilder.Property(a => a.Latitude).HasColumnName("package_latitude");
		});
		builder.Property(p => p.Status).HasConversion(
			v => v.ToString(),
			v => (PackageStatus)Enum.Parse(typeof(PackageStatus), v)
		).HasColumnName("package_status").IsRequired();
		builder.Property(p => p.PackageClientId).HasColumnName("package_client_id").IsRequired();
		builder.Property(p => p.PackagePostmanId).HasColumnName("package_postman_id").IsRequired();
	}

	#endregion
}