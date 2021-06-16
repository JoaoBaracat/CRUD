namespace CRUD.Net.Domain.Entities
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public bool Ativo { get; set; }
    }
}
