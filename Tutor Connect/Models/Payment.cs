namespace Tutor_Connect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int paymentId { get; set; }

        [Required]
        [StringLength(10)]
        public string studNumber { get; set; }

        public DateTime datePaid { get; set; }

        [Column(TypeName = "money")]
        public decimal? amount { get; set; }
    }
}
