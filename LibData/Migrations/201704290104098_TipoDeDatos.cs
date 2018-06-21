using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class TipoDeDatos : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.RestriccionesColumna", "TipoDato", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("VLD.RestriccionesColumna", "TipoDato");
        }
    }
}