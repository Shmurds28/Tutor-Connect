namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slot
    {

        public int slotId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Module: ")]
        public string moduleCode { get; set; }
        [Display(Name = "Time: ")]
        public TimeSpan startTime { get; set; }

        public TimeSpan endTime { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Session Day: ")]
        public string date { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Session Type: ")]
        public string TypeOfSlot { get; set; }
        [Display(Name = "Student Number: ")]
        public string tutorId { get; set; }
    }
}

