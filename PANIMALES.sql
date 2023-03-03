USE [master]
GO
/****** Object:  Database [PANIMALES]    Script Date: 03/03/2023 12:48:05 ******/
CREATE DATABASE [PANIMALES]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PANIMALES', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\PANIMALES.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PANIMALES_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\PANIMALES_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PANIMALES] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PANIMALES].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PANIMALES] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PANIMALES] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PANIMALES] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PANIMALES] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PANIMALES] SET ARITHABORT OFF 
GO
ALTER DATABASE [PANIMALES] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PANIMALES] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PANIMALES] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PANIMALES] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PANIMALES] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PANIMALES] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PANIMALES] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PANIMALES] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PANIMALES] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PANIMALES] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PANIMALES] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PANIMALES] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PANIMALES] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PANIMALES] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PANIMALES] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PANIMALES] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PANIMALES] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PANIMALES] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PANIMALES] SET  MULTI_USER 
GO
ALTER DATABASE [PANIMALES] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PANIMALES] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PANIMALES] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PANIMALES] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PANIMALES] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PANIMALES] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PANIMALES] SET QUERY_STORE = ON
GO
ALTER DATABASE [PANIMALES] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PANIMALES]
GO
/****** Object:  Table [dbo].[ADOPCIONES]    Script Date: 03/03/2023 12:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADOPCIONES](
	[IDMASCOTA] [int] NULL,
	[IDUSER] [int] NULL,
	[FECHA_ADOPCION] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MASCOTAS]    Script Date: 03/03/2023 12:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MASCOTAS](
	[IDMASCOTA] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[RAZA] [nvarchar](50) NULL,
	[EDAD] [int] NULL,
	[ANCHO] [float] NULL,
	[ALTO] [float] NULL,
	[PESO] [float] NULL,
	[DESCRIPCION] [nvarchar](100) NULL,
	[PELIGROSIDAD] [bit] NULL,
	[IMAGEN] [nvarchar](600) NULL,
	[ADOPTADO] [bit] NULL,
	[IDREFUGIO] [int] NULL,
 CONSTRAINT [PK_MASCOTAS] PRIMARY KEY CLUSTERED 
(
	[IDMASCOTA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REFUGIOS]    Script Date: 03/03/2023 12:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REFUGIOS](
	[IDREFUGIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[LOCALIDAD] [nvarchar](50) NULL,
	[UBICACION] [nchar](50) NULL,
	[IMAGEN] [nvarchar](50) NULL,
	[VALORACION] [nvarchar](50) NULL,
 CONSTRAINT [PK_REFUGIOS] PRIMARY KEY CLUSTERED 
(
	[IDREFUGIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 03/03/2023 12:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[IDUSER] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[BIRDTHDAY] [nvarchar](50) NULL,
	[PHONE] [nvarchar](50) NULL,
	[USERNAME] [nvarchar](50) NULL,
	[PASSWORD] [nvarchar](50) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[IDUSER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (1, N'AMBER', N'FUEGO', 21, 32.12, 21.23, 53, N'DESCRIPCION...                                                                                      ', 1, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', NULL, NULL)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (2, N'PELÍN', N'PERRO PASTO', 12, 12.12, 9.98, 33.35, N'Este perro es un perro alemán que le encanta jugar', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', NULL, NULL)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (3, N'BACÓN', N'Border collie', 4, 6, 7, 11.5, N'Este perro es un de los más queridos ene nuestra perrera...', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', NULL, NULL)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (4, N'Doggie', N'husky', 6, 3, 9.39, 7.35, N'Es un perro tranquilo que le encanta pasear....', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', NULL, NULL)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (5, N'Mulo', N'Golden Retriver', 3, 2, 4, 5, N'Es un perro pequeño...', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', NULL, NULL)
GO
INSERT [dbo].[USERS] ([IDUSER], [NAME], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD]) VALUES (1, N'Admin', N'admin@gmail.com', N'14/05/2000', N'634534985345', N'admin1234', N'admin1234')
INSERT [dbo].[USERS] ([IDUSER], [NAME], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD]) VALUES (2, N'Manolo', N'manuelolo@gmail.com', N'2023-01-30', N'10101011010', N'manu1234', N'asdasdasda')
INSERT [dbo].[USERS] ([IDUSER], [NAME], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD]) VALUES (3, N'Manolo2', N'manuelolo2@gmail.com', N'2023-01-30', N'10101011010', N'manu12345', N'manu1234')
INSERT [dbo].[USERS] ([IDUSER], [NAME], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD]) VALUES (4, N'Cristina', N'cristiaspnet@gmail.com', N'2023-01-18', N'10101011010', N'crist12345', N'cristi12345')
GO
ALTER TABLE [dbo].[ADOPCIONES]  WITH CHECK ADD  CONSTRAINT [FK_ADOPCIONES_MASCOTAS] FOREIGN KEY([IDMASCOTA])
REFERENCES [dbo].[MASCOTAS] ([IDMASCOTA])
GO
ALTER TABLE [dbo].[ADOPCIONES] CHECK CONSTRAINT [FK_ADOPCIONES_MASCOTAS]
GO
ALTER TABLE [dbo].[ADOPCIONES]  WITH CHECK ADD  CONSTRAINT [FK_ADOPCIONES_USERS] FOREIGN KEY([IDUSER])
REFERENCES [dbo].[USERS] ([IDUSER])
GO
ALTER TABLE [dbo].[ADOPCIONES] CHECK CONSTRAINT [FK_ADOPCIONES_USERS]
GO
ALTER TABLE [dbo].[MASCOTAS]  WITH CHECK ADD  CONSTRAINT [FK_MASCOTAS_REFUGIOS] FOREIGN KEY([IDREFUGIO])
REFERENCES [dbo].[REFUGIOS] ([IDREFUGIO])
GO
ALTER TABLE [dbo].[MASCOTAS] CHECK CONSTRAINT [FK_MASCOTAS_REFUGIOS]
GO
USE [master]
GO
ALTER DATABASE [PANIMALES] SET  READ_WRITE 
GO
