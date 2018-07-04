CREATE PROC Usuario_Validar_SP(
	@usuario varchar(50),
	@clave varchar(100)
)
as
begin
	declare @encriptado varchar(100) = dbo.EncriptarClave(@clave)
	select id, usuario, clave, estado from usuario where usuario = @usuario and clave = @encriptado and estado = 1
end
