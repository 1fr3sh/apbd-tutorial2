namespace UniversityEquipmentRental.Services;

using UniversityEquipmentRental.Exceptions;
using UniversityEquipmentRental.Models;

public class RentalService : IRentalService
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly List<Rental> _rentals = new();

    public RentalService(IEquipmentService equipmentService, IUserService userService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
    }

    public Rental RentEquipment(int userId, int equipmentId, int daysToRent)
    {
        var user = _userService.GetUserById(userId);
        var equipment = _equipmentService.GetEquipmentById(equipmentId);

        if (!equipment.IsAvailable)
            throw new EquipmentNotAvailableException(equipment.Name);

        var activeRentals = GetActiveRentalsByUser(userId);
        if (activeRentals.Count >= user.MaxActiveRentals)
            throw new RentalLimitExceededException(user.GetFullName(), user.MaxActiveRentals);

        equipment.IsAvailable = false;

        var rental = new Rental(user, equipment, DateTime.Now, daysToRent);
        _rentals.Add(rental);

        return rental;
    }

    public Rental ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId && r.IsActive)
                     ?? throw new RentalNotFoundException(rentalId);

        rental.CompleteReturn(returnDate);
        rental.Equipment.IsAvailable = true;

        return rental;
    }

    public List<Rental> GetActiveRentalsByUser(int userId)
    {
        return _rentals.Where(r => r.User.Id == userId && r.IsActive).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _rentals.Where(r => r.IsActive && r.DueDate < DateTime.Now).ToList();
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }
}
