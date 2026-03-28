namespace UniversityEquipmentRental.Exceptions;

public class RentalNotFoundException : Exception
{
    public RentalNotFoundException(int rentalId)
        : base($"Rental with ID {rentalId} was not found.")
    {
    }
}
