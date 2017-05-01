namespace SchoolSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolSystem.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;
        }
        
        protected override void Seed(SchoolSystem.Data.ApplicationDbContext context)
        {
            
        }
    }
}
