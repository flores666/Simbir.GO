using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Primitives;

namespace Simbir.GO.Models;

public class TokenModel
{
    public string JWT { get; set; }
    
    [JsonIgnore]
    public string RefreshToken { get; set; }
    
    [JsonIgnore]
    public DateTime RefreshTokenExpTime  { get; set; }
}