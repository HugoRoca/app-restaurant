USE [master]
GO
/****** Object:  Database [APPRestaurante]    Script Date: 24/07/2018 18:07:37 ******/
CREATE DATABASE [APPRestaurante]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'APPRestaurante', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\APPRestaurante.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'APPRestaurante_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\APPRestaurante_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [APPRestaurante] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [APPRestaurante].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [APPRestaurante] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [APPRestaurante] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [APPRestaurante] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [APPRestaurante] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [APPRestaurante] SET ARITHABORT OFF 
GO
ALTER DATABASE [APPRestaurante] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [APPRestaurante] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [APPRestaurante] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [APPRestaurante] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [APPRestaurante] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [APPRestaurante] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [APPRestaurante] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [APPRestaurante] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [APPRestaurante] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [APPRestaurante] SET  ENABLE_BROKER 
GO
ALTER DATABASE [APPRestaurante] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [APPRestaurante] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [APPRestaurante] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [APPRestaurante] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [APPRestaurante] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [APPRestaurante] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [APPRestaurante] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [APPRestaurante] SET RECOVERY FULL 
GO
ALTER DATABASE [APPRestaurante] SET  MULTI_USER 
GO
ALTER DATABASE [APPRestaurante] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [APPRestaurante] SET DB_CHAINING OFF 
GO
ALTER DATABASE [APPRestaurante] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [APPRestaurante] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [APPRestaurante] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'APPRestaurante', N'ON'
GO
USE [APPRestaurante]
GO
/****** Object:  UserDefinedFunction [dbo].[EncriptarClave]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[EncriptarClave] (@Clave NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS
BEGIN
	SET @Clave = UPPER(@Clave)

	DECLARE @ClaveEncriptada NVARCHAR(MAX);

	SELECT
		@ClaveEncriptada = UPPER(SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA1', @Clave)), 3, 40));

	RETURN @ClaveEncriptada;
END





GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[apellido] [varchar](100) NULL,
	[tipo_documento] [varchar](20) NOT NULL,
	[num_documento] [varchar](20) NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[logDate] [datetime2](7) NOT NULL,
	[logThread] [varchar](50) NOT NULL,
	[logLevel] [varchar](50) NOT NULL,
	[logSource] [varchar](300) NOT NULL,
	[logMessage] [varchar](4000) NOT NULL,
	[exception] [varchar](4000) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[idUsuario] [int] NOT NULL,
	[fecha_registro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuDetalle]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MenuDetalle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idMenu] [int] NOT NULL,
	[titulo] [varchar](250) NULL,
	[descripcion] [varchar](500) NULL,
	[tipo] [varchar](100) NULL,
	[precio] [money] NULL,
	[estado] [bit] NULL,
	[foto] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permiso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [bit] NULL,
	[padre] [int] NULL,
	[url] [varchar](100) NULL,
	[icono] [varchar](50) NULL,
	[controlador] [varchar](50) NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RolPermiso]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolPermiso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idRol] [int] NOT NULL,
	[idPermiso] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolUsuario]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUsuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idRol] [int] NOT NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NULL,
	[clave] [varchar](100) NULL,
	[estado] [bit] NULL,
	[nombres] [varchar](100) NULL,
	[foto] [varchar](250) NULL,
 CONSTRAINT [PK_dbo.Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [tipo_documento], [num_documento], [estado]) VALUES (3, N'Hugo', N'Roca', N'DNI', N'12345678', 0)
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [tipo_documento], [num_documento], [estado]) VALUES (5, N'Hugo', N'Roca', N'DNI', N'12345678', 1)
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [tipo_documento], [num_documento], [estado]) VALUES (6, N'Hugo', N'Roca', N'DNI', N'12345678', 1)
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [tipo_documento], [num_documento], [estado]) VALUES (7, N'Hugo', N'Roca', N'DNI', N'12345678', 1)
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [tipo_documento], [num_documento], [estado]) VALUES (8, N'Hugo', N'Roca', N'DNI', N'12345678', 1)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([id], [fecha], [idUsuario], [fecha_registro]) VALUES (1, CAST(N'2018-07-20 11:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[Menu] ([id], [fecha], [idUsuario], [fecha_registro]) VALUES (2, CAST(N'2018-07-21 00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[Menu] ([id], [fecha], [idUsuario], [fecha_registro]) VALUES (3, CAST(N'2018-07-22 00:00:00.000' AS DateTime), 1, NULL)
SET IDENTITY_INSERT [dbo].[Menu] OFF
SET IDENTITY_INSERT [dbo].[MenuDetalle] ON 

INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (1, 1, N'Estafado de pollo', N'Pollo agresado con rizas papas sancochadas acompañadas de arroz y una rica ensalada', N'plato de fondo', 14.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (2, 1, N'Cau Cau', N'Pollo cortados en trozos sancochadas con ricas papas en cuadraditos', N'plato de fond', 10.0000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (3, 1, N'Carne asada', N'Carne asada acompañasdas de ricas papas fritas', N'plato de fondo', 15.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (4, 1, N'Picante de carne', N'Carne y papas en trozos consochadas con las mas tieras verduras picantes', N'plato de fondo', 12.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (5, 1, N'Lomo de pollo', N'Pollo en trozos acompañadas de unas ricas papas fritas', N'plato de fondo', 16.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (6, 1, N'Aguadito', N'Rica sopa hecha con arroz acompañada con pollo', N'Entrasa', 3.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (7, 1, N'Sopa de casa', N'Sopa de carne ', N'Entrada', 3.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (8, 1, N'Ensalada de palta', N'Rica ensalada de palta ', N'Entrada', 5.0000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (9, 1, N'Selva negra', N'Postre', N'Postre', 4.0000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (10, 1, N'Gelatina', N'Postre', N'Postre', 2.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (11, 2, N'Gruipo de pollo', N'Pollo sancochado con un rico aderezo acompañada de papas sancochadas', N'Plato de fondo', 15.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (12, 2, N'Caigua Rellena', N'Caiga rellana de aderezo de pollo en trozos', N'Plato de fondo', 12.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (13, 2, N'Arroz chaufa', N'Rico arroz frito acompañada con pollo en trozos', N'plato de fondo', 15.5000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (14, 2, N'Tallarines verdes', N'Tallarines verdes acompañada de huevo frito', N'Plato de fondo', 14.6000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (15, 2, N'Churrazco', N'Carne a la plancha frita con el mas delicioso aderesos', N'Plato de fondo', 15.6000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (16, 2, N'Sopa de cemola', N'Rica sopa de cemola acompañada de verduras', N'entrada', 5.2000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (17, 2, N'Sopa de crema de alverga', N'Rica sopa de crema de alverja acompañada de su rica cancha', N'Entrada', 5.4000, 1, NULL)
INSERT [dbo].[MenuDetalle] ([id], [idMenu], [titulo], [descripcion], [tipo], [precio], [estado], [foto]) VALUES (18, 2, N'Mazamorra morada', N'Rica mazamorra morada ', N'Postre', 3.5000, 1, NULL)
SET IDENTITY_INSERT [dbo].[MenuDetalle] OFF
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (1, N'Inicio', 1, 0, N'/Admin/Inicio/Index', N'menu-icon fa fa-tachometer', N'Inicio', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (2, N'Menu', 1, 0, N'', N'menu-icon fa fa-pencil-square-o', N'', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (3, N'Registro de menú', 1, 2, N'/Admin/Menu/Index', N'', N'Menu', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (4, N'Acceso', 1, 0, N'', N'menu-icon fa fa-users', N'', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (5, N'Registro de empleado', 1, 4, N'/Admin/Empleado/Index', N'', N'Empleado', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (6, N'Registro de usuario', 1, 4, N'/Admin/Usuario/Index', N'', N'Usuario', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (7, N'Caja', 1, 0, N'', N'menu-icon fa fa-folder-open-o', N'', 0)
INSERT [dbo].[Permiso] ([id], [descripcion], [estado], [padre], [url], [icono], [controlador], [active]) VALUES (8, N'Cobro de consumo', 1, 7, N'/Admin/Cobranza/Index', N'', N'Cobranza', 0)
SET IDENTITY_INSERT [dbo].[Permiso] OFF
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([id], [descripcion], [estado]) VALUES (1, N'Administrador', 1)
INSERT [dbo].[Rol] ([id], [descripcion], [estado]) VALUES (2, N'Cajero', 1)
INSERT [dbo].[Rol] ([id], [descripcion], [estado]) VALUES (3, N'Cocinero', 1)
SET IDENTITY_INSERT [dbo].[Rol] OFF
SET IDENTITY_INSERT [dbo].[RolPermiso] ON 

INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (1, 1, 1)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (2, 1, 2)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (3, 1, 3)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (4, 1, 4)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (5, 1, 5)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (6, 1, 6)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (7, 1, 7)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (8, 1, 8)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (9, 2, 7)
INSERT [dbo].[RolPermiso] ([id], [idRol], [idPermiso]) VALUES (10, 2, 8)
SET IDENTITY_INSERT [dbo].[RolPermiso] OFF
SET IDENTITY_INSERT [dbo].[RolUsuario] ON 

INSERT [dbo].[RolUsuario] ([id], [idUsuario], [idRol], [estado]) VALUES (1, 1, 1, 1)
INSERT [dbo].[RolUsuario] ([id], [idUsuario], [idRol], [estado]) VALUES (2, 2, 2, 1)
SET IDENTITY_INSERT [dbo].[RolUsuario] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [usuario], [clave], [estado], [nombres], [foto]) VALUES (1, N'hugo.roca', N'B11DE28C967D26AA47DA5326E7B092FA9B73FD2C', 1, N'Hugo Roca', N'hugo.roca.png')
INSERT [dbo].[Usuario] ([id], [usuario], [clave], [estado], [nombres], [foto]) VALUES (2, N'jehidi.chavez', N'B11DE28C967D26AA47DA5326E7B092FA9B73FD2C', 1, N'Jehidi Chavez', N'jehidi.chavez.png')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[MenuDetalle]  WITH CHECK ADD FOREIGN KEY([idMenu])
REFERENCES [dbo].[Menu] ([id])
GO
ALTER TABLE [dbo].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermisoPermiso] FOREIGN KEY([idPermiso])
REFERENCES [dbo].[Permiso] ([id])
GO
ALTER TABLE [dbo].[RolPermiso] CHECK CONSTRAINT [FK_RolPermisoPermiso]
GO
ALTER TABLE [dbo].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermisoRol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[RolPermiso] CHECK CONSTRAINT [FK_RolPermisoRol]
GO
ALTER TABLE [dbo].[RolUsuario]  WITH CHECK ADD  CONSTRAINT [FK_RolRolUsuario] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[RolUsuario] CHECK CONSTRAINT [FK_RolRolUsuario]
GO
ALTER TABLE [dbo].[RolUsuario]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRolUsuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[RolUsuario] CHECK CONSTRAINT [FK_UsuarioRolUsuario]
GO
/****** Object:  StoredProcedure [dbo].[Menu_Lista_SP]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Menu_Lista_SP](
@desde date,
@hasta date,
@startRow int,
@endRow int
)
as
begin

	SET NOCOUNT ON;

	SELECT 
		[id],
		[fecha],
		[usuario],
		[titulo],
		[descripcion],
		[tipo],
		[precio],
		[foto]
	FROM (
		SELECT 
			ROW_NUMBER() OVER (ORDER BY m.id) fila, 
			m.id, 
			CONVERT(VARCHAR(24), m.fecha, 103) fecha,
			u.nombres usuario, 
			md.titulo, 
			md.descripcion, 
			md.tipo, 
			md.precio, 
			isnull(md.foto, '') foto
		FROM menu m
		INNER JOIN Usuario u ON u.id = m.idUsuario
		INNER JOIN MenuDetalle md ON md.idMenu = m.id
		WHERE 
			md.estado = 1 AND 
			CONVERT(DATE, m.fecha) <= @hasta AND
			CONVERT(DATE, m.fecha) >= @desde) AS Lista
	WHERE
		fila >= @startRow AND
		fila <= @endRow

	SET NOCOUNT OFF;
end

GO
/****** Object:  StoredProcedure [dbo].[procLogs_Insert]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[procLogs_Insert] 
	@log_date datetime2, 
  @log_thread varchar(50), 
  @log_level varchar(50), 
  @log_source varchar(300), 
  @log_message varchar(4000), 
  @exception varchar(4000)
AS
BEGIN
	SET NOCOUNT ON;

  insert into dbo.Logs (logDate, logThread, logLevel, logSource, logMessage, exception)
  values (@log_date, @log_thread, @log_level, @log_source, @log_message, @exception)

END






GO
/****** Object:  StoredProcedure [dbo].[Usuario_ListarOpcionesAcceso_SP]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Usuario_ListarOpcionesAcceso_SP](
@idUsuario int
)
as
begin
	select p.descripcion, p.url, p.id, p.padre, isnull(p.icono, '') icono, controlador, active
	from permiso p
	inner join rolpermiso rp on rp.idpermiso = p.id
	inner join rolusuario ru on ru.idrol = rp.idrol
	inner join usuario u on u.id = ru.idusuario
	where u.id = @idUsuario and u.estado =  1
end




GO
/****** Object:  StoredProcedure [dbo].[Usuario_Validar_SP]    Script Date: 24/07/2018 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Usuario_Validar_SP](
	@usuario varchar(50),
	@clave varchar(100)
)
as
begin
	declare @encriptado varchar(100) = dbo.EncriptarClave(@clave)
	select id, usuario, clave, estado, nombres, foto from usuario where usuario = @usuario and clave = @encriptado and estado = 1
end






GO
USE [master]
GO
ALTER DATABASE [APPRestaurante] SET  READ_WRITE 
GO
