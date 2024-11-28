using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityConfigurations
{
    public class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("vendedor_id");

            builder.Property(v => v.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.CodigoVendedor)
                .HasColumnName("codigo_vendedor")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Apelido)
                .HasColumnName("apelido")
                .HasMaxLength(100);

            builder.Property(v => v.Ativo)
                .HasColumnName("ativo")
                .IsRequired();
        }
    }
}
