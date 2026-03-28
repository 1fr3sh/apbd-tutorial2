namespace UniversityEquipmentRental.Services;

public class ReportService : IReportService
{
    private readonly IEquipmentService _equipmentService;
    private readonly IRentalService _rentalService;
    private readonly IUserService _userService;

    public ReportService(IEquipmentService equipmentService, IRentalService rentalService, IUserService userService)
    {
        _equipmentService = equipmentService;
        _rentalService = rentalService;
        _userService = userService;
    }

    public void GenerateSummaryReport()
    {
        var allEquipment = _equipmentService.GetAllEquipment();
        var allRentals = _rentalService.GetAllRentals();
        var overdueRentals = _rentalService.GetOverdueRentals();
        var allUsers = _userService.GetAllUsers();

        int totalEquipment = allEquipment.Count;
        int availableEquipment = allEquipment.Count(e => e.IsAvailable);
        int rentedEquipment = totalEquipment - availableEquipment;
        int activeRentals = allRentals.Count(r => r.IsActive);
        int overdueCount = overdueRentals.Count;
        decimal totalPenalties = allRentals.Where(r => r.Penalty.HasValue).Sum(r => r.Penalty!.Value);

        Console.WriteLine("\n========== SUMMARY REPORT ==========");
        Console.WriteLine($"Total users:              {allUsers.Count}");
        Console.WriteLine($"Total equipment:          {totalEquipment}");
        Console.WriteLine($"Available equipment:      {availableEquipment}");
        Console.WriteLine($"Currently rented:         {rentedEquipment}");
        Console.WriteLine($"Active rentals:           {activeRentals}");
        Console.WriteLine($"Overdue rentals:          {overdueCount}");
        Console.WriteLine($"Total penalties collected: {totalPenalties:C}");
        Console.WriteLine("====================================\n");
    }
}
