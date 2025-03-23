using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemBroni.Models
{
    public enum TableStatus
    {
        Available,   // Свободен
        Booked,      // Забронирован
        Occupied     // Занят (клиенты пришли)
    }

    public class Table
    {
        public Guid Id { get; set; }

        public int Number { get; set; } 

        public int Capacity { get; set; } 
 
        public TableStatus Status { get; set; } = TableStatus.Available;
    }
}
