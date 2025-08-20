﻿using Microsoft.AspNetCore.Identity;

namespace RealEstatePlatform_API.Data
{
    public class DbSeeder
    {
        public static  async Task SeadRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "Agent", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
    }
}
