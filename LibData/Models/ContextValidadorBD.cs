using System.Data.Entity;

namespace LibData.Models
{
    public class ContextValidadorBd : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'ContextDB' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'LibModel.Models.ContextDB' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'ContextDB'  en el archivo de configuración de la aplicación.
        public ContextValidadorBd()
            : base("name=ContextDB")
        {
            //Configuration.LazyLoadingEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        //public virtual DbSet<RestriccionesColumna> RestriccionesColumnas { get; set; }
        //public virtual DbSet<Columna> Columnas { get; set; }
        //public virtual DbSet<Archivo> Archivos { get; set; }
        //public virtual DbSet<Altgoritmo> Altgoritmos { get; set; }
        //public virtual DbSet<ColumnaLista> ColumnaListas { get; set; }
        //public virtual DbSet<Lista> Listas { get; set; }
        //public virtual DbSet<ColumnaAltgoritmo> ColumnaAltgoritmos { get; set; }

        public DbSet<Columna> Columnas { get; set; }
        public DbSet<RestriccionesColumna> RestriccionesColumnas { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<ArchivosProgramados> ArchivosProgramados { get; set; }
        public DbSet<ErroresArchivos> ErroresArchivos { get; set; }


        protected void Seed(ContextValidadorBd context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            SeedInicial(context);
        }


        public void SeedInicial(ContextValidadorBd context)
        {
            //context.Archivos.AddOrUpdate(
            //    new Archivo { ArchivoId = 1, CantidadColumnas = 10, Descripcion = "Prueba", Nombre = "Andres", Separador = "|" }
            //    );
        }
    }
}