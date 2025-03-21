using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemBroni.Models
{
    public class VipRoomBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int VipRoomId { get; set; }

        [ForeignKey("VipRoomId")]
        public VipRoom VipRoom { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }
    }
}
