using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemBroni.Models
{
    public class TableBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int TableId { get; set; }

        [ForeignKey("TableId")]
        public Table Table { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }
        public string Test { get; set; }
    }
}

