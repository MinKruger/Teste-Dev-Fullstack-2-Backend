using Application.DTOs;
using CrossCutting.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateClienteDtoValidator : AbstractValidator<CreateClienteDto>
    {
        public CreateClienteDtoValidator()
        {
            RuleFor(c => c.RazaoSocial)
                .NotEmpty().WithMessage("Razão Social é obrigatória.")
                .MaximumLength(200);

            RuleFor(c => c.CNPJ)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Length(14).WithMessage("CNPJ deve conter 14 caracteres.")
                .Must(BeAValidCNPJ).WithMessage("CNPJ inválido.");

            RuleFor(c => c.Estado)
                .Length(2).WithMessage("Estado deve conter 2 caracteres.");
        }

        private bool BeAValidCNPJ(string cnpj)
        {
            return CNPJValidator.IsValid(cnpj); // Reutiliza a validação criada
        }
    }
}
