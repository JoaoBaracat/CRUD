using CRUD.Net.Domain.Entities;

namespace CRUD.Net.Domain.Apps
{
    public interface IUsuarioApp : IApp<Usuario>
    {
        Usuario Authenticate(string login, string password);
        void Validate(Usuario usuario);
    }
}
