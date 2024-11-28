using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class PedidoDtoValidator : AbstractValidator<PedidoDto>
    {
        public PedidoDtoValidator()
        {
            RuleFor(p => p.DescricaoPedido)
                .NotEmpty().WithMessage("A descrição do pedido é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição do pedido pode conter no máximo 500 caracteres.");

            RuleFor(p => p.ValorTotal)
                .GreaterThan(0).WithMessage("O valor total deve ser maior que zero.");

            RuleFor(p => p.ClienteId)
                .NotEmpty().WithMessage("O ClienteId é obrigatório.");

            RuleFor(p => p.VendedorId)
                .NotEmpty().WithMessage("O VendedorId é obrigatório.");
        }
    }
}
