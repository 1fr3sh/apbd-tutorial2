# University Equipment Rental System

## Description
Console application in C# for managing university equipment rentals.
Built as Tutorial 2 for the APBD course at PJATK.

## How to Run
1. Ensure .NET 8 SDK is installed
2. Clone the repository
3. Run: `dotnet run` in the `UniversityEquipmentRental/` directory

## Design Decisions

### Project Structure and Separation of Concerns
- **Models/** — domain objects only, no business logic
- **Services/** — business logic, each service has a single responsibility
- **UI/** — console interface, separated from business logic
- **Exceptions/** — explicit error handling through custom exceptions

### Cohesion
Each class has one clear purpose:
- `Equipment` and subtypes — represent domain objects with type-specific data
- `RentalService` — manages rental operations and enforces business rules
- `ConsoleMenu` — handles user interaction only, delegates logic to services

### Coupling
Services depend on interfaces (IEquipmentService, etc.), not concrete classes.
This follows the Dependency Inversion Principle — high-level modules don't depend
on low-level modules, both depend on abstractions.

### Inheritance
- `Equipment` is abstract because every piece of equipment has a specific type —
  there's no such thing as "generic equipment" in the domain
- `User` is abstract because every user must be either a Student or Employee,
  with different rental limits
- Inheritance here models real domain relationships, not artificial OOP for its own sake

### Error Handling
Custom exceptions (RentalLimitExceededException, EquipmentNotAvailableException)
make error cases explicit and self-documenting.

### Business Rules Centralization
- Rental limits are defined in User subclasses (MaxActiveRentals property)
- Penalty calculation is in one place (Rental.CompleteReturn)
- These rules are easy to find and modify without touching multiple files
