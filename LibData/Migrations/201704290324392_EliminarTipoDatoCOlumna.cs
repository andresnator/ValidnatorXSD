using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class EliminarTipoDatoCOlumna : DbMigration
    {
        public override void Up()
        {
            DropColumn("VLD.Columna", "TipoDato");
        }

        public override void Down()
        {
            AddColumn("VLD.Columna", "TipoDato", c => c.Int(false));
        }
    }
}