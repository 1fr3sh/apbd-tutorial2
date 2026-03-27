namespace UniversityEquipmentRental.Services;

using UniversityEquipmentRental.Exceptions;
using UniversityEquipmentRental.Models.Equipment;

public class EquipmentService : IEquipmentService
{
    private readonly List<Equipment> _equipmentList = new();

    public void AddEquipment(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipmentList;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipmentList.Where(e => e.IsAvailable).ToList();
    }

    public Equipment GetEquipmentById(int id)
    {
        return _equipmentList.FirstOrDefault(e => e.Id == id)
               ?? throw new EquipmentNotFoundException(id);
    }

    public void MarkAsUnavailable(int equipmentId)
    {
        var equipment = GetEquipmentById(equipmentId);
        equipment.IsAvailable = false;
    }
}
