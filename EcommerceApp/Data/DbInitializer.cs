using EcommerceApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roles base del sistema.
            // Admin existe para tu cuenta real, pero NO se crea ningún usuario Admin automático.
            string[] roles = { "Admin", "DemoAdmin", "Cliente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Usuario DemoAdmin público para reclutadores.
            var demoAdminEmail = "demo.admin@zcommerce.cl";
            var demoAdminUser = await userManager.FindByEmailAsync(demoAdminEmail);

            if (demoAdminUser == null)
            {
                var demoAdmin = new ApplicationUser
                {
                    UserName = demoAdminEmail,
                    Email = demoAdminEmail,
                    EmailConfirmed = true,

                    Nombre = "Demo",
                    Apellido = "Admin",
                    PhoneNumber = "+56900000000",
                    TelefonoContacto = "+56900000000",

                    Region = "Región Metropolitana de Santiago",
                    Comuna = "Santiago",
                    Calle = "Av. Demo",
                    Numero = "100",
                    DeptoBlockOficina = "Oficina Demo",

                    FechaRegistro = DateTime.UtcNow
                };

                var resultado = await userManager.CreateAsync(demoAdmin, "DemoAdmin123!");

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(demoAdmin, "DemoAdmin");
                }
            }
            else
            {
                demoAdminUser.Nombre ??= "Demo";
                demoAdminUser.Apellido ??= "Admin";
                demoAdminUser.PhoneNumber ??= "+56900000000";
                demoAdminUser.TelefonoContacto ??= "+56900000000";
                demoAdminUser.Region ??= "Región Metropolitana de Santiago";
                demoAdminUser.Comuna ??= "Santiago";
                demoAdminUser.Calle ??= "Av. Demo";
                demoAdminUser.Numero ??= "100";
                demoAdminUser.DeptoBlockOficina ??= "Oficina Demo";

                await userManager.UpdateAsync(demoAdminUser);

                if (!await userManager.IsInRoleAsync(demoAdminUser, "DemoAdmin"))
                {
                    await userManager.AddToRoleAsync(demoAdminUser, "DemoAdmin");
                }
            }

            // Usuario cliente demo para probar carrito, checkout y flujo normal.
            var clienteEmail = "cliente@zcommerce.cl";
            var clienteUser = await userManager.FindByEmailAsync(clienteEmail);

            if (clienteUser == null)
            {
                var cliente = new ApplicationUser
                {
                    UserName = clienteEmail,
                    Email = clienteEmail,
                    EmailConfirmed = true,

                    Nombre = "Cliente",
                    Apellido = "Demo",
                    PhoneNumber = "+56911111111",
                    TelefonoContacto = "+56911111111",

                    Region = "Región Metropolitana de Santiago",
                    Comuna = "Quinta Normal",
                    Calle = "Calle Demo",
                    Numero = "1234",
                    DeptoBlockOficina = "Depto 101",

                    FechaRegistro = DateTime.UtcNow
                };

                var resultado = await userManager.CreateAsync(cliente, "Cliente123!");

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(cliente, "Cliente");
                }
            }
            else
            {
                clienteUser.Nombre ??= "Cliente";
                clienteUser.Apellido ??= "Demo";
                clienteUser.PhoneNumber ??= "+56911111111";
                clienteUser.TelefonoContacto ??= "+56911111111";
                clienteUser.Region ??= "Región Metropolitana de Santiago";
                clienteUser.Comuna ??= "Quinta Normal";
                clienteUser.Calle ??= "Calle Demo";
                clienteUser.Numero ??= "1234";
                clienteUser.DeptoBlockOficina ??= "Depto 101";

                await userManager.UpdateAsync(clienteUser);

                if (!await userManager.IsInRoleAsync(clienteUser, "Cliente"))
                {
                    await userManager.AddToRoleAsync(clienteUser, "Cliente");
                }
            }
        }
    }
}