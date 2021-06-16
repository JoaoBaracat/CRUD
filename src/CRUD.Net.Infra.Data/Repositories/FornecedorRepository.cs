using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Repositories;
using CRUD.Net.Infra.Data.Contexts;
using CRUD.Net.Infra.Data.ADODataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using CRUD.Net.Domain.ViewModels;
using System.Linq;

namespace CRUD.Net.Infra.Data.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        private readonly DbSet<Fornecedor> _fornecedor;
        private DataAccess _dataAccess;
        public FornecedorRepository(CrudDbContext context) : base(context)
        {
            _fornecedor = context.Fornecedores;
            _dataAccess = new DataAccess();
        }

        public IEnumerable<Fornecedor> GetAllAtivos()
        {
            return _fornecedor.Where(x => x.Ativo).AsNoTracking().ToList();
        }

        public IEnumerable<FornecedorViewModel> GetByNomeADO(string nome)
        {
            nome = $"%{nome.Trim()}%";
            _dataAccess.LimparParametros();
            string SQL = @" SELECT
                            fornecedores.Id,
                            fornecedores.Nome,
                            fornecedores.Endereco,
                            SUBSTRING(fornecedores.CNPJ,1,2) + '.'
                            + SUBSTRING(fornecedores.CNPJ,3,3) + '.'
                            + SUBSTRING(fornecedores.CNPJ,6,3) + '/'
                            + SUBSTRING(fornecedores.CNPJ,9,4) + '-'
                            + SUBSTRING(fornecedores.CNPJ,13,2) AS CNPJ,
                            fornecedores.Ativo
                        FROM fornecedores WHERE fornecedores.Nome LIKE @nome ";
            
            _dataAccess.AdicionarParametro("@nome ", SqlDbType.VarChar, nome.Trim());
            var result = _dataAccess.ExecutaConsulta(SQL);
            var retList = new List<FornecedorViewModel>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                var row = result.Rows[i];
                var temp = new FornecedorViewModel()
                {
                    Id = (Guid)row["Id"],
                    Nome = (string)row["Nome"],
                    CNPJ = (string)row["CNPJ"],
                    Endereco = (string)row["Endereco"],
                    Ativo = (bool)row["Ativo"]
                };
                retList.Add(temp);
            }
            return retList;
        }
    }
}
