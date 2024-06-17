using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HospitalDbContext _context;

    public UserRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<User> GetUserByLogin(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        return user;
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}