using Gimnasio.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gimnasio.Web.Class
{
    public class Utilities
    {
        readonly static ApplicationDbContext db = new ApplicationDbContext();

        public static void CheckRoles(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        internal static void CheckManager()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userAsp = userManager.FindByName("admin@unmir.com");

            if (userAsp == null)
            {
                CreateUserASP("admin@unmir.com", "123456", "Admin");
            }
        }

        public static void CreateUserASP(string email, string password, string rol)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userASP = new ApplicationUser()
            {
                UserName = email,
                Email = email,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, rol);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}