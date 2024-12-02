CREATE PROCEDURE obtenerMarca
AS
BEGIN
    SELECT id_marca AS 'id',descripcion AS 'name' FROM marca WHERE activo = 1;
END;