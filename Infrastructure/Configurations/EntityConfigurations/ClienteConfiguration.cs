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

            builder.Property(c => c.RazaoSocial)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.NomeFantasia)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(c => c.Logradouro)
                .HasMaxLength(500);

            builder.Property(c => c.Bairro)
                .HasMaxLength(100);

            builder.Property(c => c.Cidade)
                .HasMaxLength(100);

            builder.Property(c => c.Estado)
                .HasMaxLength(2);

            builder.Property(c => c.Ativo)
                .IsRequired();
        }
    }
}
