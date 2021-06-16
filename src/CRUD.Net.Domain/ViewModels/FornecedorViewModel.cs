using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Net.Domain.ViewModels
{
    public class FornecedorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public bool Ativo { get; set; }

    }
}
