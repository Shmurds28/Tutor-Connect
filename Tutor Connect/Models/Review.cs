namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int reviewId { get; set; }

        [Column("review")]
        [Required]
        public string review1 { get; set; }

        [Required]
        [StringLength(10)]
        public string studNumber { get; set; }

        public string Date { get; set; }
        public int tutorId { get; set; }
    }
}
