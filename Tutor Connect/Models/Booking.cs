namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int bookingId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfBooking { get; set; }

        [Required]
        [StringLength(10)]
        public string studNumber { get; set; }

        public DateTime bookingDate { get; set; }

        public int slotId { get; set; }
    }
}
