using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models.DB_Models
{
    [Table("Status")]
    public class Status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusID { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}