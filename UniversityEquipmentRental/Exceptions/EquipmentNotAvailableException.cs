namespace UniversityEquipmentRental.Exceptions;

public class EquipmentNotAvailableException : Exception
{
    public EquipmentNotAvailableException(string equipmentName)
        : base($"Equipment '{equipmentName}' is not available for rental.")
    {
    }
}
