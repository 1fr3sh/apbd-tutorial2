namespace UniversityEquipmentRental.Models;

using UniversityEquipmentRental.Models.Users;

public class Rental
{
    private static int _nextId = 1;
    private const decimal PenaltyPerDay = 10m;

    public int Id { get; }
    public User User { get; }
    public Equipment.Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ActualReturnDate { get; private set; }
    public decimal? Penalty { get; private set; }
    public bool IsActive => ActualReturnDate == null;

    public Rental(User user, Equipment.Equipment equipment, DateTime rentalDate, int daysToRent)
    {
        Id = _nextId++;
        User = user;
        Equipment = equipment;
        RentalDate = rentalDate;
        DueDate = rentalDate.AddDays(daysToRent);
    }

    public void CompleteReturn(DateTime returnDate)
    {
        ActualReturnDate = returnDate;

        if (returnDate > DueDate)
        {
            int overdueDays = (returnDate.Date - DueDate.Date).Days;
            Penalty = overdueDays * PenaltyPerDay;
        }
        else
        {
            Penalty = 0;
        }
    }
}
