CREATE PROCEDURE eliminarProducto
AS
BEGIN
    UPDATE
       Producto 
       SET activo = 0
     WHERE IdProducto = @id_producto;
END;