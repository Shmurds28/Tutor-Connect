namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [Key]
        [StringLength(10)]
        public string StudNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string fieldOfStudy { get; set; }

        public string yearOfStudy { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public byte[] Image { get; set; }
    }
}
