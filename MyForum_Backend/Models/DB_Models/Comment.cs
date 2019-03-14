using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models.DB_Models
{
    using System;

    [Table("Comment")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Rating { get; set; }

        #region Foreign keys
        [ForeignKey("Topic")]
        public int TopicRefID { get; set; }
        public virtual Topic Topic { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserRefID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } 
        #endregion
    }
}