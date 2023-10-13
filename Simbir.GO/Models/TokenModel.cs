using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Primitives;

namespace Simbir.GO.Models;

public class TokenModel
{
    public string JWT { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpTime  { get; set; }
}