using CRUD.Net.Domain.Entities;

namespace CRUD.Net.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Authenticate(string login, string password);
    }
}
