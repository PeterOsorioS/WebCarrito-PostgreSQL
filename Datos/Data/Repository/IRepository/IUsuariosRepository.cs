using Entidad;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuariosRepository : IRepository<Usuario>
    {
        void Update(Usuario usuario);
        Task<Usuario> BuscarUsuario(string Correo, string clave);
        Task<Usuario> VerificarCorreo(string Correo);
        string ConvertirSha256(string Texto);
    }
}
