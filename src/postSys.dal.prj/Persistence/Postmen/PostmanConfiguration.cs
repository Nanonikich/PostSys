using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Postmen;

namespace PostSys.Dal.Persistence.Postmen;

/// <summary>Представляет конфигурацию для типа <see cref="Postman"/>.</summary>
public class PostmanConfiguration : IEntityTypeConfiguration<Postman>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Postman> builder)
    {
        builder.ToTable("postmen").HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("postman_id");
        builder.Ignore(nameof(AggregateRoot.DomainEvents));
        builder.OwnsOne(p => p.Fullname, nameBuilder =>
        {
            nameBuilder.Property(n => n.Surname).HasColumnName("postman_surname").IsRequired();
            nameBuilder.Property(n => n.Name).HasColumnName("postman_name").IsRequired();
            nameBuilder.Property(n => n.Patronymic).HasColumnName("postman_patronymic");
        });
        builder.Property(p => p.PostmanPackageId).HasColumnName("postman_package_id");
        builder.HasIndex(p => p.PostmanPackageId)
            .IsUnique()
            .HasFilter("postman_package_id <> '00000000-0000-0000-0000-000000000000' AND NOT is_deleted")
            .HasDatabaseName("ix_postmen_postman_package_id_unique");

        builder.OwnsOne(p => p.Email,
                        emailBuilder =>
                        {
                            emailBuilder.Property(a => a.Value).HasColumnName("postman_email").IsRequired();
                            emailBuilder.HasIndex(a => a.Value)
                                .IsUnique()
                                .HasDatabaseName("ix_postmen_postman_email_unique");
                        });
        builder.OwnsOne(p => p.Password,
                        passwordBuilder =>
                        {
                            passwordBuilder.Property(a => a.Value).HasColumnName("postman_password").IsRequired();
                        });
    }
}