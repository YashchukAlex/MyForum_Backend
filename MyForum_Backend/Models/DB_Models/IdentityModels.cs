using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MyForum_Backend.Models.DB_Models;

namespace MyForum_Backend.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        #region Additional data to User

        public string NameImage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastOnline { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTimeAccount { get; set; } 
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDBInitialize
    {
        public ApplicationDBInitialize(ApplicationDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                ApplicationUserManager _userManager =
                    new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                RoleManager<IdentityRole> _roleManager =
                    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                #region Add Roles

                _roleManager.Create(new IdentityRole("Super admin"));
                _roleManager.Create(new IdentityRole("Admin"));
                _roleManager.Create(new IdentityRole("User"));

                #endregion

                #region Add Topics Statuses

                IList<Status> Statuses = new List<Status>
                {
                    new Status {Name = "New"},
                    new Status {Name = "Moderation"},
                    new Status {Name = "Blocked"}
                };
                context.Statuses.AddRange(Statuses);

                #endregion

                #region Add Default Super Admin

                RegisterBindingModel model = new RegisterBindingModel
                {
                    Email = "Test@gmail.com",
                    Login = "Admin",
                    Password = "test3132019",
                    ConfirmPassword = "test3132019",
                };

                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Login,
                    CreateTimeAccount = DateTime.Now.ToLocalTime(),
                    LastOnline = DateTime.Now.ToLocalTime()
                };

                var result = _userManager.Create(user, model.Password);
                if (result.Succeeded)
                    _userManager.AddToRole(user.Id, "Super admin");

                #endregion

                context.SaveChanges();
            }
        }
    }
}