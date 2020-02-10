using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Curso.Datos;
using Sistema_Curso.Entidades.Ventas;
using Sistema_Curso.Web.Models.Ventas.Venta;

namespace Sistema_Curso.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ventas/Listar
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<VentaViewModel>> Listar()
        {
            // include categoria, ya que es la clase padre en este caso
            var venta = await _context.Ventas
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .OrderByDescending(v => v.idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                idventa = v.idventa,
                idcliente = v.idcliente,
                cliente = v.persona.nombre,
                num_documento = v.persona.num_documento,
                direccion = v.persona.direccion,
                telefono = v.persona.telefono,
                email = v.persona.email,
                idusuario = v.idusuario,
                usuario = v.usuario.nombre,
                tipo_comprobante = v.tipo_comprobante,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_hora = v.fecha_hora,
                impuesto = v.impuesto,
                total = v.total,
                estado = v.estado
            });
        }

        // GET: api/Ventas/VentasMes12
        [Authorize(Roles = "Almacenero,Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>> VentasMes12()
        {
            // include categoria, ya que es la clase padre en este caso
            var consulta = await _context.Ventas
                .GroupBy(v=> v.fecha_hora.Month)
                .Select(x => new { etiqueta=x.Key, valor =x.Sum(v=>v.total)})
                .OrderByDescending(v => v.etiqueta)
                .Take(100)
                .ToListAsync();

            return consulta.Select(v => new ConsultaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor 
            });
        }


        // GET: api/Ventas/ListarFiltro/txt
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<VentaViewModel>> ListarFiltro([FromRoute] string texto)
        {
            // include categoria, ya que es la clase padre en este caso
            var venta = await _context.Ventas
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(v => v.num_comprobante.Contains(texto))
                // Para evitar una sobre carga, solo tomamos los 100 primeros registros
                .OrderByDescending(i => i.idventa)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                idventa = v.idventa,
                idcliente = v.idcliente,
                cliente = v.persona.nombre,
                num_documento = v.persona.num_documento,
                direccion = v.persona.direccion,
                telefono = v.persona.telefono,
                email = v.persona.email,
                idusuario = v.idusuario,
                usuario = v.usuario.nombre,
                tipo_comprobante = v.tipo_comprobante,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_hora = v.fecha_hora,
                impuesto = v.impuesto,
                total = v.total,
                estado = v.estado
            });
        }

        
        // GET: api/Ventas/ConsultaFechas/FechaInicio/FechaFin
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<VentaViewModel>> ConsultaFechas([FromRoute]DateTime FechaInicio, DateTime FechaFin)
        {
            // include categoria, ya que es la clase padre en este caso
            var venta = await _context.Ventas
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(i=>i.fecha_hora>=FechaInicio)
                .Where(i => i.fecha_hora <= FechaFin)
                .OrderByDescending(v => v.idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                idventa = v.idventa,
                idcliente = v.idcliente,
                cliente = v.persona.nombre,
                num_documento = v.persona.num_documento,
                direccion = v.persona.direccion,
                telefono = v.persona.telefono,
                email = v.persona.email,
                idusuario = v.idusuario,
                usuario = v.usuario.nombre,
                tipo_comprobante = v.tipo_comprobante,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_hora = v.fecha_hora,
                impuesto = v.impuesto,
                total = v.total,
                estado = v.estado
            });
        }


        // GET: api/Ventas/ListarDetalles
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{idventa}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idventa)
        {

            var detalle = await _context.DetallesVentas
                .Include(a => a.articulo)
                .Where(d => d.idventa == idventa)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {
                idarticulo = d.idarticulo,
                articulo = d.articulo.nombre,
                cantidad = d.cantidad,
                precio = d.precio,
                descuento = d.descuento

            });
        }

        // POST: api/Ventas/Crear
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Venta venta = new Venta
            {
                idcliente = model.idcliente,
                idusuario = model.idusuario,
                tipo_comprobante = model.tipo_comprobante,
                serie_comprobante = model.serie_comprobante,
                num_comprobante = model.num_comprobante,
                fecha_hora = fechaHora,
                impuesto = model.impuesto,
                total = model.total,
                estado = "Aceptado"
            };

            try
            {
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                var id = venta.idventa;
                foreach (var det in model.detalles)
                {
                    DetalleVenta detalle = new DetalleVenta
                    {
                        idventa = id,
                        idarticulo = det.idarticulo,
                        cantidad = det.cantidad,
                        precio = det.precio,
                        descuento = det.descuento
                    };
                    _context.DetallesVentas.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Ingresos/Anular/1
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.idventa == id);

            if (venta == null)
            {
                return NotFound();
            }

            venta.estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();

                var detalle = await _context.DetallesVentas
                    .Include(a => a.articulo)
                    .Where(d => d.idventa == id)
                    .ToListAsync();

                foreach (var det in detalle)
                {
                    var articulo = await _context.Articulos
                        .FirstOrDefaultAsync(a => a.idarticulo == det.articulo.idarticulo);
                    articulo.stock = det.articulo.stock + det.cantidad;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }



        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.idventa == id);
        }
    }
}