using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ioasys.IMDb.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceProvider CreateBasicRoles(this IServiceProvider service, params string[] rolesNames)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityResult result;
            foreach (var name in rolesNames)
            {
                var roleExist = roleManager.RoleExistsAsync(name).Result;
                if (!roleExist)
                {
                    result = roleManager.CreateAsync(new IdentityRole(name)).Result;
                }
            }

            return service;
        }
    }
}
