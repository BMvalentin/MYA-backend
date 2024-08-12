-- Table structure for table marca
CREATE TABLE marca (
  id_marca INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  descripcion TEXT NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);


-- Table structure for table categoria
CREATE TABLE categoria (
  id_categoria INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  descripcion TEXT NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);


-- Table structure for table producto
CREATE TABLE producto (
  id_producto INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre TEXT NOT NULL,
  descripcion TEXT NOT NULL,
  id_marca INT NOT NULL,
  id_categoria INT NOT NULL,
  precio DECIMAL(10, 0) NOT NULL,
  url_imagen TEXT NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE(),
  CONSTRAINT FK_producto_marca FOREIGN KEY (id_marca) REFERENCES marca(id_marca),
  CONSTRAINT FK_producto_categoria FOREIGN KEY (id_categoria) REFERENCES categoria(id_categoria)
);

CREATE TABLE talle (
  id_talle INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  talle TEXT NOT NULL,
  stock INT NOT NULL,
  activo BIT NOT NULL DEFAULT 1,
  id_producto INT NOT NULL,
  CONSTRAINT FK_id_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);

-- Table structure for table cliente
CREATE TABLE cliente (
  id_cliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre TEXT NOT NULL,
  apellido TEXT NOT NULL,
  correo TEXT NOT NULL,
  clave TEXT NOT NULL,
  reestablecer BIT NOT NULL DEFAULT 0,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);


-- Table structure for table carrito
CREATE TABLE carrito (
  id_carrito INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  id_cliente INT NOT NULL,
  id_producto INT NOT NULL,
  cantidad INT NOT NULL,
  CONSTRAINT FK_carrito_cliente FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente),
  CONSTRAINT FK_carrito_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);


-- Table structure for table localidad
CREATE TABLE localidad (
  id_localidad INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre TEXT NOT NULL
);


-- Table structure for table provincia
CREATE TABLE provincia (
  id_provincia INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre TEXT NOT NULL,
  id_localidad INT NOT NULL,
  CONSTRAINT FK_provincia_localidad FOREIGN KEY (id_localidad) REFERENCES localidad(id_localidad)
);


-- Table structure for table venta
CREATE TABLE venta (
  id_venta INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  id_cliente INT NOT NULL,
  total_producto INT NOT NULL,
  monto_total DECIMAL(10, 0) NOT NULL,
  contacto TEXT NOT NULL,
  id_provincia INT NOT NULL,
  telefono INT NOT NULL,
  direccion TEXT NOT NULL,
  id_transaccion INT NOT NULL,
  fecha_venta DATE NOT NULL DEFAULT GETDATE(),
  CONSTRAINT FK_venta_cliente FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente),
  CONSTRAINT FK_venta_provincia FOREIGN KEY (id_provincia) REFERENCES provincia(id_provincia)
);


-- Table structure for table detalleventa
CREATE TABLE detalleventa (
  id_detalleVenta INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  id_venta INT NOT NULL,
  id_producto INT NOT NULL,
  cantidad INT NOT NULL,
  total DECIMAL(10, 0) NOT NULL,
  CONSTRAINT FK_detalleventa_venta FOREIGN KEY (id_venta) REFERENCES venta(id_venta),
  CONSTRAINT FK_detalleventa_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);


-- Table structure for table usuario
CREATE TABLE usuario (
  id_usuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre TEXT NOT NULL,
  apellido TEXT NOT NULL,
  correo TEXT NOT NULL,
  clave TEXT NOT NULL,
  reestablecer BIT NOT NULL DEFAULT 1,
  activo BIT NOT NULL DEFAULT 1,
  fecha_registro DATE NOT NULL DEFAULT GETDATE()
);