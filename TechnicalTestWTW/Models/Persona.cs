using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTestWTW.Models;

[Table("Personas")]
public class Persona
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string IdentificationNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string IdentificationType { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public string FullIdentificationNumber => $"{IdentificationType}-{IdentificationNumber}";

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}
