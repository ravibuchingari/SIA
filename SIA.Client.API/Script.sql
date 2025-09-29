USE [master]
GO
/****** Object:  Database [SIA]    Script Date: 29-09-2025 06:10:01 ******/
CREATE DATABASE [SIA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SIA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SIA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SIA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SIA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SIA] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SIA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SIA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SIA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SIA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SIA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SIA] SET ARITHABORT OFF 
GO
ALTER DATABASE [SIA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SIA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SIA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SIA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SIA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SIA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SIA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SIA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SIA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SIA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SIA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SIA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SIA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SIA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SIA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SIA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SIA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SIA] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SIA] SET  MULTI_USER 
GO
ALTER DATABASE [SIA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SIA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SIA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SIA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SIA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SIA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SIA] SET QUERY_STORE = OFF
GO
USE [SIA]
GO
/****** Object:  Table [dbo].[BillingHistory]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingHistory](
	[BillingId] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [tinyint] NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[BillingStatus] [varchar](20) NOT NULL,
	[BillingDate] [datetime] NOT NULL,
	[ProviderTransactionId] [varchar](100) NULL,
	[Currency] [varchar](3) NOT NULL,
	[TaxAmount] [decimal](10, 2) NOT NULL,
	[DiscountAmount] [decimal](10, 2) NOT NULL,
	[RetryCount] [int] NOT NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BillingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarEvents]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarEvents](
	[EventId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserAccountId] [int] NOT NULL,
	[ProviderEventId] [varchar](255) NOT NULL,
	[EventTitle] [varchar](100) NULL,
	[EventDescription] [varchar](4000) NULL,
	[EventLocation] [varchar](100) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[MeetingUrl] [varchar](512) NULL,
	[MeetingPasscode] [varchar](255) NULL,
	[MeetingId] [varchar](255) NULL,
	[DialInInfo] [varchar](4000) NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponId] [int] IDENTITY(1,1) NOT NULL,
	[CouponGUID] [uniqueidentifier] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DiscountType] [varchar](20) NOT NULL,
	[DiscountValue] [decimal](10, 2) NOT NULL,
	[MaxRedemptions] [int] NULL,
	[CurrentRedemptions] [int] NULL,
	[ExpiryDate] [datetime] NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CouponGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailMessages]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailMessages](
	[EmailMessageId] [varchar](30) NOT NULL,
	[EmailSubject] [varchar](100) NOT NULL,
	[EmailBody] [nvarchar](4000) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[EmailDisplayName] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmailMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EmailSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailServer]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailServer](
	[EmailSmtpHost] [varchar](150) NOT NULL,
	[EmailPort] [int] NOT NULL,
	[EmailSSLEnabled] [bit] NOT NULL,
	[EmailUserId] [varchar](150) NOT NULL,
	[EmailPassword] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmailSmtpHost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventParticipants]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventParticipants](
	[EventParticipantId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventParticipantGUID] [uniqueidentifier] NOT NULL,
	[EventId] [bigint] NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[DisplayName] [varchar](100) NULL,
	[ParticipantStatus] [varchar](30) NOT NULL,
	[IsOrganizer] [bit] NOT NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EventParticipantGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [tinyint] NOT NULL,
	[InvoiceNumber] [varchar](50) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[InvoiceStatus] [varchar](20) NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[TaxAmount] [decimal](10, 2) NOT NULL,
	[DiscountAmount] [decimal](10, 2) NOT NULL,
	[Currency] [varchar](3) NOT NULL,
	[DownloadUrl] [varchar](255) NULL,
	[ProviderInvoiceId] [varchar](100) NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[InvoiceNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageCode] [varchar](10) NOT NULL,
	[LanguageName] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LanguageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingSettings]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingSettings](
	[EventId] [bigint] NOT NULL,
	[MeetingDriver] [varchar](20) NOT NULL,
	[ApplyToRecurring] [bit] NOT NULL,
	[MeetingType] [varchar](20) NOT NULL,
	[IsVideoMandatory] [bit] NOT NULL,
	[Agenda] [nvarchar](4000) NULL,
	[AgendaMyBrief] [nvarchar](4000) NULL,
	[AgendaParticipantBrief] [nvarchar](4000) NULL,
	[AgendaSuggestions] [nvarchar](4000) NULL,
	[ExpectedOutcome] [nvarchar](4000) NULL,
	[OutcomeMyBrief] [nvarchar](4000) NULL,
	[OutcomeParticipantBrief] [nvarchar](4000) NULL,
	[OutcomeSuggestions] [nvarchar](4000) NULL,
	[SendPreMeetingNotifications] [bit] NOT NULL,
	[NotificationParticipantScope] [varchar](20) NOT NULL,
	[NotificationDataQuery] [nvarchar](4000) NULL,
	[NotificationTime] [time](7) NULL,
	[IsSIANotesEnabled] [bit] NOT NULL,
	[SIANoteTakingLevel] [varchar](20) NOT NULL,
	[SIANoteContentType] [varchar](20) NOT NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingSummaries]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingSummaries](
	[EventId] [bigint] NOT NULL,
	[SummaryData] [varchar](max) NOT NULL,
	[GeneratorVersion] [varchar](50) NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[OrganizationId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationGUID] [uniqueidentifier] NOT NULL,
	[OrganizationName] [varchar](100) NOT NULL,
	[OrganizationSize] [int] NOT NULL,
	[ContactPerson] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Email] [varchar](150) NOT NULL,
	[CreatedUser] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
	[SubscriptionId] [tinyint] NOT NULL,
	[IsBusiness] [bit] NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[EmailVerificationToken] [varchar](60) NULL,
	[EmailVerificationTokenTime] [datetime] NOT NULL,
	[OrganizationStatusId] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrganizationGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrganizationStatus]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationStatus](
	[OrganizationStatusId] [tinyint] NOT NULL,
	[OrganizationStatusName] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrganizationStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrganizationStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Providers]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Providers](
	[ProviderId] [tinyint] NOT NULL,
	[ProviderName] [varchar](30) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProviderName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[UserId] [bigint] NOT NULL,
	[Token] [varchar](500) NOT NULL,
	[Expires] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SIATimeZones]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIATimeZones](
	[TimeZoneName] [varchar](100) NOT NULL,
	[UTCOffset] [varchar](10) NOT NULL,
	[CommonRegions] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeZoneName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscriptions]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscriptions](
	[SubscriptionId] [tinyint] NOT NULL,
	[SubscriptionName] [varchar](30) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[SubscriptionDescription] [varchar](512) NULL,
	[MaxMeetings] [int] NOT NULL,
	[AIHours] [int] NOT NULL,
	[SupportLevel] [varchar](20) NOT NULL,
	[BillingCycleOptions] [varchar](512) NOT NULL,
	[TrialPeriodDays] [int] NOT NULL,
	[AddOns] [varchar](512) NULL,
	[FeatureFlags] [varchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[SubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SubscriptionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuperUsers]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuperUsers](
	[UserRowID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](150) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[UserSaltKey] [varchar](100) NOT NULL,
	[FirstName] [varchar](150) NOT NULL,
	[LastName] [varchar](150) NOT NULL,
	[Mobile] [varchar](10) NOT NULL,
	[AccessToken] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserAccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserAccountGUID] [uniqueidentifier] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ProviderId] [tinyint] NOT NULL,
	[ProviderAccountId] [varchar](150) NOT NULL,
	[HashPassword] [varchar](255) NOT NULL,
	[CalendaIntegratedStatus] [varchar](30) NOT NULL,
	[UserAccountStatus] [varchar](30) NOT NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserAccountGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[RoleId] [tinyint] NOT NULL,
	[RoleName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DisplayName] [varchar](100) NOT NULL,
	[Email] [varchar](320) NOT NULL,
	[CountryCode] [varchar](5) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[ProfileImageUrl] [varchar](150) NOT NULL,
	[HashPassword] [varchar](255) NOT NULL,
	[PasswordSalt] [varchar](100) NOT NULL,
	[RefreshToken] [varchar](512) NOT NULL,
	[TimeZone] [varchar](50) NOT NULL,
	[Language] [varchar](10) NOT NULL,
	[TimeFormat] [varchar](10) NOT NULL,
	[DateFormat] [varchar](20) NOT NULL,
	[RoleId] [tinyint] NOT NULL,
	[IsSignUpUser] [bit] NOT NULL,
	[SecretKey] [varchar](250) NOT NULL,
	[SecurityKey] [varchar](60) NULL,
	[SocialAuthId] [varchar](30) NULL,
	[CreatedUser] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UserStatus] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSessions]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSessions](
	[UserSessionId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserSessionGUID] [uniqueidentifier] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[DeviceName] [varchar](100) NULL,
	[DeviceType] [varchar](30) NOT NULL,
	[OperatingSystem] [varchar](50) NULL,
	[GeographicLocation] [varchar](100) NULL,
	[Country] [varchar](50) NULL,
	[SessionToken] [varchar](512) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[LastActivity] [datetime] NOT NULL,
	[SessionStatus] [varchar](30) NOT NULL,
	[LogoutTime] [datetime] NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SessionToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserSessionGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSubscriptions]    Script Date: 29-09-2025 06:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSubscriptions](
	[UserSubscriptionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[SubscriptionId] [tinyint] NOT NULL,
	[BillingCycle] [varchar](20) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[NextRenewal] [datetime] NULL,
	[TrialEndDate] [datetime] NULL,
	[RenewalType] [varchar](30) NOT NULL,
	[GracePeriodEnd] [datetime] NULL,
	[RetryAttempts] [int] NOT NULL,
	[CancellationReason] [nvarchar](4000) NULL,
	[ParentSubscriptionId] [tinyint] NULL,
	[ProrationApplied] [bit] NOT NULL,
	[Currency] [varchar](3) NOT NULL,
	[CreatedUser] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DeletedUser] [bigint] NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserSubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CalendarEvents]    Script Date: 29-09-2025 06:10:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CalendarEvents] ON [dbo].[CalendarEvents]
(
	[UserId] ASC,
	[UserAccountId] ASC,
	[ProviderEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EventParticipants]    Script Date: 29-09-2025 06:10:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EventParticipants] ON [dbo].[EventParticipants]
(
	[EventId] ASC,
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT ('Pending') FOR [BillingStatus]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT ('USD') FOR [Currency]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT ((0)) FOR [RetryCount]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[BillingHistory] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CalendarEvents] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CalendarEvents] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[CalendarEvents] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Coupons] ADD  DEFAULT (newid()) FOR [CouponGUID]
GO
ALTER TABLE [dbo].[Coupons] ADD  DEFAULT ((0)) FOR [CurrentRedemptions]
GO
ALTER TABLE [dbo].[Coupons] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Coupons] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Coupons] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[EmailMessages] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmailMessages] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[EmailMessages] ADD  DEFAULT ('SIA') FOR [EmailDisplayName]
GO
ALTER TABLE [dbo].[EmailServer] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EventParticipants] ADD  DEFAULT (newid()) FOR [EventParticipantGUID]
GO
ALTER TABLE [dbo].[EventParticipants] ADD  DEFAULT ('NeedsAction') FOR [ParticipantStatus]
GO
ALTER TABLE [dbo].[EventParticipants] ADD  DEFAULT ((0)) FOR [IsOrganizer]
GO
ALTER TABLE [dbo].[EventParticipants] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EventParticipants] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ('Pending') FOR [InvoiceStatus]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ('USD') FOR [Currency]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ((0)) FOR [ApplyToRecurring]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ('Hybrid') FOR [MeetingType]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ((0)) FOR [IsVideoMandatory]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ((0)) FOR [SendPreMeetingNotifications]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ('All') FOR [NotificationParticipantScope]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ((1)) FOR [IsSIANotesEnabled]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ('Manual') FOR [SIANoteTakingLevel]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ('All') FOR [SIANoteContentType]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[MeetingSettings] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MeetingSummaries] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MeetingSummaries] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[MeetingSummaries] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Organizations] ADD  DEFAULT (newid()) FOR [OrganizationGUID]
GO
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Organizations_OrganizationSize]  DEFAULT ((1)) FOR [OrganizationSize]
GO
ALTER TABLE [dbo].[Organizations] ADD  DEFAULT ((0)) FOR [IsBusiness]
GO
ALTER TABLE [dbo].[Organizations] ADD  DEFAULT ((0)) FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Organizations] ADD  DEFAULT (getdate()) FOR [EmailVerificationTokenTime]
GO
ALTER TABLE [dbo].[Providers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ((0)) FOR [MaxMeetings]
GO
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ((0)) FOR [AIHours]
GO
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ('Basic') FOR [SupportLevel]
GO
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ('') FOR [BillingCycleOptions]
GO
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ((0)) FOR [TrialPeriodDays]
GO
ALTER TABLE [dbo].[SuperUsers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SuperUsers] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[SuperUsers] ADD  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT (newid()) FOR [UserAccountGUID]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT ('Active') FOR [UserAccountStatus]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [UserGUID]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RefreshToken]  DEFAULT (newid()) FOR [RefreshToken]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('UTC') FOR [TimeZone]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('en') FOR [Language]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('24H') FOR [TimeFormat]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('YYYY-MM-DD') FOR [DateFormat]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_SecretKey]  DEFAULT (newid()) FOR [SecretKey]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_SecurityKey]  DEFAULT (newid()) FOR [SecurityKey]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_UserStatus]  DEFAULT ('Active') FOR [UserStatus]
GO
ALTER TABLE [dbo].[UserSessions] ADD  DEFAULT (newid()) FOR [UserSessionGUID]
GO
ALTER TABLE [dbo].[UserSessions] ADD  DEFAULT (getdate()) FOR [LoginTime]
GO
ALTER TABLE [dbo].[UserSessions] ADD  DEFAULT (getdate()) FOR [LastActivity]
GO
ALTER TABLE [dbo].[UserSessions] ADD  DEFAULT ('Active') FOR [SessionStatus]
GO
ALTER TABLE [dbo].[UserSessions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserSessions] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT ('AutoRenew') FOR [RenewalType]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT ((0)) FOR [RetryAttempts]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT ((0)) FOR [ProrationApplied]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT ('USD') FOR [Currency]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[BillingHistory]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BillingHistory]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BillingHistory]  WITH CHECK ADD FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[BillingHistory]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BillingHistory]  WITH CHECK ADD FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([SubscriptionId])
GO
ALTER TABLE [dbo].[CalendarEvents]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[CalendarEvents]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[CalendarEvents]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[CalendarEvents]  WITH CHECK ADD FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[UserAccounts] ([UserAccountId])
GO
ALTER TABLE [dbo].[CalendarEvents]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD FOREIGN KEY([EventId])
REFERENCES [dbo].[CalendarEvents] ([EventId])
GO
ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([SubscriptionId])
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD FOREIGN KEY([EventId])
REFERENCES [dbo].[CalendarEvents] ([EventId])
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[MeetingSummaries]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[MeetingSummaries]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[MeetingSummaries]  WITH CHECK ADD FOREIGN KEY([EventId])
REFERENCES [dbo].[CalendarEvents] ([EventId])
GO
ALTER TABLE [dbo].[MeetingSummaries]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD FOREIGN KEY([OrganizationStatusId])
REFERENCES [dbo].[OrganizationStatus] ([OrganizationStatusId])
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([SubscriptionId])
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Users] FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_Users]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Users1] FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_Users1]
GO
ALTER TABLE [dbo].[SuperUsers]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[SuperUsers] ([UserRowID])
GO
ALTER TABLE [dbo].[SuperUsers]  WITH CHECK ADD FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[SuperUsers] ([UserRowID])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Providers] ([ProviderId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([OrganizationId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRole] ([RoleId])
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([DeletedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([SubscriptionId])
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BillingHistory]  WITH CHECK ADD CHECK  (([BillingStatus]='Disputed' OR [BillingStatus]='Refunded' OR [BillingStatus]='Failed' OR [BillingStatus]='Pending' OR [BillingStatus]='Paid'))
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD CHECK  (([DiscountType]='FixedAmount' OR [DiscountType]='Percentage'))
GO
ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD CHECK  (([ParticipantStatus]='Accepted' OR [ParticipantStatus]='Tentative' OR [ParticipantStatus]='Declined' OR [ParticipantStatus]='NeedsAction'))
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD CHECK  (([InvoiceStatus]='Draft' OR [InvoiceStatus]='Void' OR [InvoiceStatus]='Overdue' OR [InvoiceStatus]='Pending' OR [InvoiceStatus]='Paid'))
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD CHECK  (([MeetingDriver]='USER' OR [MeetingDriver]='SIA'))
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD CHECK  (([MeetingType]='Hybrid' OR [MeetingType]='InPerson' OR [MeetingType]='Virtual'))
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD CHECK  (([NotificationParticipantScope]='Selected' OR [NotificationParticipantScope]='All'))
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD CHECK  (([SIANoteTakingLevel]='AutoTrash' OR [SIANoteTakingLevel]='Manual' OR [SIANoteTakingLevel]='None'))
GO
ALTER TABLE [dbo].[MeetingSettings]  WITH CHECK ADD CHECK  (([SIANoteContentType]='Redacted' OR [SIANoteContentType]='All'))
GO
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD  CONSTRAINT [CK__Subscript__Suppo__5C37ACAD] CHECK  (([SupportLevel]='Support24x7' OR [SupportLevel]='Priority' OR [SupportLevel]='Basic'))
GO
ALTER TABLE [dbo].[Subscriptions] CHECK CONSTRAINT [CK__Subscript__Suppo__5C37ACAD]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD CHECK  (([CalendaIntegratedStatus]='Removed' OR [CalendaIntegratedStatus]='Deactivated' OR [CalendaIntegratedStatus]='Active' OR [CalendaIntegratedStatus]='Pending'))
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD CHECK  (([UserAccountStatus]='Deleted' OR [UserAccountStatus]='Deactivated' OR [UserAccountStatus]='Active'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([UserStatus]='Deleted' OR [UserStatus]='Deactivated' OR [UserStatus]='Active'))
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD CHECK  (([DeviceType]='Other' OR [DeviceType]='Web' OR [DeviceType]='Tablet' OR [DeviceType]='Mobile' OR [DeviceType]='Desktop'))
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD CHECK  (([SessionStatus]='Terminated' OR [SessionStatus]='Expired' OR [SessionStatus]='Inactive' OR [SessionStatus]='Active'))
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD CHECK  (([BillingCycle]='Yearly' OR [BillingCycle]='Monthly'))
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD CHECK  (([RenewalType]='Manual' OR [RenewalType]='AutoRenew'))
GO
USE [master]
GO
ALTER DATABASE [SIA] SET  READ_WRITE 
GO
