using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class asassas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "VLD.ErroresArchivos",
                    c => new
                    {
                        ErroresArchivosId = c.Guid(false),
                        Mensaje = c.String(false, 2000, unicode: false),
                        Columna = c.Long(false),
                        Fila = c.Long(false),
                        Estado = c.Boolean(false),
                        FechaRegistro = c.DateTime(false),
                        ArchivosProgramadosId = c.Long(false)
                    })
                .PrimaryKey(t => t.ErroresArchivosId)
                .ForeignKey("VLD.ArchivosProgramados", t => t.ArchivosProgramadosId, false)
                .Index(t => t.ArchivosProgramadosId);
        }

        public override void Down()
        {
            DropForeignKey("VLD.ErroresArchivos", "ArchivosProgramadosId", "VLD.ArchivosProgramados");
            DropIndex("VLD.ErroresArchivos", new[] {"ArchivosProgramadosId"});
            DropTable("VLD.ErroresArchivos");
        }
    }
}