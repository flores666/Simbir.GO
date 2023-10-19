using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Simbir.GO.DataAccess.Objects;

[Owned]
public class RefreshToken
{
    [Key]
    [Required]
    [Column(TypeName = "text")]
    public string Token { get; set; }
    
    [Required]
    public DateTime ExpiryTime { get; set; }
    
    public bool IsExpired => DateTime.UtcNow >= ExpiryTime;
}