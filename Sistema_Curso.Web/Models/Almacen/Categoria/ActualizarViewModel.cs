﻿
using System.ComponentModel.DataAnnotations;

namespace Sistema_Curso.Web.Models.Almacen.Categoria
{
    public class ActualizarViewModel
    {
        [Required]
        public int idcategoria { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, y menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
    }
}
