using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByTag(string tag);
    Task UpdateUser(User user);
    Task AddUser(User user);
    Task DeleteUser(User user);
}