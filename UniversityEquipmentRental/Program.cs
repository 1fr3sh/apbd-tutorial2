using UniversityEquipmentRental.Exceptions;
using UniversityEquipmentRental.Models;
using UniversityEquipmentRental.Models.Equipment;
using UniversityEquipmentRental.Models.Users;
using UniversityEquipmentRental.Services;
using UniversityEquipmentRental.UI;

// --- Create services (manual Dependency Injection) ---
var equipmentService = new EquipmentService();
var userService = new UserService();
var rentalService = new RentalService(equipmentService, userService);
var reportService = new ReportService(equipmentService, rentalService, userService);

// --- Seed demo data ---
SeedDemoData(equipmentService, userService);
RunDemoScenario(equipmentService, userService, rentalService, reportService);

// --- Launch interactive menu ---
var menu = new ConsoleMenu(equipmentService, userService, rentalService, reportService);
menu.Run();

// ========== Demo methods ==========

static void SeedDemoData(IEquipmentService equipmentService, IUserService userService)
{
    Console.WriteLine("=== Seeding demo data... ===\n");

    equipmentService.AddEquipment(new Laptop("Dell XPS 15", "High-performance laptop", 16, "Intel i7-12700H"));
    equipmentService.AddEquipment(new Laptop("MacBook Pro 14", "Apple laptop for development", 32, "Apple M3 Pro"));
    equipmentService.AddEquipment(new Projector("Epson EB-U05", "Classroom projector", 3400, "Full HD"));
    equipmentService.AddEquipment(new Camera("Canon EOS R6", "Professional mirrorless camera", 20.1, true));

    userService.AddUser(new Student("Anna", "Kowalska"));
    userService.AddUser(new Student("Jan", "Nowak"));
    userService.AddUser(new Employee("Dr. Maria", "Wisniewska"));

    Console.WriteLine("Demo data seeded: 4 equipment items, 3 users.\n");
}

static void RunDemoScenario(
    IEquipmentService equipmentService,
    IUserService userService,
    IRentalService rentalService,
    IReportService reportService)
{
    Console.WriteLine("=== Running demo scenario... ===\n");

    // 1. Successful rental — Anna rents Dell XPS 15 for 7 days
    var rental1 = rentalService.RentEquipment(1, 1, 7);
    Console.WriteLine($"[OK] Anna rented '{rental1.Equipment.Name}' (Rental ID: {rental1.Id}, Due: {rental1.DueDate:yyyy-MM-dd})");

    // 2. Successful rental — Anna rents the projector for 3 days
    var rental2 = rentalService.RentEquipment(1, 3, 3);
    Console.WriteLine($"[OK] Anna rented '{rental2.Equipment.Name}' (Rental ID: {rental2.Id}, Due: {rental2.DueDate:yyyy-MM-dd})");

    // 3. Try to exceed rental limit — Anna (Student, max 2) tries to rent a 3rd item
    try
    {
        rentalService.RentEquipment(1, 4, 5);
    }
    catch (RentalLimitExceededException ex)
    {
        Console.WriteLine($"[EXPECTED ERROR] {ex.Message}");
    }

    // 4. Try to rent already rented equipment
    try
    {
        rentalService.RentEquipment(2, 1, 5);
    }
    catch (EquipmentNotAvailableException ex)
    {
        Console.WriteLine($"[EXPECTED ERROR] {ex.Message}");
    }

    // 5. On-time return — Anna returns the projector on time
    var returnedOnTime = rentalService.ReturnEquipment(rental2.Id, DateTime.Now);
    Console.WriteLine($"[OK] Anna returned '{returnedOnTime.Equipment.Name}' on time. Penalty: {returnedOnTime.Penalty:C}");

    // 6. Overdue return — simulate by creating a rental with a past date
    var employee = userService.GetUserById(3);
    var macbook = equipmentService.GetEquipmentById(2);

    // Rent MacBook for employee (Dr. Wisniewska)
    var rental3 = rentalService.RentEquipment(3, 2, 3);
    Console.WriteLine($"[OK] Dr. Wisniewska rented '{rental3.Equipment.Name}' (Rental ID: {rental3.Id}, Due: {rental3.DueDate:yyyy-MM-dd})");

    // Simulate overdue return: return 5 days after due date
    var overdueReturnDate = rental3.DueDate.AddDays(5);
    var returnedLate = rentalService.ReturnEquipment(rental3.Id, overdueReturnDate);
    Console.WriteLine($"[OK] Dr. Wisniewska returned '{returnedLate.Equipment.Name}' late. Penalty: {returnedLate.Penalty:C}");

    // 7. Generate summary report
    Console.WriteLine();
    reportService.GenerateSummaryReport();

    Console.WriteLine("=== Demo scenario complete. Starting interactive menu... ===\n");
}
