using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- REGISTRO ---
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Por defecto, cualquier persona que se registre en la web es "Cliente"
                await _userManager.AddToRoleAsync(user, "Cliente");

                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // --- LOGIN ---
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    "",
                    "Tu cuenta fue bloqueada temporalmente por demasiados intentos fallidos. Intenta nuevamente en 10 minutos."
                );

                return View(model);
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(
                    "",
                    "No tienes permiso para iniciar sesión. Verifica tu cuenta."
                );

                return View(model);
            }

            ModelState.AddModelError(
                "",
                "Credenciales inválidas. Verifica tu correo o contraseña."
            );

            return View(model);
        }

        // --- CERRAR SESIÓN ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        // --- ACCESO DENEGADO ---
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}