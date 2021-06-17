using CRUD.Net.App.Services;
using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Notifications;
using CRUD.Net.Infra.Data.Contexts;
using CRUD.Net.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CRUD.Net.Test
{
    public class ProdutoAppTest
    {
        private ProdutoApp _produtoApp;
        private DbContextOptions<CrudDbContext> _options;
        private CrudDbContext _context;
        private Guid _guidCopel;
        private Guid _guidCocaCola;
        private Guid _guidAmbev;

        public ProdutoAppTest()
        {
            _guidCopel = Guid.Parse("cf6a14d7-9c6f-4be2-8494-f5d392cee37e");
            _guidCocaCola = Guid.Parse("6589cfa7-5aba-4157-831c-d94d9dceb210");
            _guidAmbev = Guid.Parse("43e3da4c-56eb-4881-8a84-71c56387784e");
        }
        
        private IEnumerable<Produto> ProdutosList()
        {
            List<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto { Id = _guidCopel, Nome = "Sistema de geolocalização", Quantidade = 5, Fornecedor = new Fornecedor { Id = _guidCopel, Nome = "Copel", CNPJ = "76.483.817/0001-20", Endereco = "Rua Coronel Dulcídio, 800", Ativo = true } });
            produtos.Add(new Produto { Id = _guidCocaCola, Nome = "Coca 2L", Quantidade = 3, Fornecedor = new Fornecedor { Id = _guidCocaCola, Nome = "Coca-cola", CNPJ = "45.997.418/0001-53", Endereco = "Rod. Cmte. João Ribeiro de Barros, 10", Ativo = true } });
            produtos.Add(new Produto { Id = _guidAmbev, Nome = "Brahma 600ml", Quantidade = 8, Fornecedor = new Fornecedor { Id = _guidAmbev, Nome = "Ambev", CNPJ = "07.526.557/0001-00", Endereco = "R. Nicolau Assis, 5-41", Ativo = true } });
            return produtos;
        }

        [Fact]
        public void ShouldCreateOrUpdateProdutos()
        {

            // Arrange
            _options = new DbContextOptionsBuilder<CrudDbContext>()
              .UseInMemoryDatabase(databaseName: "ShouldCreateOrUpdateProdutos")
              .Options;
            _context = new CrudDbContext(_options);


            // Act
            _produtoApp = new ProdutoApp(new ProdutoRepository(_context), new UnitOfWork(_context), new Notifier());
            foreach (var produto in ProdutosList().ToList())
            {
                _produtoApp.Create(produto);
            }
            var quantidadeToUpdate = _produtoApp.GetById(_guidCopel);
            quantidadeToUpdate.Quantidade = 3;
            _produtoApp.CreateOrUpdate(quantidadeToUpdate);
            var prod = _produtoApp.GetById(_guidCopel);
            var total = _produtoApp.GetAll();

            // Assert
            Assert.Equal(3, total.Count());
            Assert.Equal(3, prod.Quantidade);
        }

        [Fact]
        public void ShouldNotCreateOrUpdateProdutos()
        {

            // Arrange
            _options = new DbContextOptionsBuilder<CrudDbContext>()
              .UseInMemoryDatabase(databaseName: "ShouldNotCreateOrUpdateProdutos")
              .Options;
            _context = new CrudDbContext(_options);


            // Act
            _produtoApp = new ProdutoApp(new ProdutoRepository(_context), new UnitOfWork(_context), new Notifier());
            foreach (var produto in ProdutosList().ToList())
            {
                _produtoApp.Create(produto);
            }
            _produtoApp.CreateOrUpdate(new Produto { Id = Guid.Empty, Nome = "A", Quantidade = 2, Fornecedor = null });
            var total = _produtoApp.GetAll();

            // Assert
            Assert.Equal(3, total.Count());
        }

        [Fact]
        public void ShouldReturnProdutoById()
        {
            using (_context)
            {
                // Arrange
                _options = new DbContextOptionsBuilder<CrudDbContext>()
                  .UseInMemoryDatabase(databaseName: "ShouldReturnProdutoById")
                  .Options;
                _context = new CrudDbContext(_options);
                _produtoApp = new ProdutoApp(new ProdutoRepository(_context), new UnitOfWork(_context), new Notifier());
                foreach (var produto in ProdutosList().ToList())
                {
                    _produtoApp.Create(produto);
                }

                // Act
                var produtoById = _produtoApp.GetById(_guidCopel);

                // Assert
                Assert.Equal(_guidCopel, produtoById.Id);
                Assert.Equal("Sistema de geolocalização", produtoById.Nome);
                Assert.Equal(5, produtoById.Quantidade);
            }
        }

        [Fact]
        public void ShouldDeleteProduto()
        {

            // Arrange
            _options = new DbContextOptionsBuilder<CrudDbContext>()
              .UseInMemoryDatabase(databaseName: "ShouldDeleteProduto")
              .Options;
            _context = new CrudDbContext(_options);


            // Act
            _produtoApp = new ProdutoApp(new ProdutoRepository(_context), new UnitOfWork(_context), new Notifier());
            foreach (var produto in ProdutosList().ToList())
            {
                _produtoApp.Create(produto);
            }
            _produtoApp.Delete(_guidAmbev);
            var total = _produtoApp.GetAll();

            // Assert
            Assert.Equal(2, total.Count());
        }
    }
}
