namespace UniversityEquipmentRental.Services;

using UniversityEquipmentRental.Models.Users;

public interface IUserService
{
    void AddUser(User user);
    User GetUserById(int id);
    List<User> GetAllUsers();
}
