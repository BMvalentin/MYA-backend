CREATE PROCEDURE actualizarProducto
    @IdProducto INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX),
    @IdTalle INT,
    @IdCategoria INT,
    @IdMarca INT,
    @Precio DECIMAL(18, 2),
    @Stock INT,
    @RutaImagen NVARCHAR(MAX)
AS
BEGIN
    UPDATE Productos
    SET 
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        IdCategoria = @IdCategoria,
        IdMarca = @IdMarca,
        IdTalle = @IdTalle,
        Precio = @Precio,
        Stock = @Stock,
        RutaImagen = @RutaImagen
    WHERE IdProducto = @IdProducto;
END;