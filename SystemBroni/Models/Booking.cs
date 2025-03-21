using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemBroni.Models
{
    public class Booking
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } // Связь с пользователем

        public int TableId { get; set; }

        [ForeignKey("TableId")]
        public Table? Table { get; set; }

        public int? VipRoomId { get; set; }
        [ForeignKey("VipRoomId")]
        public VipRoom? VipRoom { get; set; }

        [Required]
        public DateTime BookingTime { get; set; } // Дата и время брони
    }
}
