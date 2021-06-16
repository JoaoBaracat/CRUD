using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace CRUD.Net.Domain.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<ProdutoViewModel> GetByNomeADO(string nome);
    }
}
