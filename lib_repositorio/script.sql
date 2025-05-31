CREATE DATABASE db_workshop
GO
USE db_workshop;
GO 

CREATE TABLE [Brands](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[BrandName] NVARCHAR(100) UNIQUE NOT NULL,
	[OriginCountry] NVARCHAR(100) NOT NULL
);

CREATE TABLE [Customers](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CustomerName] NVARCHAR(100) NOT NULL,
	[Identification] NVARCHAR(100) UNIQUE NOT NULL,
	[PhoneNumber] NVARCHAR(50) UNIQUE,
	[Email] NVARCHAR(100) UNIQUE
);

CREATE TABLE [Vehicles](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Plate] NVARCHAR(20) UNIQUE NOT NULL,
	[Chassis] NVARCHAR(50) UNIQUE NOT NULL,
	[Color] NVARCHAR(50) NOT NULL,
	[Engine] NVARCHAR(50) NOT NULL,
	[BrandId] INT REFERENCES [Brands](Id),
	[CustomerId] INT REFERENCES [Customers](Id)
);

CREATE TABLE [Employees](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Identification] NVARCHAR(100) UNIQUE NOT NULL,
	[EmployeeName] NVARCHAR(100) NOT NULL,
	[Active] BIT NOT NULL
);

CREATE TABLE [Orders](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[OrderRef] NVARCHAR(100) UNIQUE NOT NULL,
	[CustomerRemark] NVARCHAR(300) ,
	[ServiceCenterRemark] NVARCHAR(300),
	[VehicleId] INT REFERENCES [Vehicles](Id),
	[EmployeeId] INT REFERENCES [Employees](Id)
);

CREATE TABLE [PaymentMethods](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[PaymentMethod] NVARCHAR(50) NOT NULL,
	[Active] BIT NOT NULL
);

CREATE TABLE [Sales](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[SaleRef] NVARCHAR(100) UNIQUE NOT NULL,
	[Total] DECIMAL(20,2) NOT NULL,
	[PaymentMethodId] INT REFERENCES [PaymentMethods](Id),
	[OrderId] INT REFERENCES [Orders](Id)
);

CREATE TABLE [Services](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ServiceName] NVARCHAR(100) NOT NULL,
	[Reference] NVARCHAR(100) NOT NULL,
	[Price] DECIMAL(20,2) NOT NULL,
	[StimatedTime] NVARCHAR(50) NOT NULL
);

CREATE TABLE [OrderServices](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[OrderId] INT REFERENCES [Orders](Id),
	[ServiceId] INT REFERENCES [Services](Id)
);

CREATE TABLE [Categories](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CategoryName] NVARCHAR(100) UNIQUE NOT NULL,
	[Active] BIT NOT NULL
);


CREATE TABLE [Products](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ProductName] NVARCHAR(100) NOT NULL,
	[Reference] NVARCHAR(100) NOT NULL,
	[PurchasePrice] DECIMAL(20,2) NOT NULL,
	[SalePrice] DECIMAL(20,2) NOT NULL,
	[CategoryId] INT REFERENCES [Categories](Id)
);

CREATE TABLE [ServicesProducts](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ServiceId] INT REFERENCES [Services](Id),
	[ProductId] INT REFERENCES [Products](Id)
);


INSERT INTO [Brands] (BrandName,OriginCountry)
VALUES ('BMW','Alemania'),
('Toyota','Japon'),
('Renault','Francia');

INSERT INTO [Brands] (BrandName,OriginCountry)
VALUES('Chevrolet','Estados Unidos')

INSERT INTO [Brands] (BrandName,OriginCountry)
VALUES('Koenigsegg','Suecia'),
('Honda','Japon')


INSERT INTO [Customers] (CustomerName,Identification,PhoneNumber,Email)
VALUES ('Daniel Cardona','102358664','3034256987','danielcarne@mail.com'),
('Juan Pablo Bedoya','7210236','3054789651','juanpalo@outlook.com'),
('Tomas Cordoba','123478956','3014781234','tomatemax@gmail.com');

INSERT INTO [Customers] (CustomerName,Identification,PhoneNumber,Email)
VALUES ('Felipe Jara','84133522','3096142361','justjara@gmail.com')

INSERT INTO [Customers] (CustomerName,Identification,PhoneNumber,Email)
VALUES ('Andres Javier','45321356','3150213654','andresitoh@gmail.com'),
('Bruce Wayne','5447896','3180562147','batman@mail.com')


INSERT INTO [Vehicles] (Plate,Chassis,Color,Engine,BrandId,CustomerId)
VALUES ('XFV478','BW32-04789','Azul','Gasolina V8',1,1),
('ABC789','TY98-2341','Blanco','Gasolina V4',2,2),
('TYU351','RN78-112360','Gris','Electrico',3,3)

INSERT INTO [Vehicles] (Plate,Chassis,Color,Engine,BrandId,CustomerId)
VALUES('BAT666','KG1-1','Gris','Gasolina V12',5,6);

INSERT INTO [Employees] (EmployeeName,Identification,Active)
VALUES('David Padilla','4125896',1),
('Rafael Monrroy','478956322',0)

INSERT INTO [Orders] (OrderRef,CustomerRemark,ServiceCenterRemark,EmployeeId,VehicleId)
VALUES ('A001','Tener cuidado con la pintura',NULL,1,1),
('B002',NULL,'Espejo derecho roto',1,2),
('C003',NULL,NULL,1,3);

INSERT INTO [PaymentMethods] (PaymentMethod,Active)
VALUES ('Efectivo',1),
('Tarjeta Debito',1),
('Codigo QR',0);

INSERT INTO [Sales] (SaleRef,Total,PaymentMethodId,OrderId)
VALUES ('0123',220.22,2,2),
('456',100,1,3)

INSERT INTO [Services] (ServiceName,Reference,Price,StimatedTime)
VALUES ('Cambio de aceite','S12',40.8,'2 horas'),
('Reparacíon del chasis','S34',120.4,'12 horas');

INSERT INTO [Services] (ServiceName,Reference,Price,StimatedTime)
VALUES('Cambio de tablero','S32',100.5,'14 horas');

INSERT INTO[OrderServices] (OrderId,ServiceId)
VALUES (2,1),
(3,2);

INSERT INTO [Categories] (CategoryName,Active)
VALUES ('Aceite',1),
('Carroceria',1);

INSERT INTO [Products] (ProductName,Reference,PurchasePrice,SalePrice,CategoryId)
VALUES ('Aceite de motor','P43',18.2,20,1),
('Cofre de motor','P12',85.3,94.2,2)

INSERT INTO [ServicesProducts] (ProductId,ServiceId)
VALUES (1,1),
(2,2);

SELECT * FROM Brands
SELECT * FROM Customers
SELECT * FROM Vehicles
SELECT * FROM Employees
SELECT * FROM PaymentMethods
SELECT * FROM Orders
SELECT * FROM [Services]
SELECT * FROM Sales
SELECT * FROM Categories
SELECT * FROM Products
SELECT * FROM ServicesProducts
SELECT * FROM OrderServices



GO

CREATE TABLE Auditoria (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Tabla NVARCHAR(100),
    Operacion NVARCHAR(10), 
    Fecha DATETIME DEFAULT GETDATE(),
    Usuario NVARCHAR(100) DEFAULT SYSTEM_USER,
    Datos NVARCHAR(MAX) 
);
GO

-- ========= BRANDS =========
CREATE TRIGGER trg_Brands_INSERT ON Brands AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Brands', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Brands_UPDATE ON Brands AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Brands', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Brands_DELETE ON Brands AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Brands', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= CUSTOMERS =========
CREATE TRIGGER trg_Customers_INSERT ON Customers AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Customers', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Customers_UPDATE ON Customers AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Customers', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Customers_DELETE ON Customers AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Customers', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= VEHICLES =========
CREATE TRIGGER trg_Vehicles_INSERT ON Vehicles AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Vehicles', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Vehicles_UPDATE ON Vehicles AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Vehicles', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Vehicles_DELETE ON Vehicles AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Vehicles', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= EMPLOYEES =========
CREATE TRIGGER trg_Employees_INSERT ON Employees AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Employees', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Employees_UPDATE ON Employees AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Employees', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Employees_DELETE ON Employees AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Employees', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= ORDERS =========
CREATE TRIGGER trg_Orders_INSERT ON Orders AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Orders', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Orders_UPDATE ON Orders AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Orders', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Orders_DELETE ON Orders AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Orders', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= PAYMENTMETHODS =========
CREATE TRIGGER trg_PaymentMethods_INSERT ON PaymentMethods AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'PaymentMethods', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_PaymentMethods_UPDATE ON PaymentMethods AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'PaymentMethods', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_PaymentMethods_DELETE ON PaymentMethods AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'PaymentMethods', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= SALES =========
CREATE TRIGGER trg_Sales_INSERT ON Sales AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Sales', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Sales_UPDATE ON Sales AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Sales', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Sales_DELETE ON Sales AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Sales', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= SERVICES =========
CREATE TRIGGER trg_Services_INSERT ON Services AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Services', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Services_UPDATE ON Services AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Services', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Services_DELETE ON Services AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Services', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= ORDERSERVICES =========
CREATE TRIGGER trg_OrderServices_INSERT ON OrderServices AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'OrderServices', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_OrderServices_UPDATE ON OrderServices AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'OrderServices', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_OrderServices_DELETE ON OrderServices AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'OrderServices', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= CATEGORIES =========
CREATE TRIGGER trg_Categories_INSERT ON Categories AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Categories', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Categories_UPDATE ON Categories AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Categories', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Categories_DELETE ON Categories AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Categories', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= PRODUCTS =========
CREATE TRIGGER trg_Products_INSERT ON Products AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Products', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Products_UPDATE ON Products AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Products', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_Products_DELETE ON Products AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'Products', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

-- ========= SERVICESPRODUCTS =========
CREATE TRIGGER trg_ServicesProducts_INSERT ON ServicesProducts AFTER INSERT AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'ServicesProducts', 'INSERT', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_ServicesProducts_UPDATE ON ServicesProducts AFTER UPDATE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'ServicesProducts', 'UPDATE', (SELECT * FROM INSERTED FOR JSON AUTO);
END;
GO

CREATE TRIGGER trg_ServicesProducts_DELETE ON ServicesProducts AFTER DELETE AS
BEGIN
    INSERT INTO Auditoria (Tabla, Operacion, Datos)
    SELECT 'ServicesProducts', 'DELETE', (SELECT * FROM DELETED FOR JSON AUTO);
END;
GO

--Tabla de Usuarios

CREATE TABLE [Users](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[UserName] NVARCHAR(50) UNIQUE NOT NULL,
	[PasswordHash] NVARCHAR(50) UNIQUE NOT NULL,
	[RoleId] INT REFERENCES [Roles](Id)
);


--Tabla de Roles
CREATE TABLE [Roles](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleName] NVARCHAR(100) UNIQUE NOT NULL,
);


-- Insertar roles

INSERT INTO [Roles] (RoleName)
VALUES ('Admin')

--Insertar Usuario
INSERT INTO [Users] (UserName,PasswordHash,RoleId)
VALUES ('sam404','lurvr8PbVspJhjoGaxOmo8gTnSi0nwg2c8IZu9gfKU4=',1)

INSERT INTO [Roles] (RoleName)
VALUES ('Mecanico')

INSERT INTO [Users] (UserName,PasswordHash,RoleId)
VALUES ('santi404','oip8vInz06G3kByGah0oPaegWoTMP/w/pckOw9nGiGc=',2)











