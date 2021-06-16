using CRUD.Net.Domain.Repositories;
using CRUD.Net.Infra.Data.Contexts;

namespace CRUD.Net.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CrudDbContext _context;

        public UnitOfWork(CrudDbContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
