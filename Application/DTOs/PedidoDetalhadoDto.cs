﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PedidoDetalhadoDto
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
        public string? NomeFantasia { get; set; }
        public string? CNPJ { get; set; }
        public string? NomeVendedor { get; set; }
    }

    public class PedidoPorVendedorDto
    {
        public string? DescricaoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Observacao { get; set; }
        public bool Autorizado { get; set; }
    }
}
