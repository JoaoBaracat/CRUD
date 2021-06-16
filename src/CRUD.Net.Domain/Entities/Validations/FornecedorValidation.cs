using FluentValidation;

namespace CRUD.Net.Domain.Entities.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(3, 200).WithMessage("O Nome deve ter entre 3 e 200 caracteres");

            RuleFor(x => x.CNPJ).Custom((cnpj, context) => {
				int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
				int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
				int soma;
				int resto;
				string digito;
				string tempCnpj;
				cnpj = cnpj.Trim();
				cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
				if (cnpj.Length != 14)
					context.AddFailure("CNPJ inválido");
				tempCnpj = cnpj.Substring(0, 12);
				soma = 0;
				for (int i = 0; i < 12; i++)
					soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
				resto = (soma % 11);
				if (resto < 2)
					resto = 0;
				else
					resto = 11 - resto;
				digito = resto.ToString();
				tempCnpj = tempCnpj + digito;
				soma = 0;
				for (int i = 0; i < 13; i++)
					soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
				resto = (soma % 11);
				if (resto < 2)
					resto = 0;
				else
					resto = 11 - resto;
				digito = digito + resto.ToString();
				if (!cnpj.EndsWith(digito))
                {
					context.AddFailure("CNPJ inválido");
				}				
			});

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("O Endereço é obrigatório")
                .Length(3, 200).WithMessage("O Endereço deve ter entre 3 e 200 caracteres");
		}
    }
}
