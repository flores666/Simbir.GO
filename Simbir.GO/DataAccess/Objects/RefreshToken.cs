using System.ComponentModel.DataAnnotations.Schema;

namespace Simbir.GO.DataAccess.Objects;

public class RefreshToken
{
    public int Id { get; set; }
    
    [Column(TypeName = "text")]
    public string Token { get; set; }
    
    public DateTime ExpiryTime { get; set; }
    
    public bool IsExpired => DateTime.UtcNow >= ExpiryTime;
}