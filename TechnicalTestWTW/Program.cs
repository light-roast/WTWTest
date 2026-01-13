using Microsoft.EntityFrameworkCore;
using TechnicalTestWTW.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options => { options.AddPolicy("AllowAll", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    
    var createStoredProcedureSql = @"
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetPersonasCreadas')
        BEGIN
            EXEC('
                CREATE PROCEDURE sp_GetPersonasCreadas
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    SELECT 
                        Id,
                        FirstName,
                        LastName,
                        FullName,
                        IdentificationNumber,
                        IdentificationType,
                        FullIdentificationNumber,
                        Email,
                        CreatedDate
                    FROM Personas
                    ORDER BY CreatedDate DESC;
                END
            ')
        END
    ";
    
    dbContext.Database.ExecuteSqlRaw(createStoredProcedureSql);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
