using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;
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
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

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

    public class ApplicationDBInitialize : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        async protected override void Seed(ApplicationDbContext context)
        {
            ApplicationUserManager _userManager = 
                    new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> _roleManager =
                    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            #region Add Roles
            await _roleManager.CreateAsync(new IdentityRole("Super admin"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("User"));
            #endregion

            #region Add Topics Statuses
            IList<Status> Statuses = new List<Status>
            {
                new Status { Name = "New" },
                new Status { Name = "Moderation" },
                new Status { Name = "Blocked" }
            };
            context.Statuses.AddRange(Statuses);
            #endregion

            #region Add Default Super Admin
            RegisterBindingModel model = new RegisterBindingModel
            {
                Email = "Test@gmail.com",
                Login = "Test",
                Password = "date&3&13&2019",
                ConfirmPassword = "date&3&13&2019",
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                Login = model.Login,
                CreateTimeAccount = DateTime.Now.ToLocalTime(),
                LastOnline = DateTime.Now.ToLocalTime()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user.Id, "Super admin"); 
            #endregion

            base.Seed(context);
        }
    }
}