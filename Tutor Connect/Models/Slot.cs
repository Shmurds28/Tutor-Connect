namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int slotId { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfSlot { get; set; }

        public int? tutorId { get; set; }
    }
}
