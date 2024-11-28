using Application.DTOs;
using CrossCutting.Utils;
using FluentValidation;

namespace Application.Validators
{
    public class ClienteDtoValidator : AbstractValidator<ClienteDto>
    {
        public ClienteDtoValidator()
        {
            // Regras comuns para criação e atualização
            RuleFor(c => c.RazaoSocial)
                .NotEmpty().WithMessage("Razão Social é obrigatória.")
                .MaximumLength(200).WithMessage("Razão Social pode conter no máximo 200 caracteres.");

            RuleFor(c => c.CNPJ)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Length(14).WithMessage("CNPJ deve conter 14 caracteres.")
                .Must(BeAValidCNPJ).WithMessage("CNPJ inválido.");

            RuleFor(c => c.Estado)
                .Length(2).WithMessage("Estado deve conter 2 caracteres.");

            // Regra adicional apenas para atualização
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O ID é obrigatório para atualizações.")
                .When(c => c.Id != Guid.Empty); // Só valida se o ID foi informado
        }

        private bool BeAValidCNPJ(string cnpj)
        {
            return CNPJValidator.IsValid(cnpj); // Reutiliza a validação criada
        }
    }
}
