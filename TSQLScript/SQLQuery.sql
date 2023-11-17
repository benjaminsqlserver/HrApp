USE [master]
GO
/****** Object:  Database [HrDB]    Script Date: 17/11/2023 03:13:03 ******/
CREATE DATABASE [HrDB]
 
GO

USE [HrDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/11/2023 03:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[countries]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countries](
	[country_id] [char](2) NOT NULL,
	[country_name] [varchar](40) NULL,
	[region_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departments]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departments](
	[department_id] [int] IDENTITY(1,1) NOT NULL,
	[department_name] [varchar](30) NOT NULL,
	[location_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dependents]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dependents](
	[dependent_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[relationship] [varchar](25) NOT NULL,
	[employee_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[dependent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employees]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employees](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](20) NULL,
	[last_name] [varchar](25) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phone_number] [varchar](20) NULL,
	[hire_date] [date] NOT NULL,
	[job_id] [int] NOT NULL,
	[salary] [decimal](8, 2) NOT NULL,
	[manager_id] [int] NULL,
	[department_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobs]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobs](
	[job_id] [int] IDENTITY(1,1) NOT NULL,
	[job_title] [varchar](35) NOT NULL,
	[min_salary] [decimal](8, 2) NULL,
	[max_salary] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[job_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[locations]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locations](
	[location_id] [int] IDENTITY(1,1) NOT NULL,
	[street_address] [varchar](40) NULL,
	[postal_code] [varchar](12) NULL,
	[city] [varchar](30) NOT NULL,
	[state_province] [varchar](25) NULL,
	[country_id] [char](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[regions]    Script Date: 17/11/2023 03:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[regions](
	[region_id] [int] IDENTITY(1,1) NOT NULL,
	[region_name] [varchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[region_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.0')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'36c13ddb-9f7b-4f20-b980-f2866987f105', NULL, N'Staff', N'STAFF')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'60a346c6-ba45-4d8f-9cab-3e3c3793d660', NULL, N'HeadOfHR', N'HEADOFHR')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'76e61b08-16fd-402a-a507-dfd5b9cb9540', NULL, N'SuperAdmin', N'SUPERADMIN')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'8d6fefac-1b86-4754-b9ca-483d6256a2da', NULL, N'MD', N'MD')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1aa62af0-c065-491f-8e9f-d4eb04c61c82', N'36c13ddb-9f7b-4f20-b980-f2866987f105')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e0c11f44-5d66-43a7-a1ea-c47571f2b1ef', N'76e61b08-16fd-402a-a507-dfd5b9cb9540')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e0c11f44-5d66-43a7-a1ea-c47571f2b1ef', N'8d6fefac-1b86-4754-b9ca-483d6256a2da')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'1aa62af0-c065-491f-8e9f-d4eb04c61c82', 0, N'639105b3-7668-49d7-a3f5-b8e29ff6f48b', N'george.corkery15@ethereal.email', 1, 1, NULL, N'GEORGE.CORKERY15@ETHEREAL.EMAIL', N'GEORGE.CORKERY15@ETHEREAL.EMAIL', N'AQAAAAIAAYagAAAAEOM+RQ/fr27Y0VJNZrtgXp8nk2fxuJsyVVREfinT37GEvOVN2FPFgmIGqL7i6JlduA==', NULL, 0, N'3RLLGFXRKBB3BIQR2BKK3LJREZ6C4EG5', 0, N'george.corkery15@ethereal.email')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'6f0e2be2-1909-40eb-8314-5595ee19c049', 0, N'f0dcd33b-91d5-4b66-985f-b1780f54f4b3', N'ahmedmusa@supereagles.com', 1, 1, NULL, N'AHMEDMUSA@SUPEREAGLES.COM', N'AHMEDMUSA@SUPEREAGLES.COM', N'AQAAAAIAAYagAAAAEJqY/d/kscGYwoVWuRDE4FXK9ZBc9ikFmLuZJlfdLm16joSPuJvZqXtZwMe0AyzMxw==', NULL, 0, N'IZRZQET7WNTM3XA5FV7Q4FT6OXDH3UMM', 0, N'ahmedmusa@supereagles.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'e0c11f44-5d66-43a7-a1ea-c47571f2b1ef', 0, N'8d381aab-2b1f-4d16-bb88-5a0bd8c047d8', N'benjaminsqlserver@gmail.com', 1, 1, NULL, N'BENJAMINSQLSERVER@GMAIL.COM', N'BENJAMINSQLSERVER@GMAIL.COM', N'AQAAAAIAAYagAAAAEHnnBzC02uwzgZ7oMxFpo8o4KVu0aUtPkIq9fOdlwMLsHCCcvJI9cVeeSHyjGXi9yA==', NULL, 0, N'PFSSV4GQCCB44UU5XMNELS6E4DEVAU5K', 0, N'benjaminsqlserver@gmail.com')
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'AR', N'Argentina', 2)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'AU', N'Australia', 3)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'BE', N'Belgium', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'BR', N'Brazil', 2)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'CA', N'Canada', 2)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'CH', N'Switzerland', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'CN', N'China', 3)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'DE', N'Germany', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'DK', N'Denmark', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'EG', N'Egypt', 4)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'FR', N'France', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'HK', N'HongKong', 3)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'IL', N'Israel', 4)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'IN', N'India', 3)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'IT', N'Italy', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'JP', N'Japan', 3)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'KW', N'Kuwait', 4)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'MX', N'Mexico', 2)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'NG', N'Nigeria', 4)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'NL', N'Netherlands', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'SG', N'Singapore', 3)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'UK', N'United Kingdom', 1)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'US', N'United States of America', 2)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'ZM', N'Zambia', 4)
GO
INSERT [dbo].[countries] ([country_id], [country_name], [region_id]) VALUES (N'ZW', N'Zimbabwe', 4)
GO
SET IDENTITY_INSERT [dbo].[departments] ON 
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (1, N'Administration', 1700)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (2, N'Marketing', 1800)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (3, N'Purchasing', 1700)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (4, N'Human Resources', 2400)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (5, N'Shipping', 1500)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (6, N'IT', 1400)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (7, N'Public Relations', 2700)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (8, N'Sales', 2500)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (9, N'Executive', 1700)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (10, N'Finance', 1700)
GO
INSERT [dbo].[departments] ([department_id], [department_name], [location_id]) VALUES (11, N'Accounting', 1700)
GO
SET IDENTITY_INSERT [dbo].[departments] OFF
GO
SET IDENTITY_INSERT [dbo].[dependents] ON 
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (1, N'Penelope', N'Gietz', N'Child', 206)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (2, N'Nick', N'Higgins', N'Child', 205)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (3, N'Ed', N'Whalen', N'Child', 200)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (4, N'Jennifer', N'King', N'Child', 100)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (5, N'Johnny', N'Kochhar', N'Child', 101)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (6, N'Bette', N'De Haan', N'Child', 102)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (7, N'Grace', N'Faviet', N'Child', 109)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (8, N'Matthew', N'Chen', N'Child', 110)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (9, N'Joe', N'Sciarra', N'Child', 111)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (10, N'Christian', N'Urman', N'Child', 112)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (11, N'Zero', N'Popp', N'Child', 113)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (12, N'Karl', N'Greenberg', N'Child', 108)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (13, N'Uma', N'Mavris', N'Child', 203)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (14, N'Vivien', N'Hunold', N'Child', 103)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (15, N'Cuba', N'Ernst', N'Child', 104)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (16, N'Fred', N'Austin', N'Child', 105)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (17, N'Helen', N'Pataballa', N'Child', 106)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (18, N'Dan', N'Lorentz', N'Child', 107)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (19, N'Bob', N'Hartstein', N'Child', 201)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (20, N'Lucille', N'Fay', N'Child', 202)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (21, N'Kirsten', N'Baer', N'Child', 204)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (22, N'Elvis', N'Khoo', N'Child', 115)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (23, N'Sandra', N'Baida', N'Child', 116)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (24, N'Cameron', N'Tobias', N'Child', 117)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (25, N'Kevin', N'Himuro', N'Child', 118)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (26, N'Rip', N'Colmenares', N'Child', 119)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (27, N'Julia', N'Raphaely', N'Child', 114)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (28, N'Woody', N'Russell', N'Child', 145)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (29, N'Alec', N'Partners', N'Child', 146)
GO
INSERT [dbo].[dependents] ([dependent_id], [first_name], [last_name], [relationship], [employee_id]) VALUES (30, N'Sandra', N'Taylor', N'Child', 176)
GO
SET IDENTITY_INSERT [dbo].[dependents] OFF
GO
SET IDENTITY_INSERT [dbo].[employees] ON 
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (100, N'Steven', N'King', N'steven.king@sqltutorial.org', N'515.123.4567', CAST(N'1987-06-17' AS Date), 4, CAST(24000.00 AS Decimal(8, 2)), NULL, 9)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (101, N'Neena', N'Kochhar', N'neena.kochhar@sqltutorial.org', N'515.123.4568', CAST(N'1989-09-21' AS Date), 5, CAST(17000.00 AS Decimal(8, 2)), 100, 9)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (102, N'Lex', N'De Haan', N'lex.de haan@sqltutorial.org', N'515.123.4569', CAST(N'1993-01-13' AS Date), 5, CAST(17000.00 AS Decimal(8, 2)), 100, 9)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (103, N'Alexander', N'Hunold', N'alexander.hunold@sqltutorial.org', N'590.423.4567', CAST(N'1990-01-03' AS Date), 9, CAST(9000.00 AS Decimal(8, 2)), 102, 6)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (104, N'Bruce', N'Ernst', N'bruce.ernst@sqltutorial.org', N'590.423.4568', CAST(N'1991-05-21' AS Date), 9, CAST(6000.00 AS Decimal(8, 2)), 103, 6)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (105, N'David', N'Austin', N'david.austin@sqltutorial.org', N'590.423.4569', CAST(N'1997-06-25' AS Date), 9, CAST(4800.00 AS Decimal(8, 2)), 103, 6)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (106, N'Valli', N'Pataballa', N'valli.pataballa@sqltutorial.org', N'590.423.4560', CAST(N'1998-02-05' AS Date), 9, CAST(4800.00 AS Decimal(8, 2)), 103, 6)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (107, N'Diana', N'Lorentz', N'diana.lorentz@sqltutorial.org', N'590.423.5567', CAST(N'1999-02-07' AS Date), 9, CAST(4200.00 AS Decimal(8, 2)), 103, 6)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (108, N'Nancy', N'Greenberg', N'nancy.greenberg@sqltutorial.org', N'515.124.4569', CAST(N'1994-08-17' AS Date), 7, CAST(12000.00 AS Decimal(8, 2)), 101, 10)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (109, N'Daniel', N'Faviet', N'daniel.faviet@sqltutorial.org', N'515.124.4169', CAST(N'1994-08-16' AS Date), 6, CAST(9000.00 AS Decimal(8, 2)), 108, 10)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (110, N'John', N'Chen', N'john.chen@sqltutorial.org', N'515.124.4269', CAST(N'1997-09-28' AS Date), 6, CAST(8200.00 AS Decimal(8, 2)), 108, 10)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (111, N'Ismael', N'Sciarra', N'ismael.sciarra@sqltutorial.org', N'515.124.4369', CAST(N'1997-09-30' AS Date), 6, CAST(7700.00 AS Decimal(8, 2)), 108, 10)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (112, N'Jose Manuel', N'Urman', N'jose manuel.urman@sqltutorial.org', N'515.124.4469', CAST(N'1998-03-07' AS Date), 6, CAST(7800.00 AS Decimal(8, 2)), 108, 10)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (113, N'Luis', N'Popp', N'luis.popp@sqltutorial.org', N'515.124.4567', CAST(N'1999-12-07' AS Date), 6, CAST(6900.00 AS Decimal(8, 2)), 108, 10)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (114, N'Den', N'Raphaely', N'den.raphaely@sqltutorial.org', N'515.127.4561', CAST(N'1994-12-07' AS Date), 14, CAST(11000.00 AS Decimal(8, 2)), 100, 3)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (115, N'Alexander', N'Khoo', N'alexander.khoo@sqltutorial.org', N'515.127.4562', CAST(N'1995-05-18' AS Date), 13, CAST(3100.00 AS Decimal(8, 2)), 114, 3)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (116, N'Shelli', N'Baida', N'shelli.baida@sqltutorial.org', N'515.127.4563', CAST(N'1997-12-24' AS Date), 13, CAST(2900.00 AS Decimal(8, 2)), 114, 3)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (117, N'Sigal', N'Tobias', N'sigal.tobias@sqltutorial.org', N'515.127.4564', CAST(N'1997-07-24' AS Date), 13, CAST(2800.00 AS Decimal(8, 2)), 114, 3)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (118, N'Guy', N'Himuro', N'guy.himuro@sqltutorial.org', N'515.127.4565', CAST(N'1998-11-15' AS Date), 13, CAST(2600.00 AS Decimal(8, 2)), 114, 3)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (119, N'Karen', N'Colmenares', N'karen.colmenares@sqltutorial.org', N'515.127.4566', CAST(N'1999-08-10' AS Date), 13, CAST(2500.00 AS Decimal(8, 2)), 114, 3)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (120, N'Matthew', N'Weiss', N'matthew.weiss@sqltutorial.org', N'650.123.1234', CAST(N'1996-07-18' AS Date), 19, CAST(8000.00 AS Decimal(8, 2)), 100, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (121, N'Adam', N'Fripp', N'adam.fripp@sqltutorial.org', N'650.123.2234', CAST(N'1997-04-10' AS Date), 19, CAST(8200.00 AS Decimal(8, 2)), 100, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (122, N'Payam', N'Kaufling', N'payam.kaufling@sqltutorial.org', N'650.123.3234', CAST(N'1995-05-01' AS Date), 19, CAST(7900.00 AS Decimal(8, 2)), 100, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (123, N'Shanta', N'Vollman', N'shanta.vollman@sqltutorial.org', N'650.123.4234', CAST(N'1997-10-10' AS Date), 19, CAST(6500.00 AS Decimal(8, 2)), 100, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (126, N'Irene', N'Mikkilineni', N'irene.mikkilineni@sqltutorial.org', N'650.124.1224', CAST(N'1998-09-28' AS Date), 18, CAST(2700.00 AS Decimal(8, 2)), 120, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (145, N'John', N'Russell', N'john.russell@sqltutorial.org', NULL, CAST(N'1996-10-01' AS Date), 15, CAST(14000.00 AS Decimal(8, 2)), 100, 8)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (146, N'Karen', N'Partners', N'karen.partners@sqltutorial.org', NULL, CAST(N'1997-01-05' AS Date), 15, CAST(13500.00 AS Decimal(8, 2)), 100, 8)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (176, N'Jonathon', N'Taylor', N'jonathon.taylor@sqltutorial.org', NULL, CAST(N'1998-03-24' AS Date), 16, CAST(8600.00 AS Decimal(8, 2)), 100, 8)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (177, N'Jack', N'Livingston', N'jack.livingston@sqltutorial.org', NULL, CAST(N'1998-04-23' AS Date), 16, CAST(8400.00 AS Decimal(8, 2)), 100, 8)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (178, N'Kimberely', N'Grant', N'kimberely.grant@sqltutorial.org', NULL, CAST(N'1999-05-24' AS Date), 16, CAST(7000.00 AS Decimal(8, 2)), 100, 8)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (179, N'Charles', N'Johnson', N'charles.johnson@sqltutorial.org', NULL, CAST(N'2000-01-04' AS Date), 16, CAST(6200.00 AS Decimal(8, 2)), 100, 8)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (192, N'Sarah', N'Bell', N'sarah.bell@sqltutorial.org', N'650.501.1876', CAST(N'1996-02-04' AS Date), 17, CAST(4000.00 AS Decimal(8, 2)), 123, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (193, N'Britney', N'Everett', N'britney.everett@sqltutorial.org', N'650.501.2876', CAST(N'1997-03-03' AS Date), 17, CAST(3900.00 AS Decimal(8, 2)), 123, 5)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (200, N'Jennifer', N'Whalen', N'jennifer.whalen@sqltutorial.org', N'515.123.4444', CAST(N'1987-09-17' AS Date), 3, CAST(4400.00 AS Decimal(8, 2)), 101, 1)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (201, N'Michael', N'Hartstein', N'michael.hartstein@sqltutorial.org', N'515.123.5555', CAST(N'1996-02-17' AS Date), 10, CAST(13000.00 AS Decimal(8, 2)), 100, 2)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (202, N'Pat', N'Fay', N'pat.fay@sqltutorial.org', N'603.123.6666', CAST(N'1997-08-17' AS Date), 11, CAST(6000.00 AS Decimal(8, 2)), 201, 2)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (203, N'Susan', N'Mavris', N'susan.mavris@sqltutorial.org', N'515.123.7777', CAST(N'1994-06-07' AS Date), 8, CAST(6500.00 AS Decimal(8, 2)), 101, 4)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (204, N'Hermann', N'Baer', N'hermann.baer@sqltutorial.org', N'515.123.8888', CAST(N'1994-06-07' AS Date), 12, CAST(10000.00 AS Decimal(8, 2)), 101, 7)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (205, N'Shelley', N'Higgins', N'shelley.higgins@sqltutorial.org', N'515.123.8080', CAST(N'1994-06-07' AS Date), 2, CAST(12000.00 AS Decimal(8, 2)), 101, 11)
GO
INSERT [dbo].[employees] ([employee_id], [first_name], [last_name], [email], [phone_number], [hire_date], [job_id], [salary], [manager_id], [department_id]) VALUES (206, N'William', N'Gietz', N'william.gietz@sqltutorial.org', N'515.123.8181', CAST(N'1994-06-07' AS Date), 1, CAST(8300.00 AS Decimal(8, 2)), 205, 11)
GO
SET IDENTITY_INSERT [dbo].[employees] OFF
GO
SET IDENTITY_INSERT [dbo].[jobs] ON 
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (1, N'Public Accountant', CAST(4200.00 AS Decimal(8, 2)), CAST(9000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (2, N'Accounting Manager', CAST(8200.00 AS Decimal(8, 2)), CAST(16000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (3, N'Administration Assistant', CAST(3000.00 AS Decimal(8, 2)), CAST(6000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (4, N'President', CAST(20000.00 AS Decimal(8, 2)), CAST(40000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (5, N'Administration Vice President', CAST(15000.00 AS Decimal(8, 2)), CAST(30000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (6, N'Accountant', CAST(4200.00 AS Decimal(8, 2)), CAST(9000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (7, N'Finance Manager', CAST(8200.00 AS Decimal(8, 2)), CAST(16000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (8, N'Human Resources Representative', CAST(4000.00 AS Decimal(8, 2)), CAST(9000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (9, N'Programmer', CAST(4000.00 AS Decimal(8, 2)), CAST(10000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (10, N'Marketing Manager', CAST(9000.00 AS Decimal(8, 2)), CAST(15000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (11, N'Marketing Representative', CAST(4000.00 AS Decimal(8, 2)), CAST(9000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (12, N'Public Relations Representative', CAST(4500.00 AS Decimal(8, 2)), CAST(10500.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (13, N'Purchasing Clerk', CAST(2500.00 AS Decimal(8, 2)), CAST(5500.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (14, N'Purchasing Manager', CAST(8000.00 AS Decimal(8, 2)), CAST(15000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (15, N'Sales Manager', CAST(10000.00 AS Decimal(8, 2)), CAST(20000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (16, N'Sales Representative', CAST(6000.00 AS Decimal(8, 2)), CAST(12000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (17, N'Shipping Clerk', CAST(2500.00 AS Decimal(8, 2)), CAST(5500.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (18, N'Stock Clerk', CAST(2000.00 AS Decimal(8, 2)), CAST(5000.00 AS Decimal(8, 2)))
GO
INSERT [dbo].[jobs] ([job_id], [job_title], [min_salary], [max_salary]) VALUES (19, N'Stock Manager', CAST(5500.00 AS Decimal(8, 2)), CAST(8500.00 AS Decimal(8, 2)))
GO
SET IDENTITY_INSERT [dbo].[jobs] OFF
GO
SET IDENTITY_INSERT [dbo].[locations] ON 
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (1400, N'2014 Jabberwocky Rd', N'26192', N'Southlake', N'Texas', N'US')
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (1500, N'2011 Interiors Blvd', N'99236', N'South San Francisco', N'California', N'US')
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (1700, N'2004 Charade Rd', N'98199', N'Seattle', N'Washington', N'US')
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (1800, N'147 Spadina Ave', N'M5V 2L7', N'Toronto', N'Ontario', N'CA')
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (2400, N'8204 Arthur St', NULL, N'London', NULL, N'UK')
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (2500, N'Magdalen Centre, The Oxford Science Park', N'OX9 9ZB', N'Oxford', N'Oxford', N'UK')
GO
INSERT [dbo].[locations] ([location_id], [street_address], [postal_code], [city], [state_province], [country_id]) VALUES (2700, N'Schwanthalerstr. 7031', N'80925', N'Munich', N'Bavaria', N'DE')
GO
SET IDENTITY_INSERT [dbo].[locations] OFF
GO
SET IDENTITY_INSERT [dbo].[regions] ON 
GO
INSERT [dbo].[regions] ([region_id], [region_name]) VALUES (1, N'Europe')
GO
INSERT [dbo].[regions] ([region_id], [region_name]) VALUES (2, N'Americas')
GO
INSERT [dbo].[regions] ([region_id], [region_name]) VALUES (3, N'Asia')
GO
INSERT [dbo].[regions] ([region_id], [region_name]) VALUES (4, N'Middle East and Africa')
GO
SET IDENTITY_INSERT [dbo].[regions] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 17/11/2023 03:13:04 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 17/11/2023 03:13:04 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 17/11/2023 03:13:04 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 17/11/2023 03:13:04 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 17/11/2023 03:13:04 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 17/11/2023 03:13:04 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 17/11/2023 03:13:04 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[countries] ADD  DEFAULT (NULL) FOR [country_name]
GO
ALTER TABLE [dbo].[departments] ADD  DEFAULT (NULL) FOR [location_id]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [first_name]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [phone_number]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [manager_id]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [department_id]
GO
ALTER TABLE [dbo].[jobs] ADD  DEFAULT (NULL) FOR [min_salary]
GO
ALTER TABLE [dbo].[jobs] ADD  DEFAULT (NULL) FOR [max_salary]
GO
ALTER TABLE [dbo].[locations] ADD  DEFAULT (NULL) FOR [street_address]
GO
ALTER TABLE [dbo].[locations] ADD  DEFAULT (NULL) FOR [postal_code]
GO
ALTER TABLE [dbo].[locations] ADD  DEFAULT (NULL) FOR [state_province]
GO
ALTER TABLE [dbo].[regions] ADD  DEFAULT (NULL) FOR [region_name]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[countries]  WITH CHECK ADD FOREIGN KEY([region_id])
REFERENCES [dbo].[regions] ([region_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[departments]  WITH CHECK ADD FOREIGN KEY([location_id])
REFERENCES [dbo].[locations] ([location_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[dependents]  WITH CHECK ADD FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([employee_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD FOREIGN KEY([department_id])
REFERENCES [dbo].[departments] ([department_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD FOREIGN KEY([job_id])
REFERENCES [dbo].[jobs] ([job_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD FOREIGN KEY([manager_id])
REFERENCES [dbo].[employees] ([employee_id])
GO
ALTER TABLE [dbo].[locations]  WITH CHECK ADD FOREIGN KEY([country_id])
REFERENCES [dbo].[countries] ([country_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [HrDB] SET  READ_WRITE 
GO
