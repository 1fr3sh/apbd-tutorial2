namespace UniversityEquipmentRental.UI;

using UniversityEquipmentRental.Exceptions;
using UniversityEquipmentRental.Models.Equipment;
using UniversityEquipmentRental.Models.Users;
using UniversityEquipmentRental.Services;

public class ConsoleMenu
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;
    private readonly IReportService _reportService;

    public ConsoleMenu(
        IEquipmentService equipmentService,
        IUserService userService,
        IRentalService rentalService,
        IReportService reportService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
        _reportService = reportService;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            DisplayMenu();
            var choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1": AddEquipment(); break;
                    case "2": AddUser(); break;
                    case "3": DisplayAllEquipment(); break;
                    case "4": DisplayAvailableEquipment(); break;
                    case "5": RentEquipment(); break;
                    case "6": ReturnEquipment(); break;
                    case "7": MarkEquipmentUnavailable(); break;
                    case "8": DisplayActiveRentalsForUser(); break;
                    case "9": DisplayOverdueRentals(); break;
                    case "10": GenerateReport(); break;
                    case "0": running = false; Console.WriteLine("Goodbye!"); break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }
            }
            catch (UserNotFoundException ex) { Console.WriteLine($"Error: {ex.Message}"); }
            catch (EquipmentNotFoundException ex) { Console.WriteLine($"Error: {ex.Message}"); }
            catch (EquipmentNotAvailableException ex) { Console.WriteLine($"Error: {ex.Message}"); }
            catch (RentalLimitExceededException ex) { Console.WriteLine($"Error: {ex.Message}"); }
            catch (RentalNotFoundException ex) { Console.WriteLine($"Error: {ex.Message}"); }
            catch (FormatException) { Console.WriteLine("Error: Invalid input format. Please enter a valid value."); }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\n=== University Equipment Rental System ===");
        Console.WriteLine("1.  Add new equipment");
        Console.WriteLine("2.  Add new user");
        Console.WriteLine("3.  Display all equipment");
        Console.WriteLine("4.  Display available equipment");
        Console.WriteLine("5.  Rent equipment");
        Console.WriteLine("6.  Return equipment");
        Console.WriteLine("7.  Mark equipment as unavailable");
        Console.WriteLine("8.  Display active rentals for a user");
        Console.WriteLine("9.  Display overdue rentals");
        Console.WriteLine("10. Generate summary report");
        Console.WriteLine("0.  Exit");
        Console.Write("Select an option: ");
    }

    private void AddEquipment()
    {
        Console.WriteLine("Select equipment type: 1 - Laptop, 2 - Projector, 3 - Camera");
        var type = Console.ReadLine()?.Trim();

        Console.Write("Name: ");
        var name = Console.ReadLine()!;
        Console.Write("Description: ");
        var description = Console.ReadLine()!;

        switch (type)
        {
            case "1":
                Console.Write("RAM (GB): ");
                int ram = int.Parse(Console.ReadLine()!);
                Console.Write("Processor model: ");
                var processor = Console.ReadLine()!;
                _equipmentService.AddEquipment(new Laptop(name, description, ram, processor));
                break;
            case "2":
                Console.Write("Lumens: ");
                int lumens = int.Parse(Console.ReadLine()!);
                Console.Write("Resolution (e.g. Full HD, 4K): ");
                var resolution = Console.ReadLine()!;
                _equipmentService.AddEquipment(new Projector(name, description, lumens, resolution));
                break;
            case "3":
                Console.Write("Megapixels: ");
                double mp = double.Parse(Console.ReadLine()!);
                Console.Write("Has video recording? (yes/no): ");
                bool video = Console.ReadLine()!.Trim().ToLower() == "yes";
                _equipmentService.AddEquipment(new Camera(name, description, mp, video));
                break;
            default:
                Console.WriteLine("Invalid equipment type.");
                return;
        }

        Console.WriteLine("Equipment added successfully.");
    }

    private void AddUser()
    {
        Console.WriteLine("Select user type: 1 - Student, 2 - Employee");
        var type = Console.ReadLine()?.Trim();

        Console.Write("First name: ");
        var firstName = Console.ReadLine()!;
        Console.Write("Last name: ");
        var lastName = Console.ReadLine()!;

        switch (type)
        {
            case "1":
                _userService.AddUser(new Student(firstName, lastName));
                break;
            case "2":
                _userService.AddUser(new Employee(firstName, lastName));
                break;
            default:
                Console.WriteLine("Invalid user type.");
                return;
        }

        Console.WriteLine("User added successfully.");
    }

    private void DisplayAllEquipment()
    {
        var equipment = _equipmentService.GetAllEquipment();
        if (equipment.Count == 0)
        {
            Console.WriteLine("No equipment registered.");
            return;
        }

        Console.WriteLine("\n--- All Equipment ---");
        foreach (var e in equipment)
            Console.WriteLine($"  [ID: {e.Id}] {e.GetEquipmentDetails()}");
    }

    private void DisplayAvailableEquipment()
    {
        var equipment = _equipmentService.GetAvailableEquipment();
        if (equipment.Count == 0)
        {
            Console.WriteLine("No available equipment.");
            return;
        }

        Console.WriteLine("\n--- Available Equipment ---");
        foreach (var e in equipment)
            Console.WriteLine($"  [ID: {e.Id}] {e.GetEquipmentDetails()}");
    }

    private void RentEquipment()
    {
        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine()!);
        Console.Write("Equipment ID: ");
        int equipmentId = int.Parse(Console.ReadLine()!);
        Console.Write("Number of days to rent: ");
        int days = int.Parse(Console.ReadLine()!);

        var rental = _rentalService.RentEquipment(userId, equipmentId, days);
        Console.WriteLine($"Rental created successfully. Rental ID: {rental.Id}, Due date: {rental.DueDate:yyyy-MM-dd}");
    }

    private void ReturnEquipment()
    {
        Console.Write("Rental ID: ");
        int rentalId = int.Parse(Console.ReadLine()!);
        Console.Write("Return date (yyyy-MM-dd) or press Enter for today: ");
        var dateInput = Console.ReadLine()?.Trim();

        DateTime returnDate = string.IsNullOrEmpty(dateInput)
            ? DateTime.Now
            : DateTime.Parse(dateInput);

        var rental = _rentalService.ReturnEquipment(rentalId, returnDate);
        Console.WriteLine($"Equipment '{rental.Equipment.Name}' returned successfully.");
        Console.WriteLine($"  Penalty: {rental.Penalty:C}");
    }

    private void MarkEquipmentUnavailable()
    {
        Console.Write("Equipment ID: ");
        int id = int.Parse(Console.ReadLine()!);
        _equipmentService.MarkAsUnavailable(id);
        Console.WriteLine("Equipment marked as unavailable.");
    }

    private void DisplayActiveRentalsForUser()
    {
        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine()!);

        var rentals = _rentalService.GetActiveRentalsByUser(userId);
        if (rentals.Count == 0)
        {
            Console.WriteLine("No active rentals for this user.");
            return;
        }

        Console.WriteLine("\n--- Active Rentals ---");
        foreach (var r in rentals)
            Console.WriteLine($"  Rental ID: {r.Id} | Equipment: {r.Equipment.Name} | Due: {r.DueDate:yyyy-MM-dd}");
    }

    private void DisplayOverdueRentals()
    {
        var rentals = _rentalService.GetOverdueRentals();
        if (rentals.Count == 0)
        {
            Console.WriteLine("No overdue rentals.");
            return;
        }

        Console.WriteLine("\n--- Overdue Rentals ---");
        foreach (var r in rentals)
        {
            int overdueDays = (DateTime.Now.Date - r.DueDate.Date).Days;
            Console.WriteLine($"  Rental ID: {r.Id} | User: {r.User.GetFullName()} | Equipment: {r.Equipment.Name} | Overdue by {overdueDays} days");
        }
    }

    private void GenerateReport()
    {
        _reportService.GenerateSummaryReport();
    }
}
