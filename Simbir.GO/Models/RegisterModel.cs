using System.ComponentModel.DataAnnotations;

namespace Simbir.GO.Models;

public class RegisterModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
}