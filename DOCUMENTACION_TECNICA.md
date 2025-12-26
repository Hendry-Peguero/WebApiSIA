# 📚 DOCUMENTACIÓN TÉCNICA - WebApiSIA

> **Sistema de Inventario Avanzado (SIA)**  
> Web API RESTful para gestión completa de inventarios

**Versión:** 1.0  
**Framework:** .NET 9.0  
**Fecha:** Diciembre 2024

---

## 📋 Tabla de Contenidos

1. [Descripción General](#descripción-general)
2. [Arquitectura del Sistema](#arquitectura-del-sistema)
3. [Modelo de Datos](#modelo-de-datos)
4. [API Endpoints](#api-endpoints)
5. [Tecnologías Utilizadas](#tecnologías-utilizadas)
6. [Patrones de Diseño](#patrones-de-diseño)
7. [Seguridad y Autenticación](#seguridad-y-autenticación)
8. [Configuración](#configuración)
9. [Base de Datos](#base-de-datos)
10. [Ejecución del Proyecto](#ejecución-del-proyecto)

---

## 🎯 Descripción General

WebApiSIA es un sistema backend para la gestión integral de inventarios que proporciona:

- ✅ Gestión de artículos/productos con información detallada
- ✅ Control de movimientos de inventario (entradas, salidas, ajustes)
- ✅ Gestión de múltiples almacenes
- ✅ Autenticación y autorización de usuarios con JWT
- ✅ Sistema de permisos granulares
- ✅ Gestión de grupos de artículos y categorización
- ✅ Manejo de IVA/VAT para productos
- ✅ Soporte para múltiples códigos de barras
- ✅ Múltiples listas de precios

---

## 🏗️ Arquitectura del Sistema

### Clean Architecture (Arquitectura Limpia)

El proyecto implementa Clean Architecture, separando las responsabilidades en 4 capas principales:

```
┌─────────────────────────────────────────────────────────┐
│                  PRESENTATION LAYER                     │
│              (WebApplication1/WebApiSIA)                │
│                    Controllers                          │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│                  APPLICATION LAYER                      │
│          (WebApiSIA.Core.Application)                   │
│         Services, DTOs, Interfaces, Mappings            │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│                    DOMAIN LAYER                         │
│            (WebApiSIA.Core.Domain)                      │
│                     Entities                            │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│               INFRASTRUCTURE LAYER                      │
│       (WebApiSIA.Infrastructure.Persistence)            │
│        DbContext, Repositories, Helpers                 │
└─────────────────────────────────────────────────────────┘
```

### Estructura de Directorios

```
WebApiSIA/
│
├── 📂 WebApplication1/                    [Presentación]
│   ├── Controllers/
│   │   ├── InventoryMovementsController.cs
│   │   ├── ItemInformationController.cs
│   │   ├── UsersController.cs
│   │   ├── ItemGruopController.cs
│   │   ├── VatController.cs
│   │   └── WareHouseController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── .env
│
├── 📂 WebApiSIA.Core.Domain/             [Dominio]
│   └── Entities/
│       ├── ItemInformationEntity.cs
│       ├── InventoryMovementEntity.cs
│       ├── UserEntity.cs
│       ├── ItemGroupEntity.cs
│       ├── VatEntity.cs
│       └── WareHouseEntity.cs
│
├── 📂 WebApiSIA.Core.Application/        [Aplicación]
│   ├── Services/
│   │   ├── GenericService.cs
│   │   ├── ItemInformationService.cs
│   │   └── UserService.cs
│   ├── Interfaces/
│   │   ├── Services/
│   │   ├── Repositories/
│   │   └── Helpers/
│   ├── Dtos/
│   │   ├── ItemInformation/
│   │   ├── InventoryMovement/
│   │   ├── User/
│   │   ├── ItemGruop/
│   │   ├── Vat/
│   │   └── WareHouse/
│   ├── Mappings/
│   │   └── GeneralProfile.cs
│   ├── Helper/
│   └── DependencyInjection/
│
└── 📂 WebApiSIA.Infrastructure.Persistence/ [Infraestructura]
    ├── Contexts/
    │   └── ApplicationContext.cs
    ├── Repositories/
    │   ├── GenericRepository.cs
    │   ├── ItemInformationRepository.cs
    │   ├── InventoryMovementRepository.cs
    │   ├── UserRepository.cs
    │   ├── ItemGroupRepository.cs
    │   ├── VatRespository.cs
    │   └── WareHouseRepository.cs
    ├── Helpers/
    │   └── SqlHelper.cs
    └── DependencyInjection/
        └── DependencyInjectionPersistenceLayer.cs
```

---

## 🗄️ Modelo de Datos

### Diagrama de Entidades

```
┌─────────────────────────┐
│   ItemInformationEntity │
├─────────────────────────┤
│ PK: ITEM_ID             │
│ ItemName                │
│ UnitOfMeasure           │
│ Barcode (1,2,3)         │
│ Cost                    │
│ Price (1,2,3)           │
│ Margen                  │
│ ReorderPoint            │
│ FK: GROUP_ID            │
│ FK: WarehouseID         │
│ FK: VAT_ID              │
└─────────────────────────┘
           ↑
           │
           │ ITEM_ID (FK)
           │
┌─────────────────────────┐
│ InventoryMovementEntity │
├─────────────────────────┤
│ PK: Movement_ID         │
│ FK: ITEM_ID             │
│ Movement_Type           │
│ Quantity                │
│ Movement_Date           │
│ Reason                  │
│ FK: CreatedBy           │
└─────────────────────────┘
```

### Entidades Detalladas

#### 1. ItemInformationEntity (iteminformation)

Entidad principal que representa los artículos del inventario.

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `ITEM_ID` | int (PK) | Identificador único del artículo |
| `ItemName` | string(200) | Nombre del artículo |
| `UnitOfMeasure` | string(200) | Unidad de medida (piezas, kg, litros, etc.) |
| `Batch` | string(200) | Número de lote o serie |
| `GROUP_ID` | int? (FK) | ID del grupo/categoría |
| `Barcode` | string(200) | Código de barras principal |
| `Barcode2` | string(255) | Código de barras alternativo 1 |
| `Barcode3` | string(255) | Código de barras alternativo 2 |
| `Cost` | double? | Costo del artículo |
| `Price` | double? | Precio de venta principal |
| `Price2` | double? | Precio de venta alternativo 1 |
| `Price3` | double? | Precio de venta alternativo 2 |
| `Margen` | decimal(5,2)? | Margen de ganancia |
| `ReorderPoint` | double? | Punto mínimo de reorden |
| `VAT_Applicable` | string(10) | Si aplica IVA (Sí/No) |
| `VAT_ID` | int? (FK) | ID de la tasa de IVA |
| `WarehouseID` | int? (FK) | ID del almacén |
| `PhotoFileName` | string(200) | Nombre del archivo de imagen |
| `AllowDecimal` | bool? | Permite cantidades decimales |
| `Comment` | string | Comentarios adicionales |

#### 2. InventoryMovementEntity (inventory_movements)

Registra todos los movimientos de inventario.

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Movement_ID` | int (PK) | Identificador único del movimiento |
| `ITEM_ID` | int (FK) | ID del artículo |
| `Movement_Type` | string(15) | Tipo: "Entrada", "Salida", "Ajuste", etc. |
| `Quantity` | double | Cantidad del movimiento |
| `Movement_Date` | DateTime | Fecha y hora del movimiento |
| `Reason` | string | Razón o motivo del movimiento |
| `CreatedBy` | int (FK) | ID del usuario que creó el movimiento |

#### 3. UserEntity (users)

Gestión de usuarios del sistema.

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `USER_ID` | int (PK) | Identificador único del usuario |
| `FullName` | string(100) | Nombre completo |
| `UserName` | string(100) | Usuario para login |
| `Password` | string | Contraseña hasheada (MD5) |
| `Privilege` | string(100) | Rol o nivel de privilegio |
| `RegDate` | DateTime? | Fecha de registro |
| `Can_Add` | string(10) | Permiso para agregar (Sí/No) |
| `Can_Edit` | string(10) | Permiso para editar (Sí/No) |
| `Can_Delete` | string(10) | Permiso para eliminar (Sí/No) |
| `Can_Print` | string(10) | Permiso para imprimir (Sí/No) |

#### 4. ItemGroupEntity (itemgroup)

Categorización de artículos.

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `GROUP_ID` | int (PK) | Identificador del grupo |
| `GROUP_NAME` | string | Nombre del grupo/categoría |

#### 5. WareHouseEntity (warehouse)

Almacenes del sistema.

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `WarehouseID` | int (PK) | Identificador del almacén |
| `WarehouseAddress` | string | Dirección del almacén |

#### 6. VatEntity (vat)

Tasas de IVA.

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `ID` | int (PK) | Identificador |
| `VAT` | decimal | Porcentaje de IVA |

---

## 🔌 API Endpoints

### Base URL
- **HTTP**: `http://localhost:5037/api`
- **HTTPS**: `https://localhost:7242/api`
- **Swagger UI**: `http://localhost:5037/swagger`

---

### 🔑 Users Controller

**Ruta base:** `/api/Users`

#### POST /api/Users/login
Autenticación de usuario y generación de token JWT.

**Request Body:**
```json
{
  "userName": "string",
  "password": "string"
}
```

**Response:** `200 OK`
```json
{
  "user_ID": 1,
  "userName": "admin",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Response:** `401 Unauthorized`
```json
"Usuario o contraseña incorrectos."
```

---

### 📦 ItemInformation Controller

**Ruta base:** `/api/ItemInformation`

#### GET /api/ItemInformation
Obtener todos los artículos.

**Response:** `200 OK`
```json
[
  {
    "item_ID": 1,
    "itemName": "Producto ejemplo",
    "unitOfMeasure": "piezas",
    "barcode": "1234567890",
    "cost": 50.00,
    "price": 100.00,
    "margen": 50.00,
    ...
  }
]
```

#### GET /api/ItemInformation/{id}
Obtener artículo por ID.

**Response:** `200 OK` - ItemInformationDto  
**Response:** `404 Not Found`

#### GET /api/ItemInformation/barcode/{barcode}
Buscar artículo por código de barras.

**Parámetros:**
- `barcode` (string) - Código de barras a buscar

**Response:** `200 OK` - ItemInformationDto  
**Response:** `404 Not Found`
```json
{
  "message": "No existe artículo con Barcode '123456'."
}
```

#### POST /api/ItemInformation
Crear nuevo artículo.

**Request Body:** SaveItemInformationDto
```json
{
  "itemName": "Nuevo Producto",
  "unitOfMeasure": "piezas",
  "barcode": "1234567890",
  "cost": 50.00,
  "price": 100.00,
  "margen": 50.00,
  "reorderPoint": 10.0,
  "group_ID": 1,
  "warehouseID": 1,
  "vat_ID": 1,
  "allowDecimal": false
}
```

**Response:** `201 Created` - ItemInformationDto con ID generado

#### PUT /api/ItemInformation/{id}
Actualizar artículo existente.

**Request Body:** SaveItemInformationDto  
**Response:** `200 OK` - ItemInformationDto actualizado  
**Response:** `404 Not Found`

#### DELETE /api/ItemInformation/{id}
Eliminar artículo.

**Response:** `204 No Content`  
**Response:** `404 Not Found`

---

### 📊 InventoryMovements Controller

**Ruta base:** `/api/InventoryMovements`

#### GET /api/InventoryMovements
Obtener todos los movimientos.

**Response:** `200 OK` - Lista de InventoryMovementDto

#### GET /api/InventoryMovements/{id}
Obtener movimiento por ID.

**Response:** `200 OK` - InventoryMovementDto  
**Response:** `404 Not Found`

#### POST /api/InventoryMovements/adjust-inventory
Ajustar inventario (ejecuta stored procedure).

**Request Body:** AdjustInventoryRequestDto
```json
{
  "item_ID": 1,
  "movement_Type": "Ajuste",
  "quantity": 10.0,
  "warehouseID": 1,
  "shelf_ID": 1,
  "createdBy": 1,
  "reason": "Ajuste por inventario físico"
}
```

**Response:** `200 OK`
```json
{
  "message": "Inventario ajustado correctamente."
}
```

**Response:** `400 Bad Request` - Error de validación  
**Response:** `500 Internal Server Error`

#### PUT /api/InventoryMovements/{id}
Actualizar movimiento.

**Request Body:** SaveInventoryMovementDto  
**Response:** `200 OK` - InventoryMovementDto actualizado

#### DELETE /api/InventoryMovements/{id}
Eliminar movimiento.

**Response:** `204 No Content`  
**Response:** `404 Not Found`

---

### 🏢 ItemGruop Controller

**Ruta base:** `/api/ItemGruop`

CRUD básico para grupos de artículos:
- `GET /api/ItemGruop` - Listar todos
- `GET /api/ItemGruop/{id}` - Obtener por ID
- `POST /api/ItemGruop` - Crear
- `PUT /api/ItemGruop/{id}` - Actualizar
- `DELETE /api/ItemGruop/{id}` - Eliminar

---

### 🏭 WareHouse Controller

**Ruta base:** `/api/WareHouse`

CRUD básico para almacenes:
- `GET /api/WareHouse` - Listar todos
- `GET /api/WareHouse/{id}` - Obtener por ID
- `POST /api/WareHouse` - Crear
- `PUT /api/WareHouse/{id}` - Actualizar
- `DELETE /api/WareHouse/{id}` - Eliminar

---

### 💰 Vat Controller

**Ruta base:** `/api/Vat`

CRUD básico para tasas de IVA:
- `GET /api/Vat` - Listar todos
- `GET /api/Vat/{id}` - Obtener por ID
- `POST /api/Vat` - Crear
- `PUT /api/Vat/{id}` - Actualizar
- `DELETE /api/Vat/{id}` - Eliminar

---

## 🛠️ Tecnologías Utilizadas

### Framework y Runtime
- **.NET 9.0** - Framework principal
- **ASP.NET Core** - Web API framework

### Base de Datos
- **SQL Server** (LocalDB/Express)
- **Entity Framework Core 9.0.11** - ORM
- **Microsoft.EntityFrameworkCore.SqlServer 9.0.11** - Provider
- **Microsoft.EntityFrameworkCore.Design 9.0.11** - Herramientas de diseño
- **Microsoft.EntityFrameworkCore.Tools 9.0.11** - Herramientas de migración

### Seguridad
- **Microsoft.AspNetCore.Authentication.JwtBearer 9.0.11** - Autenticación JWT
- **Microsoft.IdentityModel.Tokens 8.15.0** - Manejo de tokens
- **System.IdentityModel.Tokens.Jwt 8.15.0** - Generación de JWT
- **MD5 Hashing** - Hash de contraseñas (implementación custom)

### Mapping
- **AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.0** - Mapeo objeto-objeto

### Configuración
- **DotNetEnv 3.1.1** - Carga de variables de entorno

### Documentación
- **Swashbuckle.AspNetCore 9.0.6** - Swagger/OpenAPI
- **Microsoft.AspNetCore.OpenApi 9.0.5** - Especificación OpenAPI

### Otros
- **Microsoft.AspNetCore.Http 2.3.0** - Abstracciones HTTP
- **Microsoft.AspNetCore.Http.Extensions 2.3.0** - Extensiones HTTP
- **Microsoft.Extensions.Options.ConfigurationExtensions 9.0.11** - Configuración

---

## 🎨 Patrones de Diseño

### 1. Repository Pattern

Abstracción del acceso a datos mediante repositorios.

**Implementación:**

```csharp
// Interfaz genérica
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}

// Implementación genérica
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
{
    private readonly ApplicationContext _context;
    private readonly DbSet<TEntity> _dbSet;
    // ...
}
```

**Repositorios específicos:**
- `ItemInformationRepository` - Incluye búsqueda por barcode
- `InventoryMovementRepository`
- `UserRepository` - Incluye búsqueda por username
- `ItemGroupRepository`
- `VatRepository`
- `WareHouseRepository`

---

### 2. Service Pattern

Capa de lógica de negocio separada de los controladores.

**Implementación:**

```csharp
// Servicio genérico
public class GenericService<TSaveDto, TDto, TEntity>
    : IGenericService<TSaveDto, TDto, TEntity>
    where TEntity : class
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    
    public async Task<List<TDto>> GetAllAsync() { ... }
    public async Task<TDto?> GetByIdAsync(int id) { ... }
    public async Task<TDto> CreateAsync(TSaveDto dto) { ... }
    public async Task<TDto> UpdateAsync(int id, TSaveDto dto) { ... }
    public async Task DeleteAsync(int id) { ... }
}
```

**Servicios especializados:**
- `ItemInformationService` - Búsqueda por barcode
- `UserService` - Autenticación, generación JWT

---

### 3. DTO Pattern (Data Transfer Objects)

Separación entre entidades de dominio y objetos de transferencia.

**Tipos de DTOs:**
- **SaveDto** - Para crear nuevos registros (sin ID)
- **UpdateDto** - Para actualizar registros
- **Dto** - Para lectura y respuestas (incluye ID)
- **RequestDto** - Para requests específicos (ej: LoginRequestDto)
- **ResponseDto** - Para respuestas específicas (ej: LoginResponseDto)

**Ejemplo:**
```csharp
// Para guardar (sin ID)
public class SaveItemInformationDto
{
    public string? ItemName { get; set; }
    public double? Cost { get; set; }
    // ...
}

// Para respuesta (con ID)
public class ItemInformationDto
{
    public int ITEM_ID { get; set; }
    public string? ItemName { get; set; }
    public double? Cost { get; set; }
    // ...
}
```

---

### 4. Dependency Injection (DI)

Inyección de dependencias nativa de ASP.NET Core.

**Registro de servicios:**

```csharp
// En DependencyInjectionPersistenceLayer.cs
services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddTransient<IItemInformationRepository, ItemInformationRepository>();
services.AddTransient<ISqlHelper, SqlHelper>();

// En ApplicationDependency.cs
services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
services.AddTransient<IUserService, UserService>();
services.AddAutoMapper(Assembly.GetExecutingAssembly());
```

---

### 5. Mapper Pattern (AutoMapper)

Mapeo automático entre entidades y DTOs.

**Configuración:**

```csharp
public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        // ItemInformation
        CreateMap<ItemInformationEntity, ItemInformationDto>();
        CreateMap<SaveItemInformationDto, ItemInformationEntity>()
            .ForMember(dest => dest.ITEM_ID, opt => opt.Ignore());
        
        // InventoryMovement
        CreateMap<InventoryMovementEntity, InventoryMovementDto>();
        CreateMap<SaveInventoryMovementDto, InventoryMovementEntity>();
        
        // User
        CreateMap<UserEntity, UserDto>();
        
        // Etc...
    }
}
```

---

## 🔐 Seguridad y Autenticación

### Flujo de Autenticación JWT

```
┌──────────┐          ┌──────────┐         ┌──────────┐
│ Cliente  │          │   API    │         │    BD    │
└────┬─────┘          └────┬─────┘         └────┬─────┘
     │                     │                    │
     │  POST /api/Users/login                   │
     ├─────────────────────>                    │
     │  {user, pass}        │                   │
     │                      │  Buscar usuario   │
     │                      ├──────────────────>│
     │                      │  <UserEntity>     │
     │                      <──────────────────┤
     │                      │                   │
     │                      │  Verificar MD5    │
     │                      │  hash             │
     │                      │                   │
     │                      │  Generar JWT      │
     │                      │  con claims       │
     │                      │                   │
     │  200 OK + JWT Token  │                   │
     <─────────────────────┤                   │
     │                      │                   │
     │                      │                   │
     │  GET /api/ItemInformation                │
     ├─────────────────────>                    │
     │  Authorization:      │                   │
     │  Bearer {token}      │                   │
     │                      │  Validar token    │
     │                      │                   │
     │                      │  Consultar items  │
     │                      ├──────────────────>│
     │                      │  <List<Items>>    │
     │  200 OK + Items      <──────────────────┤
     <─────────────────────┤                   │
```

### Configuración JWT

**Variables de entorno:**
```env
JWT_KEY=ClaveSuperSecreta_LARGA_1234567890!!!
JWT_ISSUER=WebApiSIA
JWT_AUDIENCE=WebApiSIA
JWT_EXPIRE_MINUTES=60
```

**Claims incluidos en el token:**
- `NameIdentifier` - USER_ID del usuario
- `Name` - UserName del usuario
- `Role` - Privilege/Rol del usuario

**Configuración en Program.cs:**
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!)
            )
        };
    });
```

### Hash de Contraseñas

Se utiliza **MD5** para hashear las contraseñas de usuario.

> ⚠️ **Nota de Seguridad**: MD5 es considerado criptográficamente débil. Para producción se recomienda usar **bcrypt**, **Argon2** o **PBKDF2**.

---

## ⚙️ Configuración

### Variables de Entorno (.env)

El archivo `.env` en la raíz del proyecto WebApplication1 contiene:

```env
# ENVIRONMENT
ASPNETCORE_ENVIRONMENT=Development

# URLS / PORTS
ASPNETCORE_URLS=http://localhost:5037;https://localhost:7242

# DATABASE
CONNECTION_STRING=Server=localhost\SQLEXPRESS;Database=InventorySIA;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true

# JWT
JWT_KEY=ClaveSuperSecreta_LARGA_1234567890!!!
JWT_ISSUER=WebApiSIA
JWT_AUDIENCE=WebApiSIA
JWT_EXPIRE_MINUTES=60

# CORS
CORS_ALLOWED_ORIGINS=http://localhost:5175
```

### CORS Configuration

El sistema permite peticiones desde orígenes configurados:

```csharp
var corsOrigins = Environment
    .GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
    ?.Split(",", StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(corsOrigins ?? [])
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

### Swagger Configuration

Swagger está habilitado en **todos los entornos** para facilitar el testing:

```csharp
app.UseSwagger();
app.UseSwaggerUI();
```

Acceder a: `http://localhost:5037/swagger`

---

## 💾 Base de Datos

### Motor
**SQL Server** (LocalDB o Express)

### Cadena de Conexión
```
Server=localhost\SQLEXPRESS;
Database=InventorySIA;
Trusted_Connection=True;
TrustServerCertificate=True;
MultipleActiveResultSets=true
```

### Tablas

| Tabla | Descripción |
|-------|-------------|
| `iteminformation` | Artículos/productos |
| `inventory_movements` | Movimientos de inventario |
| `users` | Usuarios del sistema |
| `itemgroup` | Grupos/categorías de artículos |
| `warehouse` | Almacenes |
| `vat` | Tasas de IVA |

### Stored Procedures

#### sp_AdjustInventory

Procedimiento almacenado para ajustes de inventario que garantiza transacciones atómicas.

**Parámetros:**
- `@ITEM_ID` (int)
- `@Movement_Type` (varchar)
- `@Quantity` (float)
- `@WarehouseID` (int)
- `@SHELF_ID` (int)
- `@CreatedBy` (int)
- `@Reason` (varchar)

**Ejecutado desde:**
```csharp
_sqlHelper.ExecuteSQLStoredProcedure("sp_AdjustInventory", parameters);
```

---

## 🚀 Ejecución del Proyecto

### Requisitos Previos

1. **.NET 9.0 SDK** o superior
2. **SQL Server** (LocalDB, Express o Full)
3. **Visual Studio 2022** (opcional) o VS Code

### Pasos para Ejecutar

#### 1. Restaurar Dependencias

```bash
dotnet restore
```

#### 2. Configurar Base de Datos

Asegúrate de que SQL Server esté corriendo y la cadena de conexión en `.env` sea correcta.

#### 3. Aplicar Migraciones (si existen)

```bash
dotnet ef database update --project WebApiSIA.Infrastructure.Persistence
```

#### 4. Compilar el Proyecto

```bash
dotnet build
```

#### 5. Ejecutar la Aplicación

```bash
dotnet run --project WebApplication1/WebApiSIA.csproj
```

O simplemente:

```bash
cd WebApplication1
dotnet run
```

#### 6. Acceder a la API

- **Swagger UI**: http://localhost:5037/swagger
- **API Base URL**: http://localhost:5037/api
- **HTTPS**: https://localhost:7242

### Detener la Aplicación

Presionar `Ctrl + C` en la terminal.

---

## 📊 Flujos de Trabajo

### Flujo 1: Agregar Nuevo Producto

```
1. Cliente → POST /api/ItemInformation
   Body: SaveItemInformationDto

2. ItemInformationController recibe request

3. Controller → GenericService.CreateAsync()

4. Service → AutoMapper mapea SaveDto → Entity

5. Service → Repository.AddAsync(entity)

6. Repository → EF Core guarda en BD

7. Repository → Retorna Entity con ID generado

8. Service → AutoMapper mapea Entity → Dto

9. Controller → Retorna 201 Created + Dto
```

### Flujo 2: Ajuste de Inventario

```
1. Cliente → POST /api/InventoryMovements/adjust-inventory
   Body: AdjustInventoryRequestDto

2. InventoryMovementsController recibe request

3. Controller → SqlHelper.ExecuteSQLStoredProcedure()

4. SqlHelper → Ejecuta sp_AdjustInventory en SQL Server

5. Stored Procedure:
   - Actualiza cantidad en iteminformation
   - Registra movimiento en inventory_movements
   - Todo en una transacción

6. SqlHelper → Retorna éxito/error

7. Controller → Retorna 200 OK o error
```

### Flujo 3: Autenticación

```
1. Cliente → POST /api/Users/login
   Body: { userName, password }

2. UsersController → UserService.LoginAsync()

3. Service → UserRepository.GetByUserNameAsync()

4. Repository → Consulta BD

5. Service → MD5Helper.VerifyMd5(password, dbHash)

6. Si válido:
   - Service → GenerateJwtToken(user)
   - Token incluye claims: NameIdentifier, Name, Role
   - Expira en JWT_EXPIRE_MINUTES

7. Service → Retorna LoginResponseDto

8. Controller → Retorna 200 OK + { userId, userName, token }

9. Cliente guarda token

10. Requests subsecuentes:
    Header: Authorization: Bearer {token}
```

---

## 🎯 Casos de Uso

### 1. Sistema de Punto de Venta (POS)
- Escanear código de barras para obtener producto
- Consultar precio según lista (Price, Price2, Price3)
- Registrar venta como movimiento de salida

### 2. Gestión de Inventario
- Control de entradas de mercancía
- Registro de salidas
- Ajustes por inventario físico
- Consulta de stock actual

### 3. Multi-almacén
- Gestión de inventario en múltiples ubicaciones
- Transferencias entre almacenes
- Consulta de disponibilidad por almacén

### 4. Control de Usuarios
- Login de empleados
- Permisos granulares (agregar, editar, eliminar, imprimir)
- Auditoría de movimientos por usuario

### 5. Gestión de Precios
- Definición de múltiples listas de precios
- Cálculo automático de margen
- Control de costos

---

## 📈 Mejoras Recomendadas

### Seguridad
- [ ] Reemplazar MD5 por **bcrypt** o **Argon2** para contraseñas
- [ ] Implementar **refresh tokens** para JWT
- [ ] Agregar **rate limiting**
- [ ] Implementar **HTTPS obligatorio** en producción

### Funcionalidad
- [ ] Implementar **migraciones de EF Core**
- [ ] Agregar **paginación** en endpoints de listado
- [ ] Implementar **filtros y búsqueda** avanzada
- [ ] Agregar **logging** estructurado (Serilog)
- [ ] Implementar **caché** (Redis)

### Arquitectura
- [ ] Agregar **Unit Tests** y **Integration Tests**
- [ ] Implementar **CQRS** para separar lectura/escritura
- [ ] Agregar **MediatR** para manejo de comandos
- [ ] Implementar **FluentValidation** para validaciones

### Base de Datos
- [ ] Agregar **índices** en campos de búsqueda frecuente
- [ ] Implementar **soft delete** (eliminación lógica)
- [ ] Agregar **auditoría** (CreatedAt, UpdatedAt, DeletedAt)
- [ ] Implementar **versionado** de registros

---

## 📝 Notas Técnicas

### Nomenclatura
- El proyecto usa una mezcla de inglés y español en nombres
- Tablas en minúsculas sin guiones bajos (ej: `iteminformation`)
- Entidades en PascalCase con sufijo `Entity`
- DTOs en PascalCase con sufijo `Dto`

### Convenciones de Código
- Interfaces con prefijo `I` (ej: `IUserService`)
- Variables privadas con guión bajo `_` (ej: `_repository`)
- Métodos async con sufijo `Async`

### Entity Framework
- Configuración **Code First**
- Mapeo fluido en `ApplicationContext.OnModelCreating()`
- Convención de nombres de columna preservada de BD legacy

---

## 📞 Soporte y Mantenimiento

### Logs de Aplicación
Los logs se generan en la consola durante la ejecución:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5037
```

### Debugging
Para depurar en Visual Studio:
1. Abrir `WebApiSIA.sln`
2. Establecer `WebApiSIA` como proyecto de inicio
3. Presionar F5

### Troubleshooting Común

**Error:** "dotnet no se reconoce como comando"
- Solución: Agregar `C:\Program Files\dotnet` al PATH

**Error:** "No se puede conectar a SQL Server"
- Verificar que SQL Server esté corriendo
- Verificar cadena de conexión en `.env`

**Error:** "JWT_KEY no está configurado"
- Verificar que el archivo `.env` exista
- Verificar que DotNetEnv esté cargando correctamente

---

## 📄 Licencia

[Especificar licencia del proyecto]

---

## 👥 Autores

[Especificar autores/equipo]

---

**Última actualización:** Diciembre 2024  
**Versión del documento:** 1.0
