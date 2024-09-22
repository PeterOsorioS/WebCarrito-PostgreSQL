using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuariosRepository
    {
        ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Usuario usuario)
        {
            var objDesdeDb = _db.Usuarios.FirstOrDefault(s => s.IdUsuario == usuario.IdUsuario);
            if (objDesdeDb != null)
            {
                objDesdeDb.Nombres = usuario.Nombres;
                objDesdeDb.Apellidos = usuario.Apellidos;
                objDesdeDb.Correo = usuario.Correo;
                objDesdeDb.Activo = usuario.Activo;
                objDesdeDb.IdRol = usuario.IdRol;
                //_db.SaveChanges();  Asegúrate de que esta línea esté descomentada
            }
            else
            {
                // Manejo del caso en que el usuario no se encuentra
                throw new Exception("Usuario no encontrado");
            }
        }
        public async Task<Usuario> BuscarUsuario(string Correo, string clave)
        {
            return await _db.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == Correo && u.Clave == clave);
        }
        public async Task<Usuario> VerificarCorreo(string Correo)
        {
            return await _db.Usuarios.Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == Correo);
        }
        public string ConvertirSha256(string Texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] resultado = hash.ComputeHash(Encoding.UTF8.GetBytes(Texto));
                foreach (byte b in resultado)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
