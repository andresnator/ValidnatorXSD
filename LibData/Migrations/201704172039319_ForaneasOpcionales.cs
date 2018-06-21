namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForaneasOpcionales : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("VLD.Columna", "RestriccionesColumnaId", "VLD.RestriccionesColumna");
            DropIndex("VLD.Columna", new[] { "RestriccionesColumnaId" });
            AlterColumn("VLD.Columna", "RestriccionesColumnaId", c => c.Long());
            CreateIndex("VLD.Columna", "RestriccionesColumnaId");
            AddForeignKey("VLD.Columna", "RestriccionesColumnaId", "VLD.RestriccionesColumna", "RestriccionesColumnaId");
        }
        
        public override void Down()
        {
            DropForeignKey("VLD.Columna", "RestriccionesColumnaId", "VLD.RestriccionesColumna");
            DropIndex("VLD.Columna", new[] { "RestriccionesColumnaId" });
            AlterColumn("VLD.Columna", "RestriccionesColumnaId", c => c.Long(nullable: false));
            CreateIndex("VLD.Columna", "RestriccionesColumnaId");
            AddForeignKey("VLD.Columna", "RestriccionesColumnaId", "VLD.RestriccionesColumna", "RestriccionesColumnaId", cascadeDelete: false);
        }
    }
}
