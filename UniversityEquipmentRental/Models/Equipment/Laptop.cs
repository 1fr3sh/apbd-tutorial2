namespace UniversityEquipmentRental.Models.Equipment;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string ProcessorModel { get; set; }

    public Laptop(string name, string description, int ramGb, string processorModel)
        : base(name, description)
    {
        RamGb = ramGb;
        ProcessorModel = processorModel;
    }

    public override string GetEquipmentDetails()
    {
        return $"Laptop: {Name} | RAM: {RamGb}GB | Processor: {ProcessorModel} | {(IsAvailable ? "Available" : "Not Available")}";
    }
}
