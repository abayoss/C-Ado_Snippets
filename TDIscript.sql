USE [master]
GO
/****** Object:  Database [TDI]    Script Date: 6/17/2018 5:11:01 PM ******/
CREATE DATABASE [TDI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TDI', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TDI.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TDI_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TDI_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TDI] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TDI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TDI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TDI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TDI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TDI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TDI] SET ARITHABORT OFF 
GO
ALTER DATABASE [TDI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TDI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TDI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TDI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TDI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TDI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TDI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TDI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TDI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TDI] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TDI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TDI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TDI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TDI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TDI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TDI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TDI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TDI] SET RECOVERY FULL 
GO
ALTER DATABASE [TDI] SET  MULTI_USER 
GO
ALTER DATABASE [TDI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TDI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TDI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TDI] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TDI] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TDI', N'ON'
GO
USE [TDI]
GO
/****** Object:  Table [dbo].[CATEGORIE]    Script Date: 6/17/2018 5:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CATEGORIE](
	[IDCAT] [int] NOT NULL,
	[Nom] [varchar](50) NULL,
 CONSTRAINT [PK_CATEGORIE] PRIMARY KEY CLUSTERED 
(
	[IDCAT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PRODUIT]    Script Date: 6/17/2018 5:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PRODUIT](
	[ref] [int] NOT NULL,
	[Designation] [varchar](50) NULL,
	[QTE] [varchar](50) NULL,
	[IDCAT] [int] NULL,
 CONSTRAINT [PK_PRODUIT] PRIMARY KEY CLUSTERED 
(
	[ref] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[STAGIAIRE]    Script Date: 6/17/2018 5:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STAGIAIRE](
	[Mat] [int] NULL,
	[nom] [nvarchar](50) NULL,
	[Prenom] [nvarchar](50) NULL,
	[Moyenne] [float] NULL,
	[Age] [int] NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PRODUIT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUIT_CATEGORIE] FOREIGN KEY([IDCAT])
REFERENCES [dbo].[CATEGORIE] ([IDCAT])
GO
ALTER TABLE [dbo].[PRODUIT] CHECK CONSTRAINT [FK_PRODUIT_CATEGORIE]
GO
/****** Object:  StoredProcedure [dbo].[stored]    Script Date: 6/17/2018 5:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stored] 
    @IDCAT int ,
    @NomCAT varchar (50)
AS
	--declare @err int
    --begin
	--set @err = 0 
	update CATEGORIE set Nom=@NomCAT where IDCAT= @IDCAT
	--set @err = @err + @@ERROR
	--if @err != 0
	--set @err = 1
	--end
--RETURN @err
GO
USE [master]
GO
ALTER DATABASE [TDI] SET  READ_WRITE 
GO
