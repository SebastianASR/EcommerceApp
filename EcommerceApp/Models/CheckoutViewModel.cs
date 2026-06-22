using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(80)]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(80)]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingresa un correo válido")]
        public string Correo { get; set; } = "";

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(30)]
        public string Telefono { get; set; } = "";

        [Required(ErrorMessage = "La región es obligatoria")]
        public string Region { get; set; } = "";

        [Required(ErrorMessage = "La comuna es obligatoria")]
        public string Comuna { get; set; } = "";

        [Required(ErrorMessage = "La calle es obligatoria")]
        [StringLength(120)]
        public string Calle { get; set; } = "";

        [Required(ErrorMessage = "El número es obligatorio")]
        [StringLength(20)]
        public string Numero { get; set; } = "";

        [StringLength(80)]
        public string? DeptoBlockOficina { get; set; }

        [StringLength(300)]
        public string? ComentarioCliente { get; set; }

        public bool CrearCuenta { get; set; }

        public bool GuardarDatosEnCuenta { get; set; } = true;

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmPassword { get; set; }

        public bool EsUsuarioAutenticado { get; set; }

        public int Total { get; set; }

        public List<CarritoItem> ResumenBolsa { get; set; } = new();
    }
}