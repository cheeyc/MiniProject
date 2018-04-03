namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public Guid Id { get; set; }

        [StringLength(200)]
        [MaxLength(160, ErrorMessage ="Maximum 160 characters")]
        [MinLength(20, ErrorMessage = "Minimum 20 characters")]
        [RegularExpression(@"^[^\$\#\@\&\%\€]+$", ErrorMessage = @"The following special characters are not allowed: $, #, @, &, %, €")]
        [Required]
        public string Message { get; set; }

        public DateTime? TimeStamp { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        [StringLength(50)]
        [DisplayName("Document")]
        public string FileName { get; set; }

    }
}
