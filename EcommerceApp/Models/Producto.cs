namespace EcommerceApp.Models
{
    public class Producto
    {
        // El ID es único para cada producto (como un RUT o código de barras)
        public int Id { get; set; }

        // Nombre del hardware (ej: "Procesador Server-X Pro")
        public string? Nombre { get; set; }

        // Detalles técnicos
        public string? Descripcion { get; set; }

        // Usamos 'int' porque en pesos chilenos (CLP) 
        public int Precio { get; set; }

        // La ruta o enlace a la foto del producto
        public string? ImagenUrl { get; set; }
    }
}
