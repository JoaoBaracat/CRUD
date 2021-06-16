namespace CRUD.Net.Domain.Entities
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int Quantidade { get; set; }
    }
}
