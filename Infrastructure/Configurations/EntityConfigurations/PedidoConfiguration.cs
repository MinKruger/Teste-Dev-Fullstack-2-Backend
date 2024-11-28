using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityConfigurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("pedido_id");

            builder.Property(p => p.DescricaoPedido)
                .HasColumnName("descricao_pedido")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.ValorTotal)
                .HasColumnName("valor_total")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DataCriacao)
                .HasColumnName("data_criacao")
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnName("observacao")
                .HasMaxLength(1000);

            builder.Property(p => p.Autorizado)
                .HasColumnName("autorizado")
                .IsRequired();

            // Relacionamento com Cliente
            builder.Property(p => p.ClienteId)
                .HasColumnName("cliente_id")
                .IsRequired();

            builder.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Vendedor
            builder.Property(p => p.VendedorId)
                .HasColumnName("vendedor_id")
                .IsRequired();

            builder.HasOne(p => p.Vendedor)
                .WithMany()
                .HasForeignKey(p => p.VendedorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
