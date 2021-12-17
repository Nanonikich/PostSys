use PostSys;

CREATE TABLE [dbo].[City] (
	[city_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[city_name] NVARCHAR (30) UNIQUE NOT NULL,
);

CREATE TABLE [dbo].[Streets] (
	[street_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[street_name] NVARCHAR (30) UNIQUE NOT NULL,
);

CREATE TABLE [dbo].[CodesAddresses] (
	[codeAddress_id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	[codeAddress_city] INT NOT NULL,
	[codeAddress_plot] INT NOT NULL,
	[codeAddress_street] INT  NOT NULL,
	[codeAddress_houses] NVARCHAR (MAX) NOT NULL,
	FOREIGN KEY ([codeAddress_city]) REFERENCES [dbo].[City] ([city_id]),
	FOREIGN KEY ([codeAddress_street]) REFERENCES [dbo].[Streets] ([street_id]),
);

CREATE TABLE [dbo].[Postmens] (
	[postmen_id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	[postmen_surname] NVARCHAR (30) NOT NULL,
    [postmen_name] NVARCHAR (30) NOT NULL,
	[postmen_patronymic] NVARCHAR (35) NULL,
	[postmen_phone] NVARCHAR (60) NOT NULL,
	[postmen_plot] INT NOT NULL,
);

CREATE TABLE [dbo].[Sender] (
	[sender_id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	[sender_surname] NVARCHAR (30) NOT NULL,
    [sender_name] NVARCHAR (30) NOT NULL,
	[sender_patronymic] NVARCHAR (35) NULL,
	[sender_city] INT NOT NULL,
    [sender_street] INT NOT NULL,
	[sender_home] NVARCHAR (5) NOT NULL,
	[sender_apartment] INT NOT NULL,
	[sender_phone] NVARCHAR (60) NOT NULL,
	FOREIGN KEY ([sender_city]) REFERENCES [dbo].[City] ([city_id]),
	FOREIGN KEY ([sender_street]) REFERENCES [dbo].[CodesAddresses] ([codeAddress_id]),
);

CREATE TABLE [dbo].[Recipient] (
	[recipient_id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [recipient_series] NVARCHAR (4) NULL,
	[recipient_number] NVARCHAR (6) NULL,
	[recipient_surname] NVARCHAR (30) NOT NULL,
    [recipient_name] NVARCHAR (30) NOT NULL,
	[recipient_patronymic] NVARCHAR (35) NULL,
	[recipient_city] INT NOT NULL,
    [recipient_street] INT NOT NULL,
	[recipient_home] NVARCHAR (5) NOT NULL,
	[recipient_apartment] INT NOT NULL,
	[recipient_phone] NVARCHAR (60) NOT NULL,
	[recipient_sender] INT NOT NULL,
	FOREIGN KEY ([recipient_sender]) REFERENCES [dbo].[Sender] ([sender_id]),
	FOREIGN KEY ([recipient_city]) REFERENCES [dbo].[City] ([city_id]),
	FOREIGN KEY ([recipient_street]) REFERENCES [dbo].[CodesAddresses] ([codeAddress_id]),
);

CREATE TABLE [dbo].[Addresses] (
	[address_id] INT IDENTITY (1, 1) NOT NULL,
	[address_plot] INT NULL,
	[address_recipient] INT NOT NULL,
	[address_city] INT NOT NULL, 
	[address_street] INT NOT NULL,
	[address_home] NVARCHAR (5) NOT NULL,
	[address_apartment] NVARCHAR (10) NOT NULL,
	[address_postmen] INT NOT NULL,
	[address_goods] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY CLUSTERED ([address_id] ASC),
	FOREIGN KEY ([address_postmen]) REFERENCES [dbo].[Postmens] ([postmen_id]),
	FOREIGN KEY ([address_recipient]) REFERENCES [dbo].[Recipient] ([recipient_id]),
);

CREATE TABLE [dbo].[Statuses] (
    [status_id] INT IDENTITY (1, 1) NOT NULL,
    [status_name] NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([status_id] ASC),
    UNIQUE NONCLUSTERED ([status_name] ASC),
);

CREATE TABLE [dbo].[Users] (
    [user_id] INT IDENTITY (1, 1) NOT NULL,
    [user_surname] NVARCHAR (30) NOT NULL,
    [user_name] NVARCHAR (30) NOT NULL,
    [user_patronymic] NVARCHAR (35) NULL,
    [user_email] VARCHAR (30) NOT NULL,
    [user_phone] VARCHAR (16) NOT NULL,
    [user_city] NVARCHAR (30) NOT NULL,
    [user_username] VARCHAR (30) NOT NULL,
    [user_password] VARCHAR (20) NOT NULL,
    [user_status]  INT DEFAULT ((2)) NOT NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC),
    UNIQUE NONCLUSTERED ([user_username] ASC),
	FOREIGN KEY ([user_status]) REFERENCES [dbo].[Statuses] ([status_id]),
);