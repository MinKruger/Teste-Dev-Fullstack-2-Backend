using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.EntityConfigurations
{
    public class PedidoDetalhadoConfiguration : IEntityTypeConfiguration<PedidoDetalhado>
    {
        public void Configure(EntityTypeBuilder<PedidoDetalhado> builder)
        {
            builder.ToView("vw_PedidosClientesVendedores").HasNoKey();
        }
    }
}
