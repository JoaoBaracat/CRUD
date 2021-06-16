using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Net.Domain.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid FornecedorId { get; set; }
        public string Fornecedor { get; set; }
        public int Quantidade { get; set; }

    }
}
