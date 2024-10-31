CREATE PROCEDURE obtenerProductos
  @Offset INT,
  @PageSize INT
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
  ORDER BY p.id_producto OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END;