using System.Diagnostics;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Simulo una base de datos 
            var listaProductos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Procesador Server-X Pro", Descripcion = "Máxima potencia para bases de datos y virtualización con excelente gestión térmica.", Precio = 349990, ImagenUrl = "https://placehold.co/400x250/212529/FFFFFF?text=Procesador+Server-X" },
                new Producto { Id = 2, Nombre = "Kit Memoria RAM 32GB", Descripcion = "Velocidad extrema y baja latencia, ideal para cargas de trabajo pesadas y gaming.", Precio = 129990, ImagenUrl = "https://placehold.co/400x250/212529/FFFFFF?text=Memoria+RAM+32GB" },
                new Producto { Id = 3, Nombre = "Compuesto Térmico Pro-Cool", Descripcion = "Conductividad superior para mantener tu hardware al máximo rendimiento sin sobrecalentamiento.", Precio = 14990, ImagenUrl = "https://placehold.co/400x250/212529/FFFFFF?text=Pasta+Termica" }
            };

            // Le enviamos esta lista a la Vista
            return View(listaProductos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}