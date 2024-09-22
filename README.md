# WebCarrito

**WebCarrito** es una aplicación web para la gestión de un carrito de compras, desarrollada en C# con ASP.NET (Entity Framework) y jQuery. Permite la administración de productos, categorías y órdenes, integrando un sistema de autenticación por cookies.

## Características
- Gestión de productos, marcas, categorías, sliders y usuarios.
- Carrito de compras dinámico.
- Implementación de capas: Datos, Servicio y Entidad.
- Uso de jQuery para la manipulación del DOM.
- Autenticación y autorización por cookies.

## Tecnologías utilizadas
- **Lenguajes**: C#, HTML, CSS, jQuery.
- **Framework**: ASP.NET, Entity Framework ORM
- **Base de datos**: SQL Server.
- **Bibliotecas adicionales**:
  - [Bootstrap](https://getbootstrap.com/): Para el diseño responsive.
  - [Toastr](https://github.com/CodeSeven/toastr): Notificaciones.
  - [SweetAlert2](https://sweetalert2.github.io/): Alertas elegantes.
  - [Font Awesome](https://fontawesome.com/): Iconos.
  - [jQuery LoadingOverlay](https://gasparesganga.com/labs/jquery-loading-overlay/): Indicador de carga.
  - [TinyMCE](https://www.tiny.cloud/): Editor de texto enriquecido.
  - [AOS (Animate on Scroll)](https://michalsnik.github.io/aos/): Animaciones en desplazamiento.
  - [DataTables](https://datatables.net/): Tablas dinámicas con Bootstrap 5.

## Instalación
1. Clona el repositorio:
   ```bash
   git clone https://github.com/PeterOsorioS/WebCarrito.git
2. Abre el proyecto en Visual Studio.
3. Establece ShopMaster como proyecto de inicio.
4. Restaura los paquetes NuGet.
   - Abre una terminal en la carpeta raíz de tu proyecto.
   - Ejecuta el siguiente comando:

   ```bash
   dotnet restore 
5. Configura la cadena de conexión en el archivo de configuración.
   - 1. Modificación del archivo `appsettings.json`

    El archivo `appsettings.json` es donde se almacenan las configuraciones de la aplicación, incluida la cadena de conexión, modifica el archivo como se muestra a continuación:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server={NombreHost};Database={NombreBaseDeDatos};User Id={Usuario};Password={Contraseña};TrustServerCertificate=True;"
      }
    }

6. Ejecuta las migraciones para crear la base de datos.
   - Ve a Herramientas → Administrador de paquetes NuGet → Consola del Administrador de paquetes.
   - Ejecuta el siguiente comando:
   ```bash
   Add-migration BaseDatos
   Update-database

## Uso
- Inicia la aplicación desde Visual Studio.
- Ingresa al panel de administracion con el corre: administrador@gmail.com  y la contraseña: Admin@1234
- Navega por las diferentes secciones como cliente, productos y pedidos.
