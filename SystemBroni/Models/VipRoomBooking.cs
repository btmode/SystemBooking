using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemBroni.Models
{
    public class VipRoomBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public VipRoom VipRoom { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }
    }
}
