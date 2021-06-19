using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Repositories;
using CRUD.Net.Domain.ViewModels;
using CRUD.Net.Infra.Data.ADODataAccess;
using CRUD.Net.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRUD.Net.Infra.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly DbSet<Produto> _produto;
        private DataAccess _dataAccess;

        public ProdutoRepository(CrudDbContext context) : base(context)
        {
            _produto = context.Produtos;
            _dataAccess = new DataAccess();
        }

        public IEnumerable<ProdutoViewModel> GetByNomeADO(string nome)
        {
            nome = $"%{nome.Trim()}%";
            _dataAccess.LimparParametros();
            string SQL = @" SELECT
                            produtos.Id,
                            produtos.Nome,
                            produtos.FornecedorId,
                            fornecedores.Nome AS Fornecedor,
                            produtos.Quantidade
                        FROM produtos 
                        INNER JOIN fornecedores on fornecedores.Id = produtos.FornecedorId 
                        WHERE produtos.Nome LIKE @nome ";

            _dataAccess.AdicionarParametro("@nome ", SqlDbType.VarChar, nome.Trim());
            var result = _dataAccess.ExecutaConsulta(SQL);
            var retList = new List<ProdutoViewModel>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                var row = result.Rows[i];
                var temp = new ProdutoViewModel()
                {
                    Id = (Guid)row["Id"],
                    Nome = (string)row["Nome"],
                    FornecedorId = (Guid)row["FornecedorId"],
                    Fornecedor = (string)row["Fornecedor"],
                    Quantidade = (Int32)row["Quantidade"]
                };
                retList.Add(temp);
            }
            return retList;
        }

        public bool HasAnyProdutoByFornecedor(Guid fornecedorId)
        {
            return _produto.Where(x => x.Fornecedor.Id == fornecedorId).Any();
        }
    }
}
