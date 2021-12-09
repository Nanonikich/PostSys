using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace postSys.application.prj
{
	public partial class PostSysContext : DbContext
	{
		public PostSysContext()
		{
		}

		public PostSysContext(DbContextOptions<PostSysContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Address> Addresses { get; set; } = null!;
		public virtual DbSet<City> Cities { get; set; } = null!;
		public virtual DbSet<CodeAddress> CodesAddresses { get; set; } = null!;
		public virtual DbSet<Postmen> Postmens { get; set; } = null!;
		public virtual DbSet<Recipient> Recipients { get; set; } = null!;
		public virtual DbSet<Sender> Senders { get; set; } = null!;
		public virtual DbSet<Status> Statuses { get; set; } = null!;
		public virtual DbSet<Street> Streets { get; set; } = null!;
		public virtual DbSet<User> Users { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PostDB"].ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Address>(entity =>
			{
				entity.Property(e => e.AddressId).HasColumnName("address_id");

				entity.Property(e => e.AddressCity).HasColumnName("address_city");

				entity.Property(e => e.AddressGoods).HasColumnName("address_goods");
				
				entity.Property(e => e.AddressApartment).HasColumnName("address_apartment");

				entity.Property(e => e.AddressHome)
					.HasMaxLength(5)
					.HasColumnName("address_home");

				entity.Property(e => e.AddressPlot).HasColumnName("address_plot");

				entity.Property(e => e.AddressPostmen).HasColumnName("address_postmen");

				entity.Property(e => e.AddressRecipient).HasColumnName("address_recipient");

				entity.Property(e => e.AddressStreet).HasColumnName("address_street");

				entity.HasOne(d => d.AddressPostmenNavigation)
					.WithMany(p => p.Addresses)
					.HasForeignKey(d => d.AddressPostmen)
					.HasConstraintName("FK__Addresses__addre__398D8EEE");

				entity.HasOne(d => d.AddressRecipientNavigation)
					.WithMany(p => p.Addresses)
					.HasForeignKey(d => d.AddressRecipient)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Addresses__addre__3A81B327");
			});

			modelBuilder.Entity<City>(entity =>
			{
				entity.ToTable("City");

				entity.HasIndex(e => e.CityName, "UQ__City__1AA4F7B5678F1B0E")
					.IsUnique();

				entity.Property(e => e.CityId).HasColumnName("city_id");

				entity.Property(e => e.CityName)
					.HasMaxLength(30)
					.HasColumnName("city_name");
			});

			modelBuilder.Entity<CodeAddress>(entity =>
			{
				entity.HasKey(e => e.CodeAddressId)
					.HasName("PK__CodesAdd__BE268BC3EDD56E1C");

				entity.Property(e => e.CodeAddressId).HasColumnName("codeAddress_id");

				entity.Property(e => e.CodeAddressCity).HasColumnName("codeAddress_city");

				entity.Property(e => e.CodeAddressHouses).HasColumnName("codeAddress_houses");

				entity.Property(e => e.CodeAddressPlot).HasColumnName("codeAddress_plot");

				entity.Property(e => e.CodeAddressStreet).HasColumnName("codeAddress_street");

				entity.HasOne(d => d.CodeAddressCityNavigation)
					.WithMany(p => p.CodesAddresses)
					.HasForeignKey(d => d.CodeAddressCity)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__CodesAddr__codeA__2A4B4B5E");

				entity.HasOne(d => d.CodeAddressStreetNavigation)
					.WithMany(p => p.CodesAddresses)
					.HasForeignKey(d => d.CodeAddressStreet)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__CodesAddr__codeA__2B3F6F97");
			});

			modelBuilder.Entity<Postmen>(entity =>
			{
				entity.Property(e => e.PostmenId).HasColumnName("postmen_id");

				entity.Property(e => e.PostmenName)
					.HasMaxLength(30)
					.HasColumnName("postmen_name");

				entity.Property(e => e.PostmenPatronymic)
					.HasMaxLength(35)
					.HasColumnName("postmen_patronymic");

				entity.Property(e => e.PostmenPhone)
					.HasMaxLength(60)
					.HasColumnName("postmen_phone");

				entity.Property(e => e.PostmenPlot).HasColumnName("postmen_plot");

				entity.Property(e => e.PostmenSurname)
					.HasMaxLength(30)
					.HasColumnName("postmen_surname");
			});

			modelBuilder.Entity<Recipient>(entity =>
			{
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

				entity.HasOne(d => d.RecipientCityNavigation)
					.WithMany(p => p.Recipients)
					.HasForeignKey(d => d.RecipientCity)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Recipient__recip__35BCFE0A");

				entity.HasOne(d => d.RecipientSenderNavigation)
					.WithMany(p => p.Recipients)
					.HasForeignKey(d => d.RecipientSender)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Recipient__recip__34C8D9D1");

				entity.HasOne(d => d.RecipientStreetNavigation)
					.WithMany(p => p.Recipients)
					.HasForeignKey(d => d.RecipientStreet)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Recipient__recip__36B12243");
			});

			modelBuilder.Entity<Sender>(entity =>
			{
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

				entity.HasOne(d => d.SenderCityNavigation)
					.WithMany(p => p.Senders)
					.HasForeignKey(d => d.SenderCity)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Sender__sender_c__300424B4");

				entity.HasOne(d => d.SenderStreetNavigation)
					.WithMany(p => p.Senders)
					.HasForeignKey(d => d.SenderStreet)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Sender__sender_s__30F848ED");
			});

			modelBuilder.Entity<Status>(entity =>
			{
				entity.HasIndex(e => e.StatusName, "UQ__Statuses__501B3753CCFA65EA")
					.IsUnique();

				entity.Property(e => e.StatusId).HasColumnName("status_id");

				entity.Property(e => e.StatusName)
					.HasMaxLength(20)
					.HasColumnName("status_name");
			});

			modelBuilder.Entity<Street>(entity =>
			{
				entity.HasIndex(e => e.StreetName, "UQ__Streets__F0A80FAA7F281DE0")
					.IsUnique();

				entity.Property(e => e.StreetId).HasColumnName("street_id");

				entity.Property(e => e.StreetName)
					.HasMaxLength(30)
					.HasColumnName("street_name");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasIndex(e => e.UserUsername, "UQ__Users__94975AD5BADAF2CC")
					.IsUnique();

				entity.Property(e => e.UserId).HasColumnName("user_id");

				entity.Property(e => e.UserAddress).HasColumnName("user_address");

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

				entity.Property(e => e.UserStatus)
					.HasColumnName("user_status")
					.HasDefaultValueSql("((2))");

				entity.Property(e => e.UserSurname)
					.HasMaxLength(30)
					.HasColumnName("user_surname");

				entity.Property(e => e.UserUsername)
					.HasMaxLength(30)
					.IsUnicode(false)
					.HasColumnName("user_username");

				entity.HasOne(d => d.UserStatusNavigation)
					.WithMany(p => p.Users)
					.HasForeignKey(d => d.UserStatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Users__user_stat__4222D4EF");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
