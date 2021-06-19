using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Entities.Validations;
using CRUD.Net.Domain.Notifications;
using CRUD.Net.Domain.Repositories;
using CRUD.Net.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CRUD.Net.App.Services
{
    public class FornecedorApp : AppBase, IFornecedorApp
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoRepository _produtoRepository;

        public FornecedorApp(IFornecedorRepository fornecedorRepository, IUnitOfWork unitOfWork, INotifier notifier, IProdutoRepository produtoRepository) : base(unitOfWork, notifier)
        {
            _fornecedorRepository = fornecedorRepository;
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<FornecedorViewModel> GetByNomeADO(string nome)
        {
            return _fornecedorRepository.GetByNomeADO(nome);
        }

        public IEnumerable<Fornecedor> GetAll()
        {
            return _fornecedorRepository.GetAll();
        }

        public Fornecedor GetById(Guid id)
        {
            return _fornecedorRepository.GetById(id);
        }

        public IEnumerable<Fornecedor> GetAllAtivos()
        {
            return _fornecedorRepository.GetAllAtivos();
        }

        public void CreateOrUpdate(Fornecedor fornecedor)
        {
            if (fornecedor.Id == Guid.Empty)
            {
                Create(fornecedor);
            }
            else
            {
                Update(fornecedor.Id, fornecedor);
            }
        }

        public void Create(Fornecedor fornecedor)
        {
            if (!Validate(new FornecedorValidation(), fornecedor))
            {
                return;
            }
            fornecedor.CNPJ = new String(fornecedor.CNPJ.Where(Char.IsDigit).ToArray());
            _fornecedorRepository.Create(fornecedor);

            UnitOfWork.Save();
        }

        public void Update(Guid id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                Notify($"Os ids {id} e {fornecedor.Id} fornecidos são diferentes.");
                return;
            }

            if (!Validate(new FornecedorValidation(), fornecedor))
            {
                return;
            }

            var fornecedorToUpdate = _fornecedorRepository.GetById(id);
            if (fornecedorToUpdate == null)
            {
                Notify($"O fornecedor {id} não foi encontrado.");
                return;
            }

            if (!fornecedor.Ativo && _produtoRepository.HasAnyProdutoByFornecedor(id))
            {
                Notify($"O fornecedor {id} não pode ser inativo, pois possui produtos vinculados a ele.");
                return;
            }

            fornecedorToUpdate.Nome = fornecedor.Nome;
            fornecedorToUpdate.Endereco = fornecedor.Endereco;
            fornecedorToUpdate.CNPJ = new String(fornecedor.CNPJ.Where(Char.IsDigit).ToArray());
            fornecedorToUpdate.Ativo = fornecedor.Ativo;

            _fornecedorRepository.Update(fornecedorToUpdate);

            UnitOfWork.Save();
        }
        

        public void Delete(Guid id)
        {
            var fornecedorToDelete = _fornecedorRepository.GetById(id);
            if (fornecedorToDelete == null)
            {
                Notify($"O fornecedor {id} não foi encontrado.");
                return;
            }

            if (_produtoRepository.HasAnyProdutoByFornecedor(id))
            {
                Notify($"O fornecedor {id} não pode ser excluído, pois possui produtos vinculados a ele.");
                return;
            }

            _fornecedorRepository.Delete(fornecedorToDelete);

            UnitOfWork.Save();
        }
    }
}
