using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Data.Inicializador
{
    public class InicializadorDb : IInicializadorDb
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _db;

        public InicializadorDb(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext db)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _db = db;
        }
        public async Task Inicializacion()
        {
            try
            {
                await _db.Database.EnsureCreatedAsync();
            }
            catch (Exception)
            {
            }

            if (!_db.Roles.Any())
            {
                await _db.Roles.AddRangeAsync(
                 new Rol { Descripcion = "Administrador" },
                 new Rol { Descripcion = "Cliente" },
                 new Rol { Descripcion = "Soporte" }
             );

                await _db.SaveChangesAsync();
            }
            if (!(_db.Municipio.Any() && _db.Departamentos.Any()))
            {
                await _db.Departamentos.AddRangeAsync(
                    new Departamento { IdDepartamento = "1", Descripcion = "Antioquia" },
                    new Departamento { IdDepartamento = "2", Descripcion = "Cundinamarca" },
                    new Departamento { IdDepartamento = "3", Descripcion = "Valle del Cauca" }
                    );
                await _db.Municipio.AddRangeAsync(
              new Municipio { Descripcion = "Medellín", IdDepartamento = "1" },
              new Municipio { Descripcion = "Bogotá ", IdDepartamento = "2" },
              new Municipio { Descripcion = "Cali", IdDepartamento = "3" }
              );
            }

            if (!_db.Usuarios.Any(u => u.Correo == "administrador@gmail.com"))
            {
                var adminRole = _db.Roles.FirstOrDefault(r => r.Descripcion == "Administrador");

                if (adminRole != null)
                {
                    // Crea el usuario administrador
                    var adminUser = new Usuario
                    {
                        Nombres = "Admin",
                        Apellidos = "User",
                        Correo = "administrador@gmail.com",
                        Activo = true,
                        Clave = _contenedorTrabajo.Usuario.ConvertirSha256("Admin@1234"),
                        IdRol = adminRole.IdRol
                    };
                    await _db.Usuarios.AddAsync(adminUser);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}