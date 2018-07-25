create proc Menu_Insertar_SP(
@fecha date,
@idUsuario int,
@titulo varchar(250),
@descripcion varchar(500),
@tipo varchar(100),
@precio money,
@foto varchar(500)
)
as
begin
	DECLARE @idMenu int = 0;

	IF (select count(*) from menu where CONVERT(DATE, fecha) = @fecha) = 0
	begin
		insert into menu(fecha, idUsuario, fecha_registro) values(@fecha, @idUsuario, getdate())
	end

	set @idMenu = (SELECT id from menu where CONVERT(DATE, fecha) = @fecha)

	insert into MenuDetalle(idMenu, titulo, descripcion, tipo, precio, estado, foto)
	values(@idMenu, @titulo, @descripcion, @tipo, @precio, 1, @foto)
end



