using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalTestWTW.Data;
using TechnicalTestWTW.Models;

namespace TechnicalTestWTW.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PersonasController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonaDto>>> GetPersonas()
    {
        var personas = await _context.Personas.ToListAsync();
        var personasDto = _mapper.Map<List<PersonaDto>>(personas);
        return Ok(personasDto);
    }

    [HttpGet("creadas")]
    public async Task<ActionResult<List<PersonaDto>>> GetPersonasCreadas()
    {
        var personas = await _context.GetPersonasCreadasAsync();
        return Ok(personas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaDto>> GetPersona(int id)
    {
        var persona = await _context.Personas.FindAsync(id);

        if (persona == null)
        {
            return NotFound();
        }

        var personaDto = _mapper.Map<PersonaDto>(persona);
        return Ok(personaDto);
    }

    [HttpPost]
    public async Task<ActionResult<PersonaDto>> CreatePersona(PersonaDto personaDto)
    {
        var persona = _mapper.Map<Persona>(personaDto);
        persona.CreatedDate = DateTime.UtcNow;

        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();

        var createdPersonaDto = _mapper.Map<PersonaDto>(persona);
        return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, createdPersonaDto);
    }
}
