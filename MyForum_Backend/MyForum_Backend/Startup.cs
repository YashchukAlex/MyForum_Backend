using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Owin;
using MyForum_Backend.Migrations;
using MyForum_Backend.Models;
using Owin;

[assembly: OwinStartup(typeof(MyForum_Backend.Startup))]

namespace MyForum_Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            new MigrateDatabaseToLatestVersion<ApplicationDbContext,Configuration>().InitializeDatabase(ApplicationDbContext.Create());
        }
    }
}
