namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Module")]
    public partial class Module
    {
        [Key]
        [StringLength(10)]
        public string moduleCode { get; set; }

        [Required]
        [StringLength(50)]
        public string moduleName { get; set; }

        public int depId { get; set; }
    }
}
