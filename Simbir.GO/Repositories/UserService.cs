using Simbir.GO.DataAccess;
using Simbir.GO.DataAccess.Objects;
using Simbir.GO.Repositories.Interfaces;

namespace Simbir.GO.Repositories;

public class UserService : IUserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext context)
    {
        _db = context;
    }

    public void Create(string name, string password)
    {
        var passwordHash = PasswordHasher.Hash(password);
        var user = new User() { Name = name, PasswordHash = passwordHash };
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public User Get(int id)
    {
        return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    public User Get(string name)
    {
        return _db.Users.FirstOrDefault(u => u.Name == name);
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}