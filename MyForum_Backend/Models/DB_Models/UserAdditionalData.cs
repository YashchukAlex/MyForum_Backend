using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models.DB_Models
{
    [Table("UserAdditionalData")]
    public class UserAdditionalData : IdentityDbContext
    {
        [Key]
        [Required]
        public int UserAdditionalDataID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        public string NameImage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastOnline { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateAccount { get; set; }

        #region Foreign keys
        [ForeignKey("ApplicationUser")]
        public string UserRefID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } 
        #endregion
    }
}