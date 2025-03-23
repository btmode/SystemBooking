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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int Number { get; set; } 

        [Required]
        public int Capacity { get; set; } 
 
        public TableStatus Status { get; set; } = TableStatus.Available;
    }
}
