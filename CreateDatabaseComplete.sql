-- ========================================
-- SCRIPT COMPLETO: Crear Base de Datos InventorySIA
-- Ejecutar desde SQL Server Management Studio
-- Conectado a: (localdb)\MSSQLLocalDB
-- ========================================

-- 1. CREAR LA BASE DE DATOS
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InventorySIA')
BEGIN
    CREATE DATABASE InventorySIA;
    PRINT '✅ Base de datos InventorySIA creada exitosamente';
END
ELSE
BEGIN
    PRINT '⚠️ La base de datos InventorySIA ya existe';
END
GO

USE InventorySIA;
GO

-- ========================================
-- 2. CREAR TABLA: users
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'users')
BEGIN
    CREATE TABLE users (
        USER_ID INT PRIMARY KEY IDENTITY(1,1),
        FullName NVARCHAR(100),
        UserName NVARCHAR(100),
        Password NVARCHAR(MAX),
        Privilege NVARCHAR(100),
        RegDate DATETIME,
        Can_Add NVARCHAR(10),
        Can_Edit NVARCHAR(10),
        Can_Delete NVARCHAR(10),
        Can_Print NVARCHAR(10)
    );
    PRINT '✅ Tabla users creada';
END
GO

-- ========================================
-- 3. CREAR TABLA: vat
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'vat')
BEGIN
    CREATE TABLE vat (
        ID INT PRIMARY KEY IDENTITY(1,1),
        VAT DECIMAL(18,2)
    );
    PRINT '✅ Tabla vat creada';
END
GO

-- ========================================
-- 4. CREAR TABLA: itemgroup
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'itemgroup')
BEGIN
    CREATE TABLE itemgroup (
        GROUP_ID INT PRIMARY KEY IDENTITY(1,1),
        GROUP_NAME NVARCHAR(MAX)
    );
    PRINT '✅ Tabla itemgroup creada';
END
GO

-- ========================================
-- 5. CREAR TABLA: warehouse
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'warehouse')
BEGIN
    CREATE TABLE warehouse (
        WarehouseID INT PRIMARY KEY IDENTITY(1,1),
        WarehouseAddress NVARCHAR(MAX)
    );
    PRINT '✅ Tabla warehouse creada';
END
GO

-- ========================================
-- 6. CREAR TABLA: iteminformation
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'iteminformation')
BEGIN
    CREATE TABLE iteminformation (
        ITEM_ID INT PRIMARY KEY IDENTITY(1,1),
        ItemName NVARCHAR(200),
        UnitOfMeasure NVARCHAR(200),
        Batch NVARCHAR(200),
        GROUP_ID INT,
        Barcode NVARCHAR(200),
        Barcode2 NVARCHAR(255),
        Barcode3 NVARCHAR(255),
        Cost FLOAT,
        Price FLOAT,
        Price2 FLOAT,
        Price3 FLOAT,
        Margen DECIMAL(5,2),
        ReorderPoint FLOAT,
        VAT_Applicable NVARCHAR(10),
        VAT_ID INT,
        WarehouseID INT,
        PhotoFileName NVARCHAR(200),
        AllowDecimal BIT,
        comment NVARCHAR(MAX),
        FOREIGN KEY (GROUP_ID) REFERENCES itemgroup(GROUP_ID),
        FOREIGN KEY (VAT_ID) REFERENCES vat(ID),
        FOREIGN KEY (WarehouseID) REFERENCES warehouse(WarehouseID)
    );
    PRINT '✅ Tabla iteminformation creada';
END
GO

-- ========================================
-- 7. CREAR TABLA: inventory_movements
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'inventory_movements')
BEGIN
    CREATE TABLE inventory_movements (
        Movement_ID INT PRIMARY KEY IDENTITY(1,1),
        ITEM_ID INT NOT NULL,
        Movement_Type NVARCHAR(15) NOT NULL,
        Quantity FLOAT NOT NULL,
        Movement_Date DATETIME NOT NULL,
        Reason NVARCHAR(MAX),
        CreatedBy INT,
        FOREIGN KEY (ITEM_ID) REFERENCES iteminformation(ITEM_ID),
        FOREIGN KEY (CreatedBy) REFERENCES users(USER_ID)
    );
    PRINT '✅ Tabla inventory_movements creada';
END
GO

-- ========================================
-- 8. CREAR TABLA: __EFMigrationsHistory (para compatibilidad con EF)
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
BEGIN
    CREATE TABLE __EFMigrationsHistory (
        MigrationId NVARCHAR(150) PRIMARY KEY,
        ProductVersion NVARCHAR(32) NOT NULL
    );
    
    -- Insertar registro de migración ficticia
    INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion)
    VALUES ('20251224_ManualCreation', '9.0.0');
    
    PRINT '✅ Tabla __EFMigrationsHistory creada';
END
GO

-- ========================================
-- 9. INSERTAR DATOS INICIALES
-- ========================================

PRINT '';
PRINT '========================================';
PRINT 'INSERTANDO DATOS INICIALES...';
PRINT '========================================';
PRINT '';

-- 9.1 INSERTAR USUARIO ADMINISTRADOR
IF NOT EXISTS (SELECT * FROM users WHERE UserName = 'admin')
BEGIN
    INSERT INTO users (FullName, UserName, Password, Privilege, RegDate, Can_Add, Can_Edit, Can_Delete, Can_Print)
    VALUES ('Administrador', 'admin', '0192023a7bbd73250516f069df18b500', 'Admin', GETDATE(), 'Si', 'Si', 'Si', 'Si');
    PRINT '✅ Usuario admin creado (Password: admin123)';
END
GO

-- 9.2 INSERTAR TASAS DE IVA
IF NOT EXISTS (SELECT * FROM vat)
BEGIN
    INSERT INTO vat (VAT) VALUES (0.00);
    INSERT INTO vat (VAT) VALUES (16.00);
    INSERT INTO vat (VAT) VALUES (18.00);
    PRINT '✅ Tasas de IVA insertadas';
END
GO

-- 9.3 INSERTAR GRUPOS DE ARTÍCULOS
IF NOT EXISTS (SELECT * FROM itemgroup)
BEGIN
    INSERT INTO itemgroup (GROUP_NAME) VALUES ('Electrónica');
    INSERT INTO itemgroup (GROUP_NAME) VALUES ('Alimentos');
    INSERT INTO itemgroup (GROUP_NAME) VALUES ('Ropa');
    INSERT INTO itemgroup (GROUP_NAME) VALUES ('Herramientas');
    PRINT '✅ Grupos de artículos insertados';
END
GO

-- 9.4 INSERTAR ALMACENES
IF NOT EXISTS (SELECT * FROM warehouse)
BEGIN
    INSERT INTO warehouse (WarehouseAddress) VALUES ('Almacén Principal - Calle 123');
    INSERT INTO warehouse (WarehouseAddress) VALUES ('Almacén Secundario - Ave. 456');
    PRINT '✅ Almacenes insertados';
END
GO

-- 9.5 INSERTAR PRODUCTOS DE PRUEBA
IF NOT EXISTS (SELECT * FROM iteminformation)
BEGIN
    INSERT INTO iteminformation 
    (ItemName, UnitOfMeasure, Barcode, Barcode2, Barcode3, Cost, Price, Price2, Price3, Margen, ReorderPoint, VAT_Applicable, VAT_ID, WarehouseID, GROUP_ID, AllowDecimal, comment)
    VALUES 
    ('Laptop HP Pavilion', 'UND', '1234567890', '7890123456', 'LAP-HP-001', 800.00, 1200.00, 1150.00, 1100.00, 33.33, 5, 'Si', 2, 1, 1, 0, 'Laptop con 16GB RAM, 512GB SSD'),
    ('Mouse Logitech M185', 'UND', '2345678901', '8901234567', 'MOU-LOG-185', 15.00, 25.00, 23.00, 20.00, 40.00, 20, 'Si', 2, 1, 1, 0, 'Mouse inalámbrico'),
    ('Arroz Blanco 1kg', 'KG', '3456789012', '9012345678', 'ARROZ-1KG', 1.50, 2.80, 2.70, 2.60, 46.43, 100, 'No', 1, 2, 2, 1, 'Arroz de primera calidad'),
    ('Camiseta Algodón', 'UND', '4567890123', NULL, 'CAM-ALG-M', 8.00, 18.00, 17.00, 16.00, 55.56, 50, 'Si', 2, 1, 3, 0, 'Talla M, 100% algodón'),
    ('Martillo 500g', 'UND', '5678901234', '0123456789', 'MART-500', 12.00, 22.00, 21.00, 20.00, 45.45, 15, 'Si', 2, 1, 4, 0, 'Martillo profesional con mango de fibra');
    
    PRINT '✅ Productos de prueba insertados';
END
GO

-- ========================================
-- 10. VERIFICACIÓN FINAL
-- ========================================
PRINT '';
PRINT '========================================';
PRINT '✅ ¡BASE DE DATOS CONFIGURADA EXITOSAMENTE!';
PRINT '========================================';
PRINT '';
PRINT 'RESUMEN DE DATOS:';
PRINT '----------------------------------------';

SELECT 'Usuarios' AS Tabla, COUNT(*) AS Total FROM users
UNION ALL
SELECT 'Tasas IVA', COUNT(*) FROM vat
UNION ALL
SELECT 'Grupos', COUNT(*) FROM itemgroup
UNION ALL
SELECT 'Almacenes', COUNT(*) FROM warehouse
UNION ALL
SELECT 'Productos', COUNT(*) FROM iteminformation
UNION ALL
SELECT 'Movimientos', COUNT(*) FROM inventory_movements;

PRINT '';
PRINT '========================================';
PRINT 'CREDENCIALES DE ACCESO:';
PRINT '----------------------------------------';
PRINT 'Usuario: admin';
PRINT 'Password: admin123';
PRINT '========================================';
PRINT '';
PRINT 'PRODUCTOS DISPONIBLES PARA PRUEBAS:';
PRINT '----------------------------------------';

SELECT Barcode, ItemName, Price AS 'Precio $'
FROM iteminformation;

PRINT '';
PRINT '✅ La base de datos está lista para usarse';
PRINT '📡 Ahora puedes ejecutar la API con: dotnet run';
GO
