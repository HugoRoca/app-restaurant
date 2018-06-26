-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************

CREATE DATABASE APPRestaurante
GO

USE  APPRestaurante
GO

--************************************** [dbo].[TablaDatos]

CREATE TABLE [dbo].[TablaDatos]
(
 [idTabla]     INT NOT NULL ,
 [codigo]      INT NOT NULL ,
 [descripcion] VARCHAR(100) NOT NULL ,
 [valor]       VARCHAR(10) NOT NULL ,
 [estado]      BIT NOT NULL CONSTRAINT [DF_TablaDatos_estado] DEFAULT 1 ,

 CONSTRAINT [PK_TablaDatos] PRIMARY KEY CLUSTERED ([idTabla] ASC, [codigo] ASC)
);
GO



--************************************** [dbo].[Cliente]

CREATE TABLE [dbo].[Cliente]
(
 [idCliente]      INT NOT NULL ,
 [nombre]         VARCHAR(100) NOT NULL ,
 [apellido]       VARCHAR(100) NULL ,
 [tipo_documento] VARCHAR(20) NULL ,
 [num_documento]  VARCHAR(20) NULL ,
 [estado]         BIT NOT NULL CONSTRAINT [DF_Cliente_estado] DEFAULT 1 ,

 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([idCliente] ASC)
);
GO



--************************************** [dbo].[Rol]

CREATE TABLE [dbo].[Rol]
(
 [idRol]       INT NOT NULL ,
 [descripcion] VARCHAR(100) NOT NULL ,
 [estado]      BIT NOT NULL CONSTRAINT [DF_Rol_estado] DEFAULT 1 ,

 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED ([idRol] ASC)
);
GO



--************************************** [dbo].[Permiso]

CREATE TABLE [dbo].[Permiso]
(
 [idPermiso]   INT NOT NULL ,
 [descripcion] VARCHAR(100) NOT NULL ,
 [estado]      BIT NOT NULL ,

 CONSTRAINT [PK_permisos] PRIMARY KEY CLUSTERED ([idPermiso] ASC)
);
GO



--************************************** [dbo].[UsuarioPermiso]

CREATE TABLE [dbo].[UsuarioPermiso]
(
 [idUsuarioPermiso] INT NOT NULL ,
 [idPermiso]        INT NOT NULL ,
 [idRol]            INT NOT NULL ,

 CONSTRAINT [PK_UsuarioPermiso] PRIMARY KEY CLUSTERED ([idUsuarioPermiso] ASC),
 CONSTRAINT [FK_Permiso_UsuarioPermiso] FOREIGN KEY ([idPermiso])
  REFERENCES [dbo].[Permiso]([idPermiso]),
 CONSTRAINT [FK_Rol_UsurioPermiso] FOREIGN KEY ([idRol])
  REFERENCES [dbo].[Rol]([idRol])
);
GO


--SKIP Index: [Index_Permiso_UsuarioPermiso]

--SKIP Index: [Index_Rol_UsuarioPermiso]


--************************************** [dbo].[Empleado]

CREATE TABLE [dbo].[Empleado]
(
 [idEmpleado] INT NOT NULL ,
 [nombre]     VARCHAR(100) NOT NULL ,
 [apellido]   VARCHAR(100) NOT NULL ,
 [direccion]  VARCHAR(100) NOT NULL ,
 [celular]    VARCHAR(100) NOT NULL ,
 [email]      VARCHAR(100) NULL ,
 [foto]       VARCHAR(250) NOT NULL ,
 [estado]     BIT NOT NULL CONSTRAINT [DF_Empleado_estado] DEFAULT 1 ,
 [idRol]      INT NOT NULL ,

 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED ([idEmpleado] ASC),
 CONSTRAINT [FK_Rol_Empleado] FOREIGN KEY ([idRol])
  REFERENCES [dbo].[Rol]([idRol])
);
GO


--SKIP Index: [Index_Rol_Empleado]


--************************************** [dbo].[Usuario]

CREATE TABLE [dbo].[Usuario]
(
 [idUsuario]  INT NOT NULL ,
 [usuario]    VARCHAR(50) NOT NULL ,
 [contraseña] VARCHAR(100) NOT NULL ,
 [idEmpleado] INT NOT NULL ,
 [estado]     BIT NOT NULL ,

 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([idUsuario] ASC),
 CONSTRAINT [FK_Emplado_Usuario] FOREIGN KEY ([idEmpleado])
  REFERENCES [dbo].[Empleado]([idEmpleado])
);
GO


--SKIP Index: [Index_Empleado_Usuario]


--************************************** [dbo].[Pedido]

CREATE TABLE [dbo].[Pedido]
(
 [idPedido]  INT NOT NULL ,
 [idCliente] INT NOT NULL ,
 [idUsuario] INT NOT NULL ,
 [fecha]     DATETIME NOT NULL ,
 [cantidad]  INT NOT NULL ,
 [total]     MONEY NOT NULL ,
 [estado]    BIT NOT NULL CONSTRAINT [DF_Pedido_estado] DEFAULT 1 ,

 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED ([idPedido] ASC),
 CONSTRAINT [FK_Cliente_Pedido] FOREIGN KEY ([idCliente])
  REFERENCES [dbo].[Cliente]([idCliente]),
 CONSTRAINT [FK_Usuario_Pedido] FOREIGN KEY ([idUsuario])
  REFERENCES [dbo].[Usuario]([idUsuario])
);
GO


--SKIP Index: [Index_Cliente_Pedido]

--SKIP Index: [Index_Usuario_Pedido]


--************************************** [dbo].[Menu]

CREATE TABLE [dbo].[Menu]
(
 [idMenu]         INT NOT NULL ,
 [fecha]          DATE NOT NULL ,
 [idUsuario]      INT NOT NULL ,
 [fecha_registro] DATETIME NOT NULL ,

 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([idMenu] ASC),
 CONSTRAINT [FK_Usuario_Menu] FOREIGN KEY ([idUsuario])
  REFERENCES [dbo].[Usuario]([idUsuario])
);
GO


--SKIP Index: [Index_Usuario_Menu]


--************************************** [dbo].[MenuDetalle]

CREATE TABLE [dbo].[MenuDetalle]
(
 [idMenuDetalle] INT NOT NULL ,
 [idMenu]        INT NOT NULL ,
 [descripcion]   VARCHAR(100) NOT NULL ,
 [precio]        MONEY NOT NULL ,
 [tipo]          INT NOT NULL ,
 [estado]        BIT NOT NULL CONSTRAINT [DF_MenuDetalle_estado] DEFAULT 1 ,

 CONSTRAINT [PK_MenuDetalle] PRIMARY KEY CLUSTERED ([idMenuDetalle] ASC),
 CONSTRAINT [FK_Menu_MenuDetalle] FOREIGN KEY ([idMenu])
  REFERENCES [dbo].[Menu]([idMenu])
);
GO


--SKIP Index: [Index_Menu_MenuDetalle]


--************************************** [dbo].[PedidoDetalle]

CREATE TABLE [dbo].[PedidoDetalle]
(
 [idPedidoDetalle] INT NOT NULL ,
 [idMenuDetalle]   INT NOT NULL ,
 [idPedido]        INT NOT NULL ,
 [precio]          MONEY NOT NULL ,
 [cantidad]        INT NOT NULL ,

 CONSTRAINT [PK_PedidoDetalle] PRIMARY KEY CLUSTERED ([idPedidoDetalle] ASC),
 CONSTRAINT [FK_MenuDetalle_PedidoDetalle] FOREIGN KEY ([idMenuDetalle])
  REFERENCES [dbo].[MenuDetalle]([idMenuDetalle]),
 CONSTRAINT [FK_Pedido_PedidoDetalle] FOREIGN KEY ([idPedido])
  REFERENCES [dbo].[Pedido]([idPedido])
);
GO


--SKIP Index: [Index_MenuDetalle_PedidoDetalle]

--SKIP Index: [Index_Pedido_PedidoDetalle]


