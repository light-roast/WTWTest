namespace TechnicalTestWTW.Models;

public class PersonaDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string IdentificationNumber { get; set; } = string.Empty;
    public string IdentificationType { get; set; } = string.Empty;
    public string FullIdentificationNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
