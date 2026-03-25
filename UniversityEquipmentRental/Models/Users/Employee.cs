namespace UniversityEquipmentRental.Models.Users;

public class Employee : User
{
    public Employee(string firstName, string lastName)
        : base(firstName, lastName)
    {
    }

    public override int MaxActiveRentals => 5;
    public override string UserType => "Employee";
}
