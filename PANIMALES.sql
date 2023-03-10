USE [master]
GO
/****** Object:  Database [PANIMALES]    Script Date: 10/03/2023 13:50:31 ******/
CREATE DATABASE [PANIMALES]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PANIMALES', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\PANIMALES.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PANIMALES_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\PANIMALES_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
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
/****** Object:  Table [dbo].[MASCOTAS]    Script Date: 10/03/2023 13:50:31 ******/
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
/****** Object:  Table [dbo].[REFUGIOS]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REFUGIOS](
	[IDREFUGIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[LOCALIDAD] [nvarchar](50) NULL,
	[UBICACION] [nchar](50) NULL,
	[IMAGEN] [nvarchar](600) NULL,
	[VALORACION] [int] NULL,
	[DESCRIPCION] [nvarchar](100) NULL,
 CONSTRAINT [PK_REFUGIOS] PRIMARY KEY CLUSTERED 
(
	[IDREFUGIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_MASCOTAS_REFUGIOS]    Script Date: 10/03/2023 13:50:31 ******/
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
/****** Object:  Table [dbo].[ADOPCIONES]    Script Date: 10/03/2023 13:50:31 ******/
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
/****** Object:  Table [dbo].[USERS]    Script Date: 10/03/2023 13:50:31 ******/
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
	[IMAGEN] [nvarchar](600) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[IDUSER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_VER_MASCOTAS_ADOPTADAS]    Script Date: 10/03/2023 13:50:31 ******/
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
/****** Object:  Table [dbo].[VOLUNTARIOS]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VOLUNTARIOS](
	[IDVOLUNTARIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[MENSAJE] [nvarchar](100) NULL,
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
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (1, N'AMBER', N'FUEGO', 21, 32.12, 21.23, 53, N'DESCRIPCION...                                                                                      ', 1, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 1)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (2, N'PELÍN', N'PERRO PASTO', 12, 12.12, 9.98, 33.35, N'Este perro es un perro alemán que le encanta jugar', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 2)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (3, N'BACÓN', N'Border collie', 4, 6, 7, 11.5, N'Este perro es un de los más queridos ene nuestra perrera...', 0, N'https://images.ecestaticos.com/h34TvzTFVdrau9Un4Wdmwhed_e4=/0x115:2265x1390/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F8ec%2F08c%2F85c%2F8ec08c85c866ccb70c4f1c36492d890f.jpg', 0, 1)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (4, N'Doggie', N'husky', 6, 3, 9.39, 7.35, N'Es un perro tranquilo que le encanta pasear....', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 2)
INSERT [dbo].[MASCOTAS] ([IDMASCOTA], [NOMBRE], [RAZA], [EDAD], [ANCHO], [ALTO], [PESO], [DESCRIPCION], [PELIGROSIDAD], [IMAGEN], [ADOPTADO], [IDREFUGIO]) VALUES (5, N'Mulo', N'Golden Retriver', 3, 2, 4, 5, N'Es un perro pequeño...', 0, N'https://img.freepik.com/foto-gratis/adorable-perro-basenji-marron-blanco-sonriendo-dando-maximo-cinco-aislado-blanco_346278-1657.jpg', 0, 1)
GO
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (1, N'RefuPerros', N'Madrid', N'C/ Las aguilas, 12                                ', N'miimgaen.jpg', 5, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (2, N'PokeMascotas', N'Mostoles', N'Camino a Mosteles, 5                              ', N'miimagen2.jpg', 7, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (3, N'REFU1', N'ALCALA DE HENARES', N'PLAZA HENARES, 7                                  ', N'imagenrefu1.jpg', 6, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (4, N'REFU2', N'ALCOBENDAS', N'CALLE RESILENTES, 5                               ', N'imagenrefu2.jgp', 7, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (5, N'REFU3', N'FUENLABRADA', N'AVENIDA FUENLABRADA, 10                           ', N'imagenrefu3.jpg', 8, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (6, N'REFU4', N'MIRAFLORES DE LA SIERRA', N'Plaza del mirador, 1                              ', N'imagenrefu4.jpg', 9, N'Nuestra valoracion en aspectos relacionados a rescate de animales, cuidado  y adopcion')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (7, N'Merlin 3', N'Leganes', N'Camino antiguo de Leganés, 12                     ', N'miimagenpruebainsert', 8, N'Este refugio es de los mas de lo último que hay pa')
INSERT [dbo].[REFUGIOS] ([IDREFUGIO], [NOMBRE], [LOCALIDAD], [UBICACION], [IMAGEN], [VALORACION], [DESCRIPCION]) VALUES (8, N'ALEPH', N'MADRID', N'SANTA EUGENIA                                     ', N'MI IMAGEN', 8, N'7')
GO
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (1, N'admin@gmail.com', N'14/05/2000', N'634534985345', N'admin1234', 0x610064006D0069006E003100320033003400, NULL, NULL, NULL)
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (2, N'manuelolo@gmail.com', N'2023-01-30', N'10101011010', N'manu1234', 0x6100730064006100730064006100730064006100, NULL, NULL, NULL)
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (3, N'manuelolo2@gmail.com', N'2023-01-30', N'10101011010', N'manu12345', 0x6D0061006E0075003100320033003400, NULL, NULL, NULL)
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (4, N'cristiaspnet@gmail.com', N'2023-01-18', N'10101011010', N'crist12345', 0x63007200690073007400690031003200330034003500, NULL, NULL, NULL)
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (5, N'pablo@gmail.com', NULL, N'5489054430958', N'rescutedogkw', 0x6A68A37D8B1E2A46DBB8236F523CCA8A7A106FA2E5EECC420F6A461BB51501D2, N'1234', N'jÓ?<ß}±¤ù£,¬áÅx2±hËIÓ·ÜtämHWª>Ï/÷$¬¯éêÓ½o½{6°Fà', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000')
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (6, N'manu@gmail.com', N'2023-02-28', N'5489054430958', N'Manu', 0xDACAF7BDB08CA8551BA859566AB4FF46F31C940A26EFC7993D7539F579CAD64E, N'1234', N'Ú¦¶¹an­ó¢¾Ço¿_26iîx²r[ûðBé»6DÈÔ¼^`ªøWóø"»zAÞ:', N'https://img.freepik.com/fotos-premium/paisaje-montana-minimalista-pincel-acuarela-papel-tapiz-estilo-tradicional-japones-arte-abstracto-impresiones-o-portadas-obras-arte-3d_76964-3466.jpg?w=2000')
INSERT [dbo].[USERS] ([IDUSER], [EMAIL], [BIRDTHDAY], [PHONE], [USERNAME], [PASSWORD], [CONTRASENA], [SALT], [IMAGEN]) VALUES (7, N'carlos@gmail.com', N'2023-02-28', N'3465359839', N'nsdansal', 0x56CCA494971F2B0FB0F1C21C5E26E5CA53689F2BFCF0EC0CB461D68AE6A9C891, N'1234', N'Üa±ÀsP«PÜ®ûÙÈeðàè¢*ìïµemO}]èù)ãâ[]ûäÅëþý\ì8ìÉYð', N'htttps://miimagen2.jpg')
GO
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (1, N'Manuel', N'Mi pasion es ayudar a cualquier Animal que lo necesite ', N'MiImagen.jpg', N'manuel@gmail.com', N'Madrid', N'Mar  4 2003 12:00AM', 1)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (2, N'Carla', N'Cada dia voy a la prrotecto ha hacer todo lo posible por las mascotas que lo necesitan', N'Miimagen2.jpg', N'carla@gmail.com', N'Mostoles', N'Ago  2 2002 12:00AM', 2)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (3, N'Enrique', N'Me encantan los perros y jugar con ellos.', N'quiqueimagen.jpg', N'enrique@gmail.com', N'Madrid', N'2023-02-27', 6)
INSERT [dbo].[VOLUNTARIOS] ([IDVOLUNTARIO], [NOMBRE], [MENSAJE], [IMAGEN], [CORREO], [MUNICIPIO], [FECHA_NACIMIENTO], [IDREFUGIO]) VALUES (4, N'Enrique', N'Me encantan los perros y jugar con ellos.', N'quiqueimagen.jpg', N'enrique@gmail.com', N'Madrid', N'2023-02-28', 5)
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
ALTER TABLE [dbo].[MASCOTAS]  WITH CHECK ADD  CONSTRAINT [FK_MASCOTAS_REFUGIOS] FOREIGN KEY([IDREFUGIO])
REFERENCES [dbo].[REFUGIOS] ([IDREFUGIO])
GO
ALTER TABLE [dbo].[MASCOTAS] CHECK CONSTRAINT [FK_MASCOTAS_REFUGIOS]
GO
ALTER TABLE [dbo].[VOLUNTARIOS]  WITH CHECK ADD  CONSTRAINT [FK_VOLUNTARIOS_REFUGIOS] FOREIGN KEY([IDREFUGIO])
REFERENCES [dbo].[REFUGIOS] ([IDREFUGIO])
GO
ALTER TABLE [dbo].[VOLUNTARIOS] CHECK CONSTRAINT [FK_VOLUNTARIOS_REFUGIOS]
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_DATOS_REFUGIO]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_DATOS_REFUGIO](@IDREFUGIO INT,@NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50), @UBICACION NVARCHAR(50), @IMAGEN NVARCHAR(50), @VALORACION INT, @DESCRIPCION NVARCHAR(50))
AS
	UPDATE REFUGIOS SET NOMBRE = @NOMBRE, LOCALIDAD =  @LOCALIDAD, UBICACION = @UBICACION, @IMAGEN = IMAGEN, VALORACION = @VALORACION, DESCRIPCION = @DESCRIPCION WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_DETAILS_REFUGIOS]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DETAILS_REFUGIOS](@IDREFUGIO INT)
   AS
		SELECT * FROM REFUGIOS WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_DETALLES_MASCOTA]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DETALLES_MASCOTA](@ID INT)
    AS

        SELECT* FROM MASCOTAS WHERE IDMASCOTA = @ID
GO
/****** Object:  StoredProcedure [dbo].[SP_DEVOLVER_ANIMAL_AL_REFUGIGO]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DEVOLVER_ANIMAL_AL_REFUGIGO](@IDMASCOTA INT)
AS
 DELETE FROM ADOPCIONES WHERE IDMASCOTA = @IDMASCOTA
GO
/****** Object:  StoredProcedure [dbo].[SP_GENERAR_INFORME_ADOPCIONES]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GENERAR_INFORME_ADOPCIONES]
AS
SELECT * FROM V_VER_MASCOTAS_ADOPTADAS
GO
/****** Object:  StoredProcedure [dbo].[SP_NEWVOLUNTARIO]    Script Date: 10/03/2023 13:50:31 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_NUEVA_ADOPCION]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_NUEVA_ADOPCION](@IDMASCOTA INT, @IDUSER INT)
AS
	INSERT INTO ADOPCIONES VALUES(@IDMASCOTA, @IDUSER, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[SP_NUEVO_REFUGIO]    Script Date: 10/03/2023 13:50:31 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_OBTENER_MASCOTAS_REFUGIOS]    Script Date: 10/03/2023 13:50:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_OBTENER_MASCOTAS_REFUGIOS](@IDREFUGIO INT)
AS
	SELECT * FROM V_MASCOTAS_REFUGIOS
	WHERE IDREFUGIO = @IDREFUGIO
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_STATE_ADOPCION]    Script Date: 10/03/2023 13:50:31 ******/
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
