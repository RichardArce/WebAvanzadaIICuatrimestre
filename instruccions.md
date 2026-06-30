# Buenas pr�cticas (resumen)

Este documento recoge recomendaciones pr�cticas para trabajar con este proyecto ASP.NET Core + EF Core (SQLite).

- Configuraci�n y secretos
  - Mantener la cadena de conexi�n fuera del c�digo fuente. Usar `appsettings.Development.json` y/o _user secrets_ (`dotnet user-secrets`) para entorno local.
  - En producci�n usar variables de entorno o un servicio de secretos (por ejemplo Azure Key Vault).

- Entity Framework Core
  - Registrar el `DbContext` v�a DI(Inyecci�n de dependencias) (ya est� en `Program.cs`).
  - No hardcodear la cadena en `OnConfiguring`; permitir que DI la inyecte.
  - Usar migraciones (`dotnet ef migrations add <Nombre>` / `dotnet ef database update`) para evolucionar el esquema. Para SQLite, revisar limitaciones de alteraciones.
  - Mantener las entidades en la carpeta `Entidades` y el contexto en `Data`.
  - Utilizar IQueryable para hacer las consultas a la base de datos
  - Utilizar Astracking cuando sea necesario en las consultas


- C�digo y estilo
  - Habilitar `nullable` (ya est� en el proyecto). Manejar referencias nulas expl�citamente y usar tipos anulables cuando corresponda.
  - Preferir m�todos `async` para acceso a datos (`ToListAsync`, `SaveChangesAsync`).
  - Seguir convenciones PascalCase para clases y propiedades.

- Control de versiones y PRs
  - Hacer commits peque�os y con mensajes claros. Abrir PRs para cambios significativos y pedir revisi�n.
  - A�adir un `.gitignore` apropiado y no commitear binarios, secretos ni bases de datos locales.

- Comandos �tiles
  - Paquetes EF Core:
    - `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`
    - `dotnet add package Microsoft.EntityFrameworkCore.Design`
  - Herramienta CLI: `dotnet tool install --global dotnet-ef`
  - Scaffold desde SQLite (genera entidades y contexto):
    - `dotnet ef dbcontext scaffold "Data Source=C:\\ruta\\a\\tu.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Entidades --context ApplicationDbContext --context-dir Data --force`
  - Migraciones:
    - `dotnet ef migrations add InitialCreate`
    - `dotnet ef database update`

- Otras recomendaciones
  - Documentar c�mo ejecutar el proyecto en desarrollo (`dotnet restore`, `dotnet build`, `dotnet run`).
  - Mantener dependencias actualizadas y planear actualizaciones mayores con pruebas.

Si quieres, puedo a�adir ejemplos concretos de `appsettings.json`, plantillas de pruebas o eliminar la clase placeholder `Ejemplo.cs` generada por el scaffold.
