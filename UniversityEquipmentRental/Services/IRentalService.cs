namespace UniversityEquipmentRental.Services;

using UniversityEquipmentRental.Models;

public interface IRentalService
{
    Rental RentEquipment(int userId, int equipmentId, int daysToRent);
    Rental ReturnEquipment(int rentalId, DateTime returnDate);
    List<Rental> GetActiveRentalsByUser(int userId);
    List<Rental> GetOverdueRentals();
    List<Rental> GetAllRentals();
}
