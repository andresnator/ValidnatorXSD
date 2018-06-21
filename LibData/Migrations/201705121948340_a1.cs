using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("VLD.Columna", "Descripcion");
        }

        public override void Down()
        {
            AddColumn("VLD.Columna", "Descripcion", c => c.String(false, 1024, unicode: false));
        }
    }
}