using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models.DB_Models
{
    [Table("Status")]
    public class Status : IdentityDbContext
    {
        [Key]
        [Required]
        public int StatusID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}