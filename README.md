Proyecto: lab 1 POE U20200218

Descripción

Proyecto Windows Forms (.NET 10) para administrar fichas de empleado. Incluye:
- Formulario principal de administración (FrmAdminEmpleados).
- Formulario de ficha (FrmFichaEmpleado) para agregar/editar empleados.
- Capa de datos con Entity Framework Core y migraciones (# carpeta Migrations).

Requisitos

- Visual Studio 2022 o 2026 (recomendado) con carga de desarrollo .NET y herramientas de EF Core.
- .NET 10 SDK instalado.
- (Opcional) SQL Server LocalDB o instancia de SQL Server a la que apuntar la cadena de conexión.

Archivos relevantes

- FrmAdminEmpleados.cs / FrmAdminEmpleados.Designer.cs: gestión y vista de la tabla de empleados.
- FrmFichaEmpleado.cs / FrmFichaEmpleado.Designer.cs: formulario para ingresar datos de empleado.
- Models/Empleado.cs: entidad Empleado.
- Data/AppDbContext.cs: DbContext y configuración de EF Core.
- Data/EmpleadoService.cs: servicio para operaciones CRUD.
- Migrations/: migraciones EF Core (ya incluidas).
- appsettings.json: cadena de conexión y configuración.

Configuración de la cadena de conexión

1. Abrir appsettings.json en la raíz del proyecto.
2. Revisar la sección de ConnectionStrings y ajustar la cadena de conexión a su servidor/instancia. Ejemplo para LocalDB:

  "ConnectionStrings": {
	"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Lab1POE_U20200218;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

3. Guardar los cambios.

Aplicar migraciones usando la Consola de Administrador de Paquetes (Package Manager Console)

1. Abrir la solución en Visual Studio.
2. Menú: Tools (Herramientas) -> NuGet Package Manager -> Package Manager Console.
3. En la ventana de Package Manager Console, seleccionar el proyecto por defecto (Default project) en el selector superior: "lab 1 POE U20200218".

Si ya existen migraciones (están en la carpeta Migrations), ejecutar:

PM> Update-Database

Esto aplicará las migraciones existentes a la base de datos indicada por la cadena de conexión.

Si necesita crear una nueva migración después de cambios en el modelo, ejecutar primero:

PM> Add-Migration NombreDeLaMigracion
PM> Update-Database

Notas:
- Si aparece un error relacionado con la conexión, revise la cadena en appsettings.json y que el servidor esté accesible.
- Si Visual Studio no encuentra el comando Add-Migration/Update-Database, instale el paquete Microsoft.EntityFrameworkCore.Tools en el proyecto (Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution).

Alternativa con dotnet-ef (CLI)

1. Abrir un terminal (PowerShell) en la carpeta del proyecto (donde está el .csproj).
2. Instalar herramienta si es necesario: dotnet tool install --global dotnet-ef
3. Ejecutar:

> dotnet ef database update

Para crear una migración:

> dotnet ef migrations add NombreDeLaMigracion
> dotnet ef database update

Cómo ejecutar la aplicación

- Desde Visual Studio: presionar F5 (Debug) o Ctrl+F5 (Run without debugging).
- Desde terminal (solo si el proyecto de inicio está correctamente configurado):
  > dotnet run --project "./lab 1 POE U20200218.csproj"


