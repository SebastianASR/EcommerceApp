using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? TelefonoContacto { get; set; }

        public string? Region { get; set; }
        public string? Comuna { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }
        public string? DeptoBlockOficina { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public List<Pedido> Pedidos { get; set; } = new();
    }
}