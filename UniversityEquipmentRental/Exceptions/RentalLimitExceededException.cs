namespace UniversityEquipmentRental.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(string userName, int maxRentals)
        : base($"User '{userName}' has reached the maximum rental limit of {maxRentals}.")
    {
    }
}
