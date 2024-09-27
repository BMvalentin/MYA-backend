CREATE PROCEDURE obtenerTalle
AS
BEGIN
    SELECT id_talle AS 'id',talle FROM talle;
END;