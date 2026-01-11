using AutoMapper;
using TechnicalTestWTW.Models;

namespace TechnicalTestWTW.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Persona, PersonaDto>();
        
        CreateMap<PersonaDto, Persona>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForMember(dest => dest.FullIdentificationNumber, opt => opt.Ignore());

        CreateMap<Usuario, UsuarioDto>();
        
        CreateMap<UsuarioDto, Usuario>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
    }
}
