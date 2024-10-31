CREATE PROCEDURE obtenerTotalDeProductos
AS
BEGIN
    SELECT 
        COUNT(*) AS 'total' 
    FROM 
        producto 
    WHERE 
        activo = 1;
END;