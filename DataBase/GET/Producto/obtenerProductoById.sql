CREATE PROCEDURE obtenerProductoById
  @Id INT
AS
BEGIN
  SELECT
    p.nombre,
    p.id_producto,
    p.id_marca,
    p.id_categoria,
    p.precio,
    p.url_imagen,
    t.talle,
    s.stock,
    m.descripcion AS descripcion_marca,
    c.descripcion AS descripcion_categoria
  FROM 
    producto AS p
    INNER JOIN marca AS m ON m.id_marca = p.id_marca
    INNER JOIN categoria AS c ON p.id_categoria = c.id_categoria
    INNER JOIN stock AS s ON s.id_producto = p.id_producto
    INNER JOIN talle AS t ON t.id_talle = s.id_talle
  WHERE 
    p.activo = 1 
    AND s.activo = 1 
    AND s.stock > 0 
    AND m.activo = 1 
    AND c.activo = 1
    AND p.id_producto = @Id
  ORDER BY 
    t.talle ASC;
END;