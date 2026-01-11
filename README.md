# Technical Test WTW - .NET 10 Web API

Sistema de gestión de personas y usuarios desarrollado con .NET 10, Entity Framework Core y SQL Server.

## 📋 Descripción

API REST completa que permite gestionar personas y usuarios, incluyendo:
- CRUD de personas con columnas calculadas automáticas
- Validación de usuarios (login)
- Stored procedures para consultas optimizadas
- Mapeo automático con AutoMapper
- Base de datos SQL Server con Entity Framework Core

## 🚀 Tecnologías Utilizadas

- **.NET 10** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core 10.0.1** - ORM
- **SQL Server** - Base de datos
- **AutoMapper 12.0.1** - Mapeo de objetos
- **CORS** - Habilitado para consumo desde frontend

## 📁 Estructura del Proyecto

```
TechnicalTestWTW/
├── Controllers/
│   ├── PersonasController.cs      # Endpoints de Personas
│   └── UsuariosController.cs      # Endpoints de Usuarios (login)
├── Data/
│   ├── ApplicationDbContext.cs           # Contexto de EF Core
│   └── StoredProcedureExtensions.cs      # Extensiones para SP
├── Mappings/
│   └── MappingProfile.cs          # Configuración de AutoMapper
├── Models/
│   ├── Persona.cs                 # Entidad Persona
│   ├── PersonaDto.cs              # DTO de Persona
│   ├── Usuario.cs                 # Entidad Usuario
│   └── UsuarioDto.cs              # DTO de Usuario
├── Program.cs                     # Configuración de la aplicación
└── appsettings.json              # Configuración (Connection Strings)
```

## 🗄️ Modelo de Datos

### Entidad: Personas

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

### Entidad: Usuarios

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Id` | int | Identificador único (auto-generado) |
| `Username` | string(50) | Nombre de usuario |
| `Password` | string(255) | Contraseña |
| `CreatedDate` | DateTime | Fecha de creación (auto-generada) |

## 🔌 Endpoints de la API

### Base URL
```
http://localhost:5241/api
```

### Personas

#### Listar todas las personas
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

#### Listar personas usando Stored Procedure
```http
GET /personas/creadas
```

#### Obtener persona por ID
```http
GET /personas/{id}
```

#### Crear nueva persona
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

### Usuarios

#### Validar usuario (Login)
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

## ⚙️ Configuración y Ejecución

### Requisitos Previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) o SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (opcional)

### Pasos para Ejecutar

1. **Clonar el repositorio**
   ```bash
   git clone <repository-url>
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

### Datos de Prueba Iniciales

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

## 🔒 Características de Seguridad

- **CORS habilitado**: Permite consumo desde cualquier origen (configurado para desarrollo)
- **Validaciones**: Data Annotations en modelos para validación de entrada
- **Columnas calculadas**: Generadas automáticamente por SQL Server
- **Stored Procedures**: Para consultas optimizadas

## 🛠️ Tecnologías y Paquetes NuGet

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
```

## 📊 Stored Procedures

### sp_GetPersonasCreadas

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

## 🌐 Consumo desde Frontend

Ver el archivo `UI_VANILLA_JS_PROMPT.md` para instrucciones detalladas sobre cómo crear una interfaz en Vanilla JavaScript que consuma esta API.

## 🐛 Troubleshooting

### Error: SQL Server LocalDB no encontrado
**Solución:** Instala SQL Server Express LocalDB desde [aquí](https://aka.ms/ssmsfullsetup)

### Error: Puerto ya en uso
**Solución:** Modifica el puerto en `launchSettings.json`:
```json
"applicationUrl": "http://localhost:5000"
```

### Error: Base de datos no se crea
**Solución:** Verifica que SQL Server LocalDB esté corriendo:
```bash
sqllocaldb info
sqllocaldb start mssqllocaldb
```

## 📝 Notas de Desarrollo

- **No usar migraciones**: El proyecto usa `EnsureCreated()` para crear la base de datos automáticamente
- **Columnas calculadas**: `FullName` y `FullIdentificationNumber` se generan automáticamente en SQL Server
- **AutoMapper**: Ignora automáticamente las propiedades auto-generadas y calculadas


## 👤 Autor

Daniel Echeverri
- Email: echeverri121@gmail.com

## 📄 Licencia

Este proyecto es una prueba técnica para WTW.

---

**Última actualización:** Enero 2026
