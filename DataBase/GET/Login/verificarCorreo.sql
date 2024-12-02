CREATE PROCEDURE verificarCorreo
  @correo NVARCHAR(255)
AS
BEGIN
  SELECT clave, r.nombre_rol AS tipo_de_usuario FROM usuario AS u 
    INNER JOIN rol AS r ON u.id_rol = r.id_rol 
  WHERE u.correo = @correo;
END;