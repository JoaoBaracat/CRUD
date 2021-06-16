using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Entities.Validations;
using CRUD.Net.Domain.Notifications;
using CRUD.Net.Domain.Repositories;
using CRUD.Net.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace CRUD.Net.App.Services
{
    public class ProdutoApp : AppBase, IProdutoApp
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoApp(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, INotifier notifier) : base(unitOfWork, notifier)
        {
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<ProdutoViewModel> GetByNomeADO(string nome)
        {
            return _produtoRepository.GetByNomeADO(nome);
        }

        public IEnumerable<Produto> GetAll()
        {
            return _produtoRepository.GetAll();
        }

        public Produto GetById(Guid id)
        {
            return _produtoRepository.GetById(id);
        }

        public void CreateOrUpdate(Produto produto)
        {
            if (produto.Id == Guid.Empty)
            {
                Create(produto);
            }
            else
            {
                Update(produto.Id, produto);
            }
        }

        public void Create(Produto produto)
        {
            if (!Validate(new ProdutoValidation(), produto))
            {
                return;
            }

            _produtoRepository.Create(produto);

            UnitOfWork.Save();
        }

        public void Update(Guid id, Produto produto)
        {
            if (id != produto.Id)
            {
                Notify($"Os ids {id} e {produto.Id} fornecidos são diferentes.");
                return;
            }

            if (!Validate(new ProdutoValidation(), produto))
            {
                return;
            }

            var produtoToUpdate = GetById(id);
            if (produtoToUpdate == null)
            {
                Notify($"O produto {id} não foi encontrado.");
                return;
            }

            produtoToUpdate.Nome = produto.Nome;
            produtoToUpdate.Fornecedor = produto.Fornecedor;
            produtoToUpdate.Quantidade = produto.Quantidade;

            _produtoRepository.Update(produtoToUpdate);

            UnitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            var produtoToDelete = GetById(id);
            if (produtoToDelete == null)
            {
                Notify($"O produto {id} não foi encontrado.");
                return;
            }

            _produtoRepository.Delete(produtoToDelete);

            UnitOfWork.Save();
        }
    }
}
