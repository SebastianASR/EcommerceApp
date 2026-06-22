using EcommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PedidosController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Historial de compras del cliente autenticado
        [HttpGet]
        public async Task<IActionResult> MisCompras()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var correoUsuario = usuario.Email ?? "";

            var pedidos = await _context.Pedidos
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .Where(p => p.UsuarioId == usuario.Id || p.Correo == correoUsuario)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();

            return View(pedidos);
        }

        // Panel de compras para Admin y DemoAdmin
        [Authorize(Roles = "Admin,DemoAdmin")]
        [HttpGet]
        public async Task<IActionResult> AdminCompras()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();

            ViewBag.EsDemoAdmin = User.IsInRole("DemoAdmin") && !User.IsInRole("Admin");

            ViewBag.TotalPedidos = pedidos.Count;

            ViewBag.TotalVendido = pedidos
                .Where(p => p.EstadoPago == "Autorizado")
                .Sum(p => p.Total);

            ViewBag.TotalClientes = pedidos
                .Where(p => !string.IsNullOrWhiteSpace(p.Correo))
                .Select(p => p.Correo)
                .Distinct()
                .Count();

            ViewBag.PedidosPendientes = pedidos
                .Count(p =>
                    p.EstadoPedido == "Pendiente" ||
                    p.EstadoPedido == "Pagado" ||
                    p.EstadoPedido == "Preparando" ||
                    p.EstadoPedido == "Enviado"
                );

            return View(pedidos);
        }
        [Authorize(Roles = "Admin,DemoAdmin")]
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var ahora = DateTime.UtcNow;
            var inicioHoy = ahora.Date;
            var inicioMes = new DateTime(ahora.Year, ahora.Month, 1);

            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();

            var pedidosAutorizados = pedidos
                .Where(p => p.EstadoPago == "Autorizado")
                .ToList();

            var ventasTotales = pedidosAutorizados.Sum(p => p.Total);
            var cantidadPedidosAutorizados = pedidosAutorizados.Count;

            var ultimos7Dias = Enumerable.Range(0, 7)
                .Select(i => ahora.Date.AddDays(-6 + i))
                .ToList();

            var ventasUltimos7Dias = ultimos7Dias
                .Select(dia =>
                {
                    var pedidosDia = pedidosAutorizados
                        .Where(p => p.FechaPedido.Date == dia.Date)
                        .ToList();

                    return new VentaPorDiaViewModel
                    {
                        Dia = dia.ToString("dd/MM"),
                        Total = pedidosDia.Sum(p => p.Total),
                        CantidadPedidos = pedidosDia.Count
                    };
                })
                .ToList();

            var ultimos6Meses = Enumerable.Range(0, 6)
                .Select(i => new DateTime(ahora.Year, ahora.Month, 1).AddMonths(-5 + i))
                .ToList();

            var ventasUltimos6Meses = ultimos6Meses
                .Select(mes =>
                {
                    var pedidosMes = pedidosAutorizados
                        .Where(p => p.FechaPedido.Year == mes.Year && p.FechaPedido.Month == mes.Month)
                        .ToList();

                    return new VentaPorMesViewModel
                    {
                        Mes = mes.ToString("MMM yyyy"),
                        Total = pedidosMes.Sum(p => p.Total),
                        CantidadPedidos = pedidosMes.Count
                    };
                })
                .ToList();

            var productosMasVendidos = pedidosAutorizados
                .SelectMany(p => p.Detalles)
                .Where(d => d.Producto != null)
                .GroupBy(d => d.Producto!.Nombre)
                .Select(g => new ProductoMasVendidoViewModel
                {
                    Nombre = g.Key,
                    CantidadVendida = g.Sum(d => d.Cantidad),
                    TotalVendido = g.Sum(d => d.Cantidad * d.PrecioUnitario)
                })
                .OrderByDescending(p => p.CantidadVendida)
                .Take(5)
                .ToList();

            var estadosPedidos = pedidos
                .GroupBy(p => string.IsNullOrWhiteSpace(p.EstadoPedido) ? "Sin estado" : p.EstadoPedido)
                .Select(g => new EstadoPedidoViewModel
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(e => e.Cantidad)
                .ToList();

            var dashboard = new VentasDashboardViewModel
            {
                TotalPedidos = pedidos.Count,

                PedidosAutorizados = cantidadPedidosAutorizados,

                PedidosPendientes = pedidos.Count(p =>
                    p.EstadoPedido == "Pendiente" ||
                    p.EstadoPedido == "Pagado" ||
                    p.EstadoPedido == "Preparando" ||
                    p.EstadoPedido == "Enviado"
                ),

                PedidosCancelados = pedidos.Count(p => p.EstadoPedido == "Cancelado"),

                ClientesUnicos = pedidos
                    .Where(p => !string.IsNullOrWhiteSpace(p.Correo))
                    .Select(p => p.Correo)
                    .Distinct()
                    .Count(),

                ProductosVendidos = pedidosAutorizados
                    .SelectMany(p => p.Detalles)
                    .Sum(d => d.Cantidad),

                VentasTotales = ventasTotales,

                VentasHoy = pedidosAutorizados
                    .Where(p => p.FechaPedido.Date == inicioHoy)
                    .Sum(p => p.Total),

                VentasMesActual = pedidosAutorizados
                    .Where(p => p.FechaPedido >= inicioMes)
                    .Sum(p => p.Total),

                TicketPromedio = cantidadPedidosAutorizados > 0
                    ? ventasTotales / cantidadPedidosAutorizados
                    : 0,

                VentasUltimos7Dias = ventasUltimos7Dias,
                VentasUltimos6Meses = ventasUltimos6Meses,
                ProductosMasVendidos = productosMasVendidos,
                EstadosPedidos = estadosPedidos,

                UltimosPedidos = pedidos
                    .Take(6)
                    .ToList()
            };

            ViewBag.EsDemoAdmin = User.IsInRole("DemoAdmin") && !User.IsInRole("Admin");

            return View(dashboard);
        }
        // Detalle de pedido
        // Cliente: solo puede ver sus propios pedidos.
        // Admin/DemoAdmin: pueden ver cualquier pedido.
        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            bool esAdminODemo = User.IsInRole("Admin") || User.IsInRole("DemoAdmin");

            if (!esAdminODemo)
            {
                var usuario = await _userManager.GetUserAsync(User);

                if (usuario == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                bool pedidoEsDelUsuario =
                    pedido.UsuarioId == usuario.Id ||
                    pedido.Correo == usuario.Email;

                if (!pedidoEsDelUsuario)
                {
                    return Forbid();
                }
            }

            ViewBag.EsDemoAdmin = User.IsInRole("DemoAdmin") && !User.IsInRole("Admin");

            return View(pedido);
        }

        // Solo Admin real puede cambiar estado del pedido
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, string estadoPedido)
        {
            var estadosPermitidos = new List<string>
            {
                "Pagado",
                "Preparando",
                "Enviado",
                "Entregado",
                "Cancelado"
            };

            if (!estadosPermitidos.Contains(estadoPedido))
            {
                TempData["ErrorMessage"] = "El estado seleccionado no es válido.";
                return RedirectToAction(nameof(AdminCompras));
            }

            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.EstadoPedido = estadoPedido;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"El pedido #{pedido.Id} fue actualizado a estado: {estadoPedido}.";

            return RedirectToAction(nameof(AdminCompras));
        }

        // Solo Admin real puede eliminar una venta
        // DemoAdmin NO puede eliminar.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                TempData["ErrorMessage"] = "El pedido que intentas eliminar no existe.";
                return RedirectToAction(nameof(AdminCompras));
            }

            var totalPedido = pedido.Total;
            var correoCliente = pedido.Correo;
            var nombreCliente = pedido.NombreCompleto;

            if (pedido.Detalles.Any())
            {
                _context.PedidosDetalle.RemoveRange(pedido.Detalles);
            }

            _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                $"El pedido #{id} de {nombreCliente} ({correoCliente}) por $ {totalPedido:N0} fue eliminado correctamente.";

            return RedirectToAction(nameof(AdminCompras));
        }
    }
}