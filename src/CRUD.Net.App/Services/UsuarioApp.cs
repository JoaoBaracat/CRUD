using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Entities;
using CRUD.Net.Domain.Entities.Validations;
using CRUD.Net.Domain.Notifications;
using CRUD.Net.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace CRUD.Net.App.Services
{
    public class UsuarioApp : AppBase, IUsuarioApp
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioApp(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, INotifier notifier) : base(unitOfWork, notifier)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Authenticate(string login, string password)
        {
            var usuario = _usuarioRepository.Authenticate(login, password);

            return usuario;
        }

        public void Validate(Usuario usuario)
        {
            if (!Validate(new UsuarioValidation(), usuario))
            {
                return;
            }
        }

        public void Create(Usuario usuario)
        {
            if (!Validate(new UsuarioValidation(), usuario))
            {
                return;
            }

            _usuarioRepository.Create(usuario);

            UnitOfWork.Save();
        }


        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void CreateOrUpdate(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
