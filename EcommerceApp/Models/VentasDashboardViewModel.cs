namespace EcommerceApp.Models
{
    public class VentasDashboardViewModel
    {
        public int TotalPedidos { get; set; }
        public int PedidosAutorizados { get; set; }
        public int PedidosPendientes { get; set; }
        public int PedidosCancelados { get; set; }

        public int ClientesUnicos { get; set; }
        public int ProductosVendidos { get; set; }

        public int VentasTotales { get; set; }
        public int VentasHoy { get; set; }
        public int VentasMesActual { get; set; }
        public int TicketPromedio { get; set; }

        public List<VentaPorDiaViewModel> VentasUltimos7Dias { get; set; } = new();
        public List<VentaPorMesViewModel> VentasUltimos6Meses { get; set; } = new();
        public List<ProductoMasVendidoViewModel> ProductosMasVendidos { get; set; } = new();
        public List<EstadoPedidoViewModel> EstadosPedidos { get; set; } = new();
        public List<Pedido> UltimosPedidos { get; set; } = new();
    }

    public class VentaPorDiaViewModel
    {
        public string Dia { get; set; } = "";
        public int Total { get; set; }
        public int CantidadPedidos { get; set; }
    }

    public class VentaPorMesViewModel
    {
        public string Mes { get; set; } = "";
        public int Total { get; set; }
        public int CantidadPedidos { get; set; }
    }

    public class ProductoMasVendidoViewModel
    {
        public string Nombre { get; set; } = "";
        public int CantidadVendida { get; set; }
        public int TotalVendido { get; set; }
    }

    public class EstadoPedidoViewModel
    {
        public string Estado { get; set; } = "";
        public int Cantidad { get; set; }
    }
}