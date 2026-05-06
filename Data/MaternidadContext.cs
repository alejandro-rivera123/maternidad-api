using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Maternidad.Dominio;

namespace Maternidad.Data
{
    public class MaternidadContext : DbContext
    {
        public MaternidadContext (DbContextOptions<MaternidadContext> options)
            : base(options)
        {
        }

        public DbSet<SeguimientoPrenatal> SeguimientoPrenatal { get; set; }
        public DbSet<ControlPrenatal> ControlPrenatal { get; set; }

        // 🔹 CATÁLOGOS
        public DbSet<TipoSignoPrenatal> TipoSignoPrenatal { get; set; }
        public DbSet<TipoSignoVitalNeonatal> TipoSignoVitalNeonatal { get; set; }

        // 🔹 PARTO Y NEONATO
        public DbSet<Parto> Parto { get; set; }
        public DbSet<RecienNacido> RecienNacido { get; set; }

        // 🔹 NEONATAL DETALLE
        public DbSet<SignoVitalNeonatal> SignoVitalNeonatal { get; set; }
        public DbSet<EvaluacionInicial> EvaluacionInicial { get; set; }
        public DbSet<TratamientoNeonatal> TratamientoNeonatal { get; set; }
        public DbSet<AlertaNeonatal> AlertaNeonatal { get; set; }
        public DbSet<ComplicacionNeonatal> ComplicacionNeonatal { get; set; }

        // 🔹 MATERNA
        public DbSet<ComplicacionMaterna> ComplicacionMaterna { get; set; }
    }
}
