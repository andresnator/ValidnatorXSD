using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class Dos : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Columna", "ArchivoId", c => c.Long(false));
            CreateIndex("VLD.Columna", "ArchivoId");
            AddForeignKey("VLD.Columna", "ArchivoId", "VLD.Archivo", "ArchivoId", false);
        }

        public override void Down()
        {
            DropForeignKey("VLD.Columna", "ArchivoId", "VLD.Archivo");
            DropIndex("VLD.Columna", new[] {"ArchivoId"});
            DropColumn("VLD.Columna", "ArchivoId");
        }
    }
}