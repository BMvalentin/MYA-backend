CREATE PROCEDURE crearProducto
  @IdProducto INT,
  @Nombre TEXT,
  @Descripcion TEXT,
  @IdMarca INT,
  @IdCategoria INT,
  @Precio DECIMAL,
  @Stock INT,
  @RutaImagen TEXT
AS
BEGIN
  INSERT INTO Producto (nombre,descripcion,id_marca,id_categoria,precio,stock,url_imagen) VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @RutaImagen);
END;