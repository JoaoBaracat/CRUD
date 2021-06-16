using FluentValidation;

namespace CRUD.Net.Domain.Entities.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("O Login é obrigatório")
                .Length(6, 50).WithMessage("O Login deve ter entre 6 e 50 caracteres");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A Senha é obrigatória")
                .Length(6, 50).WithMessage("A Senha deve ter entre 6 e 50 caracteres");
        }
    }
}
