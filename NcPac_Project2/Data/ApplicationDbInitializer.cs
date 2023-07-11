using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace NcPac_Project2.Data
{
    public static class ApplicationDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                ////Delete the database if you need to apply a new Migration
                //context.Database.EnsureDeleted();
                //Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                //Create Roles
                var RoleManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Top Admin", "Admin", "User" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                //Create Users

                //Top Admin
                var userManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (userManager.FindByEmailAsync("TopAdmin@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "TopAdmin@outlook.com",
                        Email = "TopAdmin@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Top Admin").Wait();
                    }
                }

                //Admin
                if (userManager.FindByEmailAsync("Admin@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "Admin@outlook.com",
                        Email = "Admin@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
                //User
                //if (userManager.FindByEmailAsync("user@outlook.com").Result == null)
                //{
                //    IdentityUser user = new IdentityUser
                //    {
                //        UserName = "user@outlook.com",
                //        Email = "user@outlook.com"
                //    };

                //    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                //    if (result.Succeeded)
                //    {
                //        userManager.AddToRoleAsync(user, "User").Wait();
                //    }
                //}
                if (userManager.FindByEmailAsync("MatthewsRoy@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "MatthewsRoy@outlook.com",
                        Email = "MatthewsRoy@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("KaiserGreg@gmail.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "KaiserGreg@gmail.com",
                        Email = "KaiserGreg@gmail.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("JonesMark@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "JonesMark@outlook.com",
                        Email = "JonesMark@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("WardDennis@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "WardDennis@outlook.com",
                        Email = "WardDennis@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("RumellAlicia@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "RumellAlicia@outlook.com",
                        Email = "RumellAlicia@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("BobJackson@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "BobJackson@outlook.com",
                        Email = "BobJackson@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("DDymond@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "DDymond@outlook.com",
                        Email = "DDymond@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("CWoods@gamil.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "CWoods@gamil.com",
                        Email = "CWoods@gamil.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("KellyJohnstone@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "KellyJohnstone@outlook.com",
                        Email = "KellyJohnstone@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("RyanMurphy@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "RyanMurphy@outlook.com",
                        Email = "RyanMurphy@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa$$w@rd").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }

}
