using System;

namespace Application.DTOs
{
    public class VendedorDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? CodigoVendedor { get; set; }
        public string? Apelido { get; set; }
        public bool Ativo { get; set; }
    }

    public class CreateVendedorDto
    {
        public string? Nome { get; set; }
        public string? CodigoVendedor { get; set; }
        public string? Apelido { get; set; }
    }

    public class UpdateVendedorDto
    {
        public string? Nome { get; set; }
        public string? CodigoVendedor { get; set; }
        public string? Apelido { get; set; }
        public bool Ativo { get; set; }
    }
}
