CREATE PROCEDURE obtenerProducto
AS
BEGIN
    SELECT
        producto.nombre,
        producto.descripcion,
        marca.descripcion AS marca_descripcion,
        categoria.descripcion AS categoria_descripcion,
        producto.precio,
        producto.stock,
        producto.url_imagen
    FROM
        producto
        INNER JOIN
        marca ON marca.id_marca = producto.id_marca
        INNER JOIN
        categoria ON producto.id_categoria = categoria.id_categoria
    WHERE 
    producto.activo = 1;
END;