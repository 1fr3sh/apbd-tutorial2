namespace UniversityEquipmentRental.Exceptions;

public class EquipmentNotFoundException : Exception
{
    public EquipmentNotFoundException(int equipmentId)
        : base($"Equipment with ID {equipmentId} was not found.")
    {
    }
}
