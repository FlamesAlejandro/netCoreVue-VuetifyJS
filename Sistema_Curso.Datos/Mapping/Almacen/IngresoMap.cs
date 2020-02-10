using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema_Curso.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Curso.Datos.Mapping.Almacen
{
    public class IngresoMap : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> builder)
        {
            builder.ToTable("ingreso")
                .HasKey(i => i.idingreso);            
            builder.HasOne(i => i.persona)
                .WithMany(p => p.ingresos)
                .HasForeignKey(i => i.idproveedor);

            // en casos en donde la tabla padre e hijo esten relacionado por una variable de distinto nombre, se
            // recomienda hacer este metodo, para mapear la relacion correctamente, en este caso 
            // idproveedor -> idpersona
        }
    }
}
