using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class tablaArchivoAlasas : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.ArchivosProgramados", "EstadosValidacion", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("VLD.ArchivosProgramados", "EstadosValidacion");
        }
    }
}