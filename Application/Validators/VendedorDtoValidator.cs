using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class VendedorDtoValidator : AbstractValidator<VendedorDto>
    {
        public VendedorDtoValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("O ID é obrigatório.") // Aplica-se apenas em contexto de atualização
                .When(v => v.Id != Guid.Empty);

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(200).WithMessage("O nome pode conter no máximo 200 caracteres.");

            RuleFor(v => v.CodigoVendedor)
                .NotEmpty().WithMessage("O código do vendedor é obrigatório.")
                .MaximumLength(50).WithMessage("O código do vendedor pode conter no máximo 50 caracteres.");

            RuleFor(v => v.Apelido)
                .MaximumLength(100).WithMessage("O apelido pode conter no máximo 100 caracteres.");
        }
    }
}
