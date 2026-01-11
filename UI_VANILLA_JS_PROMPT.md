# Prompt para generar UI en Vanilla JavaScript para API .NET

Crea una aplicación web completa en **vanilla JavaScript, HTML y CSS** (sin frameworks) para consumir una API REST de .NET 10 que gestiona Personas y Usuarios.

**IMPORTANTE:** Debes crear los archivos paso a paso y hacer un **commit de Git después de cada paso completado**. Usa mensajes de commit descriptivos en inglés.

## Requisitos de la UI

### 1. **Estructura de archivos**
Crea los siguientes archivos:
- `index.html` - Página principal con navegación
- `styles.css` - Estilos para toda la aplicación
- `app.js` - Lógica principal de la aplicación
- `README.md` - Documentación

**?? COMMIT 1:** Después de crear la estructura básica de archivos
```bash
git commit -m "Add initial HTML structure and basic project setup"
```

### 2. **Funcionalidades requeridas**

#### **A. Pantalla Principal - Lista de Personas (Pantalla inicial)**
- Debe ser la primera pantalla que se muestre al cargar la aplicación
- **NO requiere login previo**
- Mostrar tabla con todas las personas registradas
- Usar **únicamente** el endpoint del stored procedure: `GET http://localhost:5000/api/personas/creadas`
- Respuesta del endpoint:
```json
{
  "id": 1,
  "firstName": "Juan",
  "lastName": "Pérez",
  "fullName": "Juan Pérez",
  "identificationNumber": "123456789",
  "identificationType": "CC",
  "fullIdentificationNumber": "123456789-CC",
  "email": "juan@example.com",
  "createdDate": "2024-01-15T10:30:00Z"
}
```

**Funcionalidad:**
- La tabla debe mostrar las columnas calculadas: `fullName` y `fullIdentificationNumber`
- La lista se carga automáticamente al iniciar la aplicación
- La tabla debe actualizarse automáticamente después de agregar una nueva persona

**?? COMMIT 2:** Después de implementar la tabla de personas con stored procedure
```bash
git commit -m "Add Personas table using stored procedure endpoint"
```

#### **B. Formulario de Personas (Debajo de la tabla)**
- Ubicado debajo de la tabla de personas
- Título: "Agregar Nueva Persona"
- Campos del formulario (todos requeridos):
  - `firstName` (texto, requerido, máximo 100 caracteres)
  - `lastName` (texto, requerido, máximo 100 caracteres)
  - `identificationNumber` (texto, requerido, máximo 50 caracteres)
  - `identificationType` (select/dropdown con opciones: "CC", "TI", "CE", "Pasaporte")
  - `email` (email, requerido, máximo 100 caracteres, validar formato email)

**NO incluir en el formulario:**
- `id` (se genera automáticamente)
- `createdDate` (se genera automáticamente)
- `fullName` (se calcula automáticamente)
- `fullIdentificationNumber` (se calcula automáticamente)

**Endpoint:**
- **Crear Persona:** `POST http://localhost:5000/api/personas`
```json
{
  "firstName": "Juan",
  "lastName": "Pérez",
  "identificationNumber": "123456789",
  "identificationType": "CC",
  "email": "juan@example.com"
}
```

**Funcionalidad:**
- Botón "Agregar Persona"
- Validar todos los campos antes de enviar
- Mostrar mensajes de éxito/error
- **Recargar automáticamente la tabla** después de crear una persona
- Limpiar el formulario después de agregar exitosamente

**?? COMMIT 3:** Después de implementar el formulario de personas
```bash
git commit -m "Add Personas form with validation and table auto-refresh"
```

#### **C. Formulario de Validación de Usuario (Debajo del formulario de personas)**
- Ubicado debajo del formulario de personas
- Título: "Validar Usuario"
- Campos del formulario:
  - `username` (texto, requerido, máximo 50 caracteres)
  - `password` (contraseña, requerido, máximo 255 caracteres)
- Botón "Validar Usuario"

**Endpoint:**
- **Validar Usuario:** `POST http://localhost:5000/api/usuarios/login`
- Request body:
```json
{
  "username": "admin",
  "password": "admin123"
}
```
- Respuesta exitosa (200 OK):
```json
{
  "id": 1,
  "username": "admin",
  "password": "admin123",
  "createdDate": "2024-01-15T10:30:00Z"
}
```
- Respuesta error (401 Unauthorized):
```json
{
  "message": "Incorrect user or password"
}
```

**Funcionalidad:**
- **NO debe redirigir a ninguna otra pantalla**
- Si las credenciales son correctas, mostrar mensaje: 
  - ? **"Usuario debidamente registrado y contraseña correcta"** (color verde)
- Si las credenciales son incorrectas, mostrar mensaje:
  - ? **"Usuario o contraseña incorrectos"** (color rojo)
- El mensaje debe aparecer debajo del formulario o como notificación toast
- Limpiar el formulario después de validar
- Mantener el mensaje visible por unos segundos

**?? COMMIT 4:** Después de implementar el formulario de validación de usuario
```bash
git commit -m "Add Usuario validation form with inline feedback messages"
```

### 3. **Requisitos técnicos**

#### **JavaScript:**
- Usar `fetch()` para todas las peticiones HTTP
- Usar `async/await` para manejo de promesas
- Validar formularios antes de enviar
- Manejo de errores con `try/catch`
- **NO usar** `sessionStorage` ni `localStorage` (no hay login/logout)
- Recargar la tabla de personas automáticamente después de agregar
- Función reutilizable para cargar personas desde el stored procedure

#### **HTML:**
- Estructura semántica (usar `<form>`, `<section>`, `<table>`, etc.)
- Atributos `required`, `maxlength`, `type="email"` para validación HTML5
- Accessibility: usar `<label>`, atributos `aria-*`, roles apropiados
- La tabla debe tener encabezados claros (`<thead>`)

#### **CSS:**
- Diseño responsive (mobile-first)
- Usar CSS Grid o Flexbox para layouts
- Estilos modernos y limpios
- Estados hover, focus, active en botones e inputs
- Indicadores visuales de errores/éxito (colores rojo/verde)
- Formularios bien espaciados
- Tablas responsive para mostrar datos
- Mensajes de validación destacados visualmente

**?? COMMIT 5:** Después de completar los estilos CSS
```bash
git commit -m "Add responsive CSS styles with modern design"
```

### 4. **Configuración de CORS**

? **La API ya tiene CORS configurado correctamente** en `Program.cs`:

```csharp
builder.Services.AddCors(options => 
{ 
    options.AddPolicy("AllowAll", policy => 
    { 
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader(); 
    }); 
});

// Y está habilitado con:
app.UseCors("AllowAll");
```

**No se requiere ninguna modificación adicional en la API.**

### 5. **Características adicionales deseables**
- Spinner o loading mientras se cargan datos
- Mensajes toast/notificaciones para feedback
- Limpiar formularios después de enviar
- Deshabilitar botones mientras se procesa una petición
- Formatear fechas en formato legible (dd/mm/yyyy hh:mm)
- Animaciones suaves al mostrar/ocultar mensajes

**?? COMMIT 6:** Después de agregar características adicionales
```bash
git commit -m "Add loading spinner, toast notifications, and UX improvements"
```

### 6. **Estructura sugerida del HTML**

```html
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Gestión de Personas y Usuarios</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <div class="container">
        <header>
            <h1>Sistema de Gestión de Personas</h1>
        </header>

        <!-- Sección 1: Tabla de Personas -->
        <section id="personas-list-section">
            <h2>Personas Registradas</h2>
            <div id="loading" style="display: none;">Cargando...</div>
            <table id="personas-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre Completo</th>
                        <th>Identificación Completa</th>
                        <th>Email</th>
                        <th>Fecha de Creación</th>
                    </tr>
                </thead>
                <tbody id="personas-tbody">
                    <!-- Datos se cargan dinámicamente -->
                </tbody>
            </table>
        </section>

        <!-- Sección 2: Formulario de Personas -->
        <section id="personas-form-section">
            <h2>Agregar Nueva Persona</h2>
            <form id="persona-form">
                <label for="firstName">Nombres:</label>
                <input type="text" id="firstName" name="firstName" required maxlength="100">

                <label for="lastName">Apellidos:</label>
                <input type="text" id="lastName" name="lastName" required maxlength="100">

                <label for="identificationNumber">Número de Identificación:</label>
                <input type="text" id="identificationNumber" name="identificationNumber" required maxlength="50">

                <label for="identificationType">Tipo de Identificación:</label>
                <select id="identificationType" name="identificationType" required>
                    <option value="">Seleccione...</option>
                    <option value="CC">CC</option>
                    <option value="TI">TI</option>
                    <option value="CE">CE</option>
                    <option value="Pasaporte">Pasaporte</option>
                </select>

                <label for="email">Email:</label>
                <input type="email" id="email" name="email" required maxlength="100">

                <button type="submit">Agregar Persona</button>
            </form>
            <div id="persona-message" class="message"></div>
        </section>

        <!-- Sección 3: Formulario de Validación de Usuario -->
        <section id="usuario-validation-section">
            <h2>Validar Usuario</h2>
            <form id="usuario-form">
                <label for="username">Usuario:</label>
                <input type="text" id="username" name="username" required maxlength="50">

                <label for="password">Contraseña:</label>
                <input type="password" id="password" name="password" required maxlength="255">

                <button type="submit">Validar Usuario</button>
            </form>
            <div id="usuario-message" class="message"></div>
        </section>
    </div>

    <script src="app.js"></script>
</body>
</html>
```

### 7. **Base URL de la API**
```javascript
const API_BASE_URL = 'http://localhost:5000/api';
```

### 8. **Datos de prueba**
Usuario predeterminado en la base de datos para validación:
- **Username:** `admin`
- **Password:** `admin123`

Persona predeterminada en la base de datos (se muestra en la tabla inicial):
- **Nombre:** Daniel Echeverri
- **Identificación:** CC-1088315344
- **Email:** echeverri121@gmail.com

### 9. **README.md**

Crear un archivo README.md con:
- Descripción del proyecto
- Requisitos previos (navegador moderno, API corriendo en localhost:5000)
- Instrucciones para ejecutar la aplicación
- Credenciales de prueba para validación de usuario
- Endpoints utilizados
- Estructura de archivos
- Funcionalidades implementadas

**?? COMMIT 7:** Después de completar la documentación
```bash
git commit -m "Add comprehensive README documentation"
```

---

## Estrategia de Commits

### Resumen de commits a realizar:

1. **COMMIT 1:** `"Add initial HTML structure and basic project setup"`
2. **COMMIT 2:** `"Add Personas table using stored procedure endpoint"`
3. **COMMIT 3:** `"Add Personas form with validation and table auto-refresh"`
4. **COMMIT 4:** `"Add Usuario validation form with inline feedback messages"`
5. **COMMIT 5:** `"Add responsive CSS styles with modern design"`
6. **COMMIT 6:** `"Add loading spinner, toast notifications, and UX improvements"`
7. **COMMIT 7:** `"Add comprehensive README documentation"`

### Buenas prácticas de commits:
- Cada commit debe ser una unidad funcional completa
- Los mensajes deben ser descriptivos y en inglés
- Usa formato imperativo ("Add", "Implement", no "Added", "Implemented")
- Si encuentras bugs mientras implementas, haz un commit adicional con formato: `"Fix [descripción del bug]"`

---

## Entregables esperados:

1. **`index.html`** - Estructura completa de la aplicación con 3 secciones
2. **`styles.css`** - Estilos responsivos y modernos
3. **`app.js`** - Lógica completa de la aplicación
4. **`README.md`** - Instrucciones de uso completas
5. **Historial de Git** - 7+ commits siguiendo la estrategia definida

---

## Instrucciones finales:

- Genera código limpio, bien comentado y siguiendo las mejores prácticas de JavaScript vanilla moderno (ES6+)
- Cada función debe tener un comentario explicando su propósito
- Variables y funciones deben tener nombres descriptivos en inglés
- Implementa manejo de errores robusto en todas las peticiones API
- La aplicación debe funcionar correctamente incluso si la API no está disponible (mostrar mensaje de error apropiado)
- **La tabla de personas debe cargar automáticamente al iniciar la aplicación**
- **El formulario de validación de usuario solo muestra mensajes, NO redirige**

**¡Comienza con el COMMIT 1 y avanza paso a paso!**
