using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace CRUD.Net.Domain.Apps
{
    public interface IFornecedorApp : IApp<Fornecedor>
    {
        IEnumerable<Fornecedor> GetAllAtivos();
        IEnumerable<FornecedorViewModel> GetByNomeADO(string nome);
    }
}
