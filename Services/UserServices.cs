using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new();
    private readonly object _lock = new();

    public IEnumerable<User> GetAll()
    {
        lock (_lock)
        {
            // Return a new list to prevent concurrent modification errors during iteration
            return _users.ToList(); 
        }
    }

    public User? GetById(int id)
    {
        lock (_lock)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }

    public User Add(User user)
    {
        lock (_lock)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return user;
        }
    }

    public bool Update(int id, User updatedUser)
    {
        lock (_lock)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null) return false;

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            _users.Remove(user);
            return true;
        }
    }
}