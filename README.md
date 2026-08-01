# HotelZormat — Sistema de Gestión Hotelera

**Estudiante:** Jordan Alexander Guzman Cedano
**Matrícula:** 2024-3553
**Asignatura:** ISW-123 · Programación Media · 6to Semestre
**Profesor:** Ing. Ivan Zorrilla
**Práctica Final · 15 puntos**

Descripción

Aplicación de escritorio para la gestión operativa de un hotel boutique (HotelZormat), desarrollada en C# / .NET Framework / Windows Forms, con SQL Server como motor de base de datos y el patrón Repository para el acceso a datos.

Arquitectura

El proyecto está organizado en 4 capas:

- **HotelZormat.UI** — Formularios Windows Forms (interfaz de usuario).
- **HotelZormat.Negocio** — Servicios con la lógica y reglas de negocio (validaciones, cálculos, excepciones).
- **HotelZormat.Datos** — Repositorios con el acceso a SQL Server (SqlConnection, SqlCommand, SqlDataReader).
- **HotelZormat.Modelo** — Clases de dominio (Habitacion, Huesped, Usuario, Estadia, Factura).

Requisitos previos

- Visual Studio 2019 o superior
- SQL Server Express (o superior) instalado localmente
- .NET Framework 4.7.2

Instrucciones de configuración

1. Restaurar la base de datos

1. Abre **SQL Server Management Studio (SSMS)**.
2. Abre el archivo `script_bd.sql` incluido en la raíz de este repositorio.
3. Ejecuta el script completo (F5). Esto crea la base de datos `HotelZormatDB`, todas las tablas (Usuario, Habitacion, Huesped, Estadia, Factura, Bitacora) y los datos iniciales de prueba.

2. Configurar la cadena de conexión

El connection string se encuentra en:
```
HotelZormat.UI/App.config
```
dentro de la sección `<connectionStrings>`, con el nombre `HotelZormatDB`. Por defecto apunta a una instancia local:
```xml
<connectionStrings>
  <add name="HotelZormatDB"
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HotelZormatDB;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
Si tu instancia de SQL Server tiene otro nombre (por ejemplo `.` en vez de `.\SQLEXPRESS`), ajusta el valor de `Data Source` en ese archivo antes de correr el proyecto.

3. Ejecutar la aplicación

1. Abre `HotelZormat.sln` en Visual Studio.
2. Verifica que `HotelZormat.UI` esté marcado como proyecto de inicio (Set as Startup Project).
3. Presiona F5.

Usuarios de prueba (creados por el script SQL)

| Usuario | Contraseña | Rol |
|---|---|---|
| admin | admin123 | Administrador |
| recep1 | recep123 | Recepcionista |

Funcionalidades implementadas

- **Login y roles**: autenticación contra la tabla `Usuario`, con diferenciación entre Administrador y Recepcionista.
- **CRUD Habitaciones**: listar, crear, actualizar y eliminar habitaciones, con tablero visual por colores según estado.
- **CRUD Huéspedes**: listar, crear, actualizar y eliminar huéspedes, con validación de cédula (11 dígitos) y soporte para pasaporte.
- **Registro de estadía**: Check In / Check Out de huéspedes, con generación automática de factura (subtotal + ITBIS 18% + propina 10%) según la temporada seleccionada.
- **Manejo de excepciones**: try/catch específicos por tipo (FormatException, SqlException, Exception) en cada operación crítica, más una excepción personalizada del negocio (`HabitacionOcupadaException`).
- **Seguridad**: todas las consultas SQL usan parámetros (`@nombre` con `AddWithValue`), sin concatenación de strings.

Estructura de archivos relevantes

```
HotelZormat/
├── script_bd.sql                  ← Script de creación de la base de datos
├── HotelZormat.sln
├── HotelZormat.UI/                ← Formularios (frmLogin, FrmPrincipal, frmHabitacion, frmHuesped)
├── HotelZormat.Negocio/           ← Servicios de negocio
├── HotelZormat.Datos/             ← Repositorios de acceso a datos
└── HotelZormat.Modelo/            ← Clases de dominio
```
