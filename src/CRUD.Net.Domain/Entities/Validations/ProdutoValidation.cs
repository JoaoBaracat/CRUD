using FluentValidation;

namespace CRUD.Net.Domain.Entities.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(3, 200).WithMessage("O Nome deve ter entre 3 e 200 caracteres");

            RuleFor(x => x.Fornecedor)
                .NotEmpty().WithMessage("O Fornecedor é obrigatório");

            RuleFor(x => x.Quantidade)
                .NotEmpty().WithMessage("A Quantidade é obrigatória");
        }
    }
}
