namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asassas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "VLD.ErroresArchivos",
                c => new
                    {
                        ErroresArchivosId = c.Guid(nullable: false),
                        Mensaje = c.String(nullable: false, maxLength: 2000, unicode: false),
                        Columna = c.Long(nullable: false),
                        Fila = c.Long(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        ArchivosProgramadosId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ErroresArchivosId)
                .ForeignKey("VLD.ArchivosProgramados", t => t.ArchivosProgramadosId, cascadeDelete: false)
                .Index(t => t.ArchivosProgramadosId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("VLD.ErroresArchivos", "ArchivosProgramadosId", "VLD.ArchivosProgramados");
            DropIndex("VLD.ErroresArchivos", new[] { "ArchivosProgramadosId" });
            DropTable("VLD.ErroresArchivos");
        }
    }
}
