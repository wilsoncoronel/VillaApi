using System.Runtime;
using AutoMapper;// se importa el automapper
using VillaAPI.Modelos;
using VillaAPI.Modelos.Dto;

namespace VillaAPI
{
    public class MappingConfig: Profile // hereda de profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>(); //Indicacmos fuente(villa), destino(VillaDto)
            CreateMap<VillaDto, Villa>(); // Invertimos
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
