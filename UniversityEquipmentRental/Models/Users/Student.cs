namespace UniversityEquipmentRental.Models.Users;

public class Student : User
{
    public Student(string firstName, string lastName)
        : base(firstName, lastName)
    {
    }

    public override int MaxActiveRentals => 2;
    public override string UserType => "Student";
}
