using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface IUserRepository
{
    public Task AddUser(User user);
    public Task<User> GetUserByLogin(string login);
    public Task SaveChanges();
}