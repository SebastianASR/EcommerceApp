using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingresa un correo válido")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = "";

        public string? Region { get; set; }

        public string? Comuna { get; set; }

        public string? Calle { get; set; }

        public string? Numero { get; set; }

        public string? DeptoBlockOficina { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Debes confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = "";

        public string? ReturnUrl { get; set; }
    }
}