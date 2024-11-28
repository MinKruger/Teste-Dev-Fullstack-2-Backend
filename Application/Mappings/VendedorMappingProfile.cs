using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class VendedorMappingProfile : Profile
    {
        public VendedorMappingProfile()
        {
            CreateMap<Vendedor, VendedorDto>().ReverseMap();
            CreateMap<CreateVendedorDto, Vendedor>();
            CreateMap<UpdateVendedorDto, Vendedor>();
        }
    }
}
