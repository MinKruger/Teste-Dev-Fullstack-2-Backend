using System;

namespace Application.DTOs
{
    public class PedidoDto
    {
        public Guid Id { get; set; }
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
        public Guid ClienteId { get; set; }
        public Guid VendedorId { get; set; }
    }

    public class CreatePedidoDto
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public Guid ClienteId { get; set; }
        public Guid VendedorId { get; set; }
    }

    public class UpdatePedidoDto
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
        public Guid ClienteId { get; set; }
        public Guid VendedorId { get; set; }
    }
}
