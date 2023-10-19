using Simbir.GO.DataAccess.Objects;
using Simbir.GO.Models;

namespace Simbir.GO.Services.Interfaces;

public interface ITokenService
{
    public string GenerateJwt(string userName);
    public RefreshToken GenerateRefreshToken();
    public Response RefreshJwt(string jwt);
}