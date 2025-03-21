using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemBroni.Models
{
    public enum VipRoomStatus
    {
        Available,   // Свободен
        Booked,      // Забронирован
        Occupied     // Занят (клиенты пришли)

    }
    public class VipRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public int Capacity { get; set; } 

        public VipRoomStatus Status { get; set; } = VipRoomStatus.Available; 
    }
}
