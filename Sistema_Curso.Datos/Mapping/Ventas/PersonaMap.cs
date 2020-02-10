
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema_Curso.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Curso.Datos.Mapping.Ventas
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("persona")
                .HasKey(p => p.idpersona);
        }

    }
}
