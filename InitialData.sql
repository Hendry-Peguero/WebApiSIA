USE InventorySIA;
GO

-- ========================================
-- 1. INSERTAR USUARIO ADMINISTRADOR
-- ========================================
-- Password: "admin123" hasheado en MD5 = 0192023a7bbd73250516f069df18b500
INSERT INTO users (FullName, UserName, Password, Privilege, RegDate, Can_Add, Can_Edit, Can_Delete, Can_Print)
VALUES 
('Administrador', 'admin', '0192023a7bbd73250516f069df18b500', 'Admin', GETDATE(), 'Si', 'Si', 'Si', 'Si');

-- ========================================
-- 2. INSERTAR TASAS DE IVA
-- ========================================
INSERT INTO vat (VAT) VALUES (0.00);   -- Sin IVA
INSERT INTO vat (VAT) VALUES (16.00);  -- IVA 16%
INSERT INTO vat (VAT) VALUES (18.00);  -- IVA 18%

-- ========================================
-- 3. INSERTAR GRUPOS DE ARTÍCULOS
-- ========================================
INSERT INTO itemgroup (GROUP_NAME) VALUES ('Electrónica');
INSERT INTO itemgroup (GROUP_NAME) VALUES ('Alimentos');
INSERT INTO itemgroup (GROUP_NAME) VALUES ('Ropa');
INSERT INTO itemgroup (GROUP_NAME) VALUES ('Herramientas');

-- ========================================
-- 4. INSERTAR ALMACENES
-- ========================================
INSERT INTO warehouse (WarehouseAddress) VALUES ('Almacén Principal - Calle 123');
INSERT INTO warehouse (WarehouseAddress) VALUES ('Almacén Secundario - Ave. 456');

-- ========================================
-- 5. INSERTAR PRODUCTOS DE PRUEBA
-- ========================================
INSERT INTO iteminformation 
(ItemName, UnitOfMeasure, Barcode, Barcode2, Barcode3, Cost, Price, Price2, Price3, Margen, ReorderPoint, VAT_Applicable, VAT_ID, WarehouseID, GROUP_ID, AllowDecimal, comment)
VALUES 
-- Producto 1
('Laptop HP Pavilion', 'UND', '1234567890', '7890123456', 'LAP-HP-001', 800.00, 1200.00, 1150.00, 1100.00, 33.33, 5, 'Si', 2, 1, 1, 0, 'Laptop con 16GB RAM, 512GB SSD'),

-- Producto 2
('Mouse Logitech M185', 'UND', '2345678901', '8901234567', 'MOU-LOG-185', 15.00, 25.00, 23.00, 20.00, 40.00, 20, 'Si', 2, 1, 1, 0, 'Mouse inalámbrico'),

-- Producto 3
('Arroz Blanco 1kg', 'KG', '3456789012', '9012345678', 'ARROZ-1KG', 1.50, 2.80, 2.70, 2.60, 46.43, 100, 'No', 1, 2, 2, 1, 'Arroz de primera calidad'),

-- Producto 4
('Camiseta Algodón', 'UND', '4567890123', NULL, 'CAM-ALG-M', 8.00, 18.00, 17.00, 16.00, 55.56, 50, 'Si', 2, 1, 3, 0, 'Talla M, 100% algodón'),

-- Producto 5
('Martillo 500g', 'UND', '5678901234', '0123456789', 'MART-500', 12.00, 22.00, 21.00, 20.00, 45.45, 15, 'Si', 2, 1, 4, 0, 'Martillo profesional con mango de fibra');

GO

-- ========================================
-- 6. VERIFICAR DATOS INSERTADOS
-- ========================================
PRINT '✅ Datos insertados exitosamente!';
PRINT '';
PRINT 'Resumen:';
SELECT 'Usuarios' AS Tabla, COUNT(*) AS Total FROM users
UNION ALL
SELECT 'Tasas IVA', COUNT(*) FROM vat
UNION ALL
SELECT 'Grupos', COUNT(*) FROM itemgroup
UNION ALL
SELECT 'Almacenes', COUNT(*) FROM warehouse
UNION ALL
SELECT 'Productos', COUNT(*) FROM iteminformation;
