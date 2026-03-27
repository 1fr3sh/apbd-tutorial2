namespace UniversityEquipmentRental.Services;

using UniversityEquipmentRental.Models.Equipment;

public interface IEquipmentService
{
    void AddEquipment(Equipment equipment);
    List<Equipment> GetAllEquipment();
    List<Equipment> GetAvailableEquipment();
    Equipment GetEquipmentById(int id);
    void MarkAsUnavailable(int equipmentId);
}
