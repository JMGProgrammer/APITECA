# Actividad-BibliotecaAPI

[![GitHub](https://img.shields.io/badge/GitHub-Repository-blue?logo=github)](https://github.com/JMGProgrammer/API-Web-Bibliteca-Test)
[![.NET](https://img.shields.io/badge/.NET-10-purple)](https://dotnet.microsoft.com)
[![MySQL](https://img.shields.io/badge/MySQL-Database-orange)](https://www.mysql.com)
[![Railway](https://img.shields.io/badge/Railway-Deploy-green)](https://railway.app)

API Web para la gestión de una biblioteca personal, desarrollada con **ASP.NET Core 10** y **Entity Framework Core** usando **MySQL** como motor de base de datos.

Permite administrar una colección de libros: agregar, modificar, eliminar, buscar, y obtener estadísticas y recomendaciones basadas en la cantidad de páginas de cada libro.

---

## Enlaces

- **API en producción:** [https://apiteca.up.railway.app/](https://apiteca.up.railway.app/)
- **Documentación HTML:** [https://apiteca.up.railway.app/](https://apiteca.up.railway.app/)
- **Repositorio GitHub:** [https://github.com/JMGProgrammer/APITECA](https://github.com/JMGProgrammer/APITECA)

---

## Modelo de Datos - Libro

| Campo    | Tipo    | Descripción                        |
|----------|---------|------------------------------------|
| `Id`     | int     | Identificador único (autoincremental) |
| `Titulo` | string  | Título del libro                   |
| `Autor`  | string  | Nombre del autor                   |
| `Paginas`| int     | Cantidad de páginas                |
| `Leido`  | boolean | Indica si el libro ha sido leído   |

---

## Manual de Usuario - Endpoints

**Base URL (Producción):** `https://apiteca.up.railway.app`

Todas las rutas están bajo el prefijo: `/api/libros`

**Ejemplo completo:** `https://apiteca.up.railway.app/api/libros`

---

### 1. Obtener todos los libros

```
GET /api/libros
```

**Descripción:** Retorna la lista completa de libros almacenados en la base de datos.

**Respuesta (200):**
```json
[
  {
    "id": 1,
    "titulo": "El Hobbit",
    "autor": "J.R.R. Tolkien",
    "paginas": 310,
    "leido": true
  }
]
```

---

### 2. Obtener un libro por ID

```
GET /api/libros/{id}
```

**Parámetros de ruta:**
- `id` (int) - Identificador del libro.

**Descripción:** Busca y retorna un libro específico por su ID.

**Respuesta (200):**
```json
{
  "id": 1,
  "titulo": "El Hobbit",
  "autor": "J.R.R. Tolkien",
  "paginas": 310,
  "leido": true
}
```

**Respuesta (404):** `"Libro no encontrado"`

---

### 3. Obtener el tipo de libro (por páginas)

```
GET /api/libros/{id}/tipo
```

**Parámetros de ruta:**
- `id` (int) - Identificador del libro.

**Descripción:** Clasifica un libro según su cantidad de páginas:

| Rango de páginas | Clasificación |
|------------------|---------------|
| Menos de 100     | Libro Corto   |
| 100 - 300        | Libro Mediano |
| Más de 300       | Libro Largo   |

**Respuesta (200):**
```json
{
  "tipo": "Libro Mediano"
}
```

**Respuesta (404):** `"Libro no encontrado"`

---

### 4. Calcular Fibonacci según páginas

```
GET /api/libros/{id}/fibonacci
```

**Parámetros de ruta:**
- `id` (int) - Identificador del libro.

**Descripción:** Calcula el número de Fibonacci correspondiente al número de páginas del libro.

**Respuesta (200):**
```json
{
  "titulo": "El Hobbit",
  "paginas": 310,
  "numero": 310,
  "fibonacci": 167769361818756559872424281766416725811394149890590732499100663797129936
}
```

**Respuesta (404):** `"Libro no encontrado"`

---

### 5. Buscar libros por autor

```
GET /api/libros/autor/{autor}
```

**Parámetros de ruta:**
- `autor` (string) - Nombre del autor (no distingue mayúsculas/minúsculas).

**Descripción:** Retorna todos los libros escritos por el autor indicado.

**Respuesta (200):**
```json
[
  {
    "id": 1,
    "titulo": "El Hobbit",
    "autor": "J.R.R. Tolkien",
    "paginas": 310,
    "leido": true
  }
]
```

**Respuesta (404):** `"No se encontraron libros para el autor especificado"`

---

### 6. Resumen de la biblioteca

```
GET /api/libros/resumen
```

**Descripción:** Retorna estadísticas generales de la biblioteca: total de libros, leídos, no leídos y promedio de páginas.

**Respuesta (200):**
```json
{
  "totalLibros": 10,
  "librosLeidos": 6,
  "librosNoLeidos": 4,
  "promedioPaginas": 245.5
}
```

---

### 7. Agregar un nuevo libro

```
POST /api/libros
```

**Body (application/json):**
```json
{
  "titulo": "El Hobbit",
  "autor": "J.R.R. Tolkien",
  "paginas": 310,
  "leido": true
}
```

**Descripción:** Agrega un nuevo libro a la base de datos. El campo `Id` se genera automáticamente.

**Respuesta (200):**
```json
{
  "id": 1,
  "titulo": "El Hobbit",
  "autor": "J.R.R. Tolkien",
  "paginas": 310,
  "leido": true
}
```

**Respuesta (400):** `"Libro no válido"`

---

### 8. Agregar un libro con recomendación

```
POST /api/libros/con-recomendacion
```

**Body (application/json):**
```json
{
  "titulo": "El Hobbit",
  "autor": "J.R.R. Tolkien",
  "paginas": 310,
  "leido": true
}
```

**Descripción:** Agrega un libro y retorna una recomendación de lectura según la cantidad de páginas:

| Páginas    | Recomendación              |
|------------|----------------------------|
| < 100      | Ideal para leer en un día  |
| 100 - 300  | Lectura normal             |
| > 300      | Lectura larga              |

**Respuesta (200):**
```json
{
  "libro": {
    "id": 1,
    "titulo": "El Hobbit",
    "autor": "J.R.R. Tolkien",
    "paginas": 310,
    "leido": true
  },
  "recomendacion": "Lectura larga"
}
```

**Respuesta (400):** `"Libro no válido"`

---

### 9. Modificar un libro existente

```
PUT /api/libros/{id}
```

**Parámetros de ruta:**
- `id` (int) - Identificador del libro a modificar.

**Body (application/json):**
```json
{
  "titulo": "El Hobbit (Edición Actualizada)",
  "autor": "J.R.R. Tolkien",
  "paginas": 320,
  "leido": true
}
```

**Descripción:** Actualiza todos los campos de un libro existente.

**Respuesta (200):**
```json
{
  "id": 1,
  "titulo": "El Hobbit (Edición Actualizada)",
  "autor": "J.R.R. Tolkien",
  "paginas": 320,
  "leido": true
}
```

**Respuesta (404):** `"Libro no encontrado"`

---

### 10. Marcar un libro como leído

```
PUT /api/libros/{id}/marcar-leido
```

**Parámetros de ruta:**
- `id` (int) - Identificador del libro.

**Descripción:** Marca el campo `Leido` del libro como `true`. No requiere body.

**Respuesta (200):** `"El libro ha sido marcado como leído."`

**Respuesta (404):** `"Libro no encontrado"`

---

### 11. Eliminar un libro

```
DELETE /api/libros/{id}
```

**Parámetros de ruta:**
- `id` (int) - Identificador del libro a eliminar.

**Descripción:** Elimina permanentemente un libro de la base de datos.

**Respuesta (200):**
```json
{
  "id": 1,
  "titulo": "El Hobbit",
  "autor": "J.R.R. Tolkien",
  "paginas": 310,
  "leido": true
}
```

**Respuesta (404):** `"Libro no encontrado"`

---

## Tecnologías Utilizadas

- **ASP.NET Core 10** - Framework web
- **Entity Framework Core** - ORM para acceso a datos
- **MySQL** - Motor de base de datos
- **OpenAPI** - Documentación automática (disponible en modo Development)

---

## Deploy en Railway

> **Repositorio público:** [https://github.com/JMGProgrammer/API-Web-Bibliteca-Test](https://github.com/JMGProgrammer/API-Web-Bibliteca-Test)

### Prerrequisitos
- Cuenta en [Railway](https://railway.app)
- Cuenta en GitHub
- Git instalado

### Paso 1: Crear repositorio en GitHub
1. Ve a GitHub y crea un repositorio nuevo (público o privado).
2. Sube todo el proyecto:

```bash
cd Actividad-BibliotecaAPI
git init
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/TU_USUARIO/TU_REPOSITORIO.git
git push -u origin main
```

### Paso 2: Crear proyecto en Railway
1. Ve a [railway.app](https://railway.app) e inicia sesión con GitHub.
2. Haz clic en **"New Project"**.
3. Selecciona **"Deploy from GitHub repo"**.
4. Selecciona tu repositorio.
5. Railway detectará automáticamente que es un proyecto .NET.

### Paso 3: Agregar base de datos MySQL
1. En tu proyecto de Railway, haz clic en **"+ New"**.
2. Selecciona **"Database"** > **"MySQL"**.
3. Railway creará una base de datos MySQL automáticamente.

### Paso 4: Configurar variables de entorno
1. Ve a la pestaña **"Variables"** del servicio de tu API.
2. Railway ya habrá creado una variable `MYSQL_URL` desde la base de datos.
3. Necesitas crear la variable de conexión. Copia el valor de `MYSQL_URL` y créala como variable personalizada:

```
ConnectionStrings__connectionString=Server=HOST;Port=PORT;Database=DATABASE;User=USER;Password=PASSWORD;
```

**IMPORTANTE:** Copia el formato exacto desde la variable `MYSQL_URL` que genera Railway y convertirlo al formato ADO.NET que usa Entity Framework.

O simplemente usa esta variable si Railway te da los datos por separado:
```
ConnectionStrings__connectionString=Server=mysql.railway.internal;Port=3306;Database=railway;User=root;Password=TU_PASSWORD;
```

### Paso 5: Configurar variables adicionales
Agrega estas variables en la pestaña **"Variables"**:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:{PORT}
```

Railway asigna un puerto dinámico, la variable `PORT` se inyecta automáticamente.

### Paso 6: Deploy
1. Railway desplegará automáticamente cada vez que hagas push al repositorio.
2. Ve a la pestaña **"Deployments"** para ver el progreso.
3. Cuando termine, haz clic en **"Settings"** > **"Networking"** y genera un dominio público con **"Generate Domain"**.

### Paso 7: Crear las tablas en MySQL
Una vez desplegado, necesitas que Entity Framework cree las tablas. Agrega temporalmente este código en `Program.cs` después de `var app = builder.Build();`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
```

O si no tienes migraciones, usa:
```csharp
db.Database.EnsureCreated();
```

Haz push de ese cambio, espera a que se despliegue, y luego puedes quitar esas líneas.

### Paso 8: Verificar
1. Abre tu dominio generado: `https://api-web-bibliteca-test-production.up.railway.app`
2. Verás la documentación HTML de la API.
3. Prueba los endpoints con la URL: `https://api-web-bibliteca-test-production.up.railway.app/api/libros`

### Resumen de variables de entorno

| Variable | Valor |
|----------|-------|
| `ConnectionStrings__connectionString` | Cadena de conexión MySQL de Railway |
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ASPNETCORE_URLS` | `http://+:{PORT}` |

### Estructura del proyecto para Railway

```
Actividad-BibliotecaAPI/
├── Controllers/
│   └── LibroController.cs
├── Data/
│   └── AppDbContext.cs
├── Migrations/
├── Models/
│   └── Libro.cs
├── Properties/
├── wwwroot/
│   └── index.html          ← Página de documentación
├── Program.cs
├── Dockerfile               ← Para Railway
├── .gitignore
└── Actividad-BibliotecaAPI.csproj
```
