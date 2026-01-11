using AutoMapper;
using TechnicalTestWTW.Models;

namespace TechnicalTestWTW.Mappings;

public class PersonaMappingProfile : Profile
{
    public PersonaMappingProfile()
    {
        CreateMap<Persona, PersonaDto>();
        
        CreateMap<PersonaDto, Persona>()
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForMember(dest => dest.FullIdentificationNumber, opt => opt.Ignore());
    }
}
