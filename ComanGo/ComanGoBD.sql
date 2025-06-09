
DROP DATABASE IF EXISTS ComanGoBD;
CREATE DATABASE ComanGoBD;
USE ComanGoBD;

-- TABLAS

CREATE TABLE IF NOT EXISTS Empleados (
    IdEmpleado INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50),
    Usuario VARCHAR(30) UNIQUE,
    Contrasena VARCHAR(30),
    Rol VARCHAR(20) DEFAULT 'empleado'

);

CREATE TABLE IF NOT EXISTS Productos (
    IdProducto INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) UNIQUE,
    Precio DECIMAL(10, 2)
);


CREATE TABLE IF NOT EXISTS Mesas (
    IdMesa INT AUTO_INCREMENT PRIMARY KEY,
    NombreMesa VARCHAR(20)
);


CREATE TABLE IF NOT EXISTS Comandas (
    IdComanda INT AUTO_INCREMENT PRIMARY KEY,
    IdEmpleado INT,
    IdMesa INT,
    Fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    Estado VARCHAR(20),
    FOREIGN KEY (IdEmpleado) REFERENCES Empleados(IdEmpleado),
    FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
);


CREATE TABLE IF NOT EXISTS DetalleComanda (
    IdDetalle INT AUTO_INCREMENT PRIMARY KEY,
    IdComanda INT,
    IdProducto INT,
    Cantidad INT,
    FOREIGN KEY (IdComanda) REFERENCES Comandas(IdComanda),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);


-- PRUEBA CON DATOS

INSERT INTO Empleados (Nombre, Usuario, Contrasena, Rol) VALUES
('Ana García', 'ana', '1234', 'admin'),
('Carlos López', 'carlos', 'abcd', 'empleado');


INSERT INTO Productos (Nombre, Precio) VALUES
('Café', 1.50),
('Tostada', 2.00),
('Zumo', 2.20),
('Croissant', 1.80),
('Bocadillo', 3.50),
('Agua', 1.00),
('Refresco', 1.80);


INSERT INTO Mesas (NombreMesa) VALUES
('Mesa 1'),
('Mesa 2'),
('Mesa 3'),
('Mesa 4'),
('Mesa 5');
