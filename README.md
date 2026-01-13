# Technical Test WTW - .NET 10 Web API

[🇪🇸 Español](#español) | [🇬🇧 English](#english)

---

## Español

Sistema de gestión de personas y usuarios desarrollado con .NET 10, Entity Framework Core y SQL Server.

### 📋 Descripción

API REST completa que permite gestionar personas y usuarios, incluyendo:
- CRUD de personas con columnas calculadas automáticas
- Validación de usuarios (login)
- Stored procedures para consultas optimizadas
- Mapeo automático con AutoMapper
- Base de datos SQL Server con Entity Framework Core

### 🚀 Tecnologías Utilizadas

- **.NET 10** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core 10.0.1** - ORM
- **SQL Server** - Base de datos
- **AutoMapper 12.0.1** - Mapeo de objetos
- **CORS** - Habilitado para consumo desde frontend

### 📁 Estructura del Proyecto

```
TechnicalTestWTW/
├── Controllers/
│   ├── PersonasController.cs      # Endpoints de Personas
│   └── UsuariosController.cs      # Endpoints de Usuarios (login)
├── Data/
│   ├── ApplicationDbContext.cs           # Contexto de EF Core
│   └── StoredProcedureExtensions.cs      # Extensiones para SP
├── Mappings/
│   └── PersonaMappingProfile.cs   # Configuración de AutoMapper
├── Models/
│   ├── Persona.cs                 # Entidad Persona
│   ├── PersonaDto.cs              # DTO de Persona
│   ├── Usuario.cs                 # Entidad Usuario
│   └── UsuarioDto.cs              # DTO de Usuario
├── Program.cs                     # Configuración de la aplicación
└── appsettings.json              # Configuración (Connection Strings)
```

### 🗄️ Modelo de Datos

#### Entidad: Personas

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Id` | int | Identificador único (auto-generado) |
| `FirstName` | string(100) | Nombres |
| `LastName` | string(100) | Apellidos |
| `IdentificationNumber` | string(50) | Número de identificación |
| `IdentificationType` | string(20) | Tipo (CC, TI, CE, Pasaporte) |
| `Email` | string(100) | Correo electrónico |
| `CreatedDate` | DateTime | Fecha de creación (auto-generada) |
| `FullName` | string | **Columna calculada**: FirstName + LastName |
| `FullIdentificationNumber` | string | **Columna calculada**: IdentificationNumber-IdentificationType |

#### Entidad: Usuarios

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Id` | int | Identificador único (auto-generado) |
| `Username` | string(50) | Nombre de usuario |
| `Password` | string(255) | Contraseña |
| `CreatedDate` | DateTime | Fecha de creación (auto-generada) |

### 🔌 Endpoints de la API

#### Base URL
```
http://localhost:5241/api
```

#### Personas

##### Listar todas las personas
```http
GET /personas
```

**Respuesta:**
```json
[
  {
    "id": 1,
    "firstName": "Daniel",
    "lastName": "Echeverri",
    "fullName": "Daniel Echeverri",
    "identificationNumber": "1088315344",
    "identificationType": "CC",
    "fullIdentificationNumber": "1088315344-CC",
    "email": "echeverri121@gmail.com",
    "createdDate": "2024-01-15T10:30:00Z"
  }
]
```

##### Listar personas usando Stored Procedure
```http
GET /personas/created
```

##### Obtener persona por ID
```http
GET /personas/{id}
```

##### Crear nueva persona
```http
POST /personas
Content-Type: application/json

{
  "firstName": "Juan",
  "lastName": "Pérez",
  "identificationNumber": "123456789",
  "identificationType": "CC",
  "email": "juan@example.com"
}
```

#### Usuarios

##### Validar usuario (Login)
```http
POST /usuarios/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

**Respuesta exitosa (200 OK):**
```json
{
  "id": 1,
  "username": "admin",
  "password": "admin123",
  "createdDate": "2024-01-15T10:30:00Z"
}
```

**Respuesta error (401 Unauthorized):**
```json
{
  "message": "Incorrect user or password"
}
```

### ⚙️ Configuración y Ejecución

#### Requisitos Previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) o SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (opcional)

#### Pasos para Ejecutar

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/light-roast/WTWTest
   cd TechnicalTestWTW
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Verificar la cadena de conexión** en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TechnicalTestWTWDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```

4. **Ejecutar la aplicación**
   ```bash
   dotnet run --project TechnicalTestWTW/TechnicalTestWTW.csproj
   ```

   La API estará disponible en: `http://localhost:5241`

5. **La base de datos se crea automáticamente** al ejecutar la aplicación por primera vez gracias a `EnsureCreated()`.

#### Datos de Prueba Iniciales

Al ejecutar la aplicación, se crean automáticamente:

**Usuario 1:**
- Username: `admin`
- Password: `admin123`

**Usuario 2:**
- Username: `doublevpartners`
- Password: `doublev123`

**Persona 1:**
- Nombre: Daniel Echeverri
- Identificación: CC-1088315344
- Email: echeverri121@gmail.com

### 🔒 Características de Seguridad

- **CORS habilitado**: Permite consumo desde cualquier origen (configurado para desarrollo)
- **Validaciones**: Data Annotations en modelos para validación de entrada
- **Columnas calculadas**: Generadas automáticamente por SQL Server
- **Stored Procedures**: Para consultas optimizadas

### 🛠️ Tecnologías y Paquetes NuGet

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.1" />
```

### 📊 Stored Procedures

#### sp_GetPersonasCreadas

Consulta optimizada para obtener todas las personas con columnas calculadas:

```sql
CREATE PROCEDURE sp_GetPersonasCreadas
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        FirstName,
        LastName,
        FullName,
        IdentificationNumber,
        IdentificationType,
        FullIdentificationNumber,
        Email,
        CreatedDate
    FROM Personas
    ORDER BY CreatedDate DESC;
END
```

### 🌐 Consumo desde Frontend

Ver el archivo `UI_VANILLA_JS_PROMPT.md` para instrucciones detalladas sobre cómo crear una interfaz en Vanilla JavaScript que consuma esta API.

### 🐛 Troubleshooting

#### Error: SQL Server LocalDB no encontrado
**Solución:** Instala SQL Server Express LocalDB desde [aquí](https://aka.ms/ssmsfullsetup)

#### Error: Puerto ya en uso
**Solución:** Modifica el puerto en `launchSettings.json`:
```json
"applicationUrl": "http://localhost:5000"
```

#### Error: Base de datos no se crea
**Solución:** Verifica que SQL Server LocalDB esté corriendo:
```bash
sqllocaldb info
sqllocaldb start mssqllocaldb
```

### 📝 Notas de Desarrollo

- **No usar migraciones**: El proyecto usa `EnsureCreated()` para crear la base de datos automáticamente
- **Columnas calculadas**: `FullName` y `FullIdentificationNumber` se generan automáticamente en SQL Server con `PERSISTED`
- **AutoMapper**: Ignora automáticamente las propiedades auto-generadas y calculadas
- **GETUTCDATE()**: Usado para establecer automáticamente la fecha de creación en UTC

### 👤 Autor

Daniel Echeverri
- Email: echeverri121@gmail.com
- GitHub: [@light-roast](https://github.com/light-roast)

### 📄 Licencia

Este proyecto es una prueba técnica para WTW.

---

## English

Person and user management system developed with .NET 10, Entity Framework Core, and SQL Server.

### 📋 Description

Complete REST API that allows managing persons and users, including:
- CRUD operations for persons with automatic computed columns
- User validation (login)
- Stored procedures for optimized queries
- Automatic mapping with AutoMapper
- SQL Server database with Entity Framework Core

### 🚀 Technologies Used

- **.NET 10** - Main framework
- **ASP.NET Core Web API** - REST API
- **Entity Framework Core 10.0.1** - ORM
- **SQL Server** - Database
- **AutoMapper 12.0.1** - Object mapping
- **CORS** - Enabled for frontend consumption

### 📁 Project Structure

```
TechnicalTestWTW/
├── Controllers/
│   ├── PersonasController.cs      # Personas endpoints
│   └── UsuariosController.cs      # Usuarios endpoints (login)
├── Data/
│   ├── ApplicationDbContext.cs           # EF Core context
│   └── StoredProcedureExtensions.cs      # SP extensions
├── Mappings/
│   └── PersonaMappingProfile.cs   # AutoMapper configuration
├── Models/
│   ├── Persona.cs                 # Persona entity
│   ├── PersonaDto.cs              # Persona DTO
│   ├── Usuario.cs                 # Usuario entity
│   └── UsuarioDto.cs              # Usuario DTO
├── Program.cs                     # Application configuration
└── appsettings.json              # Configuration (Connection Strings)
```

### 🗄️ Data Model

#### Entity: Personas

| Field | Type | Description |
|-------|------|-------------|
| `Id` | int | Unique identifier (auto-generated) |
| `FirstName` | string(100) | First name |
| `LastName` | string(100) | Last name |
| `IdentificationNumber` | string(50) | Identification number |
| `IdentificationType` | string(20) | Type (CC, TI, CE, Passport) |
| `Email` | string(100) | Email address |
| `CreatedDate` | DateTime | Creation date (auto-generated) |
| `FullName` | string | **Computed column**: FirstName + LastName |
| `FullIdentificationNumber` | string | **Computed column**: IdentificationNumber-IdentificationType |

#### Entity: Usuarios

| Field | Type | Description |
|-------|------|-------------|
| `Id` | int | Unique identifier (auto-generated) |
| `Username` | string(50) | Username |
| `Password` | string(255) | Password |
| `CreatedDate` | DateTime | Creation date (auto-generated) |

### 🔌 API Endpoints

#### Base URL
```
http://localhost:5241/api
```

#### Personas

##### List all persons
```http
GET /personas
```

**Response:**
```json
[
  {
    "id": 1,
    "firstName": "Daniel",
    "lastName": "Echeverri",
    "fullName": "Daniel Echeverri",
    "identificationNumber": "1088315344",
    "identificationType": "CC",
    "fullIdentificationNumber": "1088315344-CC",
    "email": "echeverri121@gmail.com",
    "createdDate": "2024-01-15T10:30:00Z"
  }
]
```

##### List persons using Stored Procedure
```http
GET /personas/created
```

##### Get person by ID
```http
GET /personas/{id}
```

##### Create new person
```http
POST /personas
Content-Type: application/json

{
  "firstName": "Juan",
  "lastName": "Pérez",
  "identificationNumber": "123456789",
  "identificationType": "CC",
  "email": "juan@example.com"
}
```

#### Usuarios

##### Validate user (Login)
```http
POST /usuarios/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

**Successful response (200 OK):**
```json
{
  "id": 1,
  "username": "admin",
  "password": "admin123",
  "createdDate": "2024-01-15T10:30:00Z"
}
```

**Error response (401 Unauthorized):**
```json
{
  "message": "Incorrect user or password"
}
```

### ⚙️ Configuration and Execution

#### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) or SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (optional)

#### Steps to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/light-roast/WTWTest
   cd TechnicalTestWTW
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Verify the connection string** in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TechnicalTestWTWDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```

4. **Run the application**
   ```bash
   dotnet run --project TechnicalTestWTW/TechnicalTestWTW.csproj
   ```

   The API will be available at: `http://localhost:5241`

5. **The database is created automatically** when running the application for the first time thanks to `EnsureCreated()`.

#### Initial Test Data

When running the application, the following are created automatically:

**User 1:**
- Username: `admin`
- Password: `admin123`

**User 2:**
- Username: `doublevpartners`
- Password: `doublev123`

**Person 1:**
- Name: Daniel Echeverri
- Identification: CC-1088315344
- Email: echeverri121@gmail.com

### 🔒 Security Features

- **CORS enabled**: Allows consumption from any origin (configured for development)
- **Validations**: Data Annotations in models for input validation
- **Computed columns**: Automatically generated by SQL Server
- **Stored Procedures**: For optimized queries

### 🛠️ Technologies and NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.1" />
```

### 📊 Stored Procedures

#### sp_GetPersonasCreadas

Optimized query to get all persons with computed columns:

```sql
CREATE PROCEDURE sp_GetPersonasCreadas
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        FirstName,
        LastName,
        FullName,
        IdentificationNumber,
        IdentificationType,
        FullIdentificationNumber,
        Email,
        CreatedDate
    FROM Personas
    ORDER BY CreatedDate DESC;
END
```

### 🌐 Frontend Consumption

See the `UI_VANILLA_JS_PROMPT.md` file for detailed instructions on how to create a Vanilla JavaScript interface that consumes this API.

### 🐛 Troubleshooting

#### Error: SQL Server LocalDB not found
**Solution:** Install SQL Server Express LocalDB from [here](https://aka.ms/ssmsfullsetup)

#### Error: Port already in use
**Solution:** Modify the port in `launchSettings.json`:
```json
"applicationUrl": "http://localhost:5000"
```

#### Error: Database is not created
**Solution:** Verify that SQL Server LocalDB is running:
```bash
sqllocaldb info
sqllocaldb start mssqllocaldb
```

### 📝 Development Notes

- **Do not use migrations**: The project uses `EnsureCreated()` to create the database automatically
- **Computed columns**: `FullName` and `FullIdentificationNumber` are automatically generated in SQL Server with `PERSISTED`
- **AutoMapper**: Automatically ignores auto-generated and computed properties
- **GETUTCDATE()**: Used to automatically set the creation date in UTC

### 👤 Author

Daniel Echeverri
- Email: echeverri121@gmail.com
- GitHub: [@light-roast](https://github.com/light-roast)

### 📄 License

This project is a technical test for WTW.

---

**Last updated:** January 2026
