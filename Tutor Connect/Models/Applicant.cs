namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Applicant")]
    public partial class Applicant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int applicantId { get; set; }

        [Required]
        [StringLength(10)]
        public string studNumber { get; set; }

        [StringLength(10)]
        public string applicationDate { get; set; }

        [StringLength(10)]
        public string moduleCode { get; set; }
    }
}
