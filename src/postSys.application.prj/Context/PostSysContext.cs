using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

using Serilog;

using PostSys.Models;

namespace PostSys.Application.Context;

/// <summary>Контекст базы данных.</summary>
public partial class PostSysContext : DbContext
{
	private static readonly ILogger Log = Serilog.Log.ForContext<PostSysContext>();

	#region Tables

	/// <summary>Таблица, содержащая адреса.</summary>
	public virtual DbSet<Address> Address { get; set; } = null!;

	/// <summary>Таблица, содержащая города.</summary>
	public virtual DbSet<City> City { get; set; } = null!;

	/// <summary>Таблица, содержащая коды адресов.</summary>
	public virtual DbSet<AddressCode> AddressCode { get; set; } = null!;

	/// <summary>Таблица, содержащая информацию о почтальонах.</summary>
	public virtual DbSet<Postman> Postman { get; set; } = null!;

	/// <summary>Таблица, содержащая информацию о получателях.</summary>
	public virtual DbSet<Recipient> Recipient { get; set; } = null!;

	/// <summary>Таблица, содержащая информацию об отправителях.</summary>
	public virtual DbSet<Sender> Sender { get; set; } = null!;

	/// <summary>Таблица, содержащая информацию о ролях пользователей в системе.</summary>
	public virtual DbSet<Role> Role { get; set; } = null!;

	/// <summary>Таблица, содержащая названия улиц.</summary>
	public virtual DbSet<Street> Street { get; set; } = null!;

	/// <summary>Таблица, содержащая информацию о пользователях системы.</summary>
	public virtual DbSet<User> Users { get; set; } = null!;

	#endregion

	public PostSysContext()
	{
			
	}

	/// <summary>Создаёт экземпляр класса <see cref="PostSysContext"/>.</summary>
	/// <param name="options">Настройки контекста базы данных.</param>
	public PostSysContext(DbContextOptions<PostSysContext> options) : base(options)
	{
		try
		{
			if(Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator)
			{
				if(!databaseCreator.CanConnect()) databaseCreator.Create();

				if(!databaseCreator.HasTables()) databaseCreator.CreateTables();
			}
		}
		catch(Exception ex)
		{
			Log.Error(ex.Message);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Address>(entity =>
		{
			entity.HasKey(e => e.AddressId).HasName("PK__Addresse__CAA247C8C328556F");

			entity.ToTable("Address");

			entity.Property(e => e.AddressId).HasColumnName("address_id");
			entity.Property(e => e.AddressApartment)
				.HasMaxLength(10)
				.HasColumnName("address_apartment");
			entity.Property(e => e.AddressCity).HasColumnName("address_city");
			entity.Property(e => e.AddressGoods).HasColumnName("address_goods");
			entity.Property(e => e.AddressHome)
				.HasMaxLength(5)
				.HasColumnName("address_home");
			entity.Property(e => e.AddressPlot).HasColumnName("address_plot");
			entity.Property(e => e.AddressPostman).HasColumnName("address_postman");
			entity.Property(e => e.AddressRecipient).HasColumnName("address_recipient");
			entity.Property(e => e.AddressStreet).HasColumnName("address_street");

			entity.HasOne(d => d.AddressPostmanNavigation).WithMany(p => p.Addresses)
				.HasForeignKey(d => d.AddressPostman)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Addresses__addre__38996AB5");

			entity.HasOne(d => d.AddressRecipientNavigation).WithMany(p => p.Addresses)
				.HasForeignKey(d => d.AddressRecipient)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Addresses__addre__398D8EEE");
		});

		modelBuilder.Entity<AddressCode>(entity =>
		{
			entity.HasKey(e => e.AddressCodeId).HasName("PK__CodesAdd__BE268BC3327DB06A");

			entity.ToTable("AddressCode");

			entity.Property(e => e.AddressCodeId).HasColumnName("addressCode_id");
			entity.Property(e => e.AddressCodeCity).HasColumnName("addressCode_city");
			entity.Property(e => e.AddressCodeHouses).HasColumnName("addressCode_houses");
			entity.Property(e => e.AddressCodePlot).HasColumnName("addressCode_plot");
			entity.Property(e => e.AddressCodeStreet).HasColumnName("addressCode_street");

			entity.HasOne(d => d.AddressCodeCityNavigation).WithMany(p => p.AddressCodes)
				.HasForeignKey(d => d.AddressCodeCity)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__CodesAddr__codeA__2A4B4B5E");

			entity.HasOne(d => d.AddressCodeStreetNavigation).WithMany(p => p.AddressCodes)
				.HasForeignKey(d => d.AddressCodeStreet)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__CodesAddr__codeA__2B3F6F97");
		});

		modelBuilder.Entity<City>(entity =>
		{
			entity.HasKey(e => e.CityId).HasName("PK__City__031491A85A782507");

			entity.ToTable("City");

			entity.HasIndex(e => e.CityName, "UQ__City__1AA4F7B5A1FD7F67").IsUnique();

			entity.Property(e => e.CityId).HasColumnName("city_id");
			entity.Property(e => e.CityName)
				.HasMaxLength(30)
				.HasColumnName("city_name");
		});

		modelBuilder.Entity<Postman>(entity =>
		{
			entity.HasKey(e => e.PostmanId).HasName("PK__Postmens__E98A28B89FC6A52A");

			entity.ToTable("Postman");

			entity.Property(e => e.PostmanId).HasColumnName("postman_id");
			entity.Property(e => e.PostmanName)
				.HasMaxLength(30)
				.HasColumnName("postman_name");
			entity.Property(e => e.PostmanPatronymic)
				.HasMaxLength(35)
				.HasColumnName("postman_patronymic");
			entity.Property(e => e.PostmanPhone)
				.HasMaxLength(60)
				.HasColumnName("postman_phone");
			entity.Property(e => e.PostmanPlot).HasColumnName("postman_plot");
			entity.Property(e => e.PostmanSurname)
				.HasMaxLength(30)
				.HasColumnName("postman_surname");
		});

		modelBuilder.Entity<Recipient>(entity =>
		{
			entity.HasKey(e => e.RecipientId).HasName("PK__Recipien__FA0A40277007F6BD");

			entity.ToTable("Recipient");

			entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
			entity.Property(e => e.RecipientApartment).HasColumnName("recipient_apartment");
			entity.Property(e => e.RecipientCity).HasColumnName("recipient_city");
			entity.Property(e => e.RecipientHome)
				.HasMaxLength(5)
				.HasColumnName("recipient_home");
			entity.Property(e => e.RecipientName)
				.HasMaxLength(30)
				.HasColumnName("recipient_name");
			entity.Property(e => e.RecipientNumber)
				.HasMaxLength(6)
				.HasColumnName("recipient_number");
			entity.Property(e => e.RecipientPatronymic)
				.HasMaxLength(35)
				.HasColumnName("recipient_patronymic");
			entity.Property(e => e.RecipientPhone)
				.HasMaxLength(60)
				.HasColumnName("recipient_phone");
			entity.Property(e => e.RecipientSender).HasColumnName("recipient_sender");
			entity.Property(e => e.RecipientSeries)
				.HasMaxLength(4)
				.HasColumnName("recipient_series");
			entity.Property(e => e.RecipientStreet).HasColumnName("recipient_street");
			entity.Property(e => e.RecipientSurname)
				.HasMaxLength(30)
				.HasColumnName("recipient_surname");

			entity.HasOne(d => d.RecipientCityNavigation).WithMany(p => p.Recipients)
				.HasForeignKey(d => d.RecipientCity)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Recipient__recip__34C8D9D1");

			entity.HasOne(d => d.RecipientSenderNavigation).WithMany(p => p.Recipients)
				.HasForeignKey(d => d.RecipientSender)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Recipient__recip__33D4B598");

			entity.HasOne(d => d.RecipientStreetNavigation).WithMany(p => p.Recipients)
				.HasForeignKey(d => d.RecipientStreet)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Recipient__recip__35BCFE0A");
		});

		modelBuilder.Entity<Role>(entity =>
		{
			entity.HasKey(e => e.RoleId).HasName("PK__Roles__3683B531D404F724");

			entity.ToTable("Role");

			entity.HasIndex(e => e.RoleName, "UQ__Roles__501B3753F4D58345").IsUnique();

			entity.Property(e => e.RoleId).HasColumnName("role_id");
			entity.Property(e => e.RoleName)
				.HasMaxLength(20)
				.HasColumnName("role_name");
		});

		modelBuilder.Entity<Sender>(entity =>
		{
			entity.HasKey(e => e.SenderId).HasName("PK__Sender__536334E15E7B15F7");

			entity.ToTable("Sender");

			entity.Property(e => e.SenderId).HasColumnName("sender_id");
			entity.Property(e => e.SenderApartment).HasColumnName("sender_apartment");
			entity.Property(e => e.SenderCity).HasColumnName("sender_city");
			entity.Property(e => e.SenderHome)
				.HasMaxLength(5)
				.HasColumnName("sender_home");
			entity.Property(e => e.SenderName)
				.HasMaxLength(30)
				.HasColumnName("sender_name");
			entity.Property(e => e.SenderPatronymic)
				.HasMaxLength(35)
				.HasColumnName("sender_patronymic");
			entity.Property(e => e.SenderPhone)
				.HasMaxLength(60)
				.HasColumnName("sender_phone");
			entity.Property(e => e.SenderStreet).HasColumnName("sender_street");
			entity.Property(e => e.SenderSurname)
				.HasMaxLength(30)
				.HasColumnName("sender_surname");

			entity.HasOne(d => d.SenderCityNavigation).WithMany(p => p.Senders)
				.HasForeignKey(d => d.SenderCity)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Sender__sender_c__6C190EBB");

			entity.HasOne(d => d.SenderStreetNavigation).WithMany(p => p.Senders)
				.HasForeignKey(d => d.SenderStreet)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Sender__sender_s__30F848ED");
		});

		modelBuilder.Entity<Street>(entity =>
		{
			entity.HasKey(e => e.StreetId).HasName("PK__Streets__665BB66BFC187A05");

			entity.ToTable("Street");

			entity.HasIndex(e => e.StreetName, "UQ__Streets__F0A80FAA8B43C490").IsUnique();

			entity.Property(e => e.StreetId).HasColumnName("street_id");
			entity.Property(e => e.StreetName)
				.HasMaxLength(30)
				.HasColumnName("street_name");
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F23F3C8E8");

			entity.ToTable("User");

			entity.HasIndex(e => e.UserUsername, "UQ__Users__94975AD554C2023F").IsUnique();

			entity.Property(e => e.UserId).HasColumnName("user_id");
			entity.Property(e => e.UserCity)
				.HasMaxLength(30)
				.HasColumnName("user_city");
			entity.Property(e => e.UserEmail)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("user_email");
			entity.Property(e => e.UserName)
				.HasMaxLength(30)
				.HasColumnName("user_name");
			entity.Property(e => e.UserPassword)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasColumnName("user_password");
			entity.Property(e => e.UserPatronymic)
				.HasMaxLength(35)
				.HasColumnName("user_patronymic");
			entity.Property(e => e.UserPhone)
				.HasMaxLength(16)
				.IsUnicode(false)
				.HasColumnName("user_phone");
			entity.Property(e => e.UserRole)
				.HasDefaultValue(2)
				.HasColumnName("user_role");
			entity.Property(e => e.UserSurname)
				.HasMaxLength(30)
				.HasColumnName("user_surname");
			entity.Property(e => e.UserUsername)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("user_username");

			entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
				.HasForeignKey(d => d.UserRole)
				.HasConstraintName("FK__Users__user_role__6A30C649");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}