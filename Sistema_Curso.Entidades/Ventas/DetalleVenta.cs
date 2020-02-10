using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Sistema_Curso.Entidades.Almacen;

namespace Sistema_Curso.Entidades.Ventas
{
    public class DetalleVenta
    {
        public int iddetalle_venta { get; set; }
        [Required]
        public int idventa { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal descuento { get; set; }

        public Venta venta { get; set; }
        public Articulo articulo { get; set; }

    }
}
