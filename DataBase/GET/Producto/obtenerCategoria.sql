CREATE PROCEDURE obtenerCategoria
AS
BEGIN
    SELECT id_categoria AS 'id',descripcion AS 'name' FROM categoria WHERE activo = 1;
END;