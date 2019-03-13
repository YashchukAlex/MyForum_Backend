using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyForum_Backend.Models.Binding_Models
{
    public class RoleBindingModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class UserRolesAddBindingModel
    {
        [Required]
        public string[] NewRoles { get; set; }
    }
}