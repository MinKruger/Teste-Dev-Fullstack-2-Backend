using CrossCutting.Utils;
using System;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string? RazaoSocial { get; private set; }
        public string? NomeFantasia { get; private set; }
        public string? Logradouro { get; private set; }
        public string? CNPJ { get; private set; }
        public string? Bairro { get; private set; }
        public string? Cidade { get; private set; }
        public string? Estado { get; private set; }
        public bool Ativo { get; private set; }

        // Construtor
        private Cliente() { } // Para ORMs como EF Core

        public Cliente(string razaoSocial, string nomeFantasia, string cnpj, string logradouro, string bairro, string cidade, string estado)
        {
            Id = Guid.NewGuid();
            SetRazaoSocial(razaoSocial);
            SetNomeFantasia(nomeFantasia);
            SetCNPJ(cnpj);
            SetLogradouro(logradouro);
            SetBairro(bairro);
            SetCidade(cidade);
            SetEstado(estado);
            Ativo = true;
        }

        // Métodos de atualização do estado
        public void SetRazaoSocial(string razaoSocial)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial))
                throw new ArgumentException("Razão Social é obrigatória.");
            RazaoSocial = razaoSocial;
        }

        public void SetNomeFantasia(string nomeFantasia)
        {
            if (string.IsNullOrWhiteSpace(nomeFantasia))
                throw new ArgumentException("Nome Fantasia é obrigatório.");
            NomeFantasia = nomeFantasia;
        }

        public void SetCNPJ(string cnpj)
        {
            if (!IsValidCNPJ(cnpj))
                throw new ArgumentException("CNPJ inválido.");
            CNPJ = cnpj;
        }

        public void SetLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
                throw new ArgumentException("Logradouro é obrigatório.");
            Logradouro = logradouro;
        }

        public void SetBairro(string bairro)
        {
            if (string.IsNullOrWhiteSpace(bairro))
                throw new ArgumentException("Bairro é obrigatório.");
            Bairro = bairro;
        }

        public void SetCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new ArgumentException("Cidade é obrigatória.");
            Cidade = cidade;
        }

        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado) || estado.Length != 2)
                throw new ArgumentException("Estado deve conter 2 caracteres.");
            Estado = estado.ToUpper();
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        // Validação de CNPJ
        private bool IsValidCNPJ(string cnpj)
        {
            return CNPJValidator.IsValid(cnpj);
        }
    }
}
