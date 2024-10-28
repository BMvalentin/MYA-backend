CREATE PROCEDURE obtenerProductos
AS
BEGIN
  SELECT
    p.nombre,
    p.id_producto,
    p.precio,
    p.url_imagen
  FROM 
    producto AS p
  WHERE 
    p.activo = 1 
END;