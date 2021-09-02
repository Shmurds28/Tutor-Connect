namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        
        public int userId { get; set; }

        [Required]
        [StringLength(10)]
        public string username { get; set; }

        [Required]
        [StringLength(10)]
        public string password { get; set; }

        [Required]
        [StringLength(10)]
        public string type { get; set; }
    }
}
