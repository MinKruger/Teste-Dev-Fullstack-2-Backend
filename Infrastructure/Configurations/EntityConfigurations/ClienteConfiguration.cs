using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.EntityConfigurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("cliente_id");

            builder.Property(c => c.RazaoSocial)
                .HasColumnName("razao_social")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.NomeFantasia)
                .HasColumnName("nome_fantasia")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CNPJ)
                .HasColumnName("cnpj")
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(c => c.Logradouro)
                .HasColumnName("logradouro")
                .HasMaxLength(500);

            builder.Property(c => c.Bairro)
                .HasColumnName("bairro")
                .HasMaxLength(100);

            builder.Property(c => c.Cidade)
                .HasColumnName("cidade")
                .HasMaxLength(100);

            builder.Property(c => c.Estado)
                .HasColumnName("estado")
                .HasMaxLength(2);

            builder.Property(c => c.Ativo)
                .HasColumnName("ativo")
                .IsRequired();
        }
    }
}
