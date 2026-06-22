using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class SolicitudVip
    {
        public int Id { get; set; }

        [Required]
        public string NombreEmpresa { get; set; } = null!;

        [Required, EmailAddress]
        public string Correo { get; set; } = null!;

        [Required]
        public string TipoProyecto { get; set; } = null!;

        [Required]
        public string Detalles { get; set; } = null!;

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        // --- Gestión administrativa ---
        public string Estado { get; set; } = "Nuevo";

        public string? NotaInterna { get; set; }

        public bool Archivada { get; set; } = false;

        public DateTime? FechaUltimaGestion { get; set; }
    }
}