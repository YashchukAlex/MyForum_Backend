using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MyForum_Backend.Models.DB_Models;

namespace MyForum_Backend.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRating> CommentRatings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public ApplicationDbContext()
            : base("DB_Forum")
        {
            Database.SetInitializer(new ApplicationDBInitialize());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasIndex(l => l.Name)
                .IsUnique();

            modelBuilder.Entity<Status>()
                .HasIndex(l => l.Name)
                .IsUnique();

            modelBuilder.Entity<CommentRating>()
                .HasIndex(l => new { l.CommentRefID, l.UserRefID })
                .IsUnique(true);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(l => l.Login)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(l => l.NameImage)
                .IsUnique();
        }
    }
}