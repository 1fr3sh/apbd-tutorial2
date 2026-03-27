namespace UniversityEquipmentRental.Services;

using UniversityEquipmentRental.Exceptions;
using UniversityEquipmentRental.Models.Users;

public class UserService : IUserService
{
    private readonly List<User> _users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id)
               ?? throw new UserNotFoundException(id);
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }
}
