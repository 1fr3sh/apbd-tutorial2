namespace UniversityEquipmentRental.Models.Equipment;

public class Projector : Equipment
{
    public int LumensCount { get; set; }
    public string ResolutionType { get; set; }

    public Projector(string name, string description, int lumensCount, string resolutionType)
        : base(name, description)
    {
        LumensCount = lumensCount;
        ResolutionType = resolutionType;
    }

    public override string GetEquipmentDetails()
    {
        return $"Projector: {Name} | Lumens: {LumensCount} | Resolution: {ResolutionType} | {(IsAvailable ? "Available" : "Not Available")}";
    }
}
