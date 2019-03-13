using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models.DB_Models
{
    [Table("CommentRating")]
    public class CommentRating
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentRatingID { get; set; }

        [Required]
        public bool Rating { get; set; }

        #region Foreign keys
        [ForeignKey("ApplicationUser")]
        public string UserRefID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Comment")]
        public int CommentRefID { get; set; }
        public virtual Comment Comment { get; set; } 
        #endregion
    }
}