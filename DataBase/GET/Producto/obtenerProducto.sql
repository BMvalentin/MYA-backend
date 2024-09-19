CREATE PROCEDURE obtenerProducto
AS
BEGIN
  SELECT
    p.id_producto,
    p.id_marca,
    p.id_categoria,
    p.precio,
    p.url_imagen,
    t.talle,
    t.stock,
    m.descripcion AS descripcion_marca,
    c.descripcion AS descripcion_categoria
  FROM 
    producto AS p
    INNER JOIN talle AS t ON t.id_producto = p.id_producto
    INNER JOIN marca AS m ON m.id_marca = p.id_marca
    INNER JOIN categoria AS c ON p.id_categoria = c.id_categoria
  WHERE 
    p.activo = 1 
    AND t.activo = 1 
    AND t.stock > 0 
    AND m.activo = 1 
    AND c.activo = 1
  ORDER BY 
    t.talle ASC;
END;