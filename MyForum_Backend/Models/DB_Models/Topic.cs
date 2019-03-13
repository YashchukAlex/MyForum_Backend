using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models.DB_Models
{
    [Table("Topic")]
    public class Topic : IdentityDbContext
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastActive { get; set; }

        #region Foreign keys
        [ForeignKey("Category")]
        public int CategoryRefID { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserRefID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Status")]
        public string StatusRefID { get; set; }
        public virtual Status Status { get; set; } 
        #endregion
    }
}