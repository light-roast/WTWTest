using Microsoft.EntityFrameworkCore;
using TechnicalTestWTW.Models;

namespace TechnicalTestWTW.Data;

public static class StoredProcedureExtensions
{
    public static async Task<List<PersonaDto>> GetPersonasCreadasAsync(this ApplicationDbContext context)
    {
        var result = await context.Database
            .SqlQueryRaw<PersonaDto>("EXEC sp_GetPersonasCreadas")
            .ToListAsync();
        
        return result;
    }
}
