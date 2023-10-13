using Microsoft.AspNetCore.Mvc;
using Simbir.GO.DataAccess.Objects;
using Simbir.GO.Models;

namespace Simbir.GO.Repositories.Interfaces;

public interface IUserService
{
    public void Create(string name, string password);
    public User Get(string name);
    public void Update(User user);
    public bool Delete(int id);
    public Response Authenticate(LoginModel model);
}