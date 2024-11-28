using System;

namespace Domain.Entities
{
    public class Vendedor
    {
        public int Id { get; private set; }
        public string? Nome { get; private set; }
        public string? CodigoVendedor { get; private set; }
        public string? Apelido { get; private set; }
        public bool Ativo { get; private set; }

        // Construtor
        public Vendedor() { } // Para ORMs como EF Core

        public Vendedor(string nome, string codigoVendedor, string apelido)
        {
            SetNome(nome);
            SetCodigoVendedor(codigoVendedor);
            SetApelido(apelido);
            Ativo = true;
        }

        // Métodos de atualização do estado
        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");
            Nome = nome;
        }

        public void SetCodigoVendedor(string codigoVendedor)
        {
            if (string.IsNullOrWhiteSpace(codigoVendedor))
                throw new ArgumentException("Código do Vendedor é obrigatório.");
            CodigoVendedor = codigoVendedor;
        }

        public void SetApelido(string apelido)
        {
            if (string.IsNullOrWhiteSpace(apelido))
                throw new ArgumentException("Apelido é obrigatório.");
            Apelido = apelido;
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;
    }
}
