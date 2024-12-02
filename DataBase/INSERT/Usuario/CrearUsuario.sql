CREATE PROCEDURE CrearUsuario
    @nombre NVARCHAR(255),
    @apellido NVARCHAR(255),
    @correo NVARCHAR(255),
    @password NVARCHAR(255)
AS
BEGIN
    INSERT INTO usuario (nombre,apellido,correo,clave,id_rol) VALUES (@nombre,@apellido,@correo,@password,1);
END;