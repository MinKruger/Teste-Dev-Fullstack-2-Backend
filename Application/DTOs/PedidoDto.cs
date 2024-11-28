using System;

namespace Application.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
    }

    public class CreatePedidoDto
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
    }

    public class UpdatePedidoDto
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
    }
}
