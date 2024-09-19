-- Table structure for table marca
CREATE TABLE marca (
  id_marca INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  descripcion NVARCHAR(255) NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);

-- Table structure for table categoria
CREATE TABLE categoria (
  id_categoria INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  descripcion NVARCHAR(255) NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);

-- Table structure for table producto
CREATE TABLE producto (
  id_producto INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  nombre NVARCHAR(255) NOT NULL,
  descripcion NVARCHAR(MAX) NOT NULL,
  id_marca INT NOT NULL,
  id_categoria INT NOT NULL,
  precio DECIMAL(10, 2) NOT NULL,
  url_imagen NVARCHAR(255) NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE(),
  CONSTRAINT FK_producto_marca FOREIGN KEY (id_marca) REFERENCES marca(id_marca),
  CONSTRAINT FK_producto_categoria FOREIGN KEY (id_categoria) REFERENCES categoria(id_categoria)
);

-- Table structure for table talle
CREATE TABLE talle (
  id_talle INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  talle NVARCHAR(50) NOT NULL,
  stock INT NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  id_producto INT NOT NULL,
  CONSTRAINT FK_id_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);

-- Table structure for table usuario
CREATE TABLE usuario (
  id_usuario INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  nombre NVARCHAR(255) NOT NULL,
  apellido NVARCHAR(255) NOT NULL,
  correo NVARCHAR(255) NOT NULL,
  clave NVARCHAR(255) NOT NULL,
  tipo_de_usuario BIT NOT NULL DEFAULT 1, -- usuario 1 normal, usuario 0 admin
  reestablecer BIT NOT NULL DEFAULT 1,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);

-- Table structure for table carrito
CREATE TABLE carrito (
  id_carrito INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  id_usuario INT NOT NULL,
  id_producto INT NOT NULL,
  cantidad INT NOT NULL,
  CONSTRAINT FK_carrito_usuario FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario),
  CONSTRAINT FK_carrito_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);

-- Table structure for table provincia
CREATE TABLE provincia (
  id_provincia INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  nombre NVARCHAR(255) NOT NULL
);

-- Table structure for table localidad
CREATE TABLE localidad (
  id_localidad INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  nombre NVARCHAR(255) NOT NULL,
  id_provincia INT NOT NULL,
  CONSTRAINT FK_localidad_provincia FOREIGN KEY (id_provincia) REFERENCES provincia(id_provincia)
);

-- Table structure for table venta
CREATE TABLE venta (
  id_venta INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  id_usuario INT NOT NULL,
  total_producto INT NOT NULL,
  monto_total DECIMAL(10, 2) NOT NULL,
  contacto NVARCHAR(255) NOT NULL,
  id_provincia INT NOT NULL,
  telefono NVARCHAR(50) NOT NULL,
  direccion NVARCHAR(255) NOT NULL,
  id_transaccion INT NOT NULL,
  fecha_venta DATE NOT NULL DEFAULT GETDATE(),
  CONSTRAINT FK_venta_usuario FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario),
  CONSTRAINT FK_venta_provincia FOREIGN KEY (id_provincia) REFERENCES provincia(id_provincia)
);

-- Table structure for table detalleventa
CREATE TABLE detalleventa (
  id_detalleVenta INT NOT NULL PRIMARY KEY IDENTITY(1,1) CHECK (Id >= 0),
  id_venta INT NOT NULL,
  id_producto INT NOT NULL,
  cantidad INT NOT NULL,
  total DECIMAL(10, 2) NOT NULL,
  CONSTRAINT FK_detalleventa_venta FOREIGN KEY (id_venta) REFERENCES venta(id_venta),
  CONSTRAINT FK_detalleventa_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);

-- Agregar restricción única a la columna correo en la tabla usuario
ALTER TABLE usuario ADD CONSTRAINT uq_usuario_correo UNIQUE (correo);

-- Indice compuesto en id_producto y talle para acelerar las búsquedas
CREATE INDEX idx_producto_talle ON talle(id_producto, talle);

-- Indice para los correos en la tabla usuario
CREATE INDEX idx_usuario_email ON usuario(correo);
