using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Repositories;
using CRUD.Net.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CRUD.Net.Infra.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly DbSet<Usuario> _usuario;

        public UsuarioRepository(CrudDbContext context) : base(context)
        {
            _usuario = context.Usuarios;
        }

        public Usuario Authenticate(string login, string password)
        {
            return _usuario.Where(x => x.Login == login && x.Senha == password).AsNoTracking().FirstOrDefault();
        }
    }
}
