namespace UniversityEquipmentRental.Models.Equipment;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string Description { get; set; }

    protected Equipment(string name, string description)
    {
        Id = _nextId++;
        Name = name;
        Description = description;
    }

    public abstract string GetEquipmentDetails();
}
