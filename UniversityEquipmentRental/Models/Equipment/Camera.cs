namespace UniversityEquipmentRental.Models.Equipment;

public class Camera : Equipment
{
    public double MegaPixels { get; set; }
    public bool HasVideoRecording { get; set; }

    public Camera(string name, string description, double megaPixels, bool hasVideoRecording)
        : base(name, description)
    {
        MegaPixels = megaPixels;
        HasVideoRecording = hasVideoRecording;
    }

    public override string GetEquipmentDetails()
    {
        return $"Camera: {Name} | {MegaPixels}MP | Video: {(HasVideoRecording ? "Yes" : "No")} | {(IsAvailable ? "Available" : "Not Available")}";
    }
}
