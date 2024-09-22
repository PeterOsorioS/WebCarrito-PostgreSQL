# WebCarrito

**WebCarrito** es una aplicación web para la gestión de un carrito de compras, desarrollada en C# con ASP.NET (Entity Framework) y jQuery. Permite la administración de productos, categorías y órdenes, integrando un sistema de autenticación por cookies.

## Características
- Gestión de productos, marcas, categorías, sliders y usuarios.
- Carrito de compras dinámico (implementacion del localstorage).
- Implementación de capas: Datos, Servicio y Entidad.
- Uso de jQuery para la manipulación del DOM.
- Autenticación por cookies.
- Autorización
- La aplicacion se puede ejecutar con docker.

## Tecnologías utilizadas
- **Lenguajes**: C#, HTML, CSS, jQuery.
- **Framework**: ASP.NET, Entity Framework ORM
- **Base de datos**: PostgreSQL.
- **Bibliotecas adicionales**:
  - [Bootstrap](https://getbootstrap.com/): Para el diseño responsive.
  - [Toastr](https://github.com/CodeSeven/toastr): Notificaciones.
  - [SweetAlert2](https://sweetalert2.github.io/): Alertas elegantes.
  - [Font Awesome](https://fontawesome.com/): Iconos.
  - [jQuery LoadingOverlay](https://gasparesganga.com/labs/jquery-loading-overlay/): Indicador de carga.
  - [TinyMCE](https://www.tiny.cloud/): Editor de texto enriquecido.
  - [AOS (Animate on Scroll)](https://michalsnik.github.io/aos/): Animaciones en desplazamiento.
  - [DataTables](https://datatables.net/): Tablas dinámicas con Bootstrap 5.

## Requisitos Previos
Tener instalados los siguientes componentes antes de comenzar:
- **Visual Studio:** Para el desarrollo de la aplicación.
- **PostgreSQL:** (Opcional) Si prefieres gestionar la base de datos localmente.
- **Docker y Docker Compose:** (Opcional) Si prefieres usar contenedores, puedes ejecutar tanto la aplicación como la base de datos en Docker. En este caso, no es necesario instalar PostgreSQL localmente, ya que Docker puede levantar los servicios automáticamente.


## Instalación sin Docker
1. Clona el repositorio:
   ```bash
   git clone https://github.com/PeterOsorioS/WebCarrito-PostgreSQL.git
2. Abre el proyecto en Visual Studio.
3. Establece ShopMaster como proyecto de inicio.
4. Restaura los paquetes NuGet.
   - Abre una terminal en la carpeta raíz de tu proyecto.
   - Ejecuta el siguiente comando:

   ```bash
   dotnet restore 
5. Configura la cadena de conexión en el archivo de configuración.
   - Modificación del archivo `appsettings.json`
   - El archivo `appsettings.json` es donde se almacenan las configuraciones de la aplicación, incluida la cadena de conexión, modifica el archivo poniendo tus credenciales como se muestra a continuación:
    ```json
    {
      "ConnectionStrings": {
        "ConexionSQL": "Host={NombreHost}; ;Port={NumeroDelPuerto}; Database={NombreBaseDeDatos};User Id={Usuario};Password={Contraseña};Pooling=true;SSL Mode=Disable"
      }
    }
6. La creacion de la base de datos y las tablas se crean automaticamente con la primera ejecucion del codigo.
## Instalación con Docker
1. Clona el repositorio:
   ```bash
   git clone https://github.com/PeterOsorioS/WebCarrito-PostgreSQL.git
2. Abre la terminal en la carpeta raiz.
3. Ejecuta el siguiente comando:
   ```bash
   docker-compose up
4. Accede a la aplicación:

   - Pon a correr el contenedor docker
   - Recordar tener el servicio de Postgres y la aplicacion corriendo
   - Una vez que el contenedor esté corriendo, la aplicación estara disponible en http://localhost:8080.
## Uso
- Inicia la aplicación.
- Ingresa al panel de administracion con el corre: administrador@gmail.com  y la contraseña: Admin@1234
- Navega por las diferentes secciones como categorias, marcas, productos, slider y empieza a crear.
