CREATE PROCEDURE verificarCorreo
  @correo NVARCHAR(255)
AS
BEGIN
  SELECT clave , tipo_de_usuario FROM usuario WHERE correo = @correo;
END;