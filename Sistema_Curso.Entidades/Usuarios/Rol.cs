using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Curso.Entidades.Usuarios
{
    public class Rol
    {
        public int idrol { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "El nombre no debe tener más de 30 caracteres, y menos de 2 caracteres.")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        public ICollection<Usuario> usuarios { get; set; } 
    }
}
