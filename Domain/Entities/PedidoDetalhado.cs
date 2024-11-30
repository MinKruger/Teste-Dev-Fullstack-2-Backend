using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PedidoDetalhado
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
        public string? NomeFantasia { get; set; }
        public string? CNPJ { get; set; }
        public string? NomeVendedor { get; set; }

        public PedidoDetalhado() { } // Para ORMs como EF Core
    }
}
