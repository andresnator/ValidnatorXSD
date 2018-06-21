namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dos : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Columna", "ArchivoId", c => c.Long(nullable: false));
            CreateIndex("VLD.Columna", "ArchivoId");
            AddForeignKey("VLD.Columna", "ArchivoId", "VLD.Archivo", "ArchivoId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("VLD.Columna", "ArchivoId", "VLD.Archivo");
            DropIndex("VLD.Columna", new[] { "ArchivoId" });
            DropColumn("VLD.Columna", "ArchivoId");
        }
    }
}
