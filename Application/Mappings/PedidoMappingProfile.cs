using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PedidoMappingProfile : Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<PedidoDetalhado, PedidoDetalhadoDto>().ReverseMap();
            CreateMap<PedidoPorVendedor, PedidoPorVendedorDto>().ReverseMap();
            CreateMap<CreatePedidoDto, Pedido>();
            CreateMap<UpdatePedidoDto, Pedido>();
        }
    }
}
