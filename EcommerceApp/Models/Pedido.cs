using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public string? UsuarioId { get; set; }
        public ApplicationUser? Usuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "La región es obligatoria")]
        public string Region { get; set; } = null!;

        [Required(ErrorMessage = "La comuna es obligatoria")]
        public string Comuna { get; set; } = null!;

        [Required(ErrorMessage = "La calle es obligatoria")]
        public string Calle { get; set; } = null!;

        [Required(ErrorMessage = "El número es obligatorio")]
        public string Numero { get; set; } = null!;

        public string? DeptoBlockOficina { get; set; }

        public string? ComentarioCliente { get; set; }

        public string TipoCliente { get; set; } = "Invitado";

        public string EstadoPago { get; set; } = "Pendiente";

        public string EstadoPedido { get; set; } = "Pendiente";

        public string? BuyOrder { get; set; }

        public string? WebpayToken { get; set; }

        public int Total { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        public List<PedidoDetalle> Detalles { get; set; } = new();
    }
}