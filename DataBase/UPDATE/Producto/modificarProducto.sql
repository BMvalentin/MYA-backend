CREATE PROCEDURE modificarProducto
    @id_producto INT,
    @id_marca INT,
    @id_categoria INT,
    @stock INT,
    @precio DECIMAL(10,0),
    @nombre VARCHAR(255),
    @url_imagen VARCHAR(255),
    @descripcion VARCHAR(255)
AS

BEGIN
    UPDATE Productos
    SET Nombre = @nombre,
    Descripcion = @descripcion,
    IdMarca = @id_marca,
    IdCategorai = @id_categoria,
    Precio = @precio,
    Stock = @stock,
    RutaImagen = @url_imagen
    WHERE IdProducto = @id_producto;
END;