CREATE PROCEDURE verificarCorreo
  @correo NVARCHAR(255)
AS
BEGIN
    SELECT clave FROM cliente WHERE correo = @correo;
END;
