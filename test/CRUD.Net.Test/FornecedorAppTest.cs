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
    public class FornecedorAppTest
    {
        private FornecedorApp _fornecedorApp;
        private DbContextOptions<CrudDbContext> _options;
        private CrudDbContext _context;
        private Guid _guidInativo;

        public FornecedorAppTest()
        {
            _guidInativo = Guid.NewGuid();
        }
        private IEnumerable<Fornecedor> FornecedoresList()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            fornecedores.Add(new Fornecedor { Id = Guid.NewGuid(), Nome = "Copel", CNPJ = "76.483.817/0001-20", Endereco = "Rua Coronel Dulcídio, 800", Ativo = true });
            fornecedores.Add(new Fornecedor { Id = Guid.NewGuid(), Nome = "Certsys", CNPJ = "08.821.745/0001-23", Endereco = "R. Dr. Rafael de Barros, 209", Ativo = true });
            fornecedores.Add(new Fornecedor { Id = Guid.NewGuid(), Nome = "Locaweb", CNPJ = "02.351.877/0001-52", Endereco = "Rua Itapaiuna, 2434", Ativo = true });
            fornecedores.Add(new Fornecedor { Id = _guidInativo, Nome = "Sinqia", CNPJ = "04.065.791/0001-99", Endereco = "R. Bela Cintra, 755", Ativo = false });
            fornecedores.Add(new Fornecedor { Id = Guid.NewGuid(), Nome = "Coca-cola", CNPJ = "45.997.418/0001-53", Endereco = "Rod. Cmte. João Ribeiro de Barros, 10", Ativo = true });
            fornecedores.Add(new Fornecedor { Id = Guid.NewGuid(), Nome = "Ambev", CNPJ = "07.526.557/0001-00", Endereco = "R. Nicolau Assis, 5-41", Ativo = true });
            return fornecedores;
        }

        [Fact]
        public void ShouldCreateOrUpdateFornecedores()
        {

            // Arrange
            _options = new DbContextOptionsBuilder<CrudDbContext>()
              .UseInMemoryDatabase(databaseName: "ShouldCreateOrUpdateFornecedores")
              .Options;
            _context = new CrudDbContext(_options);
            
            
            // Act
            _fornecedorApp = new FornecedorApp(new FornecedorRepository(_context), new UnitOfWork(_context), new Notifier());
            foreach (var fornecedor in FornecedoresList().ToList())
            {
                _fornecedorApp.Create(fornecedor);
            }
            var inativoToUpdate = _fornecedorApp.GetById(_guidInativo);
            inativoToUpdate.Ativo = true;
            _fornecedorApp.CreateOrUpdate(inativoToUpdate);
            var fornec = _fornecedorApp.GetById(_guidInativo);
            var total = _fornecedorApp.GetAll();

            // Assert
            Assert.Equal(6, total.Count());
            Assert.True(fornec.Ativo);
        }

        [Fact]
        public void ShouldNotCreateOrUpdateFornecedores()
        {

            // Arrange
            _options = new DbContextOptionsBuilder<CrudDbContext>()
              .UseInMemoryDatabase(databaseName: "ShouldNotCreateOrUpdateFornecedores")
              .Options;
            _context = new CrudDbContext(_options);


            // Act
            _fornecedorApp = new FornecedorApp(new FornecedorRepository(_context), new UnitOfWork(_context), new Notifier());
            foreach (var fornecedor in FornecedoresList().ToList())
            {
                _fornecedorApp.Create(fornecedor);
            }
            _fornecedorApp.CreateOrUpdate(new Fornecedor { Id = Guid.Empty, CNPJ = "00", Ativo = true, Endereco = "A", Nome = "A" });
            var total = _fornecedorApp.GetAll();

            // Assert
            Assert.Equal(6, total.Count());
        }

        [Fact]
        public void ShouldReturnTheActiveFornecedores()
        {
            using (_context)
            {
                // Arrange
                _options = new DbContextOptionsBuilder<CrudDbContext>()
                  .UseInMemoryDatabase(databaseName: "ShouldReturnTheActiveFornecedores")
                  .Options;
                _context = new CrudDbContext(_options);
                _fornecedorApp = new FornecedorApp(new FornecedorRepository(_context), new UnitOfWork(_context), new Notifier());
                foreach (var fornecedor in FornecedoresList().ToList())
                {
                    _fornecedorApp.Create(fornecedor);
                }

                // Act
                var ativos = _fornecedorApp.GetAllAtivos();

                // Assert
                Assert.Equal(5, ativos.Count());
                Assert.DoesNotContain(ativos, d => d.Id == _guidInativo);
            }
        }

        [Fact]
        public void ShouldReturnFornecedorById()
        {
            using (_context)
            {
                // Arrange
                _options = new DbContextOptionsBuilder<CrudDbContext>()
                  .UseInMemoryDatabase(databaseName: "ShouldReturnFornecedorById")
                  .Options;
                _context = new CrudDbContext(_options);
                _fornecedorApp = new FornecedorApp(new FornecedorRepository(_context), new UnitOfWork(_context), new Notifier());
                foreach (var fornecedor in FornecedoresList().ToList())
                {
                    _fornecedorApp.Create(fornecedor);
                }

                // Act
                var fornecedorById = _fornecedorApp.GetById(_guidInativo);

                // Assert
                Assert.Equal(_guidInativo, fornecedorById.Id);
                Assert.Equal("Sinqia", fornecedorById.Nome);
                Assert.Equal(new String("04.065.791/0001-99".Where(Char.IsDigit).ToArray()), fornecedorById.CNPJ);
            }
        }

        [Fact]
        public void ShouldDeleteFornecedores()
        {

            // Arrange
            _options = new DbContextOptionsBuilder<CrudDbContext>()
              .UseInMemoryDatabase(databaseName: "ShouldDeleteFornecedores")
              .Options;
            _context = new CrudDbContext(_options);


            // Act
            _fornecedorApp = new FornecedorApp(new FornecedorRepository(_context), new UnitOfWork(_context), new Notifier());
            foreach (var fornecedor in FornecedoresList().ToList())
            {
                _fornecedorApp.Create(fornecedor);
            }
            _fornecedorApp.Delete(_guidInativo);
            var total = _fornecedorApp.GetAll();

            // Assert
            Assert.Equal(5, total.Count());
        }
    }
}
