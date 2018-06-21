using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class dsaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.RestriccionesColumna", "ComunTipoDato", c => c.Int(false));
            DropColumn("VLD.RestriccionesColumna", "TipoDato");
        }

        public override void Down()
        {
            AddColumn("VLD.RestriccionesColumna", "TipoDato", c => c.Int(false));
            DropColumn("VLD.RestriccionesColumna", "ComunTipoDato");
        }
    }
}