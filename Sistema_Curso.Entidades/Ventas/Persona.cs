using Sistema_Curso.Entidades.Almacen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Curso.Entidades.Ventas
{
    public class Persona
    {
        public int idpersona { get; set; }
        [Required]
        public string tipo_persona { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre de la persona debe tener más de 3 caracteres y menos de 100 caracteres")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        public ICollection<Ingreso> ingresos { get; set; }
        public ICollection<Venta> ventas { get; set; }
    }
}
