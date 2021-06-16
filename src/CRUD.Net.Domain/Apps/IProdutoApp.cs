using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace CRUD.Net.Domain.Apps
{
    public interface IProdutoApp : IApp<Produto>
    {
        IEnumerable<ProdutoViewModel> GetByNomeADO(string nome);
    }
}
