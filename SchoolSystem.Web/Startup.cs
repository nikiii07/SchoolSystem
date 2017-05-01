using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolSystem.Web.Startup))]
namespace SchoolSystem.Web
{
    using System;
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            this.InitializeUserRoles();
        }

        private void InitializeUserRoles()
        {
            var userStore = new UserStore<ApplicationUser>(ApplicationDbContext.GetContext);
            ApplicationUserManager userManager = new ApplicationUserManager(userStore);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.GetContext));

            if (!roleManager.RoleExists("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                roleManager.Create(adminRole);

                var adminProfile = new ApplicationUser()
                {
                    UserName = "admin",
                    Address = "At home",
                    BirthPlace = "A hospital",
                    FirstName = "Admin",
                    MiddleName = "Adminov",
                    LastName = "Adminov",
                    IsPublic = true,
                    Pin = "9999999999"
                };

                userManager.Create(adminProfile, "qwerty");

                var persistedAdmin = userManager.Find("admin", "qwerty");
                userManager.AddToRole(persistedAdmin.Id, "Admin");

                var admin = new Teacher();
                admin.Profile = adminProfile;
                ApplicationDbContext.GetContext.Teachers.Add(admin);
            }

            if (!roleManager.RoleExists("Teacher"))
            {
                var teacherRole = new IdentityRole("Teacher");
                roleManager.Create(teacherRole);

                var admin = userManager.Find("admin", "qwerty");
                userManager.AddToRole(admin.Id, "Teacher");
            }
        }
    }
}
