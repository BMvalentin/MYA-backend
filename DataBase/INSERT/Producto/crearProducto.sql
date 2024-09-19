CREATE PROCEDURE crearProducto
  @Nombre NVARCHAR(255),
  @Descripcion NVARCHAR(MAX),
  @IdMarca INT,
  @IdCategoria INT,
  @Precio DECIMAL,
  @Talle NVARCHAR(50),
  @Stock INT,
  @RutaImagen NVARCHAR(255)
AS
BEGIN
  INSERT INTO Producto (nombre,descripcion,id_marca,id_categoria,precio,url_imagen) VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @RutaImagen);
  
  DECLARE @IdProducto INT;
  SELECT TOP 1 @IdProducto = id_producto FROM producto ORDER BY id_producto DESC;

  INSERT INTO talle (talle,stock,id_producto) VALUES (@Talle,@Stock,@IdProducto);
END;