namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TutorModule")]
    public partial class TutorModule
    {
        [Key]
        [StringLength(10)]
        public string studNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string moduleCode { get; set; }

        public int passmark { get; set; }
    }
}
