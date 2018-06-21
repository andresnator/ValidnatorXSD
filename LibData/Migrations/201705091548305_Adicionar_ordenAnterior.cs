using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class Adicionar_ordenAnterior : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Columna", "OrdenAnterior", c => c.Short(false));
        }

        public override void Down()
        {
            DropColumn("VLD.Columna", "OrdenAnterior");
        }
    }
}