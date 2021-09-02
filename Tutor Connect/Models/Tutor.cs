namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tutor")]
    public partial class Tutor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TutorId { get; set; }

        [Required]
        [StringLength(50)]
        public string tutorType { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public string Image { get; set; }
    }
}
