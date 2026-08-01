USE HotelZormatDB;
GO

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    Rol VARCHAR(20) NOT NULL CHECK (Rol IN ('Administrador','Recepcionista')),
    NombreCompleto VARCHAR(100) NOT NULL
);

CREATE TABLE Habitacion (
    IdHabitacion INT IDENTITY(1,1) PRIMARY KEY,
    Numero INT NOT NULL UNIQUE,
    Tipo VARCHAR(20) NOT NULL CHECK (Tipo IN ('Simple','Doble','Suite')),
    Piso INT NOT NULL,
    Capacidad INT NOT NULL,
    TarifaBase DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Disponible'
        CHECK (Estado IN ('Disponible','Ocupada','Reservada','Limpieza'))
);

CREATE TABLE Huesped (
    IdHuesped INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    TipoDocumento VARCHAR(10) NOT NULL CHECK (TipoDocumento IN ('Cedula','Pasaporte')),
    NumeroDocumento VARCHAR(20) NOT NULL UNIQUE,
    Nacionalidad VARCHAR(50),
    Telefono VARCHAR(20),
    Email VARCHAR(100)
);

CREATE TABLE Estadia (
    IdEstadia INT IDENTITY(1,1) PRIMARY KEY,
    IdHabitacion INT NOT NULL FOREIGN KEY REFERENCES Habitacion(IdHabitacion),
    IdHuesped INT NOT NULL FOREIGN KEY REFERENCES Huesped(IdHuesped),
    FechaEntrada DATETIME NOT NULL,
    FechaSalida DATETIME NULL,
    Temporada VARCHAR(10) NOT NULL CHECK (Temporada IN ('Alta','Media','Baja')),
    Estado VARCHAR(20) NOT NULL DEFAULT 'Activa' CHECK (Estado IN ('Activa','Cerrada'))
);

CREATE TABLE Factura (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    IdEstadia INT NOT NULL FOREIGN KEY REFERENCES Estadia(IdEstadia),
    NCF VARCHAR(20) NOT NULL UNIQUE,
    Subtotal DECIMAL(10,2) NOT NULL,
    ITBIS DECIMAL(10,2) NOT NULL,
    Propina DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    FechaEmision DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Bitacora (
    IdBitacora INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuario(IdUsuario),
    Accion VARCHAR(50) NOT NULL,
    FechaHora DATETIME NOT NULL DEFAULT GETDATE()
);

-- Datos iniciales
INSERT INTO Usuario (NombreUsuario, Contrasena, Rol, NombreCompleto) VALUES
('admin', 'admin123', 'Administrador', 'Administrador General'),
('recep1', 'recep123', 'Recepcionista', 'Recepcionista Uno');

INSERT INTO Habitacion (Numero, Tipo, Piso, Capacidad, TarifaBase, Estado) VALUES
(101, 'Simple', 1, 1, 2500, 'Disponible'),
(202, 'Doble', 2, 2, 4000, 'Disponible'),
(305, 'Suite', 3, 4, 7000, 'Disponible');