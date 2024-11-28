using System;

namespace Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public string? DescricaoPedido { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string? Observacao { get; private set; }
        public bool Autorizado { get; private set; }
        public Guid ClienteId { get; private set; } // Chave estrangeira para Cliente
        public Guid VendedorId { get; private set; } // Chave estrangeira para Vendedor

        // Propriedades de navegação
        public virtual Cliente? Cliente { get; private set; } // Relacionamento com Cliente
        public virtual Vendedor? Vendedor { get; private set; } // Relacionamento com Vendedor

        // Construtor
        private Pedido() { } // Para ORMs como EF Core

        public Pedido(string descricaoPedido, decimal valorTotal, string observacao, Guid clienteId, Guid vendedorId)
        {
            Id = Guid.NewGuid();
            SetDescricaoPedido(descricaoPedido);
            SetValorTotal(valorTotal);
            SetObservacao(observacao);
            ClienteId = clienteId;
            VendedorId = vendedorId;
            DataCriacao = DateTime.UtcNow;
            Autorizado = false;
        }

        // Métodos de atualização do estado
        public void SetDescricaoPedido(string descricaoPedido)
        {
            if (string.IsNullOrWhiteSpace(descricaoPedido))
                throw new ArgumentException("Descrição do Pedido é obrigatória.");
            DescricaoPedido = descricaoPedido;
        }

        public void SetValorTotal(decimal valorTotal)
        {
            if (valorTotal <= 0)
                throw new ArgumentException("Valor Total deve ser maior que zero.");
            ValorTotal = valorTotal;
        }

        public void SetObservacao(string observacao)
        {
            Observacao = observacao;
        }

        public void Autorizar()
        {
            if (Autorizado)
                throw new InvalidOperationException("O pedido já está autorizado.");
            Autorizado = true;
        }

        public void CancelarAutorizacao()
        {
            if (!Autorizado)
                throw new InvalidOperationException("O pedido não está autorizado.");
            Autorizado = false;
        }
    }
}
