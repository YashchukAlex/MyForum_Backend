using MyForum_Backend.Models;

namespace MyForum_Backend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyForum_Backend.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MyForum_Backend.Models.ApplicationDbContext context)
        {
            new ApplicationDBInitialize(context);
        }
    }
}
