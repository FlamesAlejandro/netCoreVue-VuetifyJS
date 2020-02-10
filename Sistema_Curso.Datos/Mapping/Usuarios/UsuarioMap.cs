using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema_Curso.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Curso.Datos.Mapping.Usuarios
{
    class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario")
                .HasKey(u => u.idusuario);
        }
    }
}
