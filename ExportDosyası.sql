USE [master]
GO
/****** Object:  Database [hastane]    Script Date: 28.05.2024 22:52:49 ******/
CREATE DATABASE [hastane]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'hastane', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVE\MSSQL\DATA\hastane.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'hastane_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVE\MSSQL\DATA\hastane_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [hastane].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [hastane] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [hastane] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [hastane] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [hastane] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [hastane] SET ARITHABORT OFF 
GO
ALTER DATABASE [hastane] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [hastane] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [hastane] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [hastane] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [hastane] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [hastane] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [hastane] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [hastane] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [hastane] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [hastane] SET  ENABLE_BROKER 
GO
ALTER DATABASE [hastane] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [hastane] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [hastane] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [hastane] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [hastane] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [hastane] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [hastane] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [hastane] SET RECOVERY FULL 
GO
ALTER DATABASE [hastane] SET  MULTI_USER 
GO
ALTER DATABASE [hastane] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [hastane] SET DB_CHAINING OFF 
GO
ALTER DATABASE [hastane] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [hastane] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [hastane] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'hastane', N'ON'
GO
ALTER DATABASE [hastane] SET QUERY_STORE = ON
GO
ALTER DATABASE [hastane] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO
USE [hastane]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 28.05.2024 22:52:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ad] [nvarchar](50) NULL,
	[sifre] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bolumler]    Script Date: 28.05.2024 22:52:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bolumler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BölümAdı] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doktorlar]    Script Date: 28.05.2024 22:52:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doktorlar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](50) NULL,
	[Soyad] [nvarchar](50) NULL,
	[Bolumİd] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanıcı]    Script Date: 28.05.2024 22:52:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanıcı](
	[İd] [int] IDENTITY(1,1) NOT NULL,
	[Tc] [int] NULL,
	[Sifre] [nchar](50) NULL,
	[Ad] [nchar](50) NULL,
	[Soyad] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[İd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Randevu]    Script Date: 28.05.2024 22:52:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Randevu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Bolumİd] [int] NULL,
	[Doktorİd] [int] NULL,
	[Gün] [nvarchar](50) NULL,
	[Saat] [nvarchar](50) NULL,
	[KullaniciId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[admin] ON 

INSERT [dbo].[admin] ([id], [ad], [sifre]) VALUES (1, N'admin', N'123')
SET IDENTITY_INSERT [dbo].[admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Bolumler] ON 

INSERT [dbo].[Bolumler] ([id], [BölümAdı]) VALUES (1, N'Cildiye')
INSERT [dbo].[Bolumler] ([id], [BölümAdı]) VALUES (2, N'Ortopedi')
INSERT [dbo].[Bolumler] ([id], [BölümAdı]) VALUES (3, N'Kulak Burun Boğaz')
INSERT [dbo].[Bolumler] ([id], [BölümAdı]) VALUES (4, N'Dahliye')
INSERT [dbo].[Bolumler] ([id], [BölümAdı]) VALUES (5, N'Üroloji')
SET IDENTITY_INSERT [dbo].[Bolumler] OFF
GO
SET IDENTITY_INSERT [dbo].[Doktorlar] ON 

INSERT [dbo].[Doktorlar] ([id], [Ad], [Soyad], [Bolumİd]) VALUES (1, N'Yağız', N'Bektaş', 1)
INSERT [dbo].[Doktorlar] ([id], [Ad], [Soyad], [Bolumİd]) VALUES (2, N'Emirhan', N'Tali', 3)
INSERT [dbo].[Doktorlar] ([id], [Ad], [Soyad], [Bolumİd]) VALUES (3, N'Cebrail', N'Ergin', 2)
INSERT [dbo].[Doktorlar] ([id], [Ad], [Soyad], [Bolumİd]) VALUES (4, N'Eren', N'Aydın', 5)
INSERT [dbo].[Doktorlar] ([id], [Ad], [Soyad], [Bolumİd]) VALUES (5, N'Mert', N'Kaya', 4)
SET IDENTITY_INSERT [dbo].[Doktorlar] OFF
GO
SET IDENTITY_INSERT [dbo].[Kullanıcı] ON 

INSERT [dbo].[Kullanıcı] ([İd], [Tc], [Sifre], [Ad], [Soyad]) VALUES (1, 1234567891, N'elma123                                           ', N'Adnan                                             ', N'Teke                                              ')
SET IDENTITY_INSERT [dbo].[Kullanıcı] OFF
GO
ALTER TABLE [dbo].[Doktorlar]  WITH CHECK ADD FOREIGN KEY([Bolumİd])
REFERENCES [dbo].[Bolumler] ([id])
GO
ALTER TABLE [dbo].[Randevu]  WITH CHECK ADD FOREIGN KEY([Bolumİd])
REFERENCES [dbo].[Bolumler] ([id])
GO
ALTER TABLE [dbo].[Randevu]  WITH CHECK ADD FOREIGN KEY([Doktorİd])
REFERENCES [dbo].[Doktorlar] ([id])
GO
ALTER TABLE [dbo].[Randevu]  WITH CHECK ADD FOREIGN KEY([KullaniciId])
REFERENCES [dbo].[Kullanıcı] ([İd])
GO
USE [master]
GO
ALTER DATABASE [hastane] SET  READ_WRITE 
GO
