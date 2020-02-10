using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Curso.Entidades.Almacen
{
    public class Articulo
    {
        public int idarticulo { get; set; }
        [Required]
        public int idcategoria { get; set; }
        public string codigo { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, y menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public decimal precio_venta { get; set; }
        [Required]
        public int stock { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        public Categoria categoria { get; set; }
        public ICollection<DetalleIngreso> DetallesIngresos { get; set; }

    }
}
