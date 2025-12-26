USE InventorySIA;
GO

-- ==================================================================================
-- SCRIPT DE LIMPIEZA DE DATOS (Clean Database)
-- ⚠️ ESTE SCRIPT ELIMINARÁ TODOS LOS REGISTROS DE LAS TABLAS
-- ==================================================================================

PRINT '🧹 Iniciando limpieza de base de datos...';

-- 1. Eliminar Movimientos de Inventario (Tabla Hija)
DELETE FROM inventory_movements;
DBCC CHECKIDENT ('inventory_movements', RESEED, 0); -- Reiniciar contador ID
PRINT '✅ Tabla inventory_movements vaciada';

-- 2. Eliminar Productos (Tabla Padre de Movimientos, Hija de catálogos)
DELETE FROM iteminformation;
DBCC CHECKIDENT ('iteminformation', RESEED, 0);
PRINT '✅ Tabla iteminformation vaciada';

-- 3. Eliminar Catálogos (Opcional - Comentar si se quieren conservar)
-- Nota: Si se eliminan, se deben volver a crear antes de insertar productos
DELETE FROM warehouse;
DBCC CHECKIDENT ('warehouse', RESEED, 0);
PRINT '✅ Tabla warehouse vaciada';

DELETE FROM itemgroup;
DBCC CHECKIDENT ('itemgroup', RESEED, 0);
PRINT '✅ Tabla itemgroup vaciada';

-- Mantenemos los impuestos base (0, 16, 18) o los borramos?
-- Borramos todo para una limpieza total
DELETE FROM vat;
DBCC CHECKIDENT ('vat', RESEED, 0);
PRINT '✅ Tabla vat vaciada';

-- 4. Eliminar Usuarios (EXCEPTO ADMIN)
DELETE FROM users WHERE UserName <> 'admin';
-- No reiniciamos identidad para no afectar el ID del admin si se queda
PRINT '✅ Usuarios eliminados (excepto admin)';

PRINT '========================================';
PRINT '🏁 LIMPIEZA COMPLETADA';
PRINT '========================================';
GO

-- ==================================================================================
-- REINSERCIÓN DE DATOS BÁSICOS (Opcional)
-- Descomentar si se desea restaurar catálogos base después de limpiar
-- ==================================================================================
/*
-- Insertar VAT
INSERT INTO vat (VAT, Descripcion) VALUES (0.00, 'Sin IVA'), (16.00, 'IVA General'), (18.00, 'IVA Especial');

-- Insertar Almacenes
INSERT INTO warehouse (WarehouseAddress, WarehouseName) VALUES ('Calle Principal 123', 'Almacén Central');

-- Insertar Grupos
INSERT INTO itemgroup (GROUP_NAME) VALUES ('General');
*/
