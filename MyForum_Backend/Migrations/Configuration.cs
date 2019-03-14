namespace MyForum_Backend.Migrations
{
    using MyForum_Backend.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyForum_Backend.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyForum_Backend.Models.ApplicationDbContext context)
        {
            ApplicationDBInitialize initialize = new ApplicationDBInitialize(context);
        }
    }
}
