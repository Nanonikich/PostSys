USE [PostSys]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK__User__user_role__5441852A]
GO
ALTER TABLE [dbo].[Sender] DROP CONSTRAINT [FK__Sender__sender_s__534D60F1]
GO
ALTER TABLE [dbo].[Sender] DROP CONSTRAINT [FK__Sender__sender_c__52593CB8]
GO
ALTER TABLE [dbo].[Recipient] DROP CONSTRAINT [FK__Recipient__recip__5165187F]
GO
ALTER TABLE [dbo].[Recipient] DROP CONSTRAINT [FK__Recipient__recip__5070F446]
GO
ALTER TABLE [dbo].[Recipient] DROP CONSTRAINT [FK__Recipient__recip__4F7CD00D]
GO
ALTER TABLE [dbo].[AddressCode] DROP CONSTRAINT [FK__AddressCo__addre__4E88ABD4]
GO
ALTER TABLE [dbo].[AddressCode] DROP CONSTRAINT [FK__AddressCo__addre__4D94879B]
GO
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK__Address__address__4CA06362]
GO
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK__Address__address__4BAC3F29]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF__User__user_role__4AB81AF0]
GO
/****** Object:  Index [UQ__User__94975AD5FF9E0593]    Script Date: 30.06.2024 13:41:06 ******/
ALTER TABLE [dbo].[User] DROP CONSTRAINT [UQ__User__94975AD5FF9E0593]
GO
/****** Object:  Index [UQ__Street__F0A80FAAAC5D3AC3]    Script Date: 30.06.2024 13:41:06 ******/
ALTER TABLE [dbo].[Street] DROP CONSTRAINT [UQ__Street__F0A80FAAAC5D3AC3]
GO
/****** Object:  Index [UQ__Role__783254B17B76F6F7]    Script Date: 30.06.2024 13:41:06 ******/
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [UQ__Role__783254B17B76F6F7]
GO
/****** Object:  Index [UQ__City__1AA4F7B5612D6ACC]    Script Date: 30.06.2024 13:41:06 ******/
ALTER TABLE [dbo].[City] DROP CONSTRAINT [UQ__City__1AA4F7B5612D6ACC]
GO
/****** Object:  Table [dbo].[User]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Street]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Street]') AND type in (N'U'))
DROP TABLE [dbo].[Street]
GO
/****** Object:  Table [dbo].[Sender]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sender]') AND type in (N'U'))
DROP TABLE [dbo].[Sender]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Recipient]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recipient]') AND type in (N'U'))
DROP TABLE [dbo].[Recipient]
GO
/****** Object:  Table [dbo].[Postman]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Postman]') AND type in (N'U'))
DROP TABLE [dbo].[Postman]
GO
/****** Object:  Table [dbo].[City]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
DROP TABLE [dbo].[City]
GO
/****** Object:  Table [dbo].[AddressCode]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddressCode]') AND type in (N'U'))
DROP TABLE [dbo].[AddressCode]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 30.06.2024 13:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO
USE [master]
GO
/****** Object:  Database [PostSys]    Script Date: 30.06.2024 13:41:06 ******/
DROP DATABASE [PostSys]
GO
/****** Object:  Database [PostSys]    Script Date: 30.06.2024 13:41:06 ******/
CREATE DATABASE [PostSys]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PostSys', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PostSys.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PostSys_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PostSys_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PostSys] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PostSys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PostSys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PostSys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PostSys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PostSys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PostSys] SET ARITHABORT OFF 
GO
ALTER DATABASE [PostSys] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PostSys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PostSys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PostSys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PostSys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PostSys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PostSys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PostSys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PostSys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PostSys] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PostSys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PostSys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PostSys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PostSys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PostSys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PostSys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PostSys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PostSys] SET RECOVERY FULL 
GO
ALTER DATABASE [PostSys] SET  MULTI_USER 
GO
ALTER DATABASE [PostSys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PostSys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PostSys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PostSys] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PostSys] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PostSys] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PostSys', N'ON'
GO
ALTER DATABASE [PostSys] SET QUERY_STORE = OFF
GO
USE [PostSys]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[address_id] [int] IDENTITY(1,1) NOT NULL,
	[address_plot] [int] NULL,
	[address_recipient] [int] NOT NULL,
	[address_city] [int] NOT NULL,
	[address_street] [int] NOT NULL,
	[address_home] [nvarchar](5) NOT NULL,
	[address_apartment] [nvarchar](10) NOT NULL,
	[address_postman] [int] NOT NULL,
	[address_goods] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressCode]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressCode](
	[addressCode_id] [int] IDENTITY(1,1) NOT NULL,
	[addressCode_city] [int] NOT NULL,
	[addressCode_plot] [int] NOT NULL,
	[addressCode_street] [int] NOT NULL,
	[addressCode_houses] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[addressCode_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[city_id] [int] IDENTITY(1,1) NOT NULL,
	[city_name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[city_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Postman]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Postman](
	[postman_id] [int] IDENTITY(1,1) NOT NULL,
	[postman_surname] [nvarchar](30) NOT NULL,
	[postman_name] [nvarchar](30) NOT NULL,
	[postman_patronymic] [nvarchar](35) NULL,
	[postman_phone] [nvarchar](60) NOT NULL,
	[postman_plot] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[postman_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipient]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipient](
	[recipient_id] [int] IDENTITY(1,1) NOT NULL,
	[recipient_series] [nvarchar](4) NULL,
	[recipient_number] [nvarchar](6) NULL,
	[recipient_surname] [nvarchar](30) NOT NULL,
	[recipient_name] [nvarchar](30) NOT NULL,
	[recipient_patronymic] [nvarchar](35) NULL,
	[recipient_city] [int] NOT NULL,
	[recipient_street] [int] NOT NULL,
	[recipient_home] [nvarchar](5) NOT NULL,
	[recipient_apartment] [int] NOT NULL,
	[recipient_phone] [nvarchar](60) NOT NULL,
	[recipient_sender] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[recipient_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sender]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sender](
	[sender_id] [int] IDENTITY(1,1) NOT NULL,
	[sender_surname] [nvarchar](30) NOT NULL,
	[sender_name] [nvarchar](30) NOT NULL,
	[sender_patronymic] [nvarchar](35) NULL,
	[sender_city] [int] NOT NULL,
	[sender_street] [int] NOT NULL,
	[sender_home] [nvarchar](5) NOT NULL,
	[sender_apartment] [int] NOT NULL,
	[sender_phone] [nvarchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sender_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Street]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Street](
	[street_id] [int] IDENTITY(1,1) NOT NULL,
	[street_name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[street_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 30.06.2024 13:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_surname] [nvarchar](30) NOT NULL,
	[user_name] [nvarchar](30) NOT NULL,
	[user_patronymic] [nvarchar](35) NULL,
	[user_email] [varchar](30) NOT NULL,
	[user_phone] [varchar](16) NOT NULL,
	[user_city] [nvarchar](30) NOT NULL,
	[user_username] [varchar](30) NOT NULL,
	[user_password] [varchar](20) NOT NULL,
	[user_role] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([address_id], [address_plot], [address_recipient], [address_city], [address_street], [address_home], [address_apartment], [address_postman], [address_goods]) VALUES (1, 5, 1, 1, 2, N'8А', N'5', 1, N'Синтезатор')
INSERT [dbo].[Address] ([address_id], [address_plot], [address_recipient], [address_city], [address_street], [address_home], [address_apartment], [address_postman], [address_goods]) VALUES (2, 8, 2, 2, 1, N'54', N'19', 2, N'Чайник')
SET IDENTITY_INSERT [dbo].[Address] OFF
GO
SET IDENTITY_INSERT [dbo].[AddressCode] ON 

INSERT [dbo].[AddressCode] ([addressCode_id], [addressCode_city], [addressCode_plot], [addressCode_street], [addressCode_houses]) VALUES (1, 1, 5, 2, N'8А, 5Е, 112, 65')
INSERT [dbo].[AddressCode] ([addressCode_id], [addressCode_city], [addressCode_plot], [addressCode_street], [addressCode_houses]) VALUES (2, 2, 8, 1, N'6, 54, 3, 15А')
SET IDENTITY_INSERT [dbo].[AddressCode] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([city_id], [city_name]) VALUES (3, N'Казань')
INSERT [dbo].[City] ([city_id], [city_name]) VALUES (1, N'Москва')
INSERT [dbo].[City] ([city_id], [city_name]) VALUES (2, N'Череповец')
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Postman] ON 

INSERT [dbo].[Postman] ([postman_id], [postman_surname], [postman_name], [postman_patronymic], [postman_phone], [postman_plot]) VALUES (1, N'Кожинов', N'Виктор', N'Тимофеевич', N'8965-122-32-81', 5)
INSERT [dbo].[Postman] ([postman_id], [postman_surname], [postman_name], [postman_patronymic], [postman_phone], [postman_plot]) VALUES (2, N'Ананьев', N'Николай', N'Петрович', N'8975-161-21-41', 8)
SET IDENTITY_INSERT [dbo].[Postman] OFF
GO
SET IDENTITY_INSERT [dbo].[Recipient] ON 

INSERT [dbo].[Recipient] ([recipient_id], [recipient_series], [recipient_number], [recipient_surname], [recipient_name], [recipient_patronymic], [recipient_city], [recipient_street], [recipient_home], [recipient_apartment], [recipient_phone], [recipient_sender]) VALUES (1, N'1921', N'363152', N'Шохвинидзе', N'Афанасий', N'Павлович', 1, 1, N'5Е', 21, N'8953-155-21-38', 2)
INSERT [dbo].[Recipient] ([recipient_id], [recipient_series], [recipient_number], [recipient_surname], [recipient_name], [recipient_patronymic], [recipient_city], [recipient_street], [recipient_home], [recipient_apartment], [recipient_phone], [recipient_sender]) VALUES (2, N'1911', N'365283', N'Пустышев', N'Алексей', N'Витальевич', 2, 2, N'3', 7, N'8935-213-15-21', 1)
SET IDENTITY_INSERT [dbo].[Recipient] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'Оператор')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Sender] ON 

INSERT [dbo].[Sender] ([sender_id], [sender_surname], [sender_name], [sender_patronymic], [sender_city], [sender_street], [sender_home], [sender_apartment], [sender_phone]) VALUES (1, N'Сиротин', N'Василий', N'Андреевич', 1, 1, N'8А', 28, N'8952-116-21-82')
INSERT [dbo].[Sender] ([sender_id], [sender_surname], [sender_name], [sender_patronymic], [sender_city], [sender_street], [sender_home], [sender_apartment], [sender_phone]) VALUES (2, N'Никоноров', N'Борис', N'Викторович', 2, 2, N'15А', 5, N'8952-116-21-82')
SET IDENTITY_INSERT [dbo].[Sender] OFF
GO
SET IDENTITY_INSERT [dbo].[Street] ON 

INSERT [dbo].[Street] ([street_id], [street_name]) VALUES (1, N'Вологодская')
INSERT [dbo].[Street] ([street_id], [street_name]) VALUES (2, N'Ленина')
INSERT [dbo].[Street] ([street_id], [street_name]) VALUES (3, N'Чкалова')
SET IDENTITY_INSERT [dbo].[Street] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([user_id], [user_surname], [user_name], [user_patronymic], [user_email], [user_phone], [user_city], [user_username], [user_password], [user_role]) VALUES (1, N'Новиков', N'Никита', N'Ильич', N'nanonikich@gmail.com', N'8953-512-45-82', N'Череповец', N'nano', N'1', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__City__1AA4F7B5612D6ACC]    Script Date: 30.06.2024 13:41:07 ******/
ALTER TABLE [dbo].[City] ADD UNIQUE NONCLUSTERED 
(
	[city_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Role__783254B17B76F6F7]    Script Date: 30.06.2024 13:41:07 ******/
ALTER TABLE [dbo].[Role] ADD UNIQUE NONCLUSTERED 
(
	[role_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Street__F0A80FAAAC5D3AC3]    Script Date: 30.06.2024 13:41:07 ******/
ALTER TABLE [dbo].[Street] ADD UNIQUE NONCLUSTERED 
(
	[street_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__94975AD5FF9E0593]    Script Date: 30.06.2024 13:41:07 ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[user_username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((2)) FOR [user_role]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([address_postman])
REFERENCES [dbo].[Postman] ([postman_id])
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([address_recipient])
REFERENCES [dbo].[Recipient] ([recipient_id])
GO
ALTER TABLE [dbo].[AddressCode]  WITH CHECK ADD FOREIGN KEY([addressCode_city])
REFERENCES [dbo].[City] ([city_id])
GO
ALTER TABLE [dbo].[AddressCode]  WITH CHECK ADD FOREIGN KEY([addressCode_street])
REFERENCES [dbo].[Street] ([street_id])
GO
ALTER TABLE [dbo].[Recipient]  WITH CHECK ADD FOREIGN KEY([recipient_sender])
REFERENCES [dbo].[Sender] ([sender_id])
GO
ALTER TABLE [dbo].[Recipient]  WITH CHECK ADD FOREIGN KEY([recipient_city])
REFERENCES [dbo].[City] ([city_id])
GO
ALTER TABLE [dbo].[Recipient]  WITH CHECK ADD FOREIGN KEY([recipient_street])
REFERENCES [dbo].[AddressCode] ([addressCode_id])
GO
ALTER TABLE [dbo].[Sender]  WITH CHECK ADD FOREIGN KEY([sender_city])
REFERENCES [dbo].[City] ([city_id])
GO
ALTER TABLE [dbo].[Sender]  WITH CHECK ADD FOREIGN KEY([sender_street])
REFERENCES [dbo].[AddressCode] ([addressCode_id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([user_role])
REFERENCES [dbo].[Role] ([role_id])
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [PostSys] SET  READ_WRITE 
GO
