using System.Text.Json.Serialization;

namespace Simbir.GO.Models;

public class TokenModel
{
    public string JWT { get; set; }
    
    [JsonIgnore]
    public string RefreshToken { get; set; }
    
    [JsonIgnore]
    public DateTime RefreshTokenExpTime  { get; set; }
}