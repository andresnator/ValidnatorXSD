namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaProgramacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "VLD.ArchivosProgramados",
                c => new
                    {
                        ArchivosProgramadosId = c.Long(nullable: false, identity: true),
                        UrlArchivoCargado = c.String(nullable: false, maxLength: 300, unicode: false),
                        UrlArchivoErrores = c.String(maxLength: 300),
                        FechaProgramacion = c.DateTime(nullable: false),
                        FechaValidacion = c.DateTime(),
                        EstadosArchivos = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        ArchivoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ArchivosProgramadosId)
                .ForeignKey("VLD.Archivo", t => t.ArchivoId, cascadeDelete: true)
                .Index(t => t.ArchivoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("VLD.ArchivosProgramados", "ArchivoId", "VLD.Archivo");
            DropIndex("VLD.ArchivosProgramados", new[] { "ArchivoId" });
            DropTable("VLD.ArchivosProgramados");
        }
    }
}
