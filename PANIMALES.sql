USE [master]
GO
/****** Object:  Database [PANIMALES]    Script Date: 23/03/2023 8:07:48 ******/
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
/****** Object:  Table [dbo].[REFUGIOS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REFUGIOS](
	[IDREFUGIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[LOCALIDAD] [nvarchar](50) NULL,
	[UBICACION] [nchar](50) NULL,
	[IMAGEN] [nvarchar](1000) NULL,
	[VALORACION] [int] NULL,
	[DESCRIPCION] [nvarchar](100) NULL,
 CONSTRAINT [PK_REFUGIOS] PRIMARY KEY CLUSTERED 
(
	[IDREFUGIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MASCOTAS]    Script Date: 23/03/2023 8:07:48 ******/
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
	[DESCRIPCION] [nvarchar](600) NULL,
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
/****** Object:  View [dbo].[V_MASCOTAS_REFUGIOS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_MASCOTAS_REFUGIOS]
AS
	SELECT MASCOTAS.* FROM MASCOTAS
	INNER JOIN REFUGIOS
	ON REFUGIOS.IDREFUGIO = MASCOTAS.IDREFUGIO
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[IDUSER] [int] NOT NULL,
	[EMAIL] [nvarchar](50) NULL,
	[BIRDTHDAY] [nvarchar](50) NULL,
	[PHONE] [nvarchar](50) NULL,
	[USERNAME] [nvarchar](50) NULL,
	[PASSWORD] [varbinary](50) NULL,
	[CONTRASENA] [nvarchar](50) NULL,
	[SALT] [nvarchar](100) NULL,
	[IMAGEN] [nvarchar](1000) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[IDUSER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ADOPCIONES]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADOPCIONES](
	[IDMASCOTA] [int] NULL,
	[IDUSER] [int] NULL,
	[FECHA_ADOPCION] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_VER_MASCOTAS_ADOPTADAS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_VER_MASCOTAS_ADOPTADAS]
AS
SELECT MASCOTAS.* FROM MASCOTAS
INNER JOIN ADOPCIONES
ON MASCOTAS.IDMASCOTA = ADOPCIONES.IDMASCOTA
INNER JOIN USERS
ON ADOPCIONES.IDUSER = USERS.IDUSER
GO
/****** Object:  Table [dbo].[COMENTARIOS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMENTARIOS](
	[IDCOMENTARIO] [int] NOT NULL,
	[IDPOST] [int] NULL,
	[EMAIL] [nvarchar](100) NULL,
	[COMENTARIO] [nvarchar](1000) NULL,
	[FECHA] [nvarchar](50) NULL,
 CONSTRAINT [PK_COMENTARIOS] PRIMARY KEY CLUSTERED 
(
	[IDCOMENTARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESCUTEBLOG]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESCUTEBLOG](
	[IDPOST] [int] NOT NULL,
	[TITULO] [nvarchar](50) NULL,
	[CONTENIDO] [nvarchar](1000) NULL,
	[IMAGEN] [nvarchar](600) NULL,
	[IDUSER] [int] NULL,
	[FECHA] [nvarchar](50) NULL,
 CONSTRAINT [PK_RESCUTEBLOG] PRIMARY KEY CLUSTERED 
(
	[IDPOST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VOLUNTARIOS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VOLUNTARIOS](
	[IDVOLUNTARIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[MENSAJE] [nvarchar](600) NULL,
	[IMAGEN] [nvarchar](600) NULL,
	[CORREO] [nvarchar](100) NULL,
	[MUNICIPIO] [nvarchar](50) NULL,
	[FECHA_NACIMIENTO] [nvarchar](50) NULL,
	[IDREFUGIO] [int] NULL,
 CONSTRAINT [PK_VOLUNTARIOS] PRIMARY KEY CLUSTERED 
(
	[IDVOLUNTARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[COMENTARIOS] ([IDCOMENTARIO], [IDPOST], [EMAIL], [COMENTARIO], [FECHA]) VALUES (1, 1, N'admin@gmail.com', N'comentarioadministradorxdxdxdxxdxdxdxdxdxxdxdxdxdxdxxdxd', N'12/2/2022')
INSERT [dbo].[COMENTARIOS] ([IDCOMENTARIO], [IDPOST], [EMAIL], [COMENTARIO], [FECHA]) VALUES (2, 2, N'admin2@gmail.com', N'comentarioadmin2xdxdxdxdxxdxdx', N'22/8/2021')
INSERT [dbo].[COMENTARIOS] ([IDCOMENTARIO], [IDPOST], [EMAIL], [COMENTARIO], [FECHA]) VALUES (3, 1, N'admin1@gmail.com', N'comentario2comentarioadmin1xdxdxdxdxdxdx', N'3/2/2021')
INSERT [dbo].[COMENTARIOS] ([IDCOMENTARIO], [IDPOST], [EMAIL], [COMENTARIO], [FECHA]) VALUES (4, 2, N'admin1@gmail.com', N'comentarioadmin1xdxdxdxdxxdxdx', N'11/11/2021')
GO
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (1, N'AMBER', N'FUEGO', 21, 32.12, 21.23, 53, N'DESCRIPCION...                                                                                      ', 1, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 1)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (2, N'PELÍN', N'PERRO PASTO', 12, 12.12, 9.98, 33.35, N'Este perro es un perro alemán que le encanta jugar', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 2)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (3, N'BACÓN', N'Border collie', 4, 6, 7, 11.5, N'Este perro es un de los más queridos ene nuestra perrera...', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 1)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (4, N'Doggie', N'husky', 6, 3, 9.39, 7.35, N'Es un perro tranquilo que le encanta pasear....', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 2)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (5, N'Mulo', N'Golden Retriver', 3, 2, 4, 5, N'Es un perro pequeño...', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 1)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (6, N'Castel', N'Pitbull', 23, 41.21, 13.13, 44, N'Es el mas enérgico de nuestra protectora...', 1, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 1)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (7, N'Tobi', N'Leonberger', 10, 22, 11, 18, N'Es el perro más entrenado de  nuestra protectora', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 6)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (8, N'Sky', N'Beagle', 6, 6.45, 9.42, 32.21, N'Es el mas querido de nuestra protectora', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 2)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (9, N'Linda', N'Bobtail', 18, 22, 11, 22, N'Es una autentica bola de nieve que te puedes pasar acariciando dia y noche', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 2)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (10, N'Polen', N'Golden Retriver', 8, 6, 12, 22.5, N'Es un perro mágico ya que predice siempre si llueve, hace sol , frio o calor', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 3)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (11, N'Odd', N'Chiguagua', 4, 2, 3.25, 5.57, N'Es un animal mi energico y amistoso', 1, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 4)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (12, N'magnion', N'shiba inu', 8, 3.55, 4.85, 12.77, N'Es una animal tranquilo con el que es un placer estar', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 5)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (13, N'Plag', N'Pastor Aleman', 5, 4.42, 11.75, 10.55, N'Es el animal mas majestuoso de refugio, lider y amigo de todos los animales y voluntarios', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 5)
GO
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (1, N'RefuPerros', N'Madrid', N'C/ Las aguilas, 12                                ', N'https://upload.wikimedia.org/wikipedia/commons/thumb/2/26/Refugio_Zabala01.JPG/1280px-Refugio_Zabala01.JPG', 6, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (2, N'PokeMascotas', N'Mostoles', N'Camino a Mosteles, 5                              ', N'https://media-cdn.tripadvisor.com/media/photo-s/07/60/5a/15/refugio-el-pingarron.jpg', 7, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (3, N'REFU1', N'ALCALA DE HENARES', N'PLAZA HENARES, 7                                  ', N'https://media-cdn.tripadvisor.com/media/photo-s/07/60/5a/15/refugio-el-pingarron.jpg', 6, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (4, N'REFU2', N'ALCOBENDAS', N'CALLE RESILENTES, 5                               ', N'https://media-cdn.tripadvisor.com/media/photo-s/07/60/5a/15/refugio-el-pingarron.jpg', 7, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (5, N'REFU3', N'FUENLABRADA', N'AVENIDA FUENLABRADA, 10                           ', N'https://media-cdn.tripadvisor.com/media/photo-s/07/60/5a/15/refugio-el-pingarron.jpg', 8, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (6, N'REFU4', N'MIRAFLORES DE LA SIERRA', N'Plaza del mirador, 1                              ', N'https://media-cdn.tripadvisor.com/media/photo-s/07/60/5a/15/refugio-el-pingarron.jpg', 9, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
GO
INSERT [dbo].[RESCUTEBLOG] ([IDPOST], [TITULO], [CONTENIDO], [IMAGEN], [IDUSER], [FECHA]) VALUES (1, N'Mi Primer día Voluntari@', N'En mi primer día de voluntariaro me mostraron....', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', 1, N'2/12/2023')
INSERT [dbo].[RESCUTEBLOG] ([IDPOST], [TITULO], [CONTENIDO], [IMAGEN], [IDUSER], [FECHA]) VALUES (2, N'Formación y Voluntariado', N'Durante esta experiencia adquirí conocimientos a otro nnivel para complemtentar mi formacion en veterinaria', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', 2, N'1/2/2021')
INSERT [dbo].[RESCUTEBLOG] ([IDPOST], [TITULO], [CONTENIDO], [IMAGEN], [IDUSER], [FECHA]) VALUES (3, N'Viviendo Entre Animales', N'Cada día me enamoro más de estas locuras de animales que me quieren, admiran de la misma manera que yo las aprecio a estas mascotas', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', 1, N'2/6/2022')
INSERT [dbo].[RESCUTEBLOG] ([IDPOST], [TITULO], [CONTENIDO], [IMAGEN], [IDUSER], [FECHA]) VALUES (4, N'Vive y deja Vivir', N'En este refugio lo que he hecho es verdera adventura que llegue a los corazones del resto de fututarias familias de las mascotas', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', 1, N'3/3/2023')
GO
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (1, N'admin2@gmail.com', N'2023-02-28', N'5489054430958', N'admin2', 0xE9E47E71EEA4556B532033B862C4D3C4F9AD17E233E8339EBE2508592C9006A1, N'1234', N'>ÎÞ¼FõlàYµ ,[û­M`Ív6ÝÇX|[ø¤K[® ¿l¤Ãg¾ÔjAá¸&ÑRÍ4^È', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000')
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (2, N'admin3@gmail.com', N'2023-02-27', N'5489054430958', N'admin3', 0x5C945E0D6A972043FFDFF3C5CD3AC79A5C471D2B012B91C499177EA93F67DBB2, N'1234', N'ËÀøFðÉ>=mêòVÊøDxg±ãvþ×ýªzTåÕâ¸0ËZØçã`ìí2c³©âìc"#~]GaU°', N'jupiter.jpg')
GO
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (1, N'Manuel', N'Mi pasion es ayudar a cualquier Animal que lo necesite ', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', N'manuel@gmail.com', N'Madrid', N'Mar  4 2003 12:00AM', 1)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (2, N'Carla', N'Me gusta ayudar y que el resto sea ayudado', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', N'carla@gmail.com', N'Mostoles', N'Ago  2 2002 12:00AM', 2)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (3, N'Enrique', N'Me encantan los perros y jugar con ellos.', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', N'enrique@gmail.com', N'Madrid', N'2023-02-27', 6)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (4, N'Enrique', N'Me encantan los perros y jugar con ellos.', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', N'enrique@gmail.com', N'Madrid', N'2023-02-28', 5)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (5, N'María', N'María', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000', N'maria@gmail.com', N'Madrid', N'2023-03-08', 4)
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
ALTER TABLE [dbo].[COMENTARIOS]  WITH CHECK ADD  CONSTRAINT [FK_COMENTARIOS_RESCUTEBLOG] FOREIGN KEY([IDPOST])
REFERENCES [dbo].[RESCUTEBLOG] ([IDPOST])
GO
ALTER TABLE [dbo].[COMENTARIOS] CHECK CONSTRAINT [FK_COMENTARIOS_RESCUTEBLOG]
GO
ALTER TABLE [dbo].[MASCOTAS]  WITH CHECK ADD  CONSTRAINT [FK_MASCOTAS_REFUGIOS] FOREIGN KEY([IDREFUGIO])
REFERENCES [dbo].[REFUGIOS] ([IDREFUGIO])
GO
ALTER TABLE [dbo].[MASCOTAS] CHECK CONSTRAINT [FK_MASCOTAS_REFUGIOS]
GO
ALTER TABLE [dbo].[RESCUTEBLOG]  WITH CHECK ADD  CONSTRAINT [FK_RESCUTEBLOG_USERS] FOREIGN KEY([IDUSER])
REFERENCES [dbo].[USERS] ([IDUSER])
GO
ALTER TABLE [dbo].[RESCUTEBLOG] CHECK CONSTRAINT [FK_RESCUTEBLOG_USERS]
GO
ALTER TABLE [dbo].[VOLUNTARIOS]  WITH CHECK ADD  CONSTRAINT [FK_VOLUNTARIOS_REFUGIOS] FOREIGN KEY([IDREFUGIO])
REFERENCES [dbo].[REFUGIOS] ([IDREFUGIO])
GO
ALTER TABLE [dbo].[VOLUNTARIOS] CHECK CONSTRAINT [FK_VOLUNTARIOS_REFUGIOS]
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_DATOS_MASCOTA]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_DATOS_MASCOTA](  @IDMASCOTA INT, @IDREFUGIO INT, @NOMBRE NVARCHAR(50), @EDAD FLOAT, @ALTO FLOAT , @PESO FLOAT, @DESCRIPCION NVARCHAR(1000), @IMAGEN NVARCHAR(600))
AS
	UPDATE MASCOTAS SET IDREFUGIO = @IDREFUGIO, NOMBRE = @NOMBRE, EDAD = @EDAD, PESO = @PESO, DESCRIPCION = @DESCRIPCION, IMAGEN = @IMAGEN WHERE IDMASCOTA = @IDMASCOTA
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_DATOS_REFUGIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_DATOS_REFUGIO](@IDREFUGIO INT, @NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50), @UBICACION NVARCHAR(50), @IMAGEN NVARCHAR(1000), @VALORACION INT, @DESCRIPCION NVARCHAR(1000))
    AS
        UPDATE REFUGIOS SET NOMBRE = @NOMBRE, LOCALIDAD =  @LOCALIDAD, UBICACION = @UBICACION, IMAGEN = @IMAGEN, VALORACION = @VALORACION, DESCRIPCION = @DESCRIPCION WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_BAJA_REFUGIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BAJA_REFUGIO](@IDREFUGIO INT)
AS
DELETE FROM REFUGIOS WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_BAJA_VOLUNTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BAJA_VOLUNTARIO](@IDVOLUNTARIO INT)AS
	DELETE FROM VOLUNTARIOS WHERE IDVOLUNTARIO = @IDVOLUNTARIO
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_POST]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DELETE_POST](@IDPOST INT)
AS
 DELETE FROM RESCUTEBLOG WHERE IDPOST = @IDPOST
GO
/****** Object:  StoredProcedure [dbo].[SP_DELTE_COMENTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DELTE_COMENTARIO] (@IDCOMENTARIO INT)
AS
	DELETE FROM COMENTARIOS WHERE IDCOMENTARIO = @IDCOMENTARIO
GO
/****** Object:  StoredProcedure [dbo].[SP_DETAILS_REFUGIOS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DETAILS_REFUGIOS](@IDREFUGIO INT)
   AS
		SELECT * FROM REFUGIOS WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_DETALLES_MASCOTA]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DETALLES_MASCOTA](@ID INT)
    AS

        SELECT* FROM MASCOTAS WHERE IDMASCOTA = @ID
GO
/****** Object:  StoredProcedure [dbo].[SP_DEVOLVER_ANIMAL_AL_REFUGIGO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DEVOLVER_ANIMAL_AL_REFUGIGO](@IDMASCOTA INT)
AS
 DELETE FROM ADOPCIONES WHERE IDMASCOTA = @IDMASCOTA
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_VOLUNTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_FIND_VOLUNTARIO] (@IDVOLUNTARIO INT)
AS
	SELECT * FROM VOLUNTARIOS WHERE IDVOLUNTARIO = @IDVOLUNTARIO
GO
/****** Object:  StoredProcedure [dbo].[SP_GENERAR_INFORME_ADOPCIONES]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GENERAR_INFORME_ADOPCIONES]
AS
SELECT * FROM V_VER_MASCOTAS_ADOPTADAS
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_POST]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        CREATE PROCEDURE [dbo].[SP_GET_POST]
    AS
     SELECT * FROM RESCUTEBLOG ORDER BY FECHA DESC
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICAR_DATOS_VOLUNTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MODIFICAR_DATOS_VOLUNTARIO] (@IDVOLUNTARIO INT,@NOMBRE NVARCHAR(50), @MENSAJE NVARCHAR(50), @IMAGEN NVARCHAR(600), @CORREO NVARCHAR(50), @MUNICIPIO NVARCHAR(50), @FECHANAC DATE, @REFUGIO NVARCHAR(50))
AS
	DECLARE @IDREFUGIO INT
	SELECT @IDREFUGIO = IDREFUGIO FROM REFUGIOS WHERE @REFUGIO = NOMBRE
	INSERT INTO VOLUNTARIOS VALUES(@IDVOLUNTARIO, @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @IDREFUGIO)
	UPDATE VOLUNTARIOS SET NOMBRE = @NOMBRE, MENSAJE = @MENSAJE, IMAGEN = @IMAGEN,CORREO = @CORREO, MUNICIPIO = @MUNICIPIO, FECHA_NACIMIENTO = @FECHANAC, IDREFUGIO = @IDREFUGIO WHERE IDVOLUNTARIO = @IDVOLUNTARIO
GO
/****** Object:  StoredProcedure [dbo].[SP_NEW_COMENTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_NEW_COMENTARIO] (@IDPOST INT, @EMAIL NVARCHAR(50), @COMENTARIO NVARCHAR(600))
AS
	DECLARE @IDCOMENTARIO INT
	SELECT @IDCOMENTARIO = MAX(IDCOMENTARIO) FROM COMENTARIOS
	INSERT INTO COMENTARIOS VALUES(@IDCOMENTARIO, @IDPOST, @EMAIL, @COMENTARIO)
GO
/****** Object:  StoredProcedure [dbo].[SP_NEW_POST]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_NEW_POST] (@TITULO NVARCHAR(50), @CONTENIDO NVARCHAR(50), @IMAGEN NVARCHAR(600), @IDUSER INT, @FEHCA NVARCHAR(75))
AS
	DECLARE @IDMAXPOST INT
	SELECT @IDMAXPOST = MAX(IDPOST) FROM RESCUTEBLOG
 INSERT INTO RESCUTEBLOG VALUES(@IDMAXPOST, @TITULO, @CONTENIDO, @IMAGEN, @IDUSER, @FEHCA)
GO
/****** Object:  StoredProcedure [dbo].[SP_NEWVOLUNTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_NEWVOLUNTARIO](@NOMBRE NVARCHAR(50), @MENSAJE NVARCHAR(50), @IMAGEN NVARCHAR(600), @CORREO NVARCHAR(50), @MUNICIPIO NVARCHAR(50), @FECHANAC DATE, @REFUGIO NVARCHAR(50))
AS
	DECLARE @IDREFUGIO INT
	DECLARE @IDVOLUNTARIO INT
	SELECT @IDVOLUNTARIO = MAX(IDVOLUNTARIO) + 1 FROM VOLUNTARIOS;
	SELECT @IDREFUGIO = IDREFUGIO FROM REFUGIOS WHERE @REFUGIO = NOMBRE
	INSERT INTO VOLUNTARIOS VALUES(@IDVOLUNTARIO, @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @IDREFUGIO)
GO
/****** Object:  StoredProcedure [dbo].[SP_NUEVA_ADOPCION]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_NUEVA_ADOPCION](@IDMASCOTA INT, @IDUSER INT)
AS
	INSERT INTO ADOPCIONES VALUES(@IDMASCOTA, @IDUSER, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[SP_NUEVO_REFUGIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_NUEVO_REFUGIO](@NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50), @UBICACION NVARCHAR(50), @IMAGEN NVARCHAR(50), @VALORACION INT, @DESCRIPCION NVARCHAR(50))
    AS
        DECLARE @IDREFUGIO INT

        SELECT @IDREFUGIO = MAX(IDREFUGIO) + 1 FROM REFUGIOS
		INSERT INTO REFUGIOS VALUES(@IDREFUGIO, @NOMBRE, @LOCALIDAD, @UBICACION, @IMAGEN, @VALORACION, @DESCRIPCION)
GO
/****** Object:  StoredProcedure [dbo].[SP_OBTENER_MASCOTAS_REFUGIOS]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_OBTENER_MASCOTAS_REFUGIOS](@IDREFUGIO INT)
AS
	SELECT * FROM V_MASCOTAS_REFUGIOS
	WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_COMENTARIO]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_COMENTARIO] (@IDCOMENTARIO INT, @EMAIL NVARCHAR(50), @COMENTARIO NVARCHAR(600))
AS
	UPDATE COMENTARIO SET EMAIL = @EMAIL, COMENTARIO = @COMENTARIO WHERE IDCOMENTARIO = @IDCOMENTARIO
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_POST]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_POST] (@IDPOST INT, @TITULO NVARCHAR(50), @CONTENIDO NVARCHAR(50), @IMAGEN NVARCHAR(600), @IDUSER INT, @FEHCA NVARCHAR(75))
AS
	UPDATE RESCUTEBLOG SET @TITULO = @TITULO, CONTENIDO = @CONTENIDO, IMAGEN = @IMAGEN, IDUSER = @IDUSER, FECHA = @FEHCA 
	WHERE IDPOST = @IDPOST
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_STATE_ADOPCION]    Script Date: 23/03/2023 8:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_STATE_ADOPCION](@IDMASCOTA INT, @ESTADO BIT)
AS
	UPDATE MASCOTAS SET ADOPTADO = @ESTADO WHERE IDMASCOTA = @IDMASCOTA
GO
USE [master]
GO
ALTER DATABASE [PANIMALES] SET  READ_WRITE 
GO
