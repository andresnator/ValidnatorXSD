using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class tablaProgramacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "VLD.ArchivosProgramados",
                    c => new
                    {
                        ArchivosProgramadosId = c.Long(false, true),
                        UrlArchivoCargado = c.String(false, 300, unicode: false),
                        UrlArchivoErrores = c.String(maxLength: 300),
                        FechaProgramacion = c.DateTime(false),
                        FechaValidacion = c.DateTime(),
                        EstadosArchivos = c.Int(false),
                        Estado = c.Boolean(false),
                        ArchivoId = c.Long(false)
                    })
                .PrimaryKey(t => t.ArchivosProgramadosId)
                .ForeignKey("VLD.Archivo", t => t.ArchivoId, true)
                .Index(t => t.ArchivoId);
        }

        public override void Down()
        {
            DropForeignKey("VLD.ArchivosProgramados", "ArchivoId", "VLD.Archivo");
            DropIndex("VLD.ArchivosProgramados", new[] {"ArchivoId"});
            DropTable("VLD.ArchivosProgramados");
        }
    }
}