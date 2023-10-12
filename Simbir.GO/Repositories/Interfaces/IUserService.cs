using Simbir.GO.DataAccess.Objects;

namespace Simbir.GO.Repositories.Interfaces;

public interface IUserService
{
    public void Create(string name, string password);
    public User Get(int id);
    public User Get(string name);
    public void Update(User user);
    public bool Delete(int id);
}