CREATE PROCEDURE crearProducto
  @Nombre NVARCHAR(255),
  @Descripcion NVARCHAR(MAX),
  @IdMarca INT,
  @IdCategoria INT,
  @Precio DECIMAL(10,2),
  @IdTalle INT,
  @Stock INT,
  @RutaImagen NVARCHAR(255)
AS
BEGIN
  INSERT INTO Producto (nombre,descripcion,id_marca,id_categoria,precio,url_imagen) VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @RutaImagen);
  
  DECLARE @IdProducto INT = SCOPE_IDENTITY();

  INSERT INTO stock (id_producto,id_talle) VALUES (@IdProducto,@IdTalle);
END;