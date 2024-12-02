CREATE PROCEDURE obtenerTalle
AS
BEGIN
    SELECT id_talle AS 'id',talle AS 'name' FROM talle;
END;