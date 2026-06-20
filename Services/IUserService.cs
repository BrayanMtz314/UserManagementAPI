using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User? GetById(int id);
    User Add(User user);
    bool Update(int id, User updatedUser);
    bool Delete(int id);
}