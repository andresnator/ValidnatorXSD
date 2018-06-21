using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class adfdsfdsdsa : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Archivo", "CantidadColumnasHabilitadas", c => c.Int(false));
            AddColumn("VLD.Archivo", "CantidadColumnasInHabilitadas", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("VLD.Archivo", "CantidadColumnasInHabilitadas");
            DropColumn("VLD.Archivo", "CantidadColumnasHabilitadas");
        }
    }
}